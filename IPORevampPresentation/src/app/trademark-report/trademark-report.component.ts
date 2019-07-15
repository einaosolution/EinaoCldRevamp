import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';

@Component({
  selector: 'app-trademark-report',
  templateUrl: './trademark-report.component.html',
  styleUrls: ['./trademark-report.component.css']
})
export class TrademarkReportComponent implements OnInit {

  constructor(private registerapi :ApiClientService ) { }

  ngOnInit() {

    this.registerapi
.GetTrademarkdata()
.then((response: any) => {

  alert("success")
  console.log("response.content")

  console.log(response)



})
         .catch((response: any) => {
         alert("error")


})
  }

}
