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
  selector: 'app-designpublicationpending',
  templateUrl: './designpublicationpending.component.html',
  styleUrls: ['./designpublicationpending.component.css']
})
export class DesignpublicationpendingComponent implements OnInit {

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
  public row22
  public row33
  public row55
  appuser:any
  public row4  = [];
  public row5  = [];
  public row6  = [];
  public row7  = [];
  filepath:any ;


  vshow :boolean = false;
  vshow2 :boolean = false;
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
        "Select User",
        '',
        'error'
      )

      return;
     }

    if (f) {

    var  formData = new FormData();
    var userid = localStorage.getItem('UserId');
   let  table = $('#myTable').DataTable();


    formData.append("pwalletid",this.pwalletid);
   formData.append("comment",this.appcomment2);
   formData.append("description",this.appdescription);
   formData.append("fromstatus",this.Status.Fresh);
   formData.append("tostatus",this.Status.Delegate);
   formData.append("fromDatastatus",this.DataStatus.Acceptance);

   formData.append("toDatastatus",this.DataStatus.Examiner);

   formData.append("userid",userid);


   this.busy =  this.registerapi
   .SaveDesignFreshAppHistory(formData)
   .then((response: any) => {

     this.submitted=false;

  this.busy =   this.registerapi
  .DelegateDesignExaminerEmail(userid,this.appdescription,this.pwalletid)
  .then((response: any) => {

    console.log("Examiner Email")
  //  this.rows = response.content;



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


  $("#createmodel3").modal('hide');
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

    let table = $('#myTable').DataTable();

    formData.append("pwalletid",this.pwalletid);
   formData.append("comment",this.appcomment3);
   formData.append("description",this.appdescription);
   formData.append("fromstatus",this.Status.Fresh);
   formData.append("tostatus",this.Status.Approved);
   formData.append("fromDatastatus",this.DataStatus.Acceptance);
   formData.append("toDatastatus",this.DataStatus.Acceptance);
   formData.append("userid",userid);


   this.busy =  this.registerapi
   .SaveDesignFreshAppHistory(formData)
   .then((response: any) => {

     this.submitted=false;


     this.busy =   this.registerapi
     .SendDesignEmailForCertificate(userid,this.pwalletid,this.appcomment3)
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


  onSubmit3() {

    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
    .UpdateDesignApplicationBatch(userid)
    .then((response: any) => {

    //  console.log("Fresh Response")
     // this.rows = response.content;
     // console.log(response)

     let table = $('#myTable').DataTable();
     table.destroy();


    this.getallApplication2() ;


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

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getallApplication2() {

    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
    .GetDesignPendingPublication(userid)
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


  showdetail2(kk) {


    this.registerapi
    .GetDesignApplicationById(kk.applicationId)
    .then((response: any) => {



      this.row22 = response.content;

      this.appuser =response.content.userid

      let  transid =response.content.transactionID

      console.log("design Application ")

      console.log(response.content)







   this.busy =   this.registerapi
   .GetUserById(this.appuser)
   .then((response: any) => {

     console.log("Response Result")
     this.row33= response;
     console.log(this.row3)


     var self = this;

   })
            .catch((response: any) => {

              console.log(response)

})



this.busy =   this.registerapi
.GetDesignDateFormat(kk.applicationId)
.then((response: any) => {

  console.log("Response Result")
  this.row55= response.content;
  console.log("format date")

  console.log(this.row5)


  var self = this;

  this.vshow2 = true;
  $("#createmodel4").modal('show');

})
         .catch((response: any) => {

           console.log(response)

})










     // this.vimage  ="{{filepath}}Upload/" +this.row2.markinfo.logoPicture

     // alert("Generating")

     // this. generatePdf()



      console.log("ack")

      console.log(response)




  })
  .catch((response: any) => {

    console.log(response)


alert("error")
})

//this.pwalletid = kk.applicationId





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

  ngOnInit() {
    this.filepath = this.registerapi.GetFilepath2();
    if (this.registerapi.checkAccess("#/Design/Designpublicationpending"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }

    this.registerapi.setPage("DesignPublication")

    this.registerapi.VChangeEvent("DesignPublication");

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


   this.registerapi
   .GetUserFromDesignDepartment()
   .then((response: any) => {


console.log("user list")

this.row7 = response

console.log(response)






   })
            .catch((response: any) => {
             this.spinner.hide();
              console.log(response)




})



   this.busy =   this.registerapi
    .GetDesignPendingPublication(userid)
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
