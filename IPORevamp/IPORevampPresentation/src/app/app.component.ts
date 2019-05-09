import { Component , OnInit } from '@angular/core';
import {ApiClientService} from './api-client.service';
import {
  transition,
  trigger,
  query,
  style,
  animate,
  group,
  animateChild
} from '@angular/animations';


declare var jquery:any;
declare var $ :any;


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
// styleUrls: ['./app.component.css'],
 styleUrls: ['../assets/css/style.min.css' ] ,
  animations: [
    trigger('routerAnimation', [
      transition('* <=> *', [
        // Initial state of new route
        query(':enter',
          style({
            position: 'fixed',
            width:'100%',
            transform: 'translateX(-100%)'
          }),
          {optional:true}),
        // move page off screen right on leave
        query(':leave',
          animate('500ms ease',
            style({
              position: 'fixed',
              width:'100%',
              transform: 'translateX(100%)'
            })
          ),
        {optional:true}),
        // move page in screen from left to right
        query(':enter',
          animate('500ms ease',
            style({
              opacity: 1,
              transform: 'translateX(0%)'
            })
          ),
        {optional:true}),
      ])
    ])
  ]

})
export class AppComponent implements OnInit {
  title = 'Todo';
  subscription: any;

  constructor(private registerapi :ApiClientService ) {

  }

  getRouteAnimation(outlet) {
    return outlet.activatedRouteData.animation
  }



  onSubmit2 () {


    $("#loginform").slideUp();
        $("#recoverform").fadeIn();
    // ==============================================================
    // Login and Recover Password
    // ==============================================================

  }



  islogged() {
    //if (localStorage.getItem('token')) {
      var kkp =this.registerapi.gettoken();


      if (kkp) {
      return true ;
    }

    else {
      return false;
    }
  }
  ngOnInit() {




  }





}
