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
  selector: 'app-new-application',
  templateUrl: './new-application.component.html',
  styleUrls: ['./new-application.component.css']
})
export class NewApplicationComponent implements OnInit {
  dtOptions:any = {};
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = false;
  updatemode:boolean = false;
  userform: FormGroup;
  userform2: FormGroup;
  submitted:boolean=false;
  submitted2:boolean=false;
  busy: Promise<any>;
  Code: FormControl;
  Firstname: FormControl;
  Firstname2: FormControl;
  Role2: FormControl;
  Lastname: FormControl;
  Lastname2: FormControl;
  MobileNumber: FormControl;
  Gender: FormControl;
  Email: FormControl;
  Email2: FormControl;
  Unit: FormControl;
  Street: FormControl;
  City: FormControl;
  State: FormControl;
  Postal: FormControl;
  Country: FormControl;
  Occupation2: FormControl;
  MobileNumber2: FormControl;
  id:string;
  Description: FormControl;
  Ministry: FormControl;
  StaffId: FormControl;
  Department: FormControl;
  public pwalletid ="" ;
  vshow :boolean = true

  transactionid
  paymentreference
  transactionid2
  public rows = [];
  public row2 ;
  public tot ;
  public row3 = [];

  public row5 = [];
  public row6 = [];
  public row7 = [];
  public row8 = [];
  public row9 = [];
  public row10 = [];
  public row11 = [];
  public row12 :any;
  public fee_description =""
  public trademarktype =""
  public  imageSrc
  public categoryid
  public checkboxFlag =false

  public image1
  public image2
  public image3
  public image4
  public filepath
  public trademarklogo


  row:any[] =[]
  row22:any
  vshow2:boolean =false
  vshow3:boolean =false
  vfilepath:string =""
  @ViewChild("fileInput") fileInput;
  @ViewChild("fileInput1") fileInput1;
  @ViewChild("fileInput2") fileInput2;
  @ViewChild("fileInput22") fileInput22;
  @ViewChild("fileInput3") fileInput3;
  @ViewChild("fileInput33") fileInput33;
  @ViewChild("fileInput4") fileInput4;
  @ViewChild("fileInput44") fileInput44;
  public account = {
    trademarktype : null,
    trademarklogo: null,
    trademarktitle:null ,
    trademarkclass:null ,
    trademarkdesc:null
};

varray4 = [{ YearName: 'Local', YearCode: 'Local' }, { YearName: 'Foreign', YearCode: 'Foreign' } ]
varray5 = [{ YearName: 'DEVICES', YearCode: 'DEVICES' }, { YearName: 'WORD MARK', YearCode: 'WORD MARK' } , { YearName: 'WORD AND DEVICE', YearCode: 'WORD AND DEVICE' } ]
  constructor(private registerapi :ApiClientService ,private router: Router ,private route: ActivatedRoute) { }

  onChange2(deviceValue) {
    if (deviceValue =="2") {
      this.vshow = false;

    }

    else {
      this.vshow = true;
    }
  }

  generateInvoice() {
 let  pwallet =  localStorage.getItem('Pwallet');
    this.registerapi
    .UpDatePwalletById( pwallet ,this.transactionid)
    .then((response: any) => {

      this.router.navigateByUrl('/Dashboard/Acknowledgement');



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

    showval() {

      if (this.categoryid ==="2") {
return true;

      }

      else {
        return false
      }
    }

  onChange($event) {


    let obj2 = this.rows.find(o => o.type === this.account.trademarkclass);

    console.log("obj2")
    console.log(obj2)

    this.account.trademarkdesc = obj2.description





  }

   onSubmit2(f) {
    $(".validation-wizard").steps("next");

   }

   chng()
    {

      let test = this.fileInput.nativeElement;


    let test2 = test.files[0];





    if(test2.size/ 1024 >3000){

      alert("File Too Large")

      test.value = ''

      return ;

    }

    }


    chng2()
    {

      let test = this.fileInput2.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large")

      test.value = ''

      return ;

    }

    }

    chng3()
    {

      let test = this.fileInput3.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large")

      test.value = ''

      return ;

    }

    }

    chng4()
    {

      let test = this.fileInput4.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large")

      test.value = ''

      return ;

    }

    }

