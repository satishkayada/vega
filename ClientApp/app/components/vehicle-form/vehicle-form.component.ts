
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { VehicleService } from './../../services/vehicle.service';
import { ToastyService } from 'ng2-toasty';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html'
})
export class VehicleFormComponent implements OnInit {
   makes: any[]=[];
   models: any[] = [];
   features: any[] = [];
   vehicle: any = {
     features :[],
     contact: {}
   };
   
   constructor(private VehicleService: VehicleService,private toastyService: ToastyService
   ) {
   }
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
    console.log(this.makes);
    console.log(this.vehicle);
    console.log("Features" ,this.features);

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
  submit() {
     this.VehicleService.create(this.vehicle)
     .subscribe(
       x=> console.log(x),
       err => {
        this.toastyService.error({
           title: 'Error',
           msg: 'Test Message',
           theme: 'bootstrap',
           showClose: true,
           timeout: 10000
         });
       }
     );
  }
  
}
