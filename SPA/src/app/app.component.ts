import { Component } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  _userId: number = 0;

  constructor() { }


  userIdChanged(){console.log("User ID: ", this._userId)}

}
