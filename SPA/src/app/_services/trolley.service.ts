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
  _trolleyUrl = environment.TrolleyServiceUrl;


  constructor(private http: HttpClient) { }


  //----------------------------------------------- To Do:
  getProductsFromTrolley()
  {
    
  }



  addProductsToTrolley(products: TrolleyProductUpdateDTO[]): Observable<APIServiceResult>
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    })

    return this.http.post<APIServiceResult>(this._trolleyUrl + "trolley/products", {Products: products},{ headers: headers });
  }



  // removeProductsFromTrolley(): Observable<APIServiceResult>
  // {

  // }



}

