import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../environments/environment';
import { Observable, map } from 'rxjs';
import { APIServiceResult } from '../_models/APIServiceResult';





@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  _APIServiceResult: APIServiceResult | undefined;
  _productsURL = environment.gatewayUrl + 'CatalogueProduct/all/';
  

  constructor(private http: HttpClient) { }




  getProducts(): Observable<APIServiceResult>
  {
    return this.http.get<APIServiceResult>(this._productsURL);
  }


  getPhoto(url: string)
  {
    console.log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IMAGE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>", url);

    return this.http.get(url, {
      responseType: 'blob'
    });
  }

}
