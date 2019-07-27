import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2' ;
import {ApiClientService} from '../api-client.service';
declare var $ :any;


import * as jspdf from 'jspdf';

import html2canvas from 'html2canvas';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent implements OnInit {
  row:any;
  vdate:any
  row2:any;
  elementType = 'url';
  value = '';
  vshow:boolean =false;
  busy: Promise<any>;
  description :any;
  amount :any;
  constructor(private registerapi :ApiClientService) { }



  senddata (dd:FormData) {

    $(document).scrollTop(0);
     this.registerapi.SendAttachment(dd)
  .then((response: any) => {

   // alert("Email Sent")



  //   this.msgs = [];
  //  this.msgs2 = [];
  //    this.msgs2.push({severity:'success', summary:'Success', detail:'Registered  Successfully,Confirmation email sent to you email '});

     // this.router.navigateByUrl('/');
  })
           .catch((response: any) => {
  console.log(response)
  var  ppr = response.json() ;

  $(document).scrollTop(0);
  //  alert("error")
   }
   );
  }

  print(): void {
    let printContents, popupWin;
    printContents = document.getElementById('report').innerHTML;
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
  onSubmit8(){


    // var doc = new jspdf('p', 'mm', "a4");
     var data2 = new FormData();

    // alert( pfile +  " 1")
    var userid =localStorage.getItem('UserId');

    data2.append('userid' , userid);
    data2.append('transactionnumber' , this.row.transactionid);
    data2.append('amount' , this.row.amount);
    data2.append('transactiondescription' , this.row.description);
    var d = new Date()
    data2.append('message' ,"A copy of Receipt Generated on " + d);
     var AgentsData = {

       email: userid


   };

   //console.log(AgentsData)

   data2.append("RegisterBindingModel", JSON.stringify(AgentsData));
   var self = this;



     html2canvas(document.getElementById('report')).then(function(canvas) {

      const context = canvas.getContext('2d');
      context.scale(2, 2);
      context['dpi'] = 144;
      context['imageSmoothingEnabled'] = false;
      context['mozImageSmoothingEnabled'] = false;
      context['oImageSmoothingEnabled'] = false;
      context['webkitImageSmoothingEnabled'] = false;
      context['msImageSmoothingEnabled'] = false;

      // alert(self)
      var doc = new jspdf('p', 'pt', [canvas.width, canvas.height]);
     //  var img = canvas.toDataURL("image/png");
       var img = canvas.toDataURL("image/png", 1.0);

    //  var doc = new jsPDF();


    //  var doc = new jsPDF('p', 'mm', "a4");

   // doc.setFont("courier");

    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;



  //  doc.addImage(img, 'JPEG', 0, 0, width, height);
    doc.addImage(img, 'JPEG', 0, 0, width,  canvas.height);

  //  doc.addImage(img,0,0,canvas.width, canvas.height);



    var pdf = doc.output('blob');
     console.log(pdf)


   data2.append('FileUpload' , pdf);



   self.senddata(data2)
  //doc.save('converteddoc.pdf');


   });
   }

  ngOnInit() {
    var vpayment= localStorage.getItem('Payment')
    var self = this;

    this.row= JSON.parse(vpayment);
    this.vdate =new Date();
    this.value = this.row.paymentref;


    $( document ).ready(function() {

      self.onSubmit8()

  });





  }

}
