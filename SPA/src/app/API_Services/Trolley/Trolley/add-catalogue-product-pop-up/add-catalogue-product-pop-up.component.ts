import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';


@Component({
  selector: 'app-add-catalogue-product-pop-up',
  templateUrl: './add-catalogue-product-pop-up.component.html',
  styleUrls: ['./add-catalogue-product-pop-up.component.css']
})
export class AddCatalogueProductPopUpComponent implements OnInit {

  @Output() _productAmountEmiter = new EventEmitter<any>();
  _amountOnTrolley: number;
  _productId: number;




  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private dialogRef: MatDialogRef<AddCatalogueProductPopUpComponent>) {
    this._productId = data.productId;
    this._amountOnTrolley = data.amount;
  }

  ngOnInit(): void { }

  ngOnDestroy()
  {
    this.dialogRef.close(this._amountOnTrolley); 
  }




  increaseOnTrolley()
  {
    this._amountOnTrolley++;
  }


  decreaseOnTrolley()
  {
    if(this._amountOnTrolley > 0)
    this._amountOnTrolley--;
  }

}
