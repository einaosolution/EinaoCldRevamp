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
  selector: 'app-defaultdashboardsearch',
  templateUrl: './defaultdashboardsearch.component.html',
  styleUrls: ['./defaultdashboardsearch.component.css']
})
export class DefaultdashboardsearchComponent implements OnInit {

  private chart: am4charts.XYChart;
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













}

  ngOnDestroy() {
    this.zone.runOutsideAngular(() => {
      if (this.chart) {
        this.chart.dispose();
      }
    });
  }


  ngOnInit() {




  }


}
