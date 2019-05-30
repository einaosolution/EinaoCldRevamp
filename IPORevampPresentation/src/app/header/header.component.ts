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
  addCss2 = false;
  loggeduser:string ="";
  menuclass:string ="";
  menuclass2:string ="Security";
  menuclass3:string ="Country";
  row :any[]=[];
  profilepic;
  constructor(private registerapi :ApiClientService ) {

    this.subscription = this.registerapi.getNavChangeEmitter()
    .subscribe(item => this.selectedNavItem(item));
   }

   selectedNavItem(item) {

   // window.location.reload();
   this.menuclass=item

   this.registerapi.setPage(item)

   this.isSpecial()
  }
  isSpecial() {

    if (this.registerapi.getPage() == "Country") {
      this.addCss =true;
      this.addCss2 =false;
      this.menuclass3="Country"
       this.menuclass2=""

    }

    else if (this.registerapi.getPage() == "Security") {
      this.addCss =false;
      this.addCss2 =true;
      this.menuclass2="Security"
      this.menuclass3=""
    }

    else {
      this.addCss =false;
      this.addCss2 =false;
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
try {
var ppp2 = localStorage.getItem('Roles');
this.row= JSON.parse(ppp2);
console.log("roles")
console.log(this.row)

}
catch(err) {

}






  }

}
