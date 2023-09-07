import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../environments/environment';
import { Observable, map } from 'rxjs';
import { APIServiceResult } from '../_models/APIServiceResult';
import { TrolleyProductUpdateDTO } from '../_models/TrolleyProductUpdateDTO';




@Injectable({
  providedIn: 'root'
})
export class TrolleyService {

  _APIServiceResult: APIServiceResult | undefined;
  _apiGatewayUrl = environment.gatewayUrl;
  


  constructor(private http: HttpClient) { }


  //----------------------------------------------- To Do:
  getProductsFromTrolley()
  {
    
  }



  addProductsToTrolley(userId: number, products: TrolleyProductUpdateDTO[]): Observable<APIServiceResult>
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    })

    return this.http.post<APIServiceResult>(this._apiGatewayUrl + "trolley/" + userId + "/products", {Products: products},{ headers: headers });
  }



  // removeProductsFromTrolley(): Observable<APIServiceResult>
  // {

  // }




  getTrolley(userId: number): Observable<APIServiceResult>
  {
    return this.http.get<APIServiceResult>(this._apiGatewayUrl + "trolley/" + userId);
  }

}

