import { Component, OnInit , Input  } from '@angular/core';
import {ApiClientService} from '../api-client.service';

@Component({
  selector: 'app-view-change-of-name',
  templateUrl: './view-change-of-name.component.html',
  styleUrls: ['./view-change-of-name.component.css']
})
export class ViewChangeOfNameComponent implements OnInit {

  busy: Promise<any>;
  row2:any;
  filepath:any ;
  @Input()
public   pwallet: any ="";
  constructor(private registerapi :ApiClientService) { }

  ngOnInit() {
    this.filepath = this.registerapi.GetFilepath2();
    var userid = localStorage.getItem('UserId');

    this.busy =   this.registerapi
   // .GetApplicationByDocumentId("7",userid)
    .GetChangeOfNameApplicationById(this.pwallet,userid)
    .then((response: any) => {
console.log("view  change of name response")

console.log(response)

this.row2 = response.content


    })
             .catch((response: any) => {

               console.log(response)


             })

  }

}
