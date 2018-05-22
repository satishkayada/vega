import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import "rxjs/add/operator/Map";
@Injectable()
export class MakeService {
  constructor(private http:Http) { }
  public getMake(){
    return this.http.get('api/makes')
      .map(res => res.json());
  }
  
}
