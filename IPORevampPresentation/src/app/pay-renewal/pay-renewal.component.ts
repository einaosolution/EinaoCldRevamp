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
  selector: 'app-pay-renewal',
  templateUrl: './pay-renewal.component.html',
  styleUrls: ['./pay-renewal.component.css']
})
export class PayRenewalComponent implements OnInit {

  dtOptions:any = {};
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = true;
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
  public NoticeAppID = "" ;
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
  public row10 :any;
  public row11 = [];
  public fee_description =""
  public  imageSrc
  public categoryid
  public checkboxFlag =false
  public nextrenewal :any;

  public image1
  public image2
  public image3
  public image4
  public filepath

  vshow3:boolean =false


  row:any[] =[]
  row22:any
  vshow2:boolean =false
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

    name:null ,
    address:null ,
    comment:null
};

varray4 = [{ YearName: 'Local', YearCode: 'Local' }, { YearName: 'Foreign', YearCode: 'Foreign' } ]
varray5 = [{ YearName: 'DEVICES', YearCode: 'DEVICES' }, { YearName: 'WORD MARK', YearCode: 'WORD MARK' } , { YearName: 'WORD AND DEVICE', YearCode: 'WORD AND DEVICE' } ]
varray = [{ name: 'First Renewal', id: 'First Renewal' }, { name: 'Subsequent Renewal', id: 'Subsequent Renewal' }]
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

  FieldsChange(values:any){

   if (values.currentTarget.checked) {
    this.checkboxFlag =true
    Swal.fire('Click make payment button to proceed')

   // alert("Click Pay with remitta button to proceed")
   }

   else {
    this.checkboxFlag =false
    Swal.fire('Terms And Condition Unchecked', 'error')
   // alert("Ok")
   }

    }

  onChange($event) {


    let obj2 = this.rows.find(o => o.type === this.account.trademarkclass);

    console.log("obj2")
    console.log(obj2)

   // this.account.trademarkdesc = obj2.description





  }

   onSubmit2(f) {
    localStorage.setItem('NoticeAppID' ,this.NoticeAppID);
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









  $(".validation-wizard").steps("next");



if (f) {

  //let fi = this.fileInput.nativeElement;
 // let f1 = this.fileInput1.nativeElement;

 // let f22 = this.fileInput22.nativeElement;
 // let f3 = this.fileInput3.nativeElement;
 // let f33 = this.fileInput33.nativeElement;
  //let f4 = this.fileInput4.nativeElement;
 // let f44 = this.fileInput44.nativeElement;

 try {
 let f2 = this.fileInput2.nativeElement;
   if (f2.files && f2.files[0]) {
    let fileToUpload = f2.files[0];
   formData.append("FileUpload2", fileToUpload);

   }

  }

  catch(err) {

  }

  try {
    let f2 = this.fileInput1.nativeElement;
      if (f2.files && f2.files[0]) {
       let fileToUpload = f2.files[0];
      formData.append("FileUpload", fileToUpload);

      }

     }

     catch(err) {

     }














  formData.append("Name",this.account.name);
  formData.append("Address",this.account.address);
  formData.append("Comment",this.account.comment);
  formData.append("Title",this.account.trademarktitle);
  formData.append("Type",this.account.trademarktype);
 // formData.append("nice_class_description",this.account.trademarkdesc);
 formData.append("NextRenewal",this.nextrenewal);

  formData.append("NoticeAppID",this.NoticeAppID);
  formData.append("userId",userid);
  formData.append("AppID",this.pwalletid);



  this.busy = this.registerapi
  . SaveRenewalForm(formData)
  .then((response: any) => {

    this.savemode = false;
    console.log("response")
    console.log(response)

    this.NoticeAppID =response.content

   // alert(this.NoticeAppID)




    localStorage.setItem('Pwallet',this.pwalletid);


    this.busy =   this.registerapi
.GetRenewalApplicationById(this.NoticeAppID,userid)
.then((response: any) => {

  console.log("renewal by id  response")
  console.log(response.content)

  this.image1  = response.content.powerOfAttorney
  this.image2  = response.content.certificateOfTrademark


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
  Swal.fire(
    "Some Required Field are Missing",
    '',
    'error'
  )
}

//$(this).steps("next");

  }

  onSubmit3a() {
    $(".validation-wizard").steps("next");
    $(".validation-wizard").steps("next");




  }

  onSubmit3() {
    // this.makePayment()
    this.row = []
    this.row.push(15)
    var userid = localStorage.getItem('UserId');

    var kk = {
      FeeIds:this.row ,
      UserId:userid ,



    }
    this.busy =  this.registerapi
    . InitiateRemitaPayment(kk)
    .then((response: any) => {

      console.log("RemittaResponse")
    //  this.rows = response.content;
      console.log(response.content)

      this.row22 =response.content

     // this.rrr =this.row22.rrr
     localStorage.removeItem('row22');

      localStorage.setItem('row22',JSON.stringify( this.row22));

     //this.value = this.row22.rrr


      this.vshow2 = true;


      var Payment= {

       description: this.row2.description,
       quatity: "1",
       amount: this.tot ,
       paymentref:this.row22.rrr,
       transactionid:''



   };

  localStorage.setItem('Payment',JSON.stringify( Payment));

  localStorage.setItem('PaymentType',"PayRenewal");
    //  $(".validation-wizard").steps("next");

    this.router.navigateByUrl('/Dashboard/Invoice2');



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







  ngOnInit() {

    let email =localStorage.getItem('username');
   this.pwalletid =localStorage.getItem('Pwallet');




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

    this.tot =  localStorage.getItem('tot');

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
      .GetRecordalApplicationById2(this.pwalletid,userid)
      .then((response: any) => {

        this.row10 = response.content;
        console.log("Applicationbyid response")

       this.account.name = response.content.applicantName

      this.account.address= response.content.applicantAddress
      this.account.trademarktitle =response.content.productTitle

      this.nextrenewal = response.content.nextrenewalDate
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
.GetFeeListByName("RENEWAL OF TRADE/SERVICE MARKS (TO COMPEL PROPRIETORS TO RENEW AS AND WHEN DUE, REGULATION 66-70 RELATING TO RENEWALS SHOULD BE ENFORCED)" ,userid)
.then((response: any) => {

  console.log("fee  Response")
  this.row2 = response.content;
  localStorage.setItem('description',this.row2.description);
  this.tot = parseInt(this.row2.init_amt ) +  parseInt(this.row2.technologyFee )

  localStorage.setItem('tot', this.tot );
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