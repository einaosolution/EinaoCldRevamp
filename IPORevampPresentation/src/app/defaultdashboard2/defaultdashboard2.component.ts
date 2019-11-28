import { Component, OnInit ,Input } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';
import {Role} from '../Role';

import {  NgZone } from "@angular/core";
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";

am4core.useTheme(am4themes_animated);

@Component({
  selector: 'app-defaultdashboard2',
  templateUrl: './defaultdashboard2.component.html',
  styleUrls: ['./defaultdashboard2.component.css']
})
export class Defaultdashboard2Component implements OnInit {
  private chart: am4charts.XYChart;
tempuserapprovalcount= "0"
@Input() registerapi2: any;
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
totalpending=0
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
  public chartdata2=[]

  public chartdata3=[]
  public chartdata=[]

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
  constructor(private registerapi :ApiClientService ,private router: Router ,private zone: NgZone ) {

   // this.getdata();


  }

  showchart() {
    let chart = am4core.create("chartdiv", am4charts.XYChart);
    chart.data = this.chartdata;

    let categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
    categoryAxis.dataFields.category = "country";
    categoryAxis.title.text = "Countries";

    let valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
    valueAxis.title.text = "Litres sold (M)";

    // Create series
    let series = chart.series.push(new am4charts.ColumnSeries());
    series.dataFields.valueY = "litres";
    series.dataFields.categoryX = "country";
    series.name = "Sales";
    series.columns.template.tooltipText = "Series: {name}\nCategory: {categoryX}\nValue: {valueY}";
    series.columns.template.fill = am4core.color("#104547"); //

  }

