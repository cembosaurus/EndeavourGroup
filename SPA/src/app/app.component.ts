import { Component } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  _userId: number = 0;
  _trolleyExists: boolean = false;

  constructor() { }


  userIdChanged()
  {
    console.log("User ID: ", this._userId)
  }


  userIdSubmited(event: any) 
  {
    console.log(event.target.value);
  }

  trolleyExists(state: boolean)
  {
    this._trolleyExists = state;

    console.log(">>>>>>>>>>>>>>>>>>>>> trolley exists >>>>>>>>>>>>>>>>>", this._trolleyExists);
  }

}
