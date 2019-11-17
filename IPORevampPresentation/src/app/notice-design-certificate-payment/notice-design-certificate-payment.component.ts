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
import {Fee} from '../Fee';

import {Fee2} from '../Fee2';


import { map } from 'rxjs/operators';

import 'datatables.net'
import 'datatables.net-dt'


declare var $;
declare var RmPaymentEngine:any;

@Component({
  selector: 'app-notice-design-certificate-payment',
  templateUrl: './notice-design-certificate-payment.component.html',
  styleUrls: ['./notice-design-certificate-payment.component.css']
})
export class NoticeDesignCertificatePaymentComponent implements OnInit {

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

  public image1
  public image2
  public image3
  public image4
  public filepath
 public  elementType = 'url';
  public value = '';
  settingoff:boolean =false
  settingcode


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
    quantity :null ,

    name:null ,
    address:null ,
    comment:null
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

  onSubmit5a() {
    $(".validation-wizard").steps("previous");


  }


  generateInvoice() {
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

  FieldsChange(values:any){

   if (values.currentTarget.checked) {
    this.checkboxFlag =true
    Swal.fire('Click make payment button to proceed')
   // alert("Click Pay with remitta button to proceed")
   }

   else {
    this.checkboxFlag =false
   // alert("Ok")
   Swal.fire('Terms And Condition Unchecked', 'error')
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





  //let fi = this.fileInput.nativeElement;
 // let f1 = this.fileInput1.nativeElement;
 // let f2 = this.fileInput2.nativeElement;
 // let f22 = this.fileInput22.nativeElement;
 // let f3 = this.fileInput3.nativeElement;
 // let f33 = this.fileInput33.nativeElement;
  //let f4 = this.fileInput4.nativeElement;
 // let f44 = this.fileInput44.nativeElement;

















  formData.append("opponentName",this.account.name);
  formData.append("opponentAddress",this.account.address);
  formData.append("quantity",this.account.quantity);
 // formData.append("logo_descriptionID",this.account.trademarklogo);
 // formData.append("nice_class_description",this.account.trademarkdesc);
  formData.append("NoticeAppID",this.NoticeAppID);
  formData.append("userId",userid);
  formData.append("AppID",this.pwalletid);



  this.busy = this.registerapi
  .SaveCertificatePayment(formData)
  .then((response: any) => {

    this.savemode = false;
    console.log("response")
    console.log(response)

    this.NoticeAppID =response.content

   // alert(this.NoticeAppID)




    localStorage.setItem('Pwallet',this.pwalletid);


    this.busy =   this.registerapi
.GetCounterOpposeFormById(this.NoticeAppID,userid)
.then((response: any) => {

  console.log("oppose response")
  console.log(response.content)

  this.image1  = response.content.upload


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





//$(this).steps("next");

  }

  onSubmit3a() {
    $(".validation-wizard").steps("next");
    $(".validation-wizard").steps("next");




  }

  onSubmit33() {
    // this.makePayment()
    this.row = []
   // this.row.push(8)
    var userid = localStorage.getItem('UserId');

    let qty =   parseInt( localStorage.getItem('quantity'));
    this.account.quantity = qty;



    var Payment= {

      description: this.row2.description,
      quatity: this.account.quantity,
      amount: this.tot ,
      paymentref:"x13389996777",
      transactionid:''



  };

  localStorage.setItem('Payment',JSON.stringify( Payment));

  localStorage.setItem('PaymentType',"CertDesignPayment");
  localStorage.setItem('settings',this.settingcode);



    //  $(".validation-wizard").steps("next");

    this.router.navigateByUrl('/Dashboard/processinginvoice');








   }

  onSubmit3() {
    // this.makePayment()
    this.row = []

    let qty =   parseInt( localStorage.getItem('quantity'));
    this.account.quantity = qty;
   // let qty = parseInt(this.account.quantity)

    for (let i = 0; i < qty; i++) {
    //  this.row.push(27)
    this.row.push(parseInt(Fee2.CTCOFCERTIFICATEOFINDUSTRIALDESIGN))
    }


  //  this.row.push(8)
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

     this.value = this.row22.rrr


      this.vshow2 = true;


      var Payment= {

       description: this.row2.description,
       quatity: this.account.quantity,
       amount: this.tot ,
       paymentref:this.row22.rrr,
       transactionid:''



   };

  localStorage.setItem('Payment',JSON.stringify( Payment));

  localStorage.setItem('PaymentType',"CertDesignPayment");
  localStorage.setItem('settings',this.settingcode);
    //  $(".validation-wizard").steps("next");

    this.router.navigateByUrl('/Dashboard/processinginvoice');



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

  this.account.quantity =  localStorage.getItem('quantity');
   this.pwalletid =localStorage.getItem('pwalletid');






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


    }

    else {

      this.busy =   this.registerapi
      .GetEmail(email)
      .then((response: any) => {

        this.row10 = response.content;
        console.log("returned users")
        console.log(response)

        let  name = response.firstName + " " + response.lastName
        let  address = response.street


       this.account.name = name

      this.account.address= address

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
      .GetSettingsById("34",userid)
      .then((response: any) => {

      var Settings = response.content;
      console.log("Settins Value")
        console.log(response.content)
      this.settingcode =Settings.itemValue
      if (Settings.itemValue =="0"    ) {
        // alert("off")
        this.settingoff =true;
       }

       else {
         this.settingoff =false;

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
.GetFeeListByName("(A)CTC OF CERTIFICATE OF INDUSTRIAL DESIGN" ,userid)
.then((response: any) => {

  console.log("fee  Response")
  this.row2 = response.content;
  localStorage.setItem('description',this.row2.description);
 // let qty2 = parseInt(this.account.quantity)
  let qty2 =   parseInt( localStorage.getItem('quantity'));

  this.tot = (parseInt(this.row2.init_amt ) +  parseInt(this.row2.technologyFee ) )* qty2;



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
