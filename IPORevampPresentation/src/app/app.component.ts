import { Component , OnInit , OnDestroy} from '@angular/core';
import { Location } from "@angular/common";

import { Subscription } from 'rxjs';
import {ApiClientService} from './api-client.service';

import {Router} from '@angular/router';
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
export class AppComponent implements OnInit ,OnDestroy {
  title = 'Todo';
  subscription: any;

  showval=true;
  noimage="NoImage2.png"
  filepath:string =""
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

  constructor(private registerapi :ApiClientService , private router: Router ,location: Location, ) {

    this.subscription = this.registerapi.getNavChangeEmitter()
    .subscribe(item => this.selectedNavItem(item));
  }

  ngOnDestroy() {
    // unsubscribe to ensure no memory leaks
    this.subscription.unsubscribe();
}

  getRouteAnimation(outlet) {
    return outlet.activatedRouteData.animation
  }

  onSubmitProfile() {

    this.router.navigateByUrl('/Dashboard/UserProfile');
  }

  onSubmit2 () {


    $("#loginform").slideUp();
        $("#recoverform").fadeIn();
    // ==============================================================
    // Login and Recover Password
    // ==============================================================

  }

  selectedNavItem(item) {

    // window.location.reload();

    if (item =="Logout") {
      this.showval = false;
    }


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

  islogged() {
    //if (localStorage.getItem('token')) {
      var kkp =this.registerapi.gettoken();


      if (kkp) {

        this.showval=true;
      return true ;
    }

    else {
      this.showval=false;
      return false;
    }
  }
  ngOnInit() {
   //this.islogged()
   this.filepath = this.registerapi.GetFilepath2();
   if (this.islogged()) {

   //  this.router.navigateByUrl('/Dashboard/Dashboard2');
   //alert( window.location.href)
    // this.router.navigateByUrl('/Dashboard2');
   //var navigateto = window.location.href
   //window.location.href = navigateto
    //this.router.navigateByUrl(window.location.href);
     }

     else {
     // this.router.navigateByUrl('/logout');
    //  return;

     }
this.profilepic=  localStorage.getItem('profilepic')

this.loggedinemail=localStorage.getItem('username')


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
