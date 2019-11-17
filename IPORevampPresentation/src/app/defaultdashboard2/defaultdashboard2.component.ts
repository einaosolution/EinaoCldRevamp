import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';
import {Role} from '../Role';

@Component({
  selector: 'app-defaultdashboard2',
  templateUrl: './defaultdashboard2.component.html',
  styleUrls: ['./defaultdashboard2.component.css']
})
export class Defaultdashboard2Component implements OnInit {
tempuserapprovalcount= "0"
appealCount= "0"
patentappealCount= "0"
designappealCount= "0"
ReceiveappealCount= "0"
PatentTreatedAppealCount= "0"

DesignTreatedAppealCount = "0"
loginrole =""
Dvalue  :any[]=[];
Dvalue2  :any[]=[];
Dvalue3  :any[]=[];
bb:boolean=false;
bb2:boolean=false;
bb3:boolean=false;
trademarkregistra:boolean=false;
patentregistra:boolean=false;
designregistra:boolean=false;
busy: Promise<any>;
registrarole =""
public barChartOptions: ChartOptions = {
  responsive: true,
};

//public barChartLabels: Label[] = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
public barChartLabels: Label[] = ['Search Officer', 'Examiner Officer', 'Publication Officer', 'Certificate Officer ', 'Appeal Officer'];
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;
  public barChartPlugins = [];

  public barChartData: ChartDataSets[] = [
    { data: this.Dvalue, label: 'Series A' }
  ];

  public barChartData2: ChartDataSets[] = [
    { data: this.Dvalue2, label: 'Series A' }
  ];

  public barChartData3: ChartDataSets[] = [
    { data: this.Dvalue3, label: 'Series A' }
  ];

  public barColors2: any[] = [


    { backgroundColor: ['#215E21', '#215E21', '#215E21', '#215E21', '#215E21', '#215E21', '#215E21', '#215E21', '#215E21', '#215E21', '#215E21', '#215E21']}

  ];
  constructor(private registerapi :ApiClientService ,private router: Router ) { }


  receiveappeal() {
    this.router.navigateByUrl('/Dashboard/ReceiveAppeal');
  }

  receiveappeal2() {
    this.router.navigateByUrl('/Patent/ReceiveAppeal');
  }

  receiveappeal3() {
    this.router.navigateByUrl('/Design/DesignReceiveAppeal');
  }


 assignappeal() {
    this.router.navigateByUrl('/Patent/DelegateAppeal');
  }

  assignappeal2() {
    this.router.navigateByUrl('/Design/DesignRegistraAppeal');
  }

  userappeal() {
    this.router.navigateByUrl('/Dashboard/AssignAppeal');
  }


  approveuser() {
    this.router.navigateByUrl('/Dashboard/PendingUser');
  }

  LoadTrademarkRegistra() {
    var userid =localStorage.getItem('UserId');
    this.busy =     this.registerapi
    .GetAllTempCount(userid)
    .then((response: any) => {

      console.log("temp user")

      console.log(response)
      this.tempuserapprovalcount = response


    })
             .catch((response: any) => {


})

this.busy = this.registerapi
.GetAppealCount(userid)
.then((response: any) => {

  console.log("appeal count")

  console.log(response)
  this.appealCount = response


})
         .catch((response: any) => {


})



this.busy = this.registerapi
.GetReceiveAppealCount(userid)
.then((response: any) => {

  console.log("appeal count")

  console.log(response)
  this.ReceiveappealCount = response


})
         .catch((response: any) => {


})


this.busy = this.registerapi
.TrademarkUserRoleCount()
.then((response: any) => {

  console.log("TrademarkUserRoleCount")

  this.Dvalue.push(response.searchOfficerCount  )
  this.Dvalue.push(response.examinerOfficerCount  )
  this.Dvalue.push(response.publicationOfficerCount  )
  this.Dvalue.push(response.certificateOfficerCount  )
  this.Dvalue.push(response.appealOfficerCount  )

  this.bb = true

  console.log(response)
 // this.ReceiveappealCount = response


})
         .catch((response: any) => {


})
  }


  LoadPatentRegistra() {
    var userid =localStorage.getItem('UserId');
    this.busy =     this.registerapi
    .GetAllTempCount(userid)
    .then((response: any) => {

      console.log("temp user")

      console.log(response)
      this.tempuserapprovalcount = response


    })
             .catch((response: any) => {


})

this.busy = this.registerapi
.GetPatentAppealCount(userid)
.then((response: any) => {

  console.log("appeal count")

  console.log(response)
  this.patentappealCount = response


})
         .catch((response: any) => {


})



this.busy = this.registerapi
.GetPatentTreatedAppealCount(userid)
.then((response: any) => {

  console.log("appeal count")

  console.log(response)
  this.PatentTreatedAppealCount = response


})
         .catch((response: any) => {


})


this.busy = this.registerapi
.PatentUserRoleCount()
.then((response: any) => {

  console.log("PatentUserRoleCount")

  this.Dvalue2.push(response.searchOfficerCount  )
  this.Dvalue2.push(response.examinerOfficerCount  )
  this.Dvalue2.push(response.publicationOfficerCount  )
  this.Dvalue2.push(response.certificateOfficerCount  )
  this.Dvalue2.push(response.appealOfficerCount  )

  this.bb2 = true

  console.log(response)
 // this.ReceiveappealCount = response


})
         .catch((response: any) => {


})
  }

  LoaddesignRegistra() {
    var userid =localStorage.getItem('UserId');
    this.busy =     this.registerapi
    .GetAllTempCount(userid)
    .then((response: any) => {

      console.log("temp user")

      console.log(response)
      this.tempuserapprovalcount = response


    })
             .catch((response: any) => {


})

this.busy = this.registerapi
.GetDesignAppealCount(userid)
.then((response: any) => {

  console.log("appeal count")

  console.log(response)
  this.designappealCount = response


})
         .catch((response: any) => {


})



this.busy = this.registerapi
.GetDesignReceiveAppealCount(userid)
.then((response: any) => {

  console.log("appeal count")

  console.log(response)
  this.DesignTreatedAppealCount = response


})
         .catch((response: any) => {


})


this.busy = this.registerapi
.DesignUserRoleCount()
.then((response: any) => {

  console.log("DesignUserRoleCount")

  this.Dvalue3.push(response.searchOfficerCount  )
  this.Dvalue3.push(response.examinerOfficerCount  )
  this.Dvalue3.push(response.publicationOfficerCount  )
  this.Dvalue3.push(response.certificateOfficerCount  )
  this.Dvalue3.push(response.appealOfficerCount  )

  this.bb3 = true

  console.log(response)
 // this.ReceiveappealCount = response


})
         .catch((response: any) => {


})
  }
  ngOnInit() {
    this.loginrole = localStorage.getItem('Roleid');
    if (this.loginrole  ==Role.RegistrarTrademark) {
      this.LoadTrademarkRegistra()

      this.trademarkregistra = true;
    }


    if (this.loginrole  ==Role.RegistrarPatent) {
      this.LoadPatentRegistra()

      this.patentregistra = true;
    }


    if (this.loginrole  ==Role.RegistrarDesign) {
      this.LoaddesignRegistra()

      this.designregistra = true;
    }



  }

}
