import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  subscription: any;
  vdate:any =new Date()
  profilepic;
  constructor(private registerapi :ApiClientService ,private router: Router) {

  }

  selectedNavItem(item) {


  //  this.registerapi.setPage("Dashboard")
   // setPage(kk)
  }

  getprofilepic() {
        this.profilepic=localStorage.getItem('profilepic')



        if (this.profilepic && this.profilepic != null ) {
          return true ;
        }

        else {
          return false;
        }
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

  ngOnInit() {


    this.profilepic=  localStorage.getItem('profilepic')
    this.registerapi.setPage("Country")
    this.registerapi.VChangeEvent("Dashboard");
  }

}
