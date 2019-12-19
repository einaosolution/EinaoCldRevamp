import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
declare var $;

@Component({
  selector: 'app-changeofaddresscertificate',
  templateUrl: './changeofaddresscertificate.component.html',
  styleUrls: ['./changeofaddresscertificate.component.css']
})
export class ChangeofaddresscertificateComponent implements OnInit {

  busy: Promise<any>;
  row5:any
  row6:any
  row7:any
  constructor(private registerapi :ApiClientService ) { }


  print2() {

    window.print();
  }
  ngOnInit() {

    let  userid2 = localStorage.getItem('UserId');

  let result =  localStorage.getItem('Pwallet2' )

  var ppp =localStorage.getItem('addressesrecord');


     this.row7 = JSON.parse(ppp) ;

     console.log("record information")
     console.log( this.row7)

    this.busy =   this.registerapi
// .GetApplicationByDocumentId("7",userid)
 .GetChangeOfAddressApplicationById(result,userid2)
 .then((response: any) => {
console.log("view  change of name response")



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
