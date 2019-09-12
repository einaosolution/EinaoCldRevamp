import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';

@Component({
  selector: 'app-design-certificate',
  templateUrl: './design-certificate.component.html',
  styleUrls: ['./design-certificate.component.css']
})
export class DesignCertificateComponent implements OnInit {
  row2:any;
  row3:any;
  row4:any;
  row5:any;

  elementType = 'url';
  value = 'Federal Ministry Of Trade Nigeria';
  vshow :boolean = false;
  busy: Promise<any>;
  public filepath
  appuser
  vimage
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


    var pwallet =   localStorage.getItem('Pwallet');
   // alert(pwallet)

    var userid = localStorage.getItem('UserId');



    this.registerapi
    .GetDesignApplicationById(pwallet)
    .then((response: any) => {



      this.row2 = response.content;

      this.appuser =response.content.userid

      let  transid =response.content.transactionID

      console.log("design Application ")

      console.log(response.content)







   this.busy =   this.registerapi
   .GetUserById(this.appuser)
   .then((response: any) => {

     console.log("Response Result")
     this.row3= response;
     console.log(this.row3)


     var self = this;

   })
            .catch((response: any) => {

              console.log(response)

})



this.busy =   this.registerapi
.GetDesignDateFormat(pwallet)
.then((response: any) => {

  console.log("Response Result")
  this.row5= response.content;
  console.log("format date")

  console.log(this.row5)


  var self = this;

})
         .catch((response: any) => {

           console.log(response)

})









      this.vshow = true;
     // this.vimage  ="{{filepath}}Upload/" +this.row2.markinfo.logoPicture

     // alert("Generating")

     // this. generatePdf()



      console.log("ack")

      console.log(response)




  })
  .catch((response: any) => {

    console.log(response)


alert("error")
})
  }

}
