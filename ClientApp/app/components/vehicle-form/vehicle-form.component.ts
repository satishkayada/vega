import { ToastyService } from 'ng2-toasty';
import { Component, OnInit } from '@angular/core';
import { VehicleService } from './../../services/vehicle.service';
@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
   makes: any[]=[];
   models: any[] = [];
   features: any[] = [];
   vehicle: any = {
     features :[],
     contact: {}
   };
   
   constructor(private VehicleService: VehicleService,private toastyService: ToastyService) { }
   ngOnInit() {
    this.VehicleService.getMake().subscribe(makes => 
        {
          this.makes = makes;
        }
      );
    this.VehicleService.getFeature().subscribe(features => {
          this.features = features;
        }
    );
  }
  onMakeChanges()
  {
    var selectedMakes = this.makes.find(m => m.id == this.vehicle.makeId);
    console.log(selectedMakes);
    this.models=selectedMakes.models;
    delete this.vehicle.modelId;
  }
  onFeatureToggle(featureId:any, $event:any) {
    if ($event.target.checked)
      this.vehicle.features.push(featureId);
    else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }
  submit(){
        this.toastyService.error(
                    {
                      title:'Error',
                      msg:'msg',
                      timeout:2000,
                      theme:'default',
                      showClose:true,
                    }
                    );
        console.log('Save Called');
    //   this.VehicleService.create(this.vehicle)
    //  .subscribe(x=> console.log(x));
  }

}
