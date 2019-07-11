import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2' ;
import {ApiClientService} from '../api-client.service';
import {ActivatedRoute} from "@angular/router";


declare var RmPaymentEngine:any;




@Component({
  selector: 'app-remitta',
  templateUrl: './remitta.component.html',
  styleUrls: ['./remitta.component.css']
})
export class RemittaComponent implements OnInit {
amount =10000;
row:any[] =[]
row2:any
vshow:boolean =false
  constructor(private registerapi :ApiClientService ,private route: ActivatedRoute) { }

success() {
  Swal.fire(
    'Payment successful',
    '',
    'success'
  )
}

failure() {
  Swal.fire(
    "Payment not successful",
    '',
    'error'
  )
}

 onSubmit() {
    this.makePayment()
  }

  makePayment() {
   // var form = document.querySelector("#payment-form");
   var self = this;
   var date = new Date();
   var timestamp = date.getTime();
    var paymentEngine = RmPaymentEngine.init({
        key: 'MXw0MDgxNjQ5OXwyYmJkN2VjMzcxZGQ3YzA3OTE5NGI4ODM5M2ZlZTY0MDA0NThhOTM3YTYzZGVmYjhmMmJlNzBjNjQ4OThiMDQ3ZTc3Zjk5MDkxMzRhODMxYzUxMzM1ZmRjZmM3ZWVmYmY1ZTNiYzlkNWZlYTJjZDY4ZDVmMWI1MzUzOTBiYzIzNw==',
        customerId: "folivi@systemspecs.com.ng",
        amount: self.amount,

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
            alert("sucess")
         //  self.success()

            console.log('callback Successful Response', response);
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

  ngOnInit() {
    const firstParam: string = this.route.snapshot.queryParamMap.get('RRR');
    const secondparam: string = this.route.snapshot.queryParamMap.get('orderID');


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

        if (result.message ="Approved") {

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

      var userid =parseInt( localStorage.getItem('UserId'));
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



    console.log("firstParam" + firstParam)
    console.log("secondparam" + secondparam)




  }

}
