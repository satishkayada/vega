import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import "rxjs/add/operator/Map";
@Injectable()
export class VehicleService {
  constructor(private http:Http) { }
  public getMake(){
    return this.http.get('api/makes')
      .map(res => res.json());
  }
  public getFeature(){
    return this.http.get('api/features')
      .map(res => res.json());
  }
  public create(Vehicle:any){
    return this.http.post('api/vehicles/',Vehicle)
      .map(res => res.json());
  }
}
