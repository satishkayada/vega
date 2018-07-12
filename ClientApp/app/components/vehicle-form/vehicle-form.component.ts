import { Component, OnInit } from '@angular/core';
import { VehicleService } from './../../services/vehicle.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
   makes:any[]=[];
   vehicle:any[]=[];
   models: any[] = [];
   features: any[] = [];
   constructor(private VehicleService: VehicleService) { }
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
    
    var selectedMakes = this.makes.find(m => m.id == this.vehicle);
    console.log(selectedMakes);
   
    this.models=selectedMakes.models;
  }
}
