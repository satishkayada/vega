using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vega.Controllers.Resources;
using vega.Models;

namespace vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API resources

            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle,SaveVehicleResources>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom( v => new ContactResources{ Name  = v.ContactName , Email = v.ContactEmail ,Phone = v.ContactPhone}))
                .ForMember(vr => vr.Features, opt => opt.MapFrom( v => v.Features.Select(vf => vf.FeatureId) ));
            CreateMap<Vehicle,VehicleResources>()
                .ForMember(vr => vr.Make , opt => opt.MapFrom (v => v.Model.Make))
                .ForMember(vr => vr.Contact , opt => opt.MapFrom( v => new ContactResources{ Name  = v.ContactName , Email = v.ContactEmail ,Phone = v.ContactPhone}))
                .ForMember(vr => vr.Features, opt => opt.MapFrom( v => v.Features.Select(vf => new KeyValuePairResource { Id=vf.Feature.Id, Name=vf.Feature.Name } ) ));

            // API to Domain resources
            CreateMap<SaveVehicleResources,Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap ((vr, v) =>{

                    var removeFeatures = new List<VehicleFeature>();
                    foreach (var f in v.Features) {
                        if (!vr.Features.Contains(f.FeatureId)){
                            removeFeatures.Add(f);
                        }
                    }
                    foreach(var f in removeFeatures){
                        v.Features.Remove(f);
                    }
                    
                    // var removeFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));
                    // foreach(var f in removeFeatures){
                    //     v.Features.Remove(f);
                    // }
                    var addedFeatures=vr.Features.Where(id => !v.Features.Any(f => f.FeatureId==id)).Select(id => new VehicleFeature { FeatureId = id});
                    foreach(var f in addedFeatures)
                    {
                        v.Features.Add(f);
                    }
                });
        }
    }
}
