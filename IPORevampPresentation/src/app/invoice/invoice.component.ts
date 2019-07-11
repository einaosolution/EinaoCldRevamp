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
  value = 'Federal Ministry Of Trade Nigeria';
  vshow:boolean =false;
  constructor(private registerapi :ApiClientService) { }



  senddata (dd:FormData) {

    $(document).scrollTop(0);
this.registerapi.SendAttachment(dd)
  .then((response: any) => {

    alert("success")


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


     var doc = new jspdf('p', 'mm', "a4");
     var data2 = new FormData();

    // alert( pfile +  " 1")
    var userid =localStorage.getItem('UserId');

    data2.append('userid' , userid);
    var d = new Date()
    data2.append('message' ,"A copy of Invoice Generated on " + d);
     var AgentsData = {

       email: userid


   };

   //console.log(AgentsData)

   data2.append("RegisterBindingModel", JSON.stringify(AgentsData));
   var self = this;



     html2canvas(document.getElementById('report')).then(function(canvas) {
      // alert(self)

       var img = canvas.toDataURL("image/png");

    //  var doc = new jsPDF();


    //  var doc = new jsPDF('p', 'mm', "a4");

    doc.setFont("courier");

    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;


    // doc.addImage(img,'PNG',15,10);
    doc.addImage(img, 'JPEG', 0, 0, width, height);



     var pdf = doc.output('blob');
     console.log(pdf)


   data2.append('FileUpload' , pdf);

   //formData.append("FileUpload", fileToUpload);

   self.senddata(data2)



   });
   }

  ngOnInit() {
    var vpayment= localStorage.getItem('Payment')

    this.row= JSON.parse(vpayment);
    this.vdate =new Date();

    var self = this;

    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false,
    })

        swalWithBootstrapButtons.fire({
          title: 'A Copy Will be sent to your email ',
          text: "",
          type: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Yes, Proceed!',
          cancelButtonText: 'No, cancel!',
          reverseButtons: true
        }).then((result) => {
          if (result.value) {

            self.onSubmit8();
      } else if (
            // Read more about handling dismissals
            result.dismiss === Swal.DismissReason.cancel
          ) {

          }
        })




  }

}