  ngAfterViewInit() {

    localStorage.setItem('ip',"dashboard" );
  this.loginrole = localStorage.getItem('Roleid');
  if (this.loginrole  ==Role.RegistrarTrademark) {
 this.trademarkregistra = true;
this.LoadTrademarkRegistra()

    this.busy = this.registerapi
.TrademarkUserRoleCount()
.then((response: any) => {

let data =  {
  officer:"Appeal Officer" ,

  NumberInUnit:response.appealOfficerCount

  }



  this.chartdata3.push(data)


  data =  {
    officer:"Certificate Officer" ,

    NumberInUnit:response.certificateOfficerCount

    }

    this.chartdata3.push(data)


    data =  {
    officer:"Examiner Officer" ,

    NumberInUnit:response.examinerOfficerCount

    }

    this.chartdata3.push(data)


     data =  {
    officer:"Publication Officer" ,

    NumberInUnit:response.publicationOfficerCount

    }

    this.chartdata3.push(data)





    console.log("chart data 3")
    console.log(this.chartdata3)



  this.zone.runOutsideAngular(() => {


   let  chart4 = am4core.create("chartdiv", am4charts.XYChart3D);

// Add data
chart4.data = this.chartdata3;

 let  title = chart4.titles.create();
   title.text = "Number Of Employee Per Trademark Unit";
   title.fontSize = 25;
   title.marginBottom = 30;

// Create axes
let categoryAxis2 = chart4.xAxes.push(new am4charts.CategoryAxis());


categoryAxis2.dataFields.category = "officer";
categoryAxis2.renderer.labels.template.rotation = 270;
categoryAxis2.renderer.labels.template.hideOversized = false;
categoryAxis2.renderer.minGridDistance = 20;
categoryAxis2.renderer.labels.template.horizontalCenter = "right";
categoryAxis2.renderer.labels.template.verticalCenter = "middle";
categoryAxis2.tooltip.label.rotation = 270;
categoryAxis2.tooltip.label.horizontalCenter = "right";
categoryAxis2.tooltip.label.verticalCenter = "middle";

let valueAxis2 = chart4.yAxes.push(new am4charts.ValueAxis());
valueAxis2.title.text = "Number Of Staff";
valueAxis2.title.fontWeight = "bold";

// Create series
let  series2 = chart4.series.push(new am4charts.ColumnSeries3D());
series2.dataFields.valueY = "NumberInUnit";
series2.dataFields.categoryX = "officer";
series2.name = "Officer Per Unit";
series2.tooltipText = "{categoryX}: [bold]{valueY}[/]";
series2.columns.template.fillOpacity = .8;
//series2.columns.template.fill = am4core.color("#006837"); //

let  columnTemplate = series2.columns.template;
columnTemplate.strokeWidth = 2;
columnTemplate.strokeOpacity = 1;
columnTemplate.stroke = am4core.color("#FFFFFF");




chart4.cursor = new am4charts.XYCursor();
chart4.cursor.lineX.strokeOpacity = 0;
chart4.cursor.lineY.strokeOpacity = 0;

chart4.colors.list = [
  am4core.color("#006837"),
 am4core.color("#D65DB1"),
  am4core.color("#FF6F91"),
  am4core.color("#FF9671"),
  am4core.color("#FFC75F"),
  am4core.color("#F9F871")
];


columnTemplate.adapter.add("fill", function(fill, target) {
  return chart4.colors.getIndex(target.dataItem.index);
})

columnTemplate.adapter.add("stroke", function(stroke, target) {
  return chart4.colors.getIndex(target.dataItem.index);
})





})
 // this.ReceiveappealCount = response


})
         .catch((response: any) => {


})



  }



  if (this.loginrole  ==Role.RegistrarPatent) {

    this.patentregistra = true;
    this.LoadPatentRegistra()

       this.busy = this.registerapi
   .PatentUserRoleCount()
   .then((response: any) => {

   let data =  {
     officer:"Appeal Officer" ,

     NumberInUnit:response.appealOfficerCount

     }

     this.chartdata3.push(data)


     data =  {
       officer:"Certificate Officer" ,

       NumberInUnit:response.certificateOfficerCount

       }

       this.chartdata3.push(data)


       data =  {
       officer:"Examiner Officer" ,

       NumberInUnit:response.examinerOfficerCount

       }

       this.chartdata3.push(data)


        data =  {
       officer:"Publication Officer" ,

       NumberInUnit:response.publicationOfficerCount

       }

       this.chartdata3.push(data)





       console.log("chart data 3")
       console.log(this.chartdata3)



     this.zone.runOutsideAngular(() => {






      //  added by tony

      let  chart4 = am4core.create("chartdiv2", am4charts.XYChart3D);

// Add data
chart4.data = this.chartdata3;

 let  title = chart4.titles.create();
   title.text = "Number Of Employee Per Patent Unit";
   title.fontSize = 25;
   title.marginBottom = 30;

// Create axes
let categoryAxis2 = chart4.xAxes.push(new am4charts.CategoryAxis());
categoryAxis2.dataFields.category = "officer";
categoryAxis2.renderer.labels.template.rotation = 270;
categoryAxis2.renderer.labels.template.hideOversized = false;
categoryAxis2.renderer.minGridDistance = 20;
categoryAxis2.renderer.labels.template.horizontalCenter = "right";
categoryAxis2.renderer.labels.template.verticalCenter = "middle";
categoryAxis2.tooltip.label.rotation = 270;
categoryAxis2.tooltip.label.horizontalCenter = "right";
categoryAxis2.tooltip.label.verticalCenter = "middle";

let valueAxis2 = chart4.yAxes.push(new am4charts.ValueAxis());
valueAxis2.title.text = "Number Of Staff";
valueAxis2.title.fontWeight = "bold";

// Create series
let  series2 = chart4.series.push(new am4charts.ColumnSeries3D());
series2.dataFields.valueY = "NumberInUnit";
series2.dataFields.categoryX = "officer";
series2.name = "Officer Per Unit";
series2.tooltipText = "{categoryX}: [bold]{valueY}[/]";
series2.columns.template.fillOpacity = .8;
//series2.columns.template.fill = am4core.color("#006837"); //

let  columnTemplate = series2.columns.template;
columnTemplate.strokeWidth = 2;
columnTemplate.strokeOpacity = 1;
columnTemplate.stroke = am4core.color("#FFFFFF");




chart4.cursor = new am4charts.XYCursor();
chart4.cursor.lineX.strokeOpacity = 0;
chart4.cursor.lineY.strokeOpacity = 0;

chart4.colors.list = [
  am4core.color("#006837"),
 am4core.color("#D65DB1"),
  am4core.color("#FF6F91"),
  am4core.color("#FF9671"),
  am4core.color("#FFC75F"),
  am4core.color("#F9F871")
];


columnTemplate.adapter.add("fill", function(fill, target) {
  return chart4.colors.getIndex(target.dataItem.index);
})

columnTemplate.adapter.add("stroke", function(stroke, target) {
  return chart4.colors.getIndex(target.dataItem.index);
})




   })
    // this.ReceiveappealCount = response


   })
            .catch((response: any) => {


   })



     }

     if (this.loginrole  ==Role.RegistrarDesign) {
    this.LoaddesignRegistra()
      this.designregistra = true;

      this.busy = this.registerapi
  .DesignUserRoleCount()
  .then((response: any) => {

  let data =  {
    officer:"Appeal Officer" ,

    NumberInUnit:response.appealOfficerCount

    }

    this.chartdata3.push(data)


    data =  {
      officer:"Certificate Officer" ,

      NumberInUnit:response.certificateOfficerCount

      }

      this.chartdata3.push(data)


      data =  {
      officer:"Examiner Officer" ,

      NumberInUnit:response.examinerOfficerCount

      }

      this.chartdata3.push(data)


       data =  {
      officer:"Publication Officer" ,

      NumberInUnit:response.publicationOfficerCount

      }

      this.chartdata3.push(data)





      console.log("chart data 3")
      console.log(this.chartdata3)



    this.zone.runOutsideAngular(() => {






     //  added by tony

     let  chart4 = am4core.create("chartdiv3", am4charts.XYChart3D);

// Add data
chart4.data = this.chartdata3;

let  title = chart4.titles.create();
  title.text = "Number Of Employee Per Design Unit";
  title.fontSize = 25;
  title.marginBottom = 30;

// Create axes
let categoryAxis2 = chart4.xAxes.push(new am4charts.CategoryAxis());
categoryAxis2.dataFields.category = "officer";
categoryAxis2.renderer.labels.template.rotation = 270;
categoryAxis2.renderer.labels.template.hideOversized = false;
categoryAxis2.renderer.minGridDistance = 20;
categoryAxis2.renderer.labels.template.horizontalCenter = "right";
categoryAxis2.renderer.labels.template.verticalCenter = "middle";
categoryAxis2.tooltip.label.rotation = 270;
categoryAxis2.tooltip.label.horizontalCenter = "right";
categoryAxis2.tooltip.label.verticalCenter = "middle";

let valueAxis2 = chart4.yAxes.push(new am4charts.ValueAxis());
valueAxis2.title.text = "Number Of Staff";
valueAxis2.title.fontWeight = "bold";

// Create series
let  series2 = chart4.series.push(new am4charts.ColumnSeries3D());
series2.dataFields.valueY = "NumberInUnit";
series2.dataFields.categoryX = "officer";
series2.name = "Officer Per Unit";
series2.tooltipText = "{categoryX}: [bold]{valueY}[/]";
series2.columns.template.fillOpacity = .8;
//series2.columns.template.fill = am4core.color("#006837"); //

let  columnTemplate = series2.columns.template;
columnTemplate.strokeWidth = 2;
columnTemplate.strokeOpacity = 1;
columnTemplate.stroke = am4core.color("#FFFFFF");




chart4.cursor = new am4charts.XYCursor();
chart4.cursor.lineX.strokeOpacity = 0;
chart4.cursor.lineY.strokeOpacity = 0;

chart4.colors.list = [
 am4core.color("#006837"),
am4core.color("#D65DB1"),
 am4core.color("#FF6F91"),
 am4core.color("#FF9671"),
 am4core.color("#FFC75F"),
 am4core.color("#F9F871")
];


columnTemplate.adapter.add("fill", function(fill, target) {
 return chart4.colors.getIndex(target.dataItem.index);
})

columnTemplate.adapter.add("stroke", function(stroke, target) {
 return chart4.colors.getIndex(target.dataItem.index);
})




  })
   // this.ReceiveappealCount = response


  })
           .catch((response: any) => {


  })


     }

  }

  ngOnDestroy() {
    this.zone.runOutsideAngular(() => {
      if (this.chart) {
        this.chart.dispose();
      }
    });
  }

getdata() {
  localStorage.setItem('ip',"dashboard" );
  this.loginrole = localStorage.getItem('Roleid');
  if (this.loginrole  ==Role.RegistrarTrademark) {
    //this.LoadTrademarkRegistra()

  //  this.trademarkregistra = true;

  //  this.showchart();
  }


  if (this.loginrole  ==Role.RegistrarPatent) {
   // this.LoadPatentRegistra()

    //this.patentregistra = true;
  }


  if (this.loginrole  ==Role.RegistrarDesign) {
  //  this.LoaddesignRegistra()

    this.designregistra = true;
  }
}
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




  }

}
