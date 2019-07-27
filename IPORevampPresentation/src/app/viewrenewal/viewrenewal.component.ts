import { Component, OnInit , Input  } from '@angular/core';
import {ApiClientService} from '../api-client.service';

@Component({
  selector: 'app-viewrenewal',
  templateUrl: './viewrenewal.component.html',
  styleUrls: ['./viewrenewal.component.css']
})
export class ViewrenewalComponent implements OnInit {
  busy: Promise<any>;
  row2:any;
  @Input()
public   pwallet: any ="";
  constructor(private registerapi :ApiClientService) { }

  ngOnInit() {
    var userid = localStorage.getItem('UserId');

    this.busy =   this.registerapi
   // .GetApplicationByDocumentId("7",userid)
    .GetApplicationByDocumentId(this.pwallet,userid)
    .then((response: any) => {
console.log("view response")

console.log(response)
this.row2 = response.content


    })
             .catch((response: any) => {

               console.log(response)


             })

  }

}
