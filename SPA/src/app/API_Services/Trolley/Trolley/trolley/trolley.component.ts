import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../../../../_services/products.service';
import { TrolleyService } from '../../../../_services/trolley.service';
import { CatalogueProduct } from '../../../../_models/catalogueProduct';
import { APIServiceResult } from '../../../../_models/APIServiceResult';
import { environment } from '../../../../environments/environment';
import { MatDialog } from '@angular/material/dialog';
import { AddCatalogueProductPopUpComponent } from 'src/app/API_Services/Trolley/Trolley/add-catalogue-product-pop-up/add-catalogue-product-pop-up.component';
import { TrolleyProductUpdateDTO } from 'src/app/_models/TrolleyProductUpdateDTO';
import { TrolleyReadDTO } from 'src/app/_models/TrolleyReadDTO';



@Component({
  selector: 'app-trolley',
  templateUrl: './trolley.component.html',
  styleUrls: ['./trolley.component.css']
})
export class TrolleyComponent implements OnInit{


  _photosURL = environment.gatewayUrl + 'photos/';
  _catalogueProducts: CatalogueProduct[] | undefined;
  _selectedProducts: TrolleyProductUpdateDTO[] = new Array<TrolleyProductUpdateDTO>;

  constructor(private trolleyService: TrolleyService) { }


  ngOnInit(): void {
    this.getProducts();
  }



  getProducts()
  {
    this.trolleyService.getTrolley()
      .subscribe((result: APIServiceResult) => {
        this._catalogueProducts = result.data;
      });
  }




}
