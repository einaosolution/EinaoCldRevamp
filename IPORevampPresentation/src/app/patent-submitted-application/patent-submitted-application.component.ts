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

@Component({
  selector: 'app-patent-submitted-application',
  templateUrl: './patent-submitted-application.component.html',
  styleUrls: ['./patent-submitted-application.component.css']
})
export class PatentSubmittedApplicationComponent implements OnInit {

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
  model: any = {};
  Code: FormControl;
  id:string;
  pwalletid:string ;
  appdescription:string ;
  appcomment3:string ;
  appcomment2:string ;
  Description: FormControl;
  public rows = [];
  public row2
  public row3 = [];
  public row4  = [];
  public row5  = [];
  public row6  = [];


  filepath:any ;


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
    console.log(this.model)



    if (!this.appcomment2  ) {

      Swal.fire(
        "Comment   Required",
        '',
        'error'
      )

      return;
     }


     if (!this.appdescription) {

      Swal.fire(
        " Mark Description  Required",
        '',
        'error'
      )

      return;
     }

    if (f) {

    var  formData = new FormData();
    var userid = localStorage.getItem('UserId');
    var table = $('#myTable').DataTable();
    let fi = this.fileInput.nativeElement;
    if (fi.files && fi.files[0]) {
      let fileToUpload = fi.files[0];
     formData.append("FileUpload", fileToUpload);

     }

    formData.append("pwalletid",this.pwalletid);
   formData.append("comment",this.appcomment2);
   formData.append("description",this.appdescription);
   formData.append("fromstatus","Fresh");
   formData.append("tostatus","Fresh");
   formData.append("fromDatastatus","Search");
   formData.append("toDatastatus","Examiner");
   formData.append("userid",userid);


   this.busy =  this.registerapi
   .SaveFreshAppHistory(formData)
   .then((response: any) => {

     this.submitted=false;

  //  this.router.navigate(['/Emailverification']);


  $("#createmodel3").modal('hide');
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

    else {
     // alert("input not valid")

      Swal.fire(
        "input not valid",
        '',
        'error'
      )
    }

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


  onSubmit3(kk) {

  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getallApplication() {
    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
    .GetApplicationByUserid(userid,"","")
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

    //document.getElementById("openModalButton").click();
   // this.modalRef = this.modalService.show(ref );

   var userid = localStorage.getItem('UserId');

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
    if (this.registerapi.checkAccess("#/Patent/SubmittedApplication"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }

    this.registerapi.setPage("PatentSearch")

    this.registerapi.VChangeEvent("PatentSearch");

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
    .GetPatentSubmittedApplication( )
    .then((response: any) => {

      console.log("Submitted Application ")
      this.rows = response;
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