    chng22()
    {

      let test = this.fileInput22.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large")

      test.value = ''

      return ;

    }

    }


    chng23()
    {

      let test = this.fileInput1.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large")

      test.value = ''

      return ;

    }

    }

    chng24()
    {

      let test = this.fileInput33.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large")

      test.value = ''

      return ;

    }

    }

    chng25()
    {

      let test = this.fileInput44.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large")

      test.value = ''

      return ;

    }

    }

  onSubmit(f) {



var  formData = new FormData();
var userid = localStorage.getItem('UserId');



if (this.categoryid =="2") {

  let f2 = this.fileInput2.nativeElement;
 // let f22 = this.fileInput22.nativeElement;

  if (f2.files[0]) {


   }

   else {

    Swal.fire(
     "Please Upload Power Of Attorney ",
      '',
      'error'
    )

    return;

   }

  }





  $(".validation-wizard").steps("next");



if (f) {

  let fi = this.fileInput.nativeElement;
 // let f1 = this.fileInput1.nativeElement;
  let f2 = this.fileInput2.nativeElement;
  //let f22 = this.fileInput22.nativeElement;
  let f3 = this.fileInput3.nativeElement;
 // let f33 = this.fileInput33.nativeElement;
  let f4 = this.fileInput4.nativeElement;
 // let f44 = this.fileInput44.nativeElement;
  if (fi.files && fi.files[0]) {
    let fileToUpload = fi.files[0];
   formData.append("FileUpload", fileToUpload);

   }

  // if (f1.files && f1.files[0]) {
  //  let fileToUpload = f1.files[0];
  // formData.append("FileUpload", fileToUpload);

  // }


   if (f2.files && f2.files[0]) {
    let fileToUpload = f2.files[0];
   formData.append("FileUpload2", fileToUpload);

   }

  // if (f22.files && f22.files[0]) {
  //  let fileToUpload = f22.files[0];
  // formData.append("FileUpload2", fileToUpload);

 //  }

   if (f3.files && f3.files[0]) {
    let fileToUpload = f3.files[0];
   formData.append("FileUpload3", fileToUpload);

   }

 //  if (f33.files && f33.files[0]) {
  //  let fileToUpload = f33.files[0];
  // formData.append("FileUpload3", fileToUpload);

  // }

   if (f4.files && f4.files[0]) {
    let fileToUpload = f4.files[0];
   formData.append("FileUpload4", fileToUpload);

   }


 //  if (f44.files && f44.files[0]) {
  //  let fileToUpload = f44.files[0];
  // formData.append("FileUpload4", fileToUpload);

  // }





  formData.append("tm_typeID",this.account.trademarktype);
  formData.append("product_title",this.account.trademarktitle);
  formData.append("nation_classID",this.account.trademarkclass);
  formData.append("logo_descriptionID",this.account.trademarklogo);
  formData.append("nice_class_description",this.account.trademarkdesc);
  formData.append("Applicationtypeid","1");
  formData.append("userId",userid);
  formData.append("pwalletid",this.pwalletid);



  this.busy = this.registerapi
  .SavePwallet(formData)
  .then((response: any) => {


    console.log("response")
    console.log(response)

    this.pwalletid =response.content




    localStorage.setItem('Pwallet',this.pwalletid);


    this.busy =   this.registerapi
.GetAknwoledgment(this.pwalletid)
.then((response: any) => {

  console.log("ackno response")
  console.log(response.content)
  this.savemode = true;
this.trademarktype=response.content.markinfo.trademarktype.description
  this.image1  = response.content.markinfo.logoPicture
  this.image2  = response.content.markinfo.approvalDocument
  this.image3  = response.content.markinfo.supportDocument1
  this.image4  = response.content.markinfo.supportDocument2

  let obj =  response.content.trademarkLogo.find(o => o.id === response.content.markinfo.tradeMarkTypeID);
this.trademarklogo =obj.type

})
         .catch((response: any) => {

           console.log(response)


         })

   // alert("succcess")

  })
           .catch((response: any) => {
             this.submitted= false;

             Swal.fire(
               response.error.message,
               '',
               'error'
             )
   })

}

else {
  alert("Form  Not Valid")
}

//$(this).steps("next");

  }

  onSubmit3() {
    this.makePayment()
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

              description: self.row2.description,
              quatity: "1",
              amount: self.tot ,
              paymentref:response.paymentReference ,
              transactionid:self.transactionid



          };

        //  localStorage.setItem('Payment',JSON.stringify( Payment));
        alert("Payment Sucessful")
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


  ngOnInit() {

    let email =localStorage.getItem('username');

    if (this.registerapi.checkAccess("#/Dashboard/NewApplication"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }


    this.registerapi.setPage("PrelimSearch")

    this.registerapi.VChangeEvent("PrelimSearch");


    var form = $(".validation-wizard").show();

    var self = this;
    $(".validation-wizard").steps({
        headerTag: "h6",
        bodyTag: "section",
        transitionEffect: "fade",
        enableFinishButton: false,
        enablePagination:false,
        titleTemplate: '<span class="step">#index#</span> #title#',
        labels: {
            finish: "Submit"
        },
        onStepChanging: function(event, currentIndex, newIndex) {
            return currentIndex > newIndex || !(3 === newIndex && Number($("#age-2").val()) < 18) && (currentIndex < newIndex && (form.find(".body:eq(" + newIndex + ") label.error").remove(), form.find(".body:eq(" + newIndex + ") .error").removeClass("error")), form.validate().settings.ignore = ":disabled,:hidden", form.valid())
        },
        onFinishing: function(event, currentIndex) {
            return form.validate().settings.ignore = ":disabled", form.valid()
        },
        onFinished: function(event, currentIndex) {
          //  swal("Form Submitted!", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem erat eleifend ex semper, lobortis purus sed.");

        }
    }), $(".validation-wizard").validate({
        ignore: "input[type=hidden]",
        errorClass: "text-danger",
        successClass: "text-success",
        highlight: function(element, errorClass) {
            $(element).removeClass(errorClass)
        },
        unhighlight: function(element, errorClass) {
            $(element).removeClass(errorClass)
        },
        errorPlacement: function(error, element) {
            error.insertAfter(element)
        },
        rules: {
            email: {
                email: !0
            }
        }
    })


    //var userid = localStorage.getItem('UserId');

    this.filepath = this.registerapi.GetFilepath2();

    this.vfilepath =this.registerapi.GetFilepath();

    const firstParam: string = this.route.snapshot.queryParamMap.get('RRR');
    const secondparam: string = this.route.snapshot.queryParamMap.get('orderID');
    var userid = localStorage.getItem('UserId');
  var vdesc =  localStorage.getItem('description');
    this.transactionid = secondparam ;

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

            description: vdesc ,
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



    this.busy =   this.registerapi
    .GetAknwoledgmentByUserid(userid)
    .then((response: any) => {

      console.log("ackno by user")
      console.log(response.content)

      if (response.content) {

        const swalWithBootstrapButtons = Swal.mixin({
          customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
          },
          buttonsStyling: false,
        })

            swalWithBootstrapButtons.fire({
              title: 'You Have Pending Application ,Proceed to file them  ',
              text: "",
              type: 'warning',
              showCancelButton: true,
              confirmButtonText: 'Yes, Proceed!',
              cancelButtonText: 'No, cancel!',
              reverseButtons: true
            }).then((result) => {
              if (result.value) {

                this.image1  = response.content.markinfo.logoPicture
                this.image2  = response.content.markinfo.approvalDocument
                this.image3  = response.content.markinfo.supportDocument1
                this.image4  = response.content.markinfo.supportDocument2
                this.pwalletid =response.content.application.id
                this.account.trademarktype=response.content.markinfo.tradeMarkTypeID
                this.account.trademarklogo =response.content.markinfo.logo_descriptionID
                this.account.trademarktitle =response.content.markinfo.productTitle
                this.account.trademarkclass=response.content.markinfo.niceClass

                this.account.trademarkdesc =response.content.markinfo.niceClassDescription

              //  $(".validation-wizard").steps("next");





               // self.onSubmit8();
          } else if (
                // Read more about handling dismissals
                result.dismiss === Swal.DismissReason.cancel
              ) {

              }
            })

      }




    })
             .catch((response: any) => {

               console.log(response)


             })


      this.row.push(8)


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

     this.row22 =response.content

     this.vshow2 = true;





   })
            .catch((response: any) => {

              console.log(response)


             Swal.fire(
               response.error.message,
               '',
               'error'
             )

})

