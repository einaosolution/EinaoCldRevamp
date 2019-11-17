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
import {Status} from '../Status';
import {DataStatus} from '../DataStatus';



import { map } from 'rxjs/operators';

import 'datatables.net'
import 'datatables.net-dt'


declare var $;

@Component({
  selector: 'app-examiner-design-fresh',
  templateUrl: './examiner-design-fresh.component.html',
  styleUrls: ['./examiner-design-fresh.component.css']
})
export class ExaminerDesignFreshComponent implements OnInit {


  @ViewChild('dataTable') table;
  @ViewChild("fileInput") fileInput;
  dtOptions:any = {};
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  busy: Promise<any>;
  appcount:any
  model: any = {};
  Code: FormControl;
  userid=""
  id:string;
  pwalletid:string ;
  appdescription:string ;

  Description: FormControl;
  public Status = Status;
  public DataStatus = DataStatus;
  public rows = [];
  public row2
  public row3 = [];
  public row4 = [];
  public row5  = [];
  public row6  = [];
  public coapplicant  = [];
  filepath:any ;

  public appcomment =""
  appcomment3="";
  appcomment2="" ;
  appcomment4 ="" ;

  vshow :boolean = false;
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }

  onSubmit() {

    if (parseInt(this.appcount) > 0 ) {

      Swal.fire(
        "This Login has been used in previous approval ",
        '',
        'error'
      )

      return ;
    }
    $("#createmodel2").modal('show');



}

onSubmit10() {

  if (parseInt(this.appcount) > 0 ) {

    Swal.fire(
      "This Login has been used in previous approval ",
      '',
      'error'
    )

    return ;
  }
  $("#createmodel3").modal('show');



}

onSubmit11() {

 // if (parseInt(this.appcount) > 0 ) {

  //  Swal.fire(
   //   "This Login has been used in previous approval ",
   //   '',
   //   'error'
   // )

  //  return ;
 // }
  $("#createmodel4").modal('show');



}

onSubmit12() {
  if (parseInt(this.appcount) > 0 ) {

    Swal.fire(
      "This Login has been used in previous approval ",
      '',
      'error'
    )

    return ;
  }
  $("#createmodel5").modal('show');



}


showdetail(kk) {

  this.savemode = false;
  this.updatemode = true;
this.row4 = kk;
this.vshow = true;
this.pwalletid = kk.applicationId

  //document.getElementById("openModalButton").click();
 // this.modalRef = this.modalService.show(ref );

 localStorage.setItem('Pwallet',kk.applicationId);

 var userid = localStorage.getItem('UserId');


 this.busy =   this.registerapi
 .GetAddressOfServiceById3( this.pwalletid,userid )
 .then((response: any) => {

   console.log("Address Of Service  By Id ")
   this.row5 = response.content;
   console.log(response.content)







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

this.busy =this.registerapi
.GetDesignCoApplicantById(this.pwalletid ,userid  )
.then((response: any) => {

  console.log("co applicant")
  this.coapplicant = response.content;
  console.log(response.content)







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

this.busy =   this.registerapi
.GetExaminerPreviousComment2( userid ,this.pwalletid )
.then((response: any) => {

  console.log("previous comment ")
  this.row6 = response.content;
  console.log(response.content)







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


 this.busy =   this.registerapi
 .GetDesignInventorById( this.pwalletid,userid )
 .then((response: any) => {

   console.log("Inventor By Id ")
   this.row2 = response.content;
   console.log(response)


   this.busy =   this.registerapi
   .GetDesignPriority( this.pwalletid,userid )
   .then((response: any) => {

     console.log("Priority  By Id ")
     this.row3 = response.content;
     console.log(response)

     this.savemode = true;

     $("#createmodel").modal('show');




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

onSubmit2() {

  if (this.appcomment =="") {
    Swal.fire(
      "Enter Comment",
      '',
      'error'
    )

    return;
   }


   var  formData = new FormData();
    var userid = localStorage.getItem('UserId');

    var table = $('#myTable').DataTable();

    formData.append("pwalletid",this.pwalletid);
   formData.append("comment",this.appcomment);
   formData.append("description","");
   formData.append("fromstatus",Status.Fresh);
   formData.append("tostatus",Status.Fresh);
   formData.append("fromDatastatus",DataStatus.Examiner);
   formData.append("toDatastatus",DataStatus.Acceptance);
   formData.append("userid",userid);


   this.busy =  this.registerapi
   .SaveDesignFreshAppHistory(formData)
   .then((response: any) => {

     this.submitted=false;

  //  this.router.navigate(['/Emailverification']);


  $("#createmodel2").modal('hide');
  $("#createmodel").modal('hide');
  table.destroy();

  this. getallApplication()

  this.router.navigateByUrl('/Design/AcceptanceptanceLetter');

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


onSubmit3() {
  if (this.appcomment2 =="") {
    Swal.fire(
      "Enter Comment",
      '',
      'error'
    )

    return;
   }


   var  formData = new FormData();
    var userid = localStorage.getItem('UserId');

    var table = $('#myTable').DataTable();

    formData.append("pwalletid",this.pwalletid);
   formData.append("comment",this.appcomment2);
   formData.append("description","");
   formData.append("fromstatus",Status.Fresh);
   formData.append("tostatus",Status.Refused);
   formData.append("fromDatastatus",DataStatus.Examiner);
   formData.append("toDatastatus",DataStatus.Examiner);
   formData.append("userid",userid);


   this.busy =  this.registerapi
   .SaveDesignFreshAppHistory(formData)
   .then((response: any) => {

     this.submitted=false;

  //  this.router.navigate(['/Emailverification']);


  $("#createmodel3").modal('hide');
  $("#createmodel").modal('hide');
  table.destroy();

  this. getallApplication()

  this.router.navigateByUrl('/Design/RefusalDesignLetter');

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





onSubmit44() {
  if (this.appcomment3 =="") {
    Swal.fire(
      "Enter Comment",
      '',
      'error'
    )

    return;
   }


   var  formData = new FormData();
    var userid = localStorage.getItem('UserId');

    var table = $('#myTable').DataTable();

    formData.append("pwalletid",this.pwalletid);
   formData.append("comment",this.appcomment3);
   formData.append("description","");
   formData.append("fromstatus",Status.Fresh);
   formData.append("tostatus",Status.Kiv);
   formData.append("fromDatastatus",DataStatus.Examiner);
   formData.append("toDatastatus",DataStatus.Examiner);
   formData.append("userid",userid);


   this.busy =  this.registerapi
   .SaveDesignFreshAppHistory(formData)
   .then((response: any) => {

     this.submitted=false;

  //  this.router.navigate(['/Emailverification']);


  $("#createmodel4").modal('hide');
  $("#createmodel").modal('hide');
  table.destroy();

  this. getallApplication()


  this.busy =   this.registerapi
  . SendUserEmail2(userid,this.userid,this.appcomment3)
  .then((response: any) => {



  })
           .catch((response: any) => {

             console.log(response)


           })





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

valuechange(een ) {




  }


  onSubmit55() {
    if (this.appcomment4 =="") {
      Swal.fire(
        "Enter Comment",
        '',
        'error'
      )

      return;
     }


     var  formData = new FormData();
      var userid = localStorage.getItem('UserId');

      var table = $('#myTable').DataTable();

      formData.append("pwalletid",this.pwalletid);
     formData.append("comment",this.appcomment4);
     formData.append("description","");
     formData.append("fromstatus",Status.Fresh);
     formData.append("tostatus",Status.ReconductSearch);
     formData.append("fromDatastatus",DataStatus.Examiner);
     formData.append("toDatastatus",DataStatus.ReconductSearch);
     formData.append("userid",userid);


     this.busy =  this.registerapi
     .SaveDesignFreshAppHistory(formData)
     .then((response: any) => {

       this.submitted=false;

    //  this.router.navigate(['/Emailverification']);


    $("#createmodel5").modal('hide');
    $("#createmodel").modal('hide');
    table.destroy();

    this. getallApplication()


    this.busy =   this.registerapi
    .SendMailReconductSearch2(userid,this.userid,this.appcomment3)
    .then((response: any) => {



    })
             .catch((response: any) => {

               console.log(response)


             })





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




onSubmit4() {
  $("#createmodel3").modal('show');
}

onChange( deviceValue) {
  console.log(deviceValue);
  this.appdescription = deviceValue
}



  onSubmit5(f) {
    this.submitted = true;

   if (!this.appcomment3) {

    Swal.fire(
      "Comment Required",
      '',
      'error'
    )

    return;
   }



    var  formData = new FormData();
    var userid = localStorage.getItem('UserId');

    var table = $('#myTable').DataTable();

    formData.append("pwalletid",this.pwalletid);
   formData.append("comment",this.appcomment3);
   formData.append("description",this.appdescription);
   formData.append("fromstatus","Fresh");
   formData.append("tostatus","Kiv");
   formData.append("fromDatastatus","Search");
   formData.append("toDatastatus","Search");
   formData.append("userid",userid);


   this.busy =  this.registerapi
   .SaveFreshAppHistory(formData)
   .then((response: any) => {

     this.submitted=false;

  //  this.router.navigate(['/Emailverification']);


  $("#createmodel2").modal('hide');
  $("#createmodel").modal('hide');
  table.destroy();

  this. getallApplication()

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

showcountry2() {


  }


  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getallApplication() {
    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
    .GetDesignFreshExaminerapplication(userid)
    .then((response: any) => {

      console.log("Fresh Response")
      this.rows = response.content;
      console.log(response)
      this.dtTrigger.next();


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



  getApplicationCount(Appid ,userid) {


    this.busy =   this.registerapi
    .GetApplicationCount(Appid,userid)
    .then((response: any) => {


     this.appcount = response.result;
  console.log("Application Count")
  console.log(this.appcount)



     $("#createmodel").modal('show');

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

  showcountry(kk) {

    this.savemode = false;
    this.updatemode = true;
this.row2 = kk;
this.vshow = true;
this.pwalletid = kk.pwalletid

this.userid = kk.userid

localStorage.setItem('Pwallet' ,kk.pwalletid)

let userid = localStorage.getItem('UserId');

var kk2 =["Search"]


this.busy =   this.registerapi
.GetPreviousComment(kk2,userid,this.pwalletid)
.then((response: any) => {

  console.log("Sector Response")
  this.row4 = response.content;
  console.log(response)

 let  userid = localStorage.getItem('UserId');
  this.getApplicationCount(kk.pwalletid ,userid)



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
  //  $("#createmodel").modal('show');
    //document.getElementById("openModalButton").click();
   // this.modalRef = this.modalService.show(ref );
  }

  ngOnInit() {
    this.filepath = this.registerapi.GetFilepath2();

    if (this.registerapi.checkAccess("#/Design/ExaminerFresh"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }

    this.registerapi.setPage("ExaminerFresh")

    this.registerapi.VChangeEvent("ExaminerFresh");

  this.dtOptions = {
    pagingType: 'full_numbers',
    pageLength: 10,
    dom: 'Bfrtip',
    // Configure the buttons
    buttons: [

      'colvis',
      'copy',
      'print',
      'csv',
      'excel',
      'pdf'

    ]

  };


    this.Code = new FormControl('', [
      Validators.required
    ]);

    this.Description = new FormControl('', [
      Validators.required
    ]);



    this.userform = new FormGroup({

      Code: this.Code,
      Description: this.Description ,


    });

   // (<FormControl> this.userform.controls['Code']).setValue("<p> Testing </>");

   var userid = localStorage.getItem('UserId');



   this.busy =   this.registerapi
    .GetDesignFreshExaminerapplication(userid)
    .then((response: any) => {

      console.log("Examiner Response")
      this.rows = response.content;
      console.log(response)
      this.dtTrigger.next();


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
