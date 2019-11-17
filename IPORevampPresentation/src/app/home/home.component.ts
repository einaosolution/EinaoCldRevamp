import { Component, OnInit ,AfterViewInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import '../../assets/js/perfect-scrollbar.jquery.min.js';
import {Router} from '@angular/router';



declare var jquery:any;
declare var $ :any;



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
 //styleUrls: ['./home.component.css']
 styleUrls: []
  //styleUrls: []
})
export class HomeComponent implements OnInit,AfterViewInit {
  title = 'Test';
  pagerefreshed:boolean =false;
  subscription: any;

  constructor(private registerapi :ApiClientService ,private router: Router) {

this.getIpAddress()

   }

   getIpAddress() {

    $.getJSON('https://api.ipify.org?format=json', function(data){
      console.log("ip");
      localStorage.setItem('ip',data.ip );


      console.log(data);
  });
}


  ngAfterViewInit(){
   // window.location.reload();

  // window.location.href=window.location.href;

}

  onSubmit2() {
    $("#loginform").slideUp();
    $("#recoverform").fadeIn();
  }

  islogged() {
   var vpassord = localStorage.getItem('ChangePassword');
    if (this.registerapi.gettoken() && vpassord =="True" ) {

      return true ;
    }

    else {

      return false;
    }
  }

  onSubmit() {
    this.router.navigateByUrl('/Dashboard');
  }
  ngOnInit() {

    if( window.localStorage )
    {

      if( !localStorage.getItem('firstLoad') )
      {
        localStorage['firstLoad'] = true;
       window.location.reload();
      }
      else {


    if (this.islogged()) {

     // this.router.navigateByUrl('/Dashboard2');
      this.router.navigateByUrl('/home');
     }

     else {
      this.router.navigateByUrl('/logout');



     }
        localStorage.removeItem('firstLoad');

    }

    }














  }

}
