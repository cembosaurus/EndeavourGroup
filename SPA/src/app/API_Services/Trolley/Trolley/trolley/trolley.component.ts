import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
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
  @Output() _trolleyExists = new EventEmitter<boolean>();
  _trolley: TrolleyReadDTO | undefined;


  constructor(private trolleyService: TrolleyService) { }

  


  ngOnChanges() {
    this.getTroley();
  }


  getTroley()
  {
    this.trolleyService.getTrolley(this._userId)
      .subscribe((result: APIServiceResult) => {
        this._trolley = result.data;


        console.log("xxxxxxxxxxxxxxxxxxxxxxxxxxx TROLLEY: ", this._trolley);


        this._trolleyExists.emit(result.data == null ? false : true);
      });
  }




}
