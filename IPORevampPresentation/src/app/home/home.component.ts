import { Component, OnInit ,AfterViewInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import '../../assets/js/perfect-scrollbar.jquery.min.js';
import {Router} from '@angular/router';

import { PerfectScrollbar } from '../../../node_modules/perfect-scrollbar/dist/perfect-scrollbar.min.js';

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

    this.subscription = this.registerapi.getNavChangeEmitter()
    .subscribe(item => this.selectedNavItem(item));

   }

   selectedNavItem(item) {
    this.islogged()
    window.location.reload();
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

    if (this.registerapi.gettoken()) {

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
   // this.router.navigateByUrl('/Dashboard');

   //this.registerapi.VChangeEvent("kkkkk") ;

   if (this.islogged()) {
    this.router.navigateByUrl('/Dashboard/Dashboard2');
   }


  }

}
