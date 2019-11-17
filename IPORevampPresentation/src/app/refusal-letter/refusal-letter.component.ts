import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
declare var $ :any;
import Swal from 'sweetalert2' ;

import * as jspdf from 'jspdf';

import html2canvas from 'html2canvas';

@Component({
  selector: 'app-refusal-letter',
  templateUrl: './refusal-letter.component.html',
  styleUrls: ['./refusal-letter.component.css']
})
export class RefusalLetterComponent implements OnInit {

  row2:any;
  row3:any;
  elementType = 'url';
  value = 'Techiediaries';
  vshow :boolean = false;
  busy: Promise<any>;
  appuser:any
  vimage
  constructor(private registerapi :ApiClientService) { }


  senddata (dd:FormData) {

    $(document).scrollTop(0);
     this.registerapi.SendAttachmentRefusal(dd)
  .then((response: any) => {




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

  onSubmit8(){


    // var doc = new jspdf('p', 'mm', "a4");
     var data2 = new FormData();

    // alert( pfile +  " 1")
    var userid =localStorage.getItem('UserId');

    data2.append('userid' , this.appuser);
    var d = new Date()
    data2.append('message' ,"A copy of Refusal Letter  Generated on " + d);
     var AgentsData = {

       email: this.appuser


   };

   //console.log(AgentsData)

   data2.append("RegisterBindingModel", JSON.stringify(AgentsData));
   var self = this;



     html2canvas(document.getElementById('report')).then(function(canvas) {
      // alert(self)

      const context = canvas.getContext('2d');
      context.scale(2, 2);
      context['dpi'] = 144;
      context['imageSmoothingEnabled'] = false;
      context['mozImageSmoothingEnabled'] = false;
      context['oImageSmoothingEnabled'] = false;
      context['webkitImageSmoothingEnabled'] = false;
      context['msImageSmoothingEnabled'] = false;
      var doc = new jspdf('p', 'pt', [canvas.width, canvas.height]);
      // var img = canvas.toDataURL("image/png");
      var img = canvas.toDataURL("image/png", 1.0);

    //  var doc = new jsPDF();


    //  var doc = new jsPDF('p', 'mm', "a4");

    //doc.setFont("courier");

    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;



    //doc.addImage(img, 'JPEG', 0, 0, width, height);
    doc.addImage(img, 'JPEG', 0, 0, width,  canvas.height);




     var pdf = doc.output('blob');
     console.log(pdf)


   data2.append('FileUpload' , pdf);

   //formData.append("FileUpload", fileToUpload);

   self.senddata(data2)



   });
   }
  ngOnInit() {

    var pwallet =   localStorage.getItem('Pwallet');
    var userid = localStorage.getItem('UserId');

    this.registerapi
    .GetAknwoledgment(pwallet)
    .then((response: any) => {


      this.row2 = response.content;

      this.appuser =response.content.applicationUser.id

      this.vshow = true;
      this.vimage  ="http://5.77.54.44/EinaoCldRevamp2/Upload/" +this.row2.markinfo.logoPicture

      console.log("ack")

      console.log(response)







  this.registerapi
  .GetRefuseApplicationById(userid,pwallet)
  .then((response: any) => {


    this.row3 = response.content;



    console.log("refusal application")

    console.log(response)

    var self = this;

    $( document ).ready(function() {

      self.onSubmit8();

  });

  })
           .catch((response: any) => {

             console.log(response)


     alert("error")
})
  })
  .catch((response: any) => {

    console.log(response)


alert("error")
})



}
}
