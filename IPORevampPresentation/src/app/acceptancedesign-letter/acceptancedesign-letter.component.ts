import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
declare var $ :any;


import * as jspdf from 'jspdf';

import html2canvas from 'html2canvas';

import Swal from 'sweetalert2' ;

@Component({
  selector: 'app-acceptancedesign-letter',
  templateUrl: './acceptancedesign-letter.component.html',
  styleUrls: ['./acceptancedesign-letter.component.css']
})
export class AcceptancedesignLetterComponent implements OnInit {

  row2:any;
  row3:any;
  row4:any;

  elementType = 'url';
  value = 'Federal Ministry Of Trade Nigeria';
  vshow :boolean = false;
  show :boolean = false;
  showpriority:boolean = false;
  busy: Promise<any>;
  public filepath
  appuser
  vimage
  constructor(private registerapi :ApiClientService) { }


  senddata (dd:FormData) {

    $(document).scrollTop(0);
   this.registerapi.SendAttachmentPatentAcceptance(dd)
  .then((response: any) => {


  //  alert("success")


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
    data2.append('message' ,"A copy of Acceptance Letter Generated on " + d);
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



   // doc.addImage(img, 'JPEG', 0, 0, width, height);
   doc.addImage(img, 'JPEG', 0, 0, width,  canvas.height);



     var pdf = doc.output('blob');
     console.log(pdf)


   data2.append('FileUpload' , pdf);



   self.senddata(data2)

  // doc.save('acceptance.pdf');



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
  ngOnInit() {

    var pwallet =   localStorage.getItem('Pwallet');
   // alert(pwallet)
    this.filepath = this.registerapi.GetFilepath2();
    var userid = localStorage.getItem('UserId');



    this.registerapi
    .GetDesignApplicationById(pwallet)
    .then((response: any) => {



      this.row2 = response.content;

      if (this.row2.designCoApplicant) {
  this.show = true
      }

      else {
        this.show = false
      }

       if (this.row2.designPriority.length > 0 ) {
  this.showpriority = true
      }

      else {
        this.showpriority = false
      }

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

     $( document ).ready(function() {

       self.onSubmit8();

   });





   })
            .catch((response: any) => {

              console.log(response)

})


this.registerapi
.GetApplicationTransactionAmount(userid,transid)
.then((response: any) => {

console.log("payment response")

this.row4 = response.content

console.log(response.content)

    })
         .catch((response: any) => {

           console.log(response)

})

this.registerapi
    .SendDesignRegistraEmail(userid,pwallet)
    .then((response: any) => {

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
