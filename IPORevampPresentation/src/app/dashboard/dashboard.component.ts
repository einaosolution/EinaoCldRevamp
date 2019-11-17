import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';
import {Role} from '../Role';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  subscription: any;
  vdate:any =new Date()
  registrarole =""
  noimage="NoImage2.png"
  patentregistrarole =""
  designregistrarole =""
 corporateusersrole =""
 individualusersrole =""
 randnumber
  loginrole =""
  profilepic;
  filepath:string =""
  constructor(private registerapi :ApiClientService ,private router: Router) {

  }

  selectedNavItem(item) {


  //  this.registerapi.setPage("Dashboard")
   // setPage(kk)
  }

  getprofilepic() {
        this.profilepic=localStorage.getItem('profilepic')

     console.log("profile pic ")
     console.log(this.profilepic)

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
    this.filepath = this.registerapi.GetFilepath2();

    this.loginrole = localStorage.getItem('Roleid');
    this.registrarole = Role.RegistrarTrademark;
    this.patentregistrarole = Role.RegistrarPatent;
    this.designregistrarole = Role.RegistrarDesign;
    this.corporateusersrole = Role.Corporate;
    this.individualusersrole = Role.Individual;
        this.profilepic=  localStorage.getItem('profilepic')
        this.registerapi.setPage("Country")
        this.registerapi.VChangeEvent("Dashboard");


//alert(Role.RegistrarTrademark)


  }

}
