import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import Swal from 'sweetalert2' ;
declare var $ :any;


import * as jspdf from 'jspdf';

import html2canvas from 'html2canvas';

@Component({
  selector: 'app-acknowledgement',
  templateUrl: './acknowledgement.component.html',
  styleUrls: ['./acknowledgement.component.css']
})
export class AcknowledgementComponent implements OnInit {
  row2:any;
  row:any;
  rrr:any
  public filepath
  vdate:any;
  elementType = 'url';
  value = 'Federal Ministry Of Trade Nigeria';
  vshow:boolean =false;
  constructor(private registerapi :ApiClientService ) { }


  senddata (dd:FormData) {

    $(document).scrollTop(0);
this.registerapi.SendAttachment(dd)
  .then((response: any) => {

    alert("success")

  })
           .catch((response: any) => {
  console.log(response)
  var  ppr = response.json() ;

  $(document).scrollTop(0);
  //  alert("error")
   }
   );
  }


  onSubmit9(){

    html2canvas(document.getElementById('report2')).then(function(canvas) {
      // alert(self)


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



   // var pdf = doc.output('blob');

   doc.save('Output.pdf');

    // var pdf = doc.output('blob');
     //console.log(pdf)




   //formData.append("FileUpload", fileToUpload);

   //self.senddata(data2)



   });

  }
  onSubmit8(){


   //  var doc = new jspdf('p', 'mm', "a4");
     var data2 = new FormData();

    // alert( pfile +  " 1")
    var userid =localStorage.getItem('UserId');

    data2.append('userid' , userid);
    var d = new Date()
    data2.append('message' ,"A copy of Acknowledgement Generated on " + d);
     var AgentsData = {

       email: userid


   };


   data2.append("RegisterBindingModel", JSON.stringify(AgentsData));
   var self = this;



     html2canvas(document.getElementById('report')).then(function(canvas) {
      // alert(self)
      var doc = new jspdf('p', 'pt', [canvas.width, canvas.height]);
     //  var img = canvas.toDataURL("image/png");
     var img = canvas.toDataURL("image/png", 1.0);



    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;





    doc.addImage(img, 'JPEG', 0, 0, width,  canvas.height);



     var pdf = doc.output('blob');
     console.log(pdf)


   data2.append('FileUpload' , pdf);

   //formData.append("FileUpload", fileToUpload);

   self.senddata(data2)



   });
   }
  generatePdf() {
    var doc = new jspdf('p', 'mm', "a4");
    html2canvas(document.getElementById('report')).then(function(canvas) {

      var img = canvas.toDataURL("image/png");


   doc.setFont("courier");



    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;
    doc.addImage(img, 'JPEG', 0, 0, width, height);

    doc.save('Output.pdf');

  });
  }

  onSubmit4 () {
    this. generatePdf()
  }

  print2() {

    window.print();
  }
  ngOnInit() {
let pwalletid = localStorage.getItem('pwalletid');
  //  this.onSubmit9() ;
  this.filepath = this.registerapi.GetFilepath2();

  this.registerapi.GetAknwoledgment(pwalletid)
  .then((response: any) => {

  console.log("trademark data")
  this.row2 = response.content
  this.vshow = true
  console.log(this.row2)

    //  alert("success")

  })
           .catch((response: any) => {
  console.log(response)
  var  ppr = response.json() ;

  $(document).scrollTop(0);
  //  alert("error")
   }
   );




  }

}


