import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { TrolleyService } from '../../../../_services/trolley.service';
import { CatalogueProduct } from '../../../../_models/catalogueProduct';
import { APIServiceResult } from '../../../../_models/APIServiceResult';
import { environment } from '../../../../environments/environment';
import { TrolleyReadDTO } from 'src/app/_models/TrolleyReadDTO';



@Component({
  selector: 'app-trolley',
  templateUrl: './trolley.component.html',
  styleUrls: ['./trolley.component.css']
})
export class TrolleyComponent implements OnInit, OnChanges{

  @Input() _userId: number = 0;
  _photosURL = environment.gatewayUrl + 'photos/';
  _catalogueProducts: CatalogueProduct[] | undefined;
  _trolley: TrolleyReadDTO | undefined;

  constructor(private trolleyService: TrolleyService) { }


  ngOnInit(): void {
    this.getTroley();
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
