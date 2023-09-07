import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../../../../_services/products.service';
import { TrolleyService } from '../../../../_services/trolley.service';
import { CatalogueProduct } from '../../../../_models/catalogueProduct';
import { APIServiceResult } from '../../../../_models/APIServiceResult';
import { environment } from '../../../../environments/environment';
import { MatDialog } from '@angular/material/dialog';
import { AddCatalogueProductPopUpComponent } from 'src/app/API_Services/Trolley/Trolley/add-catalogue-product-pop-up/add-catalogue-product-pop-up.component';
import { TrolleyProductUpdateDTO } from 'src/app/_models/TrolleyProductUpdateDTO';



@Component({
  selector: 'app-products-album',
  templateUrl: './catalogue-products-album.component.html',
  styleUrls: ['./catalogue-products-album.component.css']
})
export class ProductsAlbumComponent implements OnInit {

  _photosURL = environment.gatewayUrl + 'photos/';
  _catalogueProducts: CatalogueProduct[] | undefined;
  _selectedProducts: TrolleyProductUpdateDTO[] = new Array<TrolleyProductUpdateDTO>;

  constructor(private productsService: ProductsService, private trolleyService: TrolleyService, private addProductPopUpDialog: MatDialog) { }


  ngOnInit(): void {
    this.getProducts();
  }



  getProducts()
  {
    this.productsService.getProducts()
      .subscribe((result: APIServiceResult) => {
        this._catalogueProducts = result.data;

        console.log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> ", this._catalogueProducts );


      });
  }



  openAddProductDialog(productId: number) {

    var product = this.getProductFromList(productId);

    this.addProductPopUpDialog
    .open(AddCatalogueProductPopUpComponent, { data: product ?? {productId: productId, amount: 0} })
    .afterClosed().subscribe(
      (amount: number)=>{

        if(amount > 0)
        {
         this.addProductToList(productId, amount);
        }
        else{
          this.removeProductFromList(productId);
        }
      }
    );
    
  }



  removeProductFromList(productId: number)
  {
    var index = this._selectedProducts.findIndex(i => i.productId === productId)

    if (index > -1) {
      this._selectedProducts.splice(index, 1);
    }

  }




  addProductToList(productId: number, amountToAdd: number)
  {
    var index = this._selectedProducts.findIndex(i => i.productId === productId);

    if(index < 0)
    {
      this._selectedProducts?.push({productId: productId, amount: amountToAdd});
    }
    else{
      this._selectedProducts.forEach(product => {
        if(product.productId === productId)
        {
          this._selectedProducts[index].amount = amountToAdd;
        }
      });
    }
  }


  getProductFromList(productId: number): any
  {
    var result = undefined;

    var index = this._selectedProducts.findIndex(i => i.productId === productId);

    if(index > -1)
    {
      this._selectedProducts.forEach(product => {
        if(product.productId === productId)
        {
          result = product;
        }
      });
    }

    return result;
  }







// ------------------- To Do: load products on trolley from API to add or delete --------------------------





  addToTrolley()
  {
    console.log("--------- ADD to trolley -------------> ITEM ID: ", this._selectedProducts);

    this.trolleyService.addProductsToTrolley(this._selectedProducts)
    .subscribe(data => {
      console.log("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX API RESPONSE XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", data);
    });    
  }


  removeFromTrolley(productId: number)
  {
    console.log("---------- REMOVE from trolley ------------> ITEM ID: ", productId);
  }


  getPhoto(id: string)
  {
    this.productsService.getPhoto(this._photosURL + id);
  }

}
