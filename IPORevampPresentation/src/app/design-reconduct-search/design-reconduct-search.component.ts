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
  selector: 'app-design-reconduct-search',
  templateUrl: './design-reconduct-search.component.html',
  styleUrls: ['./design-reconduct-search.component.css']
})
export class DesignReconductSearchComponent implements OnInit {

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
  appdescription:string ="" ;
  display =false
  appcomment3:string ;
  appcomment2:string ;
  Description: FormControl;
  markdescription =[{ title: "Similar Marks Exist", value: "Similar Marks Exist" },
  { title: "No Similar Marks", value: "No Similar Marks" }]
  public rows = [];
  public row2
  public row3 = [];
  public row4  = [];
  public row5  = [];
  public row50  = [];
  public row6  = [];
  uploads
  dataid


  vshow :boolean = false;
  filepath:any ;
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }

  onSubmit() {
    this.appcomment3 = ""
    $("#createmodel2").modal('show');

}

valuechange(een ) {




  }


onSubmit4() {
  this.appdescription =""
  this.display = false;
  this.onChange("")
  this.appcomment2 =""

  let test = this.fileInput.nativeElement;
  this.markdescription = []


  this.markdescription =[{ title: "Similar Marks Exist", value: "Similar Marks Exist" },
  { title: "No Similar Marks", value: "No Similar Marks" }]

  test.value = ''

  $("#createmodel3").modal('show');
}

onChange( deviceValue) {
  console.log(deviceValue);
  this.appdescription = deviceValue
}

onSubmit22(data) {


  this.display = true;

 // $('#vselect').val(data.description).change();

 // this.onChange(data.description)

  this.appcomment2 = data.patentcomment
  this.dataid = data.id


  if (data.uploadsPath1) {
    this.uploads = data.uploadsPath1
  }

 // this.appdescription ="\""+ data.description + "\"" ;
  this.appdescription =data.description  ;

 // this.appdescription ="No Similar Marks"


}
onSubmit20(f) {
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
 formData.append("fromstatus",this.Status.Fresh);
 formData.append("tostatus",this.Status.SaveMode);
 formData.append("fromDatastatus",this.DataStatus.Search);
 formData.append("toDatastatus",this.DataStatus.Search);
 formData.append("userid",userid);


 this.busy =  this.registerapi
 .SaveDesignStateAppHistory(formData)
 .then((response: any) => {

   this.submitted=false;




$("#createmodel3").modal('hide');
$("#createmodel").modal('hide');


table.destroy();

this. getallApplication2()



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

     else {

      if (this.uploads) {

      }
      else {
      Swal.fire(
        "Upload Search Result",
        '',
        'error'
      )

      return ;

      }

     }

    formData.append("pwalletid",this.pwalletid);
   formData.append("comment",this.appcomment2);
   formData.append("description",this.appdescription);
   formData.append("fromstatus",this.Status.ReconductSearch);
   formData.append("tostatus",this.Status.Fresh);
   formData.append("fromDatastatus",this.DataStatus.ReconductSearch);
   formData.append("toDatastatus",this.DataStatus.Examiner);
   formData.append("userid",userid);
   formData.append("Uploads",this.uploads);


   this.busy =  this.registerapi
   .SaveDesignFreshAppHistory(formData)
   .then((response: any) => {

     this.submitted=false;




  $("#createmodel3").modal('hide');
  $("#createmodel").modal('hide');


  table.destroy();

  this. getallApplication2()

  this.registerapi
  .SendDesignExaminerEmail(userid)
  .then((response: any) => {


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
   formData.append("fromstatus",this.Status.ReconductSearch);
   formData.append("tostatus",this.Status.Kiv);
   formData.append("fromDatastatus",this.DataStatus.ReconductSearch);
   formData.append("toDatastatus",this.DataStatus.Search);
   formData.append("userid",userid);


   this.busy =  this.registerapi
   .SaveDesignFreshAppHistory(formData)
   .then((response: any) => {

     this.submitted=false;


     this.busy =   this.registerapi
     .SendDesignUserEmail(userid,this.pwalletid,this.appcomment3)
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
    .GetDesignReconductSearchApplication(userid)
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




 this.registerapi
.GetDesignSearchState( userid ,this.pwalletid )
.then((response: any) => {

  console.log("History content")
  this.row50 = response.content;
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

    if (this.registerapi.checkAccess("#/Design/DesignReconductSearch"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }

    this.registerapi.setPage("DesignSearch")

    this.registerapi.VChangeEvent("DesignSearch");

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
    .GetDesignReconductSearchApplication(userid)
    .then((response: any) => {

      console.log("Submitted Design  Application ")
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
