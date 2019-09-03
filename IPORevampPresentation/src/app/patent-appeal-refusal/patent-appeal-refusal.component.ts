import { Component, OnInit,ViewChild,OnDestroy } from '@angular/core';
import {ApiClientService} from '../api-client.service';

import {Status} from '../Status';
import {DataStatus} from '../DataStatus';
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

@Component({
  selector: 'app-patent-appeal-refusal',
  templateUrl: './patent-appeal-refusal.component.html',
  styleUrls: ['./patent-appeal-refusal.component.css']
})
export class PatentAppealRefusalComponent implements OnInit {

  @ViewChild('dataTable') table;
  @ViewChild("fileInput") fileInput;
  dtOptions:any = {};
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  public Status = Status;
  public DataStatus = DataStatus;
  dataTable: any;
  savemode:boolean = true;
  updatemode:boolean = false;
  filepath="" ;
  userform: FormGroup;
  submitted:boolean=false;
  busy: Promise<any>;
  model: any = {};
  Code: FormControl;
  id:string;
  pwalletid:string ;
  appdescription:string ;
  appcomment3:string ;
  appcomment2:string ;
  appcomment :string ;
  Description: FormControl;
  public rows = [];
  public row2
  public row3 = [];
  public row4  = [];
  public row5  = [];
  public row6  = [];
  public row7


  vshow :boolean = false;
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }

  onSubmit() {
    $("#createmodel2").modal('show');

}

valuechange(een ) {




  }


onSubmit4() {
  $("#createmodel3").modal('show');
}

onChange( deviceValue) {
  console.log(deviceValue);
  this.appdescription = deviceValue
}

onSubmit2(f) {

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
   let fi = this.fileInput.nativeElement;
   if (fi.files && fi.files[0]) {
     let fileToUpload = fi.files[0];
    formData.append("FileUpload", fileToUpload);

    }

   formData.append("pwalletid",this.pwalletid);
  formData.append("comment",this.appcomment);
  formData.append("description","");
  formData.append("fromstatus",Status.Refused);
  formData.append("tostatus",Status.Registra);
  formData.append("fromDatastatus",DataStatus.Examiner);
  formData.append("toDatastatus",DataStatus.Examiner);
  formData.append("userid",userid);


   this.busy =  this.registerapi
   .SavePatentFreshAppHistory(formData)
   .then((response: any) => {

     this.submitted=false;

  //  this.router.navigate(['/Emailverification']);


  $("#createmodel2").modal('hide');
  $("#createmodel").modal('hide');
  table.destroy();

  this. getallApplication2()



  this.registerapi
.SendRegistraAppealEmail(userid,this.pwalletid)
.then((response: any) => {




})
         .catch((response: any) => {

           console.log(response)


         })

  //this.router.navigateByUrl('/Dashboard/AcceptanceLetter');

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
   formData.append("fromstatus",this.Status.Fresh);
   formData.append("tostatus",this.Status.Kiv);
   formData.append("fromDatastatus",this.DataStatus.Search);
   formData.append("toDatastatus",this.DataStatus.Search);
   formData.append("userid",userid);


   this.busy =  this.registerapi
   .SavePatentFreshAppHistory(formData)
   .then((response: any) => {

     this.submitted=false;


     this.busy =   this.registerapi
     .SendPatentUserEmail(userid,this.pwalletid,this.appcomment3)
     .then((response: any) => {

       console.log("Examiner Email")
      // this.rows = response.content;



     })
              .catch((response: any) => {
               this.spinner.hide();
                console.log(response)



   })

  //  this.router.navigate(['/Emailverification']);


  $("#createmodel2").modal('hide');
  $("#createmodel").modal('hide');
  table.destroy();

  this.getallApplication2()

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


  onSubmit3(kk) {

  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getallApplication2() {
    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
    .GetPatentRefuseApplicationByUserid(userid)
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




  showdetail(kk) {

    this.savemode = false;
    this.updatemode = true;
this.row4 = kk;
this.vshow = true;
this.pwalletid = kk.applicationId

console.log("application detail ")

var result = new Date(kk.filingDate);

var Current= new Date();
var Compare= new Date();






Current.setDate(result.getDate()+ parseInt(Status.AppealDate));

this.row7 = Current

if (Compare >  Current)  {
  Swal.fire(
    "Max 30 days  Appeal Period Exceeded",
    '',
    'error'
  )

  return;
}



//alert(result)
console.log(kk)

    //document.getElementById("openModalButton").click();
   // this.modalRef = this.modalService.show(ref );

   var userid = localStorage.getItem('UserId');

   this.busy =   this.registerapi
.GetExaminerPreviousComment( userid ,this.pwalletid )
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
   .GetAddressOfServiceById2( this.pwalletid,userid )
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


   this.busy =   this.registerapi
   .GetPatentInventorById( this.pwalletid,userid )
   .then((response: any) => {

     console.log("Inventor By Id ")
     this.row2 = response.content;
     console.log(response)


     this.busy =   this.registerapi
     .GetPatentPriority( this.pwalletid,userid )
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

  ngOnInit() {

    this.filepath = this.registerapi.GetFilepath2();
    if (this.registerapi.checkAccess("#/Patent/PatentAppealRefusal"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }

    this.registerapi.setPage("PrelimSearch")

    this.registerapi.VChangeEvent("PrelimSearch");

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

   var userid = localStorage.getItem('UserId');

   this.busy =   this.registerapi
    .GetPatentRefuseApplicationByUserid(userid)
    .then((response: any) => {

      console.log("Submitted Application ")
      this.rows = response.content;
      console.log(response.content)
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