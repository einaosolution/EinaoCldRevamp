import { Component, OnInit,ViewChild,OnDestroy } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { Student } from '../Student';
import { Invention } from '../Invention';
import { Priority } from '../Priority';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl , FormArray } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';
import { Subject } from 'rxjs';
import {Fee} from '../Fee';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import {formatDate} from '@angular/common';


import { map } from 'rxjs/operators';

import 'datatables.net'
import 'datatables.net-dt'




declare var $;




import * as jspdf from 'jspdf';

import html2canvas from 'html2canvas';

declare var RmPaymentEngine:any;

interface Person {
  fullName: string;

 }

@Component({
  selector: 'app-nnew-design',
  templateUrl: './nnew-design.component.html',
  styleUrls: ['./nnew-design.component.css']
})
export class NnewDesignComponent implements OnDestroy ,OnInit {

  dtOptions:any = {};
  students: Student[] = [];
 invention:Invention[] = [];
 priority:Priority[] = [];
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = false;
  savemode2:boolean = false;
  savemode3:boolean = false;
  updatemode:boolean = false;
  userform: FormGroup;
  userform2: FormGroup;
  userform3: FormGroup;
  Inventor: FormArray;
  Priority2: FormArray;

  public Fee = Fee;


  submitted:boolean=false;
  submitted2:boolean=false;
  submitted3:boolean=false;
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

  vshow10 :boolean = false


  elementType = 'url';
  value = '';

  transactionid
  paymentreference
  transactionid2

  public rows=[];
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
  public row100  = [];
  public row101:any[] = [];
  public row102 :any[] = [];
  public row500 :any[] = [];
  public row600 :any[] = [];

  public row501 =[];

  public row502  =[];
  public vid ="";

  public rrr ="";

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
  public image5
  public image6
  public image7
  public image8
  public image9
  public filepath
  public trademarklogo


  row:any[] =[]
  row33:any[] =[]
  row22:any
  vshow2:boolean =false
  vshow3:boolean =false
  settingoff:boolean =false

   phonepattern ="^[\s()+-]*([0-9][\s()+-]*){6,20}$";

  emailpattern ="^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$";
  settingcode
  vfilepath:string =""
  @ViewChild("fileInput") fileInput;
  @ViewChild("fileInput1") fileInput1;
  @ViewChild("fileInput2") fileInput2;
  @ViewChild("fileInput22") fileInput22;
  @ViewChild("fileInput23") fileInput23;
  @ViewChild("fileInput24") fileInput24;
  @ViewChild("fileInput25") fileInput25;
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
  constructor(private registerapi :ApiClientService ,private router: Router ,private route: ActivatedRoute ,private formBuilder: FormBuilder) { }

  getlogo(vid) {
    return this.trademarklogo
  }

  feelist(description:string ) {
  //  alert("called")
    let  userid = localStorage.getItem('UserId');
    this.registerapi
.GetFeeListByName(description ,userid)
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


  }



  onChange2(deviceValue) {

   // let obj =  this.row11.find(o => o.id === deviceValue);
    //this.trademarklogo =obj.type

    if (deviceValue =="2") {
    // alert("2")
      this.vshow = false;

this.feelist(Fee.REGISTRATIONOFDESIGNSTEXTILE)
    }

    else {
    //  alert("1")
      this.vshow = true;
      this.feelist(Fee.REGISTRATIONOFDESIGNSNONTEXTILE)
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

   // Swal.fire('Click make payment button to proceed')


   }

   else {
    this.checkboxFlag =false
    Swal.fire('Terms And Condition Unchecked', 'error')
   //alert("Terms And Condition Unchecked")
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



   onSubmit2(f) {
    $(".validation-wizard").steps("next");

   }

   chng()
    {

      let test = this.fileInput.nativeElement;


    let test2 = test.files[0];





    if(test2.size/ 1024 >3000){

      alert("File Too Large,maxsize is 3mb")

      test.value = ''

      return ;

    }

    }


    ngOnDestroy(): void {
      // Do not forget to unsubscribe the event
      this.dtTrigger.unsubscribe();
    }

    chng2()
    {

      let test = this.fileInput2.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large,maxsize is 3mb")

      test.value = ''

      return ;

    }

    }

    chng3()
    {

      let test = this.fileInput3.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large,maxsize is 3mb")

      test.value = ''

      return ;

    }

    }

