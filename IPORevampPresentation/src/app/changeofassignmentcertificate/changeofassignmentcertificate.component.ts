import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
declare var $;

@Component({
  selector: 'app-changeofassignmentcertificate',
  templateUrl: './changeofassignmentcertificate.component.html',
  styleUrls: ['./changeofassignmentcertificate.component.css']
})
export class ChangeofassignmentcertificateComponent implements OnInit {

  busy: Promise<any>;
  row5:any
  row6:any
  row7:any
  filepath:any ;
  logopath:any
  constructor(private registerapi :ApiClientService ) { }


  print2() {

    window.print();
  }
  ngOnInit() {
    this.filepath = this.registerapi.GetFilepath2();

    let  userid2 = localStorage.getItem('UserId');

  let result =  localStorage.getItem('Pwallet2' )

  var ppp =localStorage.getItem('addressesrecord');


     this.row7 = JSON.parse(ppp) ;
     if (this.row7.logo_pic) {
     this.logopath =  this.filepath + "Upload/" +  this.row7.logo_pic

     }

     console.log("logo file path")
     console.log(this.logopath)

     console.log("record information")
     console.log( this.row7)

    this.busy =   this.registerapi
// .GetApplicationByDocumentId("7",userid)
 .GetMergerById(result,userid2)
 .then((response: any) => {
console.log("view  merger response")



this.row5 =  response.content
console.log(this.row5)



this.busy =   this.registerapi
// .GetApplicationByDocumentId("7",userid)
 .GetUserById(this.row5.userid)
 .then((response: any) => {
console.log("user  response")



this.row6 =  response
console.log(this.row6)


 })
          .catch((response: any) => {

            console.log(response)


          })


 })
          .catch((response: any) => {

            console.log(response)


          })




  }

}
