import { Component, OnInit ,AfterViewInit } from '@angular/core';
import Swal from 'sweetalert2' ;
import {ApiClientService} from '../api-client.service';
import {ActivatedRoute} from "@angular/router";
import {Router} from '@angular/router';

declare var $ :any;


import * as jspdf from 'jspdf';

import html2canvas from 'html2canvas';

@Component({
  selector: 'app-invoice2',
  templateUrl: './invoice2.component.html',
  styleUrls: ['./invoice2.component.css']
})
export class Invoice2Component implements OnInit ,AfterViewInit {

  row:any;
  vdate:any
  row2:any;
  row22:any
  elementType = 'url';
  loggeduser :any;
  vfilepath:string =""
  transactionid  :string =""
  email :any;
  value = 'Federal Ministry Of Trade Nigeria';
  description :any;
  amount :any;
  vshow:boolean =false;
  busy: Promise<any>;
  messages = ""

  constructor(private registerapi :ApiClientService ,private route: ActivatedRoute ,private router: Router) {


   }



  senddata (dd:FormData) {

    $(document).scrollTop(0);
    this.registerapi.SendAttachmentReceipt(dd)
  .then((response: any) => {

    //alert("Email Sent")

   // Swal.fire(
     // 'Email Sent Succesfully ',
      //'',
    //  'success'
   // )


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


  generateInvoice7() {
    let  pwallet =  localStorage.getItem('NoticeAppID');
    let  pwallet2 =  localStorage.getItem('Pwallet');


    let  userid = localStorage.getItem('UserId');
       this.registerapi
       .UpdateMergerFormById( pwallet ,userid ,this.transactionid)
       .then((response: any) => {

         console.log("response after payment")
         console.log(response.content)

         this.router.navigateByUrl('/Dashboard/Invoice');





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

  generateInvoice6() {

    var vpayment= localStorage.getItem('Payment')
    var vpayment2= JSON.parse(vpayment);
    var vprelimdata= localStorage.getItem('PrelimData')
    var vprelimdata2= JSON.parse(vprelimdata);

    var userid =parseInt( localStorage.getItem('UserId'));
    var kk = {
      first_name:vprelimdata2.Firstname ,
      last_name:vprelimdata2.Lastname ,
      userid:userid ,
      type:vprelimdata2.industry ,
      description:vprelimdata2.description ,
      payment_reference:vpayment2.transactionid ,
      status:"Paid" ,
      CreatedBy:userid ,
      UserEmail :vprelimdata2.UserEmail


    }
    this.busy = this.registerapi
    .SavePrelimSearch(kk)
    .then((response: any) => {

      this.router.navigateByUrl('/Dashboard/Invoice');



    })
             .catch((response: any) => {
             // this.spinner.hide();
               console.log(response)


              Swal.fire(
                response.error.message,
                '',
                'error'
              )

})




  }

  generateInvoice5() {
    let  pwallet =  localStorage.getItem('NoticeAppID');
    let  pwallet2 =  localStorage.getItem('Pwallet');


    let  userid = localStorage.getItem('UserId');
       this.registerapi
       .UpdateRenewalFormById( pwallet ,userid ,this.transactionid)
       .then((response: any) => {

         console.log("response after payment")
         console.log(response.content)

         this.router.navigateByUrl('/Dashboard/Invoice');





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


  generateInvoice4() {
    let  pwallet =  localStorage.getItem('NoticeAppID');
    let  pwallet2 =  localStorage.getItem('Pwallet');


    let  userid = localStorage.getItem('UserId');
       this.registerapi
       .UpdateOpposeFormById2( pwallet ,userid ,this.transactionid)
       .then((response: any) => {

         console.log("response after payment")
         console.log(response.content)


         var  formData = new FormData();
         var userid = localStorage.getItem('UserId');



         formData.append("pwalletid",pwallet2);
        formData.append("comment",response.content.comment);
        formData.append("description","");
        formData.append("fromstatus","");
        formData.append("tostatus","Fresh");
        formData.append("fromDatastatus","");
        formData.append("toDatastatus","Opposition");
        formData.append("userid",userid);
        formData.append("uploadpath",response.content.upload);


        this.busy =  this.registerapi
        .SaveFreshAppHistory2(formData)
        .then((response: any) => {
         this.router.navigateByUrl('/Dashboard/Invoice');

        })
                 .catch((response: any) => {
                //  this.spinner.hide();
                   console.log(response)


                  Swal.fire(
                    response.error.message,
                    '',
                    'error'
                  )

     })

       //  this.router.navigateByUrl('/Dashboard/Acknowledgement');



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


  generateInvoice3() {
    let  pwallet =  localStorage.getItem('NoticeAppID');
    let  pwallet2 =  localStorage.getItem('Pwallet');


    let  userid = localStorage.getItem('UserId');
       this.registerapi
       .UpdateCounterOpposeFormById( pwallet ,userid ,this.transactionid)
       .then((response: any) => {

         console.log("response after payment")
         console.log(response.content)


         var  formData = new FormData();
         var userid = localStorage.getItem('UserId');



         formData.append("pwalletid",pwallet2);
        formData.append("comment",response.content.comment);
        formData.append("description","");
        formData.append("fromstatus","");
        formData.append("tostatus","Counter");
        formData.append("fromDatastatus","");
        formData.append("toDatastatus","Opposition");
        formData.append("userid",userid);
        formData.append("uploadpath",response.content.upload);


        this.busy =  this.registerapi
        .SaveFreshAppHistory2(formData)
        .then((response: any) => {
         this.router.navigateByUrl('/Dashboard/Invoice');

        })
                 .catch((response: any) => {
                //  this.spinner.hide();
                   console.log(response)


                  Swal.fire(
                    response.error.message,
                    '',
                    'error'
                  )

     })

       //  this.router.navigateByUrl('/Dashboard/Acknowledgement');



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

  generateInvoice2() {
    let  pwallet =  localStorage.getItem('NoticeAppID');
    let  pwallet2 =  localStorage.getItem('Pwallet');


    let  userid = localStorage.getItem('UserId');
       this.registerapi
       .UpdateCertPaymentById( pwallet ,userid ,this.transactionid)
       .then((response: any) => {

         console.log("response after payment")
         console.log(response.content)


         var  formData = new FormData();
         var userid = localStorage.getItem('UserId');

         var array = pwallet2.split(",");

         for (let i = 0; i < array.length; i++) {


           array[i]
        // formData.append("pwalletid",pwallet2);
         formData.append("pwalletid",array[i]);
        formData.append("comment","Certificate Payment Sucessful");
        formData.append("description","");
        formData.append("fromstatus","");
        formData.append("tostatus","Paid");
        formData.append("fromDatastatus","");
        formData.append("toDatastatus","Certificate");
        formData.append("userid",userid);
        formData.append("uploadpath","");


        this.busy =  this.registerapi
        .SaveFreshAppHistory2(formData)
        .then((response: any) => {
         this.router.navigateByUrl('/Dashboard/Invoice');

        })
                 .catch((response: any) => {
                //  this.spinner.hide();
                   console.log(response)


                  Swal.fire(
                    response.error.message,
                    '',
                    'error'
                  )

     })

   }

       //  this.router.navigateByUrl('/Dashboard/Acknowledgement');



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


  generateInvoice() {
    let  pwallet =  localStorage.getItem('Pwallet');
       this.registerapi
       .UpDatePwalletById( pwallet ,this.transactionid)
       .then((response: any) => {

         this.router.navigateByUrl('/Dashboard/Invoice');



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
    data2.append('transactionnumber' , this.transactionid);
    data2.append('amount' , this.amount);
    data2.append('transactiondescription' , this.description);
    var d = new Date()
    data2.append('message' ,"A copy of Invoice Generated on " + d);
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

    //doc.setFont("courier");

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

   ngAfterViewInit() {

   // alert("called")
   // this.onSubmit8();


   }

   setpaytype() {

    var vdesc =  localStorage.getItem('description');
    var Payment= {

               description: vdesc ,
               quatity: "1",
               amount: this.row.amount ,
               paymentref:this.row.paymentref,
               transactionid:"1111166777"



           };

   this.transactionid ="1111166777" ;

           localStorage.setItem('Payment',JSON.stringify( Payment));

           var paytype = localStorage.getItem('PaymentType');

           if (paytype =="FileT002") {
             this.generateInvoice()
           }

           if (paytype =="CertPayment") {
             this.generateInvoice2()
           }


           if (paytype =="CounterStatement") {
             this.generateInvoice3()
           }

           if (paytype =="opposeApplication") {
             this.generateInvoice4()
           }


           if (paytype =="PayRenewal") {
             this.generateInvoice5()
           }


           if (paytype =="PrelimSearch") {
             this.generateInvoice6()
           }


           if (paytype =="MergeApplication") {
             this.generateInvoice7()
           }









   }

  ngOnInit() {
    var vpayment= localStorage.getItem('Payment')

   this.email = localStorage.getItem('username');
   this.loggeduser = localStorage.getItem('loggeduser');

   var vpayment2= localStorage.getItem('row22')

   this.vfilepath =this.registerapi.GetFilepath()





    this.row= JSON.parse(vpayment);

    this.amount =this.row.amount;

   this.row22 = JSON.parse(vpayment2);

   this.vshow = true;

   var settings = localStorage.getItem('settings');

    this.value = this.row.paymentref
    this.vdate =new Date();

    var self = this;



    if (settings =="0") {

      this. setpaytype()

    }

    else {




    const firstParam: string = this.route.snapshot.queryParamMap.get('RRR');
    const secondparam: string = this.route.snapshot.queryParamMap.get('orderID');
    var userid = localStorage.getItem('UserId');
  var vdesc =  localStorage.getItem('description');
  this.description = localStorage.getItem('description');
    this.transactionid = secondparam ;

    if (firstParam) {

      var kk2 = {
        RRR:firstParam



      }

      this.busy =  this.registerapi
      .RemitaTransactionRequeryPayment(kk2)
      .then((response: any) => {

        console.log("RemittaResponse")
      //  this.rows = response.content;
        console.log(response)

      //  this.row2 =response.content

        var result =response.content

        if (result.message ="Approved")

        {


          var Payment= {

            description: vdesc ,
            quatity: "1",
            amount: this.row.amount ,
            paymentref:firstParam ,
            transactionid:secondparam



        };

        localStorage.setItem('Payment',JSON.stringify( Payment));

        var paytype = localStorage.getItem('PaymentType');

        if (paytype =="FileT002") {
          this.generateInvoice()
        }

        if (paytype =="CertPayment") {
          this.generateInvoice2()
        }


        if (paytype =="CounterStatement") {
          this.generateInvoice3()
        }

        if (paytype =="opposeApplication") {
          this.generateInvoice4()
        }


        if (paytype =="PayRenewal") {
          this.generateInvoice5()
        }


        if (paytype =="PrelimSearch") {
          this.generateInvoice6()
        }


        if (paytype =="MergeApplication") {
          this.generateInvoice7()
        }







        }

        else {
          alert("Payment Not Successful")
        }

        this.vshow = true;

    //    alert("success")



      })
               .catch((response: any) => {

                 console.log(response)




                Swal.fire(
                  response.error.message,
                  '',
                  'error'
                )

             //  this. setpaytype()

   })

    }

    else {

      var self = this;
    $( document ).ready(function() {

      self.onSubmit8()

  });
    }

  }


  }

}
