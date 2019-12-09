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
 trademarksearch =""
 trademarkexaminer =""
 trademarkpublication =""
 trademarkcertificate =""
 patentcertificate =""
 trademarkrecordal =""
 trademarkopposition =""
 individualusersrole =""
 patentsearch =""
 patentexaminer =""
 designsearch =""
 designexaminer =""
 designpublication =""
 designcertificate =""
 designrecordal =""
 AppealOfficerTrademark=""
 AppealOfficerPatent=""
 AppealOfficerDesign =""
 trademarkmigration=""
 patentmigration=""
 designmigration=""
 SuperAdmin= ""
 Admin =""
 randnumber
  loginrole =""
  profilepic;
  filepath:string =""
  recordcount:any ;
  constructor(public registerapi :ApiClientService ,private router: Router) {

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
    this.trademarksearch= Role.TrademarkSearch;
    this.trademarkexaminer= Role.TrademarkExaminer;
    this.trademarkpublication =Role.TrademarkPublicationOfficer ;
    this.trademarkcertificate =Role.TrademarkCertificateOfficer ;
    this.trademarkrecordal = Role.TrademarkRecordalOfficer ;
    this.trademarkopposition =Role.TrademarkOppositionOfficer ;
    this.patentsearch =Role.PatentSearch ;
    this.patentexaminer = Role.PatentExaminer ;
    this.patentcertificate = Role.PatentCertificateOfficer;
    this.designsearch = Role.DesignSearch;
    this.designexaminer = Role.DesignExaminer;
    this.designpublication = Role.DesignPublicationOfficer;
    this.designcertificate = Role.DesignCertificateOfficer;
    this.designrecordal = Role.DesignRecordalOfficer;
    this.AppealOfficerTrademark = Role.AppealOfficerTrademark ;
    this.AppealOfficerPatent= Role.AppealOfficerPatent
    this.AppealOfficerDesign= Role.AppealOfficerDesign ;
    this.SuperAdmin = Role.SuperAdmin;
    this.Admin= Role.Admin
    this.trademarkmigration = Role.DataMigration
    this.patentmigration = Role.DataMigration
    this.designmigration = Role.DataMigration
        this.profilepic=  localStorage.getItem('profilepic')
        this.registerapi.setPage("Country")
        this.registerapi.VChangeEvent("Dashboard");







//alert(Role.RegistrarTrademark)


  }

}