this.busy =   this.registerapi
.GetFeeListByName("APPLICATION FOR REGISTRATION OF TRADE/SERVICE MARK" ,userid)
.then((response: any) => {

  console.log("fee  Response")
  this.row2 = response.content;
  localStorage.setItem('description',this.row2.description);
  this.tot = parseInt(this.row2.init_amt ) +  parseInt(this.row2.technologyFee )
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

this.busy =   this.registerapi
.GetNationalClass()
.then((response: any) => {

  this.rows = response.content;
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



this.busy =   this.registerapi
.GetTrademarkLogo()
.then((response: any) => {

  this.row11 = response.content;
  console.log("Trademark Logo")
  console.log(response.content)



})
         .catch((response: any) => {

           console.log(response)


          Swal.fire(
            response.error.message,
            '',
            'error'
          )

})



this.busy =   this.registerapi
.GetEmail(email)
.then((response: any) => {

  console.log("User")

  console.log(response)

this.categoryid = response.categoryId

if (this.categoryid =="2") {
  this.vshow3 = true

}

else {
  this.vshow3 = false

}




})
         .catch((response: any) => {

           console.log(response)


          Swal.fire(
            response.error.message,
            '',
            'error'
          )

})

this.busy =   this.registerapi
.GetTradeMarkType(userid)
.then((response: any) => {

  this.row10 = response.content;
  console.log("TrademarkType")
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

    }

  }

}
