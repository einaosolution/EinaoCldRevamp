import { Component, OnInit,Output,EventEmitter } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';

import '../../assets/js/perfect-scrollbar.jquery.min.js';
declare var jquery:any;
declare var $ :any;
import * as jwt_decode from  "jwt-decode"

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
  datediff :number ;
  row :any[]=[];
  profilepic;
  constructor(private registerapi :ApiClientService ,private router: Router ) {

    this.subscription = this.registerapi.getNavChangeEmitter()
    .subscribe(item => this.selectedNavItem(item));
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

    if (this.islogged()) {
//testing

     }

  else {
      this.router.navigateByUrl('/logout');
      return;

     }
this.profilepic=  localStorage.getItem('profilepic')

this.loggedinemail=localStorage.getItem('username')

try {
  var lastpasswordchange= localStorage.getItem('lastpasswordchange')
  if (!isNaN(Date.parse(lastpasswordchange))) {

    var dd = Date.parse(lastpasswordchange)
    if (dd !=NaN) {
     var today = new Date();
     var diff =  Math.floor(( (Date.parse(lastpasswordchange))  - Date.parse(today.toString()) ) / 86400000);
   this.datediff = diff + 60;
  //   alert(diff + 60)

 }
  }
 else {
  this.datediff =0;
  // alert("Invalid Date")
 }

}

catch(err) {
  console.log("Date Error")

}
try {
var ppp2 = localStorage.getItem('Roles');
this.row=[]
this.row= JSON.parse(ppp2);
console.log("roles")
console.log(this.row)

}
catch(err) {

}






  }

}
