import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';

import '../../assets/js/perfect-scrollbar.jquery.min.js';

declare var jquery:any;
declare var $ :any;

@Component({
  selector: 'app-home2',
  templateUrl: './home2.component.html',
 // styleUrls: ['./home2.component.css']
 styleUrls: ['../../assets/css/style.min.css']
})
export class Home2Component implements OnInit {

  title = 'Test';
  constructor(private registerapi :ApiClientService,private router: Router) { }

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
  ngOnInit() {
   // location.reload()
   // this.router.navigateByUrl('/home');
   this.router.navigateByUrl('/redirect');
  }

}
