import { Component, OnInit,Output,EventEmitter } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import {ApiClientService} from '../api-client.service';

import {Router} from '@angular/router';
import {
  trigger,
  state,
  style,
  animate,
  transition,
  query,
} from '@angular/animations';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: [ '../../assets/css/style.min.css']

})
export class HeaderComponent implements OnInit {
  subscription: any;
  loggedinuser:string =""
  loggedinemail:string =""
  addCss = false;
  loggeduser:string ="";
  profilepic;
  constructor(private registerapi :ApiClientService ) {

    this.subscription = this.registerapi.getNavChangeEmitter()
    .subscribe(item => this.selectedNavItem(item));
   }

   selectedNavItem(item) {

   // window.location.reload();
   this.registerapi.setPage(item)

   this.isSpecial()
  }
  isSpecial() {

    if (this.registerapi.getPage() == "Country") {
      this.addCss =true;

    }

    else {
      this.addCss =false;
    }


      }
      getprofilepic() {
        this.profilepic=localStorage.getItem('profilepic')

        this.loggeduser  =localStorage.getItem('loggeduser')
        this.loggedinemail=localStorage.getItem('username')

        if (this.profilepic && this.profilepic != null ) {
          return true ;
        }

        else {
          return false;
        }
      }
  ngOnInit() {
this.profilepic=  localStorage.getItem('profilepic')
this.loggedinemail=localStorage.getItem('username')






  }

}
