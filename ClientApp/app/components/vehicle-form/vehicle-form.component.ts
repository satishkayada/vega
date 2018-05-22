import { Component, OnInit } from '@angular/core';
import { MakeService } from './../../services/make.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
   makes:any[]=[];
   vehicle:any={};
   models:any[]=[];

  constructor(private MakeService: MakeService) { }
  ngOnInit() {
    this.MakeService.getMake().subscribe(makes => 
      {
        this.makes = makes;
      }
    );
  }
  onMakeChanges()
  {
    console.log("Chang called" ,this.vehicle);
    //var selectedModels=this.makes.find(m => m.Id == this.vehicle.make);
    
  }
}
