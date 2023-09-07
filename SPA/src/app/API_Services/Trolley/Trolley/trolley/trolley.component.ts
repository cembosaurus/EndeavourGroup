import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { TrolleyService } from '../../../../_services/trolley.service';
import { APIServiceResult } from '../../../../_models/APIServiceResult';
import { TrolleyReadDTO } from 'src/app/_models/TrolleyReadDTO';



@Component({
  selector: 'app-trolley',
  templateUrl: './trolley.component.html',
  styleUrls: ['./trolley.component.css']
})
export class TrolleyComponent implements OnChanges{

  @Input() _userId: number = 0;
  _trolley: TrolleyReadDTO;


  constructor(private trolleyService: TrolleyService) { 
    this._trolley = {
        TrolleyId: "",
        UserId: 0,
        Total: 0,
        DiscountedTotal: 0,
        SavedTotal: 0,
        TrolleyProducts: [{
          ProductId: 0,
          Name: "",
          SalePrice: 0,
          Amount: 0,
          ProductTotal: 0,
          ProductDiscountedPrice: 0,
          Saved: 0,
        }]
      };
  }

  


  ngOnChanges() {
    this.getTroley();
  }


  getTroley()
  {
    this.trolleyService.getTrolley(this._userId)
      .subscribe((result: APIServiceResult) => {
        this._trolley = result.data;


        console.log("xxxxxxxxxxxxxxxxxxxxxxxxxxx TROLLEY: ", this._trolley);

      });
  }




}
