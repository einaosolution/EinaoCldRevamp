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
  selector: 'app-defaultdashboardtrademarktemplate',
  templateUrl: './defaultdashboardtrademarktemplate.component.html',
  styleUrls: ['./defaultdashboardtrademarktemplate.component.css']
})
export class DefaultdashboardtrademarktemplateComponent implements OnInit {


 // private chart: any;
 // private chart4: any;

tempuserapprovalcount= "0"
@Input() registerapi2: any;
appealCount= "0"
patentappealCount= "0"
newapplication="0"
treatedapplication= "0"
kivapplication= "0"
designappealCount= "0"
totalsearchCount= 0
ReceiveappealCount= "0"
PatentTreatedAppealCount= "0"
Label1= "New Applications"
Label2= "Treated Applications"
Label3= "Re-search Application"
Label4= "Total Search"
ShowLabel1= true;
ShowLabel2= true;
ShowLabel3= true;
ShowLabel4= true;

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
searchrole:boolean=false;
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



  constructor(private registerapi :ApiClientService ,private router: Router ,private zone: NgZone ) {




  }





  ngAfterViewInit() {

    localStorage.setItem('ip',"dashboard" );
    this.loginrole = localStorage.getItem('Roleid');
    if (this.loginrole  ==Role.TrademarkSearch) {

 this.trademarksearch()


    }

    if (this.loginrole  ==Role.TrademarkExaminer) {

      this.trademarkexaminer()


         }

         if (this.loginrole  ==Role.TrademarkPublicationOfficer) {

          this.trademarkPublication()


             }



         if (this.loginrole  ==Role.TrademarkCertificateOfficer) {

          this.trademarkCertificate()


             }


             if (this.loginrole  ==Role.TrademarkRecordalOfficer) {

              this.trademarkCertificate()


                 }


                 if (this.loginrole  ==Role.TrademarkOppositionOfficer) {

                  this.trademarkOpposition()


                     }











}


trademarksearch() {



    this.busy = this.registerapi
.GetAllSearchDashBoardCount()
.then((response: any) => {

  console.log("search template response")

  console.log(response)

let data =  {
  officer:"New Application" ,

  NumberInUnit:response.result.newapplicationcount

  }
this.newapplication =response.result.newapplicationcount
  this.chartdata3.push(data)


  data =  {
    officer:"Treated Application" ,

    NumberInUnit:response.result.treatedApplicationcount

    }
    this.treatedapplication =response.result.treatedApplicationcount
    this.chartdata3.push(data)


    data =  {
    officer:"Re-Search Application" ,

    NumberInUnit:response.result.researchApplicationcount

    }
    this.kivapplication =response.result.researchApplicationcount
    this.chartdata3.push(data)








    console.log("chart data 3")
    console.log(this.chartdata3)



this.totalsearchCount = parseInt(this.kivapplication) + parseInt( this.treatedapplication)  + parseInt( this.newapplication)

  this.zone.runOutsideAngular(() => {


let chart4 = am4core.create("chartdiv", am4charts.XYChart3D);

// Add data
chart4.data = this.chartdata3;

 let  title = chart4.titles.create();
   title.text = "Search Unit";
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
valueAxis2.title.text = "Number Of Application";
valueAxis2.title.fontWeight = "bold";

// Create series
let  series2 = chart4.series.push(new am4charts.ColumnSeries3D());
series2.dataFields.valueY = "NumberInUnit";
series2.dataFields.categoryX = "officer";
series2.name = "Application";
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

 //next chart

 let totalsearchCount2 = parseInt(response.result.treatedApplicationcount) + parseInt( response.result.researchApplicationcount)  + parseInt( response.result.newapplicationcount)
 data =  {
    officer:"Total Search" ,

    NumberInUnit:totalsearchCount2

    }

    this.chartdata2.push(data)


    data =  {
      officer:"Treated Application" ,

      NumberInUnit:response.result.treatedApplicationcount

      }

      this.chartdata2.push(data)











   this.zone.runOutsideAngular(() => {


   let  chart = am4core.create("chartdiv2", am4charts.XYChart3D);

 // Add data
 chart.data = this.chartdata2;

 // let  title = chart4.titles.create();
  //  title.text = "Search Unit";
  //  title.fontSize = 25;
  //  title.marginBottom = 30;

 // Create axes
 let categoryAxis3 =  chart.xAxes.push(new am4charts.CategoryAxis());


 categoryAxis3.dataFields.category = "officer";
 categoryAxis3.renderer.labels.template.rotation = 270;
 categoryAxis3.renderer.labels.template.hideOversized = false;
 categoryAxis3.renderer.minGridDistance = 20;
 categoryAxis3.renderer.labels.template.horizontalCenter = "right";
 categoryAxis3.renderer.labels.template.verticalCenter = "middle";
 categoryAxis3.tooltip.label.rotation = 270;
 categoryAxis3.tooltip.label.horizontalCenter = "right";
 categoryAxis3.tooltip.label.verticalCenter = "middle";

 let valueAxis3 = chart.yAxes.push(new am4charts.ValueAxis());
 valueAxis3.title.text = "Number Of Application";
 valueAxis3.title.fontWeight = "bold";

 // Create series
 let  series3 = chart.series.push(new am4charts.ColumnSeries3D());
 series3.dataFields.valueY = "NumberInUnit";
 series3.dataFields.categoryX = "officer";
 series3.name = "Application";
 series3.tooltipText = "{categoryX}: [bold]{valueY}[/]";
 series3.columns.template.fillOpacity = .8;
 //series2.columns.template.fill = am4core.color("#006837"); //

 let  columnTemplate2 = series3.columns.template;
 columnTemplate2.strokeWidth = 2;
 columnTemplate2.strokeOpacity = 1;
 columnTemplate2.stroke = am4core.color("#FFFFFF");




chart.cursor = new am4charts.XYCursor();
chart.cursor.lineX.strokeOpacity = 0;
chart.cursor.lineY.strokeOpacity = 0;

chart.colors.list = [
   am4core.color("#006837"),
  am4core.color("#D65DB1"),
   am4core.color("#FF6F91"),
   am4core.color("#FF9671"),
   am4core.color("#FFC75F"),
   am4core.color("#F9F871")
 ];


 columnTemplate2.adapter.add("fill", function(fill, target) {
   return chart.colors.getIndex(target.dataItem.index);
 })

 columnTemplate2.adapter.add("stroke", function(stroke, target) {
   return chart.colors.getIndex(target.dataItem.index);
 })





 })


})
         .catch((response: any) => {


})











}




trademarkexaminer() {



  this.busy = this.registerapi
.GetAllSearchDashBoardCount()
.then((response: any) => {

console.log("search template response")

console.log(response)

let data =  {
officer:"New Application" ,

NumberInUnit:response.result.trademarkExaminerFreshcount

}
this.newapplication =response.result.trademarkExaminerFreshcount
this.chartdata3.push(data)


data =  {
  officer:"Treated Application" ,

  NumberInUnit:response.result.trademarkExaminerTreatedcount

  }
  this.treatedapplication =response.result.trademarkExaminerTreatedcount
  this.chartdata3.push(data)


  data =  {
  officer:"Re-Search Application" ,

  NumberInUnit:response.result.researchApplicationcount

  }
  this.kivapplication =response.result.researchApplicationcount
  this.chartdata3.push(data)








  console.log("chart data 3")
  console.log(this.chartdata3)



this.totalsearchCount = parseInt(this.kivapplication) + parseInt( this.treatedapplication)  + parseInt( this.newapplication)

this.zone.runOutsideAngular(() => {


let chart4 = am4core.create("chartdiv", am4charts.XYChart3D);

// Add data
chart4.data = this.chartdata3;

let  title = chart4.titles.create();
 title.text = "Examiner Unit";
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
valueAxis2.title.text = "Number Of Application";
valueAxis2.title.fontWeight = "bold";

// Create series
let  series2 = chart4.series.push(new am4charts.ColumnSeries3D());
series2.dataFields.valueY = "NumberInUnit";
series2.dataFields.categoryX = "officer";
series2.name = "Application";
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

//next chart

let totalsearchCount2 = parseInt(response.result.trademarkExaminerTreatedcount) + parseInt( response.result.researchApplicationcount)  + parseInt( response.result.trademarkExaminerFreshcount)
data =  {
  officer:"Total Search" ,

  NumberInUnit:totalsearchCount2

  }

  this.chartdata2.push(data)


  data =  {
    officer:"Treated Application" ,

    NumberInUnit:response.result.trademarkExaminerTreatedcount

    }

    this.chartdata2.push(data)











 this.zone.runOutsideAngular(() => {


 let  chart = am4core.create("chartdiv2", am4charts.XYChart3D);

// Add data
chart.data = this.chartdata2;

// let  title = chart4.titles.create();
//  title.text = "Search Unit";
//  title.fontSize = 25;
//  title.marginBottom = 30;

// Create axes
let categoryAxis3 =  chart.xAxes.push(new am4charts.CategoryAxis());


categoryAxis3.dataFields.category = "officer";
categoryAxis3.renderer.labels.template.rotation = 270;
categoryAxis3.renderer.labels.template.hideOversized = false;
categoryAxis3.renderer.minGridDistance = 20;
categoryAxis3.renderer.labels.template.horizontalCenter = "right";
categoryAxis3.renderer.labels.template.verticalCenter = "middle";
categoryAxis3.tooltip.label.rotation = 270;
categoryAxis3.tooltip.label.horizontalCenter = "right";
categoryAxis3.tooltip.label.verticalCenter = "middle";

let valueAxis3 = chart.yAxes.push(new am4charts.ValueAxis());
valueAxis3.title.text = "Number Of Application";
valueAxis3.title.fontWeight = "bold";

// Create series
let  series3 = chart.series.push(new am4charts.ColumnSeries3D());
series3.dataFields.valueY = "NumberInUnit";
series3.dataFields.categoryX = "officer";
series3.name = "Application";
series3.tooltipText = "{categoryX}: [bold]{valueY}[/]";
series3.columns.template.fillOpacity = .8;
//series2.columns.template.fill = am4core.color("#006837"); //

let  columnTemplate2 = series3.columns.template;
columnTemplate2.strokeWidth = 2;
columnTemplate2.strokeOpacity = 1;
columnTemplate2.stroke = am4core.color("#FFFFFF");




chart.cursor = new am4charts.XYCursor();
chart.cursor.lineX.strokeOpacity = 0;
chart.cursor.lineY.strokeOpacity = 0;

chart.colors.list = [
 am4core.color("#006837"),
am4core.color("#D65DB1"),
 am4core.color("#FF6F91"),
 am4core.color("#FF9671"),
 am4core.color("#FFC75F"),
 am4core.color("#F9F871")
];


columnTemplate2.adapter.add("fill", function(fill, target) {
 return chart.colors.getIndex(target.dataItem.index);
})

columnTemplate2.adapter.add("stroke", function(stroke, target) {
 return chart.colors.getIndex(target.dataItem.index);
})





})


})
       .catch((response: any) => {


})



}




trademarkPublication() {
this.ShowLabel3 = false;
this.Label1 = "Pending Publication";
this.Label2 = "Published Publication";
this.Label4 = "Unpublished Publication";



  this.busy = this.registerapi
.GetAllSearchDashBoardCount()
.then((response: any) => {

console.log("search template response")

console.log(response)

let data =  {
officer:"Pending Publication" ,

NumberInUnit:response.result.trademarkPublicationFreshcount

}
this.newapplication =response.result.trademarkPublicationFreshcount
this.chartdata3.push(data)


data =  {
  officer:"Published Publication" ,

  NumberInUnit:response.result.trademarkPublicationPublishcount

  }
  this.treatedapplication =response.result.trademarkPublicationPublishcount
  this.chartdata3.push(data)


  data =  {
  officer:"UnPublished Publication" ,

  NumberInUnit:response.result.trademarkUnPublishcount

  }
  this.kivapplication =response.result.trademarkUnPublishcount
  this.chartdata3.push(data)








  console.log("chart data 3")
  console.log(this.chartdata3)



this.totalsearchCount = parseInt(response.result.trademarkUnPublishcount)

this.zone.runOutsideAngular(() => {


let chart4 = am4core.create("chartdiv", am4charts.XYChart3D);

// Add data
chart4.data = this.chartdata3;

let  title = chart4.titles.create();
 title.text = "Publication Unit";
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
valueAxis2.title.text = "Number Of Application";
valueAxis2.title.fontWeight = "bold";

// Create series
let  series2 = chart4.series.push(new am4charts.ColumnSeries3D());
series2.dataFields.valueY = "NumberInUnit";
series2.dataFields.categoryX = "officer";
series2.name = "Application";
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

//next chart

let totalsearchCount2 = parseInt(response.result.trademarkPublicationPublishcount) + parseInt( response.result.trademarkUnPublishcount)  + parseInt( response.result.trademarkPublicationFreshcount)
data =  {
  officer:"Total Publication" ,

  NumberInUnit:totalsearchCount2

  }

  this.chartdata2.push(data)


  data =  {
    officer:"Published Publication" ,

    NumberInUnit:response.result.trademarkPublicationPublishcount

    }

    this.chartdata2.push(data)











 this.zone.runOutsideAngular(() => {


 let  chart = am4core.create("chartdiv2", am4charts.XYChart3D);

// Add data
chart.data = this.chartdata2;

// let  title = chart4.titles.create();
//  title.text = "Search Unit";
//  title.fontSize = 25;
//  title.marginBottom = 30;

// Create axes
let categoryAxis3 =  chart.xAxes.push(new am4charts.CategoryAxis());


categoryAxis3.dataFields.category = "officer";
categoryAxis3.renderer.labels.template.rotation = 270;
categoryAxis3.renderer.labels.template.hideOversized = false;
categoryAxis3.renderer.minGridDistance = 20;
categoryAxis3.renderer.labels.template.horizontalCenter = "right";
categoryAxis3.renderer.labels.template.verticalCenter = "middle";
categoryAxis3.tooltip.label.rotation = 270;
categoryAxis3.tooltip.label.horizontalCenter = "right";
categoryAxis3.tooltip.label.verticalCenter = "middle";

let valueAxis3 = chart.yAxes.push(new am4charts.ValueAxis());
valueAxis3.title.text = "Number Of Application";
valueAxis3.title.fontWeight = "bold";

// Create series
let  series3 = chart.series.push(new am4charts.ColumnSeries3D());
series3.dataFields.valueY = "NumberInUnit";
series3.dataFields.categoryX = "officer";
series3.name = "Application";
series3.tooltipText = "{categoryX}: [bold]{valueY}[/]";
series3.columns.template.fillOpacity = .8;
//series2.columns.template.fill = am4core.color("#006837"); //

let  columnTemplate2 = series3.columns.template;
columnTemplate2.strokeWidth = 2;
columnTemplate2.strokeOpacity = 1;
columnTemplate2.stroke = am4core.color("#FFFFFF");




chart.cursor = new am4charts.XYCursor();
chart.cursor.lineX.strokeOpacity = 0;
chart.cursor.lineY.strokeOpacity = 0;

chart.colors.list = [
 am4core.color("#006837"),
am4core.color("#D65DB1"),
 am4core.color("#FF6F91"),
 am4core.color("#FF9671"),
 am4core.color("#FFC75F"),
 am4core.color("#F9F871")
];


columnTemplate2.adapter.add("fill", function(fill, target) {
 return chart.colors.getIndex(target.dataItem.index);
})

columnTemplate2.adapter.add("stroke", function(stroke, target) {
 return chart.colors.getIndex(target.dataItem.index);
})





})


})
       .catch((response: any) => {


})



}



trademarkCertificate() {
 // this.ShowLabel3 = false;
  this.Label1 = "Paid Certificate";
  this.Label2 = "Issued Certificate";
  this.Label3 = "Renewed Certificate";
  this.Label4 = "Total Certificate";



    this.busy = this.registerapi
  .GetAllSearchDashBoardCount()
  .then((response: any) => {

  console.log("search template response")

  console.log(response)

  let data =  {
  officer:"Paid Certificate" ,

  NumberInUnit:response.result.trademarkPaidCertificatecount

  }
  this.newapplication =response.result.trademarkPaidCertificatecount
  this.chartdata3.push(data)


  data =  {
    officer:"Issued Certificate" ,

    NumberInUnit:response.result.trademarkIssuedCertificatecount

    }
    this.treatedapplication =response.result.trademarkIssuedCertificatecount
    this.chartdata3.push(data)


    data =  {
    officer:"Renewed Certificate" ,

    NumberInUnit:response.result.trademarkRenewedCertificatecount

    }
    this.kivapplication =response.result.trademarkRenewedCertificatecount
    this.chartdata3.push(data)








    console.log("chart data 3")
    console.log(this.chartdata3)



  this.totalsearchCount = parseInt(this.newapplication) + parseInt(this.treatedapplication)

  this.zone.runOutsideAngular(() => {


  let chart4 = am4core.create("chartdiv", am4charts.XYChart3D);

  // Add data
  chart4.data = this.chartdata3;

  let  title = chart4.titles.create();
   title.text = "Certificate Unit";
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
  valueAxis2.title.text = "Number Of Application";
  valueAxis2.title.fontWeight = "bold";

  // Create series
  let  series2 = chart4.series.push(new am4charts.ColumnSeries3D());
  series2.dataFields.valueY = "NumberInUnit";
  series2.dataFields.categoryX = "officer";
  series2.name = "Application";
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

  //next chart

  let totalsearchCount2 = parseInt(response.result.trademarkPaidCertificatecount) + parseInt( response.result.trademarkIssuedCertificatecount)
  data =  {
    officer:"Total Certificate" ,

    NumberInUnit:totalsearchCount2

    }

    this.chartdata2.push(data)


    data =  {
      officer:"Issued Certificate" ,

      NumberInUnit:response.result.trademarkIssuedCertificatecount

      }

      this.chartdata2.push(data)











   this.zone.runOutsideAngular(() => {


   let  chart = am4core.create("chartdiv2", am4charts.XYChart3D);

  // Add data
  chart.data = this.chartdata2;

  // let  title = chart4.titles.create();
  //  title.text = "Search Unit";
  //  title.fontSize = 25;
  //  title.marginBottom = 30;

  // Create axes
  let categoryAxis3 =  chart.xAxes.push(new am4charts.CategoryAxis());


  categoryAxis3.dataFields.category = "officer";
  categoryAxis3.renderer.labels.template.rotation = 270;
  categoryAxis3.renderer.labels.template.hideOversized = false;
  categoryAxis3.renderer.minGridDistance = 20;
  categoryAxis3.renderer.labels.template.horizontalCenter = "right";
  categoryAxis3.renderer.labels.template.verticalCenter = "middle";
  categoryAxis3.tooltip.label.rotation = 270;
  categoryAxis3.tooltip.label.horizontalCenter = "right";
  categoryAxis3.tooltip.label.verticalCenter = "middle";

  let valueAxis3 = chart.yAxes.push(new am4charts.ValueAxis());
  valueAxis3.title.text = "Number Of Application";
  valueAxis3.title.fontWeight = "bold";

  // Create series
  let  series3 = chart.series.push(new am4charts.ColumnSeries3D());
  series3.dataFields.valueY = "NumberInUnit";
  series3.dataFields.categoryX = "officer";
  series3.name = "Application";
  series3.tooltipText = "{categoryX}: [bold]{valueY}[/]";
  series3.columns.template.fillOpacity = .8;
  //series2.columns.template.fill = am4core.color("#006837"); //

  let  columnTemplate2 = series3.columns.template;
  columnTemplate2.strokeWidth = 2;
  columnTemplate2.strokeOpacity = 1;
  columnTemplate2.stroke = am4core.color("#FFFFFF");




  chart.cursor = new am4charts.XYCursor();
  chart.cursor.lineX.strokeOpacity = 0;
  chart.cursor.lineY.strokeOpacity = 0;

  chart.colors.list = [
   am4core.color("#006837"),
  am4core.color("#D65DB1"),
   am4core.color("#FF6F91"),
   am4core.color("#FF9671"),
   am4core.color("#FFC75F"),
   am4core.color("#F9F871")
  ];


  columnTemplate2.adapter.add("fill", function(fill, target) {
   return chart.colors.getIndex(target.dataItem.index);
  })

  columnTemplate2.adapter.add("stroke", function(stroke, target) {
   return chart.colors.getIndex(target.dataItem.index);
  })





  })


  })
         .catch((response: any) => {


  })



  }


  trademarkOpposition() {
     this.ShowLabel3 = false;
     this.ShowLabel4 = false;
     this.Label1 = "New Application";
     this.Label2 = "Judgement";
     this.Label3 = "";
     this.Label4 = "";



       this.busy = this.registerapi
     .GetAllSearchDashBoardCount()
     .then((response: any) => {

     console.log("search template response")

     console.log(response)

     let data =  {
     officer:"New Application" ,

     NumberInUnit:response.result.trademarkOppositionFreshCount

     }
     this.newapplication =response.result.trademarkOppositionFreshCount
     this.chartdata3.push(data)


     data =  {
       officer:"Judgement" ,

       NumberInUnit:response.result.trademarkOppositionJudgementCount

       }
       this.treatedapplication =response.result.trademarkOppositionJudgementCount
       this.chartdata3.push(data)









     this.zone.runOutsideAngular(() => {








     })
     // this.ReceiveappealCount = response

     //next chart








      this.zone.runOutsideAngular(() => {







     })


     })
            .catch((response: any) => {


     })



     }
  ngOnDestroy() {
    this.zone.runOutsideAngular(() => {
     // if (this.chart) {
     //   this.chart.dispose();
    //  }

     // if (this.chart4) {
      //  this.chart4.dispose();
      //}
    });
  }


  ngOnInit() {




  }

}
