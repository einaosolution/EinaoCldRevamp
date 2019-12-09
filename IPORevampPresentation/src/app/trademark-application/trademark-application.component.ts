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
import {formatDate} from '@angular/common';

import {Status} from '../Status';
import {DataStatus} from '../DataStatus';


import { map } from 'rxjs/operators';

import 'datatables.net'
import 'datatables.net-dt'
import { STATUS_CODES } from 'http';


declare var $;

@Component({
  selector: 'app-trademark-application',
  templateUrl: './trademark-application.component.html',
  styleUrls: ['./trademark-application.component.css']
})
export class TrademarkApplicationComponent implements OnInit {

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
  start_date:string =""
  end_date:string ="" ;
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
  public Status = Status;
  public DataStatus = DataStatus;
  vshow :boolean = false;
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }


  search( ) {
let userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
    .GetApplicationByUserid(userid,formatDate(this.start_date, 'yyyy-MM-dd', 'en'),formatDate(this.end_date, 'yyyy-MM-dd', 'en'))
    .then((response: any) => {

     let  table = $('#myTable').DataTable();


      table.destroy();

      console.log("Trademark Application Response")
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
  onSubmit() {
    $("#createmodel2").modal('show');

}

valuechange(een ) {




  }


  refusal() {


localStorage.setItem('Pwallet',this.pwalletid );

    $("#createmodel").modal('hide');

    this.router.navigateByUrl('/Dashboard/RefusalLetterReprint');

  }



  accept() {


    localStorage.setItem('Pwallet',this.pwalletid );

        $("#createmodel").modal('hide');

        this.router.navigateByUrl('/Dashboard/AcceptanceLetterReprint');

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

showcountry2() {


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
    .GetApplicationByUserid(userid,formatDate(this.start_date, 'yyyy-MM-dd', 'en'),formatDate(this.end_date, 'yyyy-MM-dd', 'en'))
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


  Acceptstatus(pp) {

    if ( pp.datastatus ==DataStatus.Acceptance  || pp.datastatus == DataStatus.Acceptance || pp.datastatus == DataStatus.Examiner || pp.datastatus == DataStatus.Publication  || pp.datastatus == DataStatus.Appeal || pp.datastatus == DataStatus.Recordal || pp.datastatus == DataStatus.Certificate ) {

      if (pp.status== Status.Fresh && pp.datastatus ==DataStatus.Examiner) {
        return false
      }

      if (pp.status== Status.Kiv && pp.datastatus ==DataStatus.Examiner) {
        return false
      }

      if (pp.status== Status.ReconductSearch && pp.datastatus ==DataStatus.Examiner) {
        return false
      }

      if (pp.status== Status.Refused && pp.datastatus ==DataStatus.Examiner) {
        return false
      }



      return true
    }

    else {
      return false
    }
  }


  checkstatus(pp) {
    if (pp.status ==Status.Refused && pp.datastatus == DataStatus.Examiner) {
      return true
    }

    else {
      return false
    }
  }



  showcountry(kk) {

    this.savemode = false;
    this.updatemode = true;
this.row2 = kk;
this.vshow = true;
this.pwalletid = kk.pwalletid
localStorage.setItem('pwalletid',this.pwalletid );
console.log("Response Result")
console.log(kk)

    $("#createmodel").modal('show');

    //document.getElementById("openModalButton").click();
   // this.modalRef = this.modalService.show(ref );

   var userid = localStorage.getItem('UserId');
   this.busy =   this.registerapi
.GetPreviousComment([],userid,this.pwalletid)
.then((response: any) => {

 console.log("Sector Response")
 this.row4 = response.content;
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


  acknowledment() {
    $("#createmodel").modal('hide');

    this.router.navigateByUrl('/Dashboard/Acknowledgement');
  }

  ngOnInit() {

    if (this.registerapi.checkAccess("#/Dashboard/TrademarkApplication"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }

    this.registerapi.setPage("trademark")

    this.registerapi.VChangeEvent("trademark");

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

let  userid = localStorage.getItem('UserId');



   this.busy =   this.registerapi
    .GetApplicationByUserid("00","","")
    .then((response: any) => {

      console.log("Trademark Application Response")
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
