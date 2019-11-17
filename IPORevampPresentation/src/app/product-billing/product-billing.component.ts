import { Component, OnInit,ViewChild,OnDestroy } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';
import { Subject } from 'rxjs';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';



import { map } from 'rxjs/operators';

import 'datatables.net'
import 'datatables.net-dt'


declare var $;
declare var RmPaymentEngine:any;

@Component({
  selector: 'app-product-billing',
  templateUrl: './product-billing.component.html',
  styleUrls: ['./product-billing.component.css']
})
export class ProductBillingComponent implements OnInit {
  @ViewChild('dataTable') table;
  dtOptions:any = {};
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  busy: Promise<any>;
  Code: FormControl;
  id:string;
  Description: FormControl;
  public rows :any;
  public tot= 0;
  public transactionid;
  public paymentreference;
  public transactionid2;
  public checkboxFlag =false
  row:any[] =[]
row2:any
vshow:boolean =false
vfilepath:string =""
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }

  onSubmit() {
    this. makePayment()
  }

  FieldsChange(values:any){

    if (values.currentTarget.checked) {
     this.checkboxFlag =true

     alert("Click Pay with remitta button to proceed")
    }

    else {
     this.checkboxFlag =false
     alert("Ok")
    }

     }
  generateInvoice() {

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
              this.spinner.hide();
               console.log(response)


              Swal.fire(
                response.error.message,
                '',
                'error'
              )

})




  }

  makePayment() {
    // var form = document.querySelector("#payment-form");
    var self = this;
    var date = new Date();
    var timestamp = date.getTime();
    this.transactionid = timestamp
     var paymentEngine = RmPaymentEngine.init({
         key: 'MXw0MDgxNjQ5OXwyYmJkN2VjMzcxZGQ3YzA3OTE5NGI4ODM5M2ZlZTY0MDA0NThhOTM3YTYzZGVmYjhmMmJlNzBjNjQ4OThiMDQ3ZTc3Zjk5MDkxMzRhODMxYzUxMzM1ZmRjZmM3ZWVmYmY1ZTNiYzlkNWZlYTJjZDY4ZDVmMWI1MzUzOTBiYzIzNw==',
         customerId: "folivi@systemspecs.com.ng",
         amount: self.tot,

         lastName: "Folivi",
         firstName: "Joshua",
         email: "folivi@systemspecs.com.ng",
         transactionId: timestamp,

         narration: "Test Payment" ,
         lineItems: [
           {
              lineItemsId: "itemid1",
               beneficiaryName: "Alozie Michael",
               beneficiaryAccount: "0360883515",
               bankCode: "020",
               beneficiaryAmount: "7000",
               deductFeeFrom: "1"
           },
           {
               lineItemsId: "itemid2",
               beneficiaryName: "Folivi Joshua",
               beneficiaryAccount: "4017904612",
               bankCode: "022",
               beneficiaryAmount: "3000",
               deductFeeFrom: "0"
           }
       ] ,

         onSuccess: function (response) {

          //  self.success()

             console.log('callback Successful Response', response);

             self.paymentreference =response.paymentReference
             self.transactionid2 =response.transactionId ;

             var Payment= {

              description: self.rows.description,
              quatity: "1",
              amount: self.tot ,
              paymentref:response.paymentReference ,
              transactionid:self.transactionid



          };

          localStorage.setItem('Payment',JSON.stringify( Payment));

          self.generateInvoice()



         },
         onError: function (response) {
             alert("Error")
           // self.failure()
             console.log('callback Error Response', response);
         },
         onClose: function () {
             console.log("closed");
         }
     });

     try {

      paymentEngine.showPaymentWidget();


     }

     catch(err) {
       alert(err.message)

     }
 }

 islogged() {
  var vpassord = localStorage.getItem('ChangePassword');
   if (this.registerapi.gettoken() && vpassord =="True" ) {

     return true ;
   }

   else {

     return false;
   }
 }
  ngOnInit() {

    if (this.registerapi.checkAccess("#/Dashboard/PremSearch"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }



    this.registerapi.setPage("PrelimSearch")

    this.registerapi.VChangeEvent("PrelimSearch");
    this.vfilepath =this.registerapi.GetFilepath();

    const firstParam: string = this.route.snapshot.queryParamMap.get('RRR');
    const secondparam: string = this.route.snapshot.queryParamMap.get('orderID');
    var userid = localStorage.getItem('UserId');

    if (firstParam) {

      var kk2 = {
        RRR:firstParam



      }

      this.registerapi
      . RemitaTransactionRequeryPayment(kk2)
      .then((response: any) => {

        console.log("RemittaResponse")
      //  this.rows = response.content;
        console.log(response)

      //  this.row2 =response.content

        var result =response.content

        if (result.message ="Approved")

        {


          var Payment= {

            description: this.rows.description,
            quatity: "1",
            amount: this.tot ,
            paymentref:firstParam ,
            transactionid:secondparam



        };

        localStorage.setItem('Payment',JSON.stringify( Payment));

        this.generateInvoice()

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

   })

    }

    else {


      this.row.push(1)


    var kk = {
      FeeIds:this.row ,
      UserId:userid ,



    }

     this.registerapi
   . InitiateRemitaPayment(kk)
   .then((response: any) => {

     console.log("RemittaResponse")
   //  this.rows = response.content;
     console.log(response)

     this.row2 =response.content

     this.vshow = true;





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



    this.busy =   this.registerapi
    .GetFeeListByName("PRELIMINARY SEARCH REPORT" ,userid)
    .then((response: any) => {

      console.log("fee  Response")
      this.rows = response.content;
      this.tot = parseInt(this.rows.init_amt ) +  parseInt(this.rows.technologyFee )
      console.log(response)



    })
             .catch((response: any) => {
              this.spinner.hide();
               console.log(response)


              Swal.fire(
                response.error.message,
                '',
                'error'
              )

})
  }



}