    chng4()
    {

      let test = this.fileInput4.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large,maxsize is 3mb")

      test.value = ''

      return ;

    }

    }

    chng22()
    {

      let test = this.fileInput22.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large,maxsize is 3mb")

      test.value = ''

      return ;

    }

    }


    chng23()
    {
//tony
      let test = this.fileInput23.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large,maxsize is 3mb")

      test.value = ''

      return ;

    }

    }



    chng24()
    {

      let test = this.fileInput24.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large")

      test.value = ''

      return ;

    }

    }

    chng25()
    {

      let test = this.fileInput25.nativeElement;


    let test2 = test.files[0];



    if(test2.size/ 1024>3000){

      alert("File Too Large")

      test.value = ''

      return ;

    }

    }


    onSubmit8(){



        html2canvas(document.getElementById('report')).then(function(canvas) {
        var doc = new jspdf('p', 'pt', [canvas.width, canvas.height]);
        //  var img = canvas.toDataURL("image/png");
        var img = canvas.toDataURL("image/png", 1.0);


       var width = doc.internal.pageSize.width;
       var height = doc.internal.pageSize.height;

       doc.addImage(img, 'JPEG', 0, 0, width,  canvas.height);

     doc.save('Output.pdf');

     alert("success")


      });
      }

      onSubmit105(pwalletid) {


        this.row101  = [];
        let result = this.registerapi.getInvention2();

     //   for (let i = 0; i < this.InventorFormGroup.controls.length; i++) {
          for (let i = 0; i < result.length; i++) {
         let invention =  {
          PatentApplicationID:pwalletid ,

          CountryId :result[i].NationalityId ,
        //  InventorName :this.InventorFormGroup.controls[i].value.name ,
           InventorName :result[i].Name ,
          InventorAddress :result[i].Address ,
          InventorEmail :result[i].Email,
          InventorMobileNumber :result[i].phonenumber ,


         }

        this.row101.push(invention)

         // console.log(this.InventorFormGroup.controls[i].value.name)
         }

        this.busy =   this.registerapi
        .SaveDesignInvention(this.row101)
        .then((response: any) => {

       //   this.onSubmit101(this.pwalletid) ;


let result2 = this.registerapi.getPriority2();



if (result2.length == 0 ) {

 this.onSubmit103(this.pwalletid)

}


else {
 this.onSubmit101(this.pwalletid) ;
}



        })
                 .catch((response: any) => {



                  Swal.fire(
                    response.error.message,
                    '',
                    'error'
                  )

     })

      }


      onSubmit103(pwalletid) {

        this.busy =   this.registerapi
        .GetDesignApplicationById(pwalletid)
        .then((response: any) => {

         console.log("Latest Pwallet ")
       //  this.row4 = response.content;

       this.row500 = response.content

       this.savemode = true;

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



      }

      onSubmit101(pwalletid) {


        this.row102  = [];
let result = this.registerapi.getPriority2()

       for (let i = 0; i < result.length; i++) {
        let priority =  {
          PatentApplicationID:pwalletid ,

          CountryId :result[i].NationalityId ,
          ApplicationNumber :result[i].ApplicationNumber,
          RegistrationDate : result[i].RegistrationDate



         }

        this.row102.push(priority)

         // console.log(this.InventorFormGroup.controls[i].value.name)
         }

        if (this.row102.length > 0) {
        this.busy =   this.registerapi
        .SaveDesignPriority(this.row102)
        .then((response: any) => {


          this.onSubmit103(this.pwalletid)

        })
                 .catch((response: any) => {

                  this.onSubmit103(this.pwalletid)

                //  Swal.fire(
                 //   response.error.message,
                 //   '',
                 //   'error'
                 // )

     })

    }

      }


    Onsubmit300(emp) {



    }


         Onsubmit401(emp) {

this.savemode = false;
  this.updatemode = true;

  console.log("emp value")

   console.log(emp) ;






 (<FormControl> this.userform3.controls['applicationnumber']).setValue(emp.ApplicationNumber);
 (<FormControl> this.userform3.controls['country']).setValue(emp.NationalityId);
 (<FormControl> this.userform3.controls['priodate']).setValue(emp.RegistrationDate);


  this.vid = emp.id ;

 // (<FormControl> this.userform.controls['Description']).setValue("");
  $("#createmodel2").modal('show');

     }

 ReintializeData() {
   var table = $('#myTable2').DataTable();
   var table2 = $('#myTable').DataTable();

  table.destroy();
  table2.destroy();



 const priorityObservable = this.registerapi.getPriority();
  priorityObservable.subscribe((priorityData: Priority[]) => {
      this.priority = priorityData;
      console.log("Priority Data")
       console.log(this.priority)
  });


  const inventionObservable = this.registerapi.getInvention();
        inventionObservable.subscribe((inventionData: Invention[]) => {
            this.invention = inventionData;
        });



this.dtTrigger.next()



 }


      Onsubmit201(emp) {

        this.registerapi.RemovePriority(emp.id)




  this.ReintializeData()

      }


     Onsubmit400(emp) {

this.savemode = false;
  this.updatemode = true;

  console.log("emp value")

   console.log(emp) ;






 (<FormControl> this.userform2.controls['name']).setValue(emp.Name);
 (<FormControl> this.userform2.controls['address']).setValue(emp.Address);
 (<FormControl> this.userform2.controls['phone']).setValue(emp.phonenumber);
 (<FormControl> this.userform2.controls['email']).setValue(emp.Email);
  (<FormControl> this.userform2.controls['nationality']).setValue(emp.NationalityId);

  this.vid = emp.id ;

 // (<FormControl> this.userform.controls['Description']).setValue("");
  $("#createmodel").modal('show');

     }

      Onsubmit200(emp) {

        this.registerapi.RemoveInvention(emp.id)


        this.ReintializeData() ;



      }


      Onsubmit202(emp) {

        this.registerapi.RemovePriority(emp.id)


        this.ReintializeData() ;



      }

       onSubmit500() {

        this.submitted=true;


        var regexp2 = /^[\s()+-]*([0-9][\s()+-]*){6,20}$/


        if (this.userform2.value.name=='' || this.userform2.value.address=='' || this.userform2.value.email=='' || this.userform2.value.nationality==''  ) {

          return ;
        }


        if (this.userform2.value.phone.match(regexp2) == null) {
         Swal.fire(
           "Invalid Phone Number ",
            '',
            'error'
          )

          return ;
        }

    let countryname =     this.row11.find(s => s.id == this.userform2.value.nationality);







  var Invention = {
    Name:this.userform2.value.name ,
    Address:this.userform2.value.address ,
    phonenumber:this.userform2.value.phone ,
    Email:this.userform2.value.email ,
    NationalityId:this.userform2.value.nationality ,
    Nationality:countryname.name  ,
    id:this.registerapi.GetRandomNumber()



  }
 this.registerapi.RemoveInvention(this.vid)
  this.registerapi.AddInvention(Invention)
  var table = $('#myTable').DataTable();
  $("#createmodel").modal('hide');
  this.ReintializeData() ;





      }



      onSubmit501() {

        this.submitted=true;

        let countryname =     this.row11.find(s => s.id == this.userform3.value.country);





        if (this.userform3.valid) {
  var priority = {
    ApplicationNumber:this.userform3.value.applicationnumber ,
    RegistrationDate:formatDate(this.userform3.value.priodate, 'MM/dd/yyyy', 'en'),
    Nationality:countryname.name ,

    NationalityId:this.userform3.value.country   ,
    id:this.registerapi.GetRandomNumber()



  }
this.registerapi.RemovePriority(this.vid)
  this.registerapi.AddPriority(priority)

  $("#createmodel2").modal('hide');

  this.ReintializeData();



        }




      }


            onSubmit1000() {

        this.submitted3=true;



    let countryname =     this.row11.find(s => s.id == this.userform3.value.country);





        if (this.userform3.valid) {
  var priority = {
    ApplicationNumber:this.userform3.value.applicationnumber ,
    RegistrationDate:formatDate(this.userform3.value.priodate, 'MM/dd/yyyy', 'en'),
    Nationality:countryname.name ,

    NationalityId:this.userform3.value.country   ,
    id:this.registerapi.GetRandomNumber()



  }

  this.registerapi.AddPriority(priority)

  $("#createmodel2").modal('hide');

  this.ReintializeData();



        }




      }

      onSubmit100() {

        this.submitted=true;

        var regexp = '^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$'


        if (this.userform2.value.name=='' || this.userform2.value.address=='' || this.userform2.value.email=='' || this.userform2.value.nationality==''  ) {

          return ;
        }



        if (this.userform2.value.email.match(regexp) == null) {
         Swal.fire(
           "Invalid Email  ",
            '',
            'error'
          )

          return ;
        }


        var regexp2 = /^[\s()+-]*([0-9][\s()+-]*){6,20}$/


   if (this.userform2.value.phone.match(regexp2) == null) {
    Swal.fire(
      "Invalid Phone Number ",
       '',
       'error'
     )

     return ;
   }

    let countryname =     this.row11.find(s => s.id == this.userform2.value.nationality);






  var Invention = {
    Name:this.userform2.value.name ,
    Address:this.userform2.value.address ,
    phonenumber:this.userform2.value.phone ,
    Email:this.userform2.value.email ,
    NationalityId:this.userform2.value.nationality ,
    Nationality:countryname.name  ,
    id:this.registerapi.GetRandomNumber()



  }

  this.registerapi.AddInvention(Invention)

  $("#createmodel").modal('hide');

  this.ReintializeData();









      }

  onSubmit() {

    this.AddressOfService() ;
this.submitted2=true;



var  formData = new FormData();
var userid = localStorage.getItem('UserId');

let result = this.registerapi.getInvention2();

let result2 = this.registerapi.getPriority2();



if (result.length == 0 ) {

  Swal.fire(
    "Enter Inventor Information   ",
     '',
     'error'
   )

   return ;
}


try {
let f4 = this.fileInput.nativeElement;
if ((f4.files[0]  ||this.image1) ) {


}

else {

  Swal.fire(
    "Please Upload NOVELTY STATEMENT ",
     '',
     'error'
   )

   return;

}
}
catch(err) {

}


try {

let f3 = this.fileInput22.nativeElement;
if ((f3.files[0]  ||this.image6) ) {



}

else {

  Swal.fire(
    "Please Upload REPRESENTATION OF DESIGN 1",
     '',
     'error'
   )

   return;

}

}

catch(err) {

}



try {

  let f22 = this.fileInput4.nativeElement;
  if ((f22.files[0]  ||this.image3) ) {



  }

  else {

    Swal.fire(
      "Please Upload Deed of Assignment ",
       '',
       'error'
     )

     return;

  }

  }

  catch(err) {

  }



if (this.categoryid =="2") {

  let f2 = this.fileInput2.nativeElement;
 // let f22 = this.fileInput22.nativeElement;

  if (f2.files[0]  ||this.image2) {


   }

   else {

    Swal.fire(
     "Please Upload Letter of Authorization ",
      '',
      'error'
    )

    return;

   }

  }

//alert(this.userform.value.patenttype)




//for (let i = 0; i < this.InventorFormGroup.controls.length; i++) {
 //console.log(this.InventorFormGroup.controls[i].value.name)
//}












  //$(".validation-wizard").steps("previous");



if (this.userform.valid) {



 // let f1 = this.fileInput1.nativeElement;

  //let f22 = this.fileInput22.nativeElement;

 // let f33 = this.fileInput33.nativeElement;

 // let f44 = this.fileInput44.nativeElement;

 try {
  let fi = this.fileInput.nativeElement;
  if (fi.files && fi.files[0]) {
    let fileToUpload = fi.files[0];
   formData.append("FileUpload", fileToUpload);

   }

  }
  catch(err) {


  }

  // if (f1.files && f1.files[0]) {
  //  let fileToUpload = f1.files[0];
  // formData.append("FileUpload", fileToUpload);

  // }

  try {
    let f2 = this.fileInput3.nativeElement;
   if (f2.files && f2.files[0]) {
    let fileToUpload = f2.files[0];
   formData.append("FileUpload2", fileToUpload);

   }

  }
  catch(err) {


  }

  // if (f22.files && f22.files[0]) {
  //  let fileToUpload = f22.files[0];
  // formData.append("FileUpload2", fileToUpload);

 //  }

 try {
  let f3 = this.fileInput4.nativeElement;
   if (f3.files && f3.files[0]) {
    let fileToUpload = f3.files[0];
   formData.append("FileUpload3", fileToUpload);

   }

  }
  catch(err) {


  }

 //  if (f33.files && f33.files[0]) {
  //  let fileToUpload = f33.files[0];
  // formData.append("FileUpload3", fileToUpload);

  // }

  try {
    let f4 = this.fileInput3.nativeElement;
   if (f4.files && f4.files[0]) {
    let fileToUpload = f4.files[0];
   formData.append("FileUpload4", fileToUpload);

   }

  }
  catch(err) {


  }


  try {
    let f4 = this.fileInput22.nativeElement;
   if (f4.files && f4.files[0]) {
    let fileToUpload = f4.files[0];
   formData.append("FileUpload5", fileToUpload);

   }

  }
  catch(err) {


  }



  try {
    let f4 = this.fileInput23.nativeElement;
   if (f4.files && f4.files[0]) {
    let fileToUpload = f4.files[0];
   formData.append("FileUpload6", fileToUpload);

   }

  }
  catch(err) {


  }


    try {
    let f4 = this.fileInput24.nativeElement;
   if (f4.files && f4.files[0]) {
    let fileToUpload = f4.files[0];
   formData.append("FileUpload7", fileToUpload);

   }

  }
  catch(err) {


  }


    try {
    let f4 = this.fileInput25.nativeElement;
   if (f4.files && f4.files[0]) {
    let fileToUpload = f4.files[0];
   formData.append("FileUpload8", fileToUpload);

   }

  }
  catch(err) {


  }


 //  if (f44.files && f44.files[0]) {
  //  let fileToUpload = f44.files[0];
  // formData.append("FileUpload4", fileToUpload);

  // }


console.log("this.row101")

console.log(this.row101)


  formData.append("patenttype",this.userform.value.patenttype);
  formData.append("titleofinvention",this.userform.value.titleofinvention);
  formData.append("description",this.userform.value.description);
  formData.append("AssigneeName",this.userform.value.AssigneeName);
  formData.append("AssigneeAddress",this.userform.value.AssigneeAddress);
  formData.append("AssigneeNationality",this.userform.value.AssigneeNationality);
  formData.append("userId",userid);
  formData.append("AssignorName",this.userform.value.AssignorName);
  formData.append("AssignorAddress",this.userform.value.AssignorAddress);
  formData.append("AssignorNationality",this.userform.value.AssignorNationality);

  formData.append("DesignClass",this.userform.value.DesignClass);


  formData.append("ApplicationId",this.pwalletid);
  formData.append("PatentInvention",JSON.stringify(this.row101));
  formData.append("AttorneyCode",this.userform.value.AttorneyCode);
  formData.append("AttorneyName",this.userform.value.AttorneyName);
  formData.append("Address",this.userform.value.Address);
  formData.append("PhoneNumber",this.userform.value.PhoneNumber);
  formData.append("Email",this.userform.value.Email);
  formData.append("State",this.userform.value.State);



  this.busy = this.registerapi
  .SaveDesign(formData)
  .then((response: any) => {


    console.log("response")
    console.log(response)

    this.pwalletid =response.content




    this.onSubmit105(this.pwalletid ) ;









    localStorage.setItem('Pwallet',this.pwalletid);







         this.row.push(8)


    var kk = {
      FeeIds:this.row ,
      UserId:userid ,



    }





  })
           .catch((response: any) => {
             this.submitted= false;

             Swal.fire(
               response.error.message,
               '',
               'error'
             )
   })

   $(".validation-wizard").steps("next");

}

else {
 // alert("Form  Not Valid")

 Object.keys(this.userform.controls).forEach(field => { // {1}
  const control = this.userform.get(field);            // {2}
  control.markAsTouched({ onlySelf: true });       // {3}
});

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

  onSubmit4a() {
    $(".validation-wizard").steps("next");


  }

  onSubmit5a() {
    $(".validation-wizard").steps("previous");


  }

  onSubmit33() {
    // this.makePayment()
    this.row = []
   // this.row.push(8)
    var userid = localStorage.getItem('UserId');



    var Payment= {

      description: this.row2.description,
      quatity: "1",
      amount: this.tot ,
      paymentref:"x13389996777",
      transactionid:''



  };

  localStorage.setItem('Payment',JSON.stringify( Payment));

  localStorage.setItem('PaymentType',"dsT002");
  localStorage.setItem('settings',this.settingcode);



  this.registerapi.Reset()
    this.router.navigateByUrl('/Dashboard/Invoice2');








   }

  onSubmit3() {
   // this.makePayment()
   this.row = []
   if (this.userform.value.patenttype =="1") {
 this.row.push(24)
   }

   if (this.userform.value.patenttype =="2") {
 this.row.push(26)
   }

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
      quatity: "1",
      amount: this.tot ,
      paymentref:this.row22.rrr,
      transactionid:''



  };

 localStorage.setItem('Payment',JSON.stringify( Payment));
   localStorage.setItem('settings',this.settingcode);

 //localStorage.setItem('PaymentType',"FileT002");
   localStorage.setItem('PaymentType',"dsT002");

   this.registerapi.Reset()
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

         localStorage.setItem('Payment',JSON.stringify( Payment));
      //  alert("Payment Sucessful")
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

 AddressOfService() {
  const AttorneyCode = this.userform.get('AttorneyCode');
  const AttorneyName = this.userform.get('AttorneyName');
  const Address = this.userform.get('Address');
 const PhoneNumber = this.userform.get('PhoneNumber');
 const Email = this.userform.get('Email');
  const State = this.userform.get('State');



      if (this.vshow3) {
        AttorneyCode.setValidators([Validators.required]);
        AttorneyName.setValidators([Validators.required]);
        Address.setValidators([Validators.required]);
        PhoneNumber.setValidators([Validators.required]);
        Email.setValidators([Validators.required]);
        State.setValidators([Validators.required]);


        AttorneyCode.updateValueAndValidity();
        AttorneyName.updateValueAndValidity();
        Address.updateValueAndValidity();
        Email.updateValueAndValidity();
        State.updateValueAndValidity();

      }

 }


 getInventorFormGroup(index): FormGroup {
  this.Inventor = this.userform.get('Inventor') as FormArray;
  const formGroup = this.Inventor.controls[index] as FormGroup;
  return formGroup;
}

//getPriorityFormGroup(index): FormGroup {
  //this.Priority = this.userform.get('Priority') as FormArray;
 // const formGroup = this.Priority.controls[index] as FormGroup;
 // return formGroup;
//}
 addItem(): void {
  this.Inventor = this.userform.get('Inventor') as FormArray;
  this.Inventor.push(this.createItem());
}

removePriority(index) {
  // this.contactList = this.form.get('contacts') as FormArray;
 // this.Priority.removeAt(index);
}

removeInventor(index) {
  // this.contactList = this.form.get('contacts') as FormArray;
  this.Inventor.removeAt(index);
}

addItem2(): void {
  //this.Priority = this.userform.get('Priority') as FormArray;
  //this.Priority.push(this.createItem2());
}
 createItem(): FormGroup {
  return this.formBuilder.group({
    name: ["",Validators.required ],
    address: ["",Validators.required ],
    phone: ["",Validators.required ] ,
    email: [null, Validators.compose([Validators.required, Validators.email])] ,
    nationality: ["",Validators.required ]
  });
}


createItemB(name,address,phone,email,nationality): FormGroup {
  return this.formBuilder.group({
    name: [name,Validators.required ],
    address: [address,Validators.required ],
    phone: [phone,Validators.required ] ,
    email: [email, Validators.compose([Validators.required, Validators.email])] ,
    nationality: [nationality,Validators.required ]
  });
}

createItem2(): FormGroup {
  return this.formBuilder.group({
    applicationnumber: null,
    country: null,
    priodate: null

  });
}

createItem2B(appnumber ,prioritydate,country ): FormGroup {
  return this.formBuilder.group({
    applicationnumber: appnumber,
    country: country,
    priodate: prioritydate
  });
}



get InventorFormGroup() {
  return this.userform.get('Inventor') as FormArray;
}

get PriorityFormGroup() {
  return this.userform.get('Priority') as FormArray;
}


loaddata() {


    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
        .GetDesignApplicationByUserId(userid)
        .then((response: any) => {

          console.log("Design  Application 2")
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


                   this.row500 = response.content
                   this.pwalletid =response.content.id

                 let  ptifo =response.content.designInformation

                 let  ptiAssignment =response.content.designAssignment

                 let  addressofservice =response.content.designAddressOfService
              if ( this.vshow3) {
                 if (ptifo[0].letterOfAuthorization) {
                 this.image2 = ptifo[0].letterOfAuthorization

                 }

                }
                 if (ptifo[0].deedOfAssignment) {
                 this.image3 = ptifo[0].deedOfAssignment

                 }
                 if (ptifo[0].noveltyStatement) {
                 this.image1 =ptifo[0].noveltyStatement

                 }
                 if (ptifo[0].priorityDocument) {
                  this.image5 =ptifo[0].priorityDocument

                 }
                  if (ptifo[0].representationOfDesign1) {
                  this.image6 =ptifo[0].representationOfDesign1

                  }

                   if (ptifo[0].representationOfDesign2) {
                  this.image7 =ptifo[0].representationOfDesign2

                  }

                   if (ptifo[0].representationOfDesign3) {
                  this.image8 =ptifo[0].representationOfDesign3

                  }

                   if (ptifo[0].representationOfDesign4) {
                  this.image9 =ptifo[0].representationOfDesign4

                  }






                //   this.removeInventor(0);
                //   this.removePriority(0) ;
                 //  this.createItem();

                 (<FormControl> this.userform.controls['titleofinvention']).setValue(ptifo[0].titleOfDesign);
                 (<FormControl> this.userform.controls['patenttype']).setValue(ptifo[0].designTypeID);
                 (<FormControl> this.userform.controls['DesignClass']).setValue(ptifo[0].nationClassID);
                 (<FormControl> this.userform.controls['description']).setValue(ptifo[0].designDescription);
                 (<FormControl> this.userform.controls['AssigneeName']).setValue(ptiAssignment[0].assigneeName);
                 (<FormControl> this.userform.controls['AssigneeAddress']).setValue(ptiAssignment[0].assigneeAddress);
                 (<FormControl> this.userform.controls['AssigneeNationality']).setValue(ptiAssignment[0].assigneeNationalityId);
                 (<FormControl> this.userform.controls['AssignorName']).setValue(ptiAssignment[0].assignorName );
                 (<FormControl> this.userform.controls['AssignorAddress']).setValue(ptiAssignment[0].assignorAddress );
                 (<FormControl> this.userform.controls['AssignorNationality']).setValue(ptiAssignment[0].assignorNationalityId);
                 if (ptifo[0].letterOfAuthorization) {
                 (<FormControl> this.userform.controls['AttorneyCode']).setValue(addressofservice[0].attorneyCode);
                 (<FormControl> this.userform.controls['AttorneyName']).setValue(addressofservice[0].attorneyName);
                 (<FormControl> this.userform.controls['Address']).setValue(addressofservice[0].address );
                 (<FormControl> this.userform.controls['PhoneNumber']).setValue(addressofservice[0].phoneNumber );
                 (<FormControl> this.userform.controls['Email']).setValue(addressofservice[0].email );
                 (<FormControl> this.userform.controls['State']).setValue(addressofservice[0].stateID );

                  let obj2 = this.row33.find(o => o.id === ptifo[0].nationClassID);


  (<FormControl> this.userform.controls['DesignDescription']).setValue(obj2.description);
                 }

                 this.Inventor = this.userform.get('Inventor') as FormArray;



                 // this.Priority = this.userform.get('Priority') as FormArray;
                 this.row501 = response.content.designInvention

                 this.row502 = response.content.designPriority



                 if (ptifo[0].designTypeID =="1") {
                   this.feelist("REGISTRATION OF DESIGNS (TEXTILE)")
                 }

                 if (ptifo[0].patentTypeID =="2") {
                     this.feelist("REGISTRATION OF DESIGNS (NON- TEXTILES)")
                 }




                 for ( let i = 0; i <  this.row501.length; i++) {
              //   this.Inventor.push(  this.createItemB(this.row501[i].inventorName,this.row501[i].inventorAddress,this.row501[i].inventorMobileNumber,this.row501[i].inventorEmail
               // ,this.row501[i].countryId) )


     let countryname =     this.row11.find(s => s.id == this.row501[i].countryId);

 var Invention = {
    Name:this.row501[i].inventorName,
    Address:this.row501[i].inventorAddress,
    phonenumber:this.row501[i].inventorMobileNumber ,
    Email:this.row501[i].inventorEmail,
    NationalityId:this.row501[i].countryId ,
    Nationality:countryname.name  ,
    id:this.registerapi.GetRandomNumber()
    }

  this.registerapi.AddInvention(Invention)





           }


           for ( let i = 0; i <  this.row502.length; i++) {

            let countryname =     this.row11.find(s => s.id == this.row502[i].countryId);


            var priority = {
              ApplicationNumber:this.row502[i].applicationNumber,
              RegistrationDate:this.row502[i].registrationDate,
              Nationality:countryname.name ,

              NationalityId:this.row502[i].countryId   ,
              id:this.registerapi.GetRandomNumber()



            }

            this.registerapi.AddPriority(priority)


      }

      this.ReintializeData();

      this.savemode2 = true;
                //   this.createItemB("test name ","test address","+23407059394683","ozoton@yahoo.com","42")


//for (let i = 0; i <  this.row500.patentInvention.length; i++) {

//}
                   console.log("pending application ")

                   console.log(this.row500)

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
}

showInventor() {

 this.savemode = true;
  this.updatemode = false;
  this.submitted = false ;

this.userform2.reset()
 // (<FormControl> this.userform.controls['Code']).setValue("");

 // (<FormControl> this.userform.controls['Description']).setValue("");
  $("#createmodel").modal('show');

}


showPriority() {

 this.savemode = true;
  this.updatemode = false;
  this.submitted3 = false ;
this.userform3.reset()

 // (<FormControl> this.userform.controls['Code']).setValue("");

 // (<FormControl> this.userform.controls['Description']).setValue("");
  $("#createmodel2").modal('show');

}

onChange($event) {


  let obj2 = this.row33.find(o => o.type === this.userform.value.DesignClass);


  (<FormControl> this.userform.controls['DesignDescription']).setValue(obj2.description);








}
  ngOnInit() {


  this.dtOptions = {
    pagingType: 'full_numbers',
    pageLength: 10,
    dom: 'Bfrtip',
    // Configure the buttons
    buttons: [



    ]

  };



    let email =localStorage.getItem('username');


    const studentsObservable = this.registerapi.getStudents();
        studentsObservable.subscribe((studentsData: Student[]) => {
            this.students = studentsData;
            this.dtTrigger.next()
        });


        const inventionObservable = this.registerapi.getInvention();
        inventionObservable.subscribe((inventionData: Invention[]) => {
            this.invention = inventionData;
        });


        const priorityObservable = this.registerapi.getPriority();
        priorityObservable.subscribe((priorityData: Priority[]) => {
            this.priority = priorityData;
        });




    //this.dtTrigger.next();

   // var table = $('#myTable').DataTable();




  //  this.dtTrigger.next();

    if (this.registerapi.checkAccess("#/Design/newDesign"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }


    this.registerapi.setPage("PrelimSearch")

    this.registerapi.VChangeEvent("PrelimSearch");

    this.userform2 = this.formBuilder.group({
      name: [null,Validators.required ],
      address: [null,Validators.required ],
      phone: ['',Validators.required] ,
      email: new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')
 ])),
      nationality: [null,Validators.required ]

    });


     this.userform3 = this.formBuilder.group({
      applicationnumber: [null,Validators.required ],
      country: [null,Validators.required ],
      priodate: [null,Validators.required ]


    });


    this.userform = this.formBuilder.group({
      patenttype:  [null,Validators.required ],
      titleofinvention: [null,Validators.required ],
      description: [null,Validators.required ],
      terms: [null],
      AssigneeName: [null,Validators.required],
      AssigneeAddress: [null,Validators.required],
      AssigneeNationality: [null,Validators.required],
      AssignorName: [null,Validators.required],
      AssignorAddress: [null,Validators.required],
      AssignorNationality: [null,Validators.required],
      AttorneyCode: [null],
      AttorneyName: [null],
      Address: [null],
      PhoneNumber: [null],
      Email: [null],
      State: [null],
      DesignClass: [null,Validators.required],
      DesignDescription: [null,Validators.required],

   //   Inventor: this.formBuilder.array([ this.createItem() ]) ,

      Priority: this.formBuilder.array([ this.createItem2() ])
    });

  this.Inventor = this.userform.get('Inventor') as FormArray;
  //  this.Priority = this.userform.get('Priority') as FormArray;


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





this.registerapi.Reset()
this.loaddata()

this.busy =   this.registerapi
.GetAllStates(userid)
.then((response: any) => {


  this.row600 = response.content;
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
.GetEmail(email)
.then((response: any) => {

  console.log("User")

  console.log(response)

this.categoryid = response.categoryId

if (this.categoryid =="2") {
  this.vshow3 = true
  this.AddressOfService();

}

else {
  this.vshow3 = false

  this.AddressOfService();

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
.GetCountry("true",userid)
.then((response: any) => {


  this.row11 = response.content;
  console.log("rsponse.content")

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
.GetNationalClass()
.then((response: any) => {

  this.row33 = response.content;
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
.GetAllDesignType(userid)
.then((response: any) => {

  this.row100 = response.content;
  console.log("Design Type")
  console.log(this.row100)



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

