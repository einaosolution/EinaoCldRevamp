import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import Swal from 'sweetalert2' ;

@Component({
  selector: 'app-patent-certificate',
  templateUrl: './patent-certificate.component.html',
  styleUrls: ['./patent-certificate.component.css']
})
export class PatentCertificateComponent implements OnInit {
  public row = [];
  TodayDate:any;
  public row2
  public row3 :any;
  public row4  = [];
  public row5  = [];
  public row6  = [];
  public show = false;
  busy: Promise<any>;
  constructor(private registerapi :ApiClientService) { }


  print(): void {
    let printContents, popupWin;
    printContents = document.getElementById('searchform').innerHTML;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <title>Print tab</title>
          <style>
          //........Customized style.......
          </style>
        </head>
    <body onload="window.print();window.close()">${printContents}</body>
      </html>`
    );
    popupWin.document.close();
}

  ngOnInit() {
    let  applicationid  = localStorage.getItem('Applicationid');
    let  userid = localStorage.getItem('UserId');

    this.TodayDate = new Date();


    this.busy =   this.registerapi
    .GetPatentApplicationById(applicationid)
    .then((response: any) => {

     console.log("Latest Pwallet ")
     this.row = response.content.patentInformation;

     console.log(response.content)

     this.busy =   this.registerapi
     .GetPatentUserByApplication(userid,applicationid)
     .then((response: any) => {

      console.log("Latest user ")
      this.row3= response.content;

      this.show  = true;

     console.log(response)



     })
             .catch((response: any) => {

               console.log(response)

              Swal.fire(
                response.error.message,
                '',
                'error'
              )

     })






    })
            .catch((response: any) => {

              console.log(response)


             Swal.fire(
               response.error.message,
               '',
               'error'
             )

    })







  }

}
