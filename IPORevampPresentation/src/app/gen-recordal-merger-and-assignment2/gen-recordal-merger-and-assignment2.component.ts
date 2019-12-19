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
  selector: 'app-gen-recordal-merger-and-assignment2',
  templateUrl: './gen-recordal-merger-and-assignment2.component.html',
  styleUrls: ['./gen-recordal-merger-and-assignment2.component.css']
})
export class GenRecordalMergerAndAssignment2Component implements OnInit {

  @ViewChild('dataTable') table;
  @ViewChild("fileInput") fileInput;
  dtOptions:any = {};
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = true;
  updatemode:boolean = false;
  public Status = Status;
  public DataStatus = DataStatus;

  showstatus :boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  busy: Promise<any>;
  model: any = {};
  Code: FormControl;
  userid=""
  id:string;
  pwalletid:string ;
  pwalletid2:string ;
  appdescription:string ;

  Description: FormControl;
  public rows = [];
  public row2
  public row3 = [];
  public row4
  public row5 = [];
  public appcomment =""
  appcomment3="";
  appcomment2="" ;
  appcomment4 ="" ;
  filepath:any ;

  public row10 = [];
  vbatch=1;

  vshow :boolean = false;
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }

  onSubmit() {
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false,
    })

   swalWithBootstrapButtons.fire({
      title: 'Are you sure?',
      text: "",
      type: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, Approve it!',
      cancelButtonText: 'No, cancel!',
      reverseButtons: true
    }).then((result) => {
      if (result.value) {


  //  $("#createmodel2").modal('show');
  $("#createmodel").modal('hide');
  //this.router.navigateByUrl("/Dashboard/Certificate")
  var userid = localStorage.getItem('UserId');
  this.busy =   this.registerapi.UpdateMergerAssignment(this.pwalletid2,userid,Status.Approved)
.then((response: any) => {

  var table = $('#myTable').DataTable();
  table.destroy();

  this.getallApplication()


})
         .catch((response: any) => {

           console.log(response)


         })


      } else if (
        // Read more about handling dismissals
        result.dismiss === Swal.DismissReason.cancel
      ) {

      }
    })





}


onSubmit20() {
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false,
  })

 swalWithBootstrapButtons.fire({
    title: 'Are you sure?',
    text: "",
    type: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Yes, Reject it!',
    cancelButtonText: 'No, cancel!',
    reverseButtons: true
  }).then((result) => {
    if (result.value) {


//  $("#createmodel2").modal('show');
$("#createmodel").modal('hide');
//this.router.navigateByUrl("/Dashboard/Certificate")
var userid = localStorage.getItem('UserId');
this.busy =   this.registerapi.UpdateMergerAssignment(this.pwalletid2,userid,Status.Rejected)
.then((response: any) => {

var table = $('#myTable').DataTable();
table.destroy();

this.getallApplication()


})
       .catch((response: any) => {

         console.log(response)


       })


    } else if (
      // Read more about handling dismissals
      result.dismiss === Swal.DismissReason.cancel
    ) {

    }
  })





}


onSubmit2() {
  //  $("#createmodel2").modal('show');
  $("#createmodel").modal('hide');
  var userid = localStorage.getItem('UserId');

  this.busy =   this.registerapi
.UpdateApplicationById(this.pwalletid,userid)
.then((response: any) => {

  var table = $('#myTable').DataTable();
  table.destroy();

  this.getallApplication()


})
         .catch((response: any) => {

           console.log(response)


         })
 // this.router.navigateByUrl("/Dashboard/Certificate")



}


onSubmit10() {
  $("#createmodel3").modal('show');



}

onSubmit11() {
  $("#createmodel4").modal('show');



}

onSubmit12() {
  $("#createmodel5").modal('show');



}



onSubmit3() {
  let vcount = 0;

  var userid = localStorage.getItem('UserId');

  for (var i = 0; i < this.rows.length; i++) {
    if (this.rows[i].sn ==true) {
      vcount = vcount + 1;
      this.row10.push(this.rows[i].pwalletid)
    }

    //Do something
}

if ( this.row10.length > 0) {

this.busy =   this.registerapi
.UpdateBatch(userid,this.row10)
.then((response: any) => {

  var table = $('#myTable').DataTable();
  table.destroy();

  this.getallApplication()


})
         .catch((response: any) => {

           console.log(response)


         })

        }

        else {
          alert("No Row Selected")
        }


// console.log("this.rows")
 // console.log(vcount)


 // alert(vcount)

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
   formData.append("fromstatus","Fresh");
   formData.append("tostatus","ApplicantKiv");
   formData.append("fromDatastatus","Examiner");
   formData.append("toDatastatus","ApplicantKiv");
   formData.append("userid",userid);


   this.busy =  this.registerapi
   .SaveFreshAppHistory(formData)
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
     formData.append("fromstatus","Fresh");
     formData.append("tostatus","Reconduct-Search");
     formData.append("fromDatastatus","Examiner");
     formData.append("toDatastatus","Reconduct-Search");
     formData.append("userid",userid);


     this.busy =  this.registerapi
     .SaveFreshAppHistory(formData)
     .then((response: any) => {

       this.submitted=false;

    //  this.router.navigate(['/Emailverification']);


    $("#createmodel5").modal('hide');
    $("#createmodel").modal('hide');
    table.destroy();

    this. getallApplication()


    this.busy =   this.registerapi
    .SendUserEmail3(userid,this.userid,this.appcomment3)
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


  onSubmit30() {
    $("#createmodel").modal('hide');
    this.router.navigateByUrl("/Dashboard/Changeofassignmentcertificate")



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
    .GetRecordalMerger(userid,Status.Approved)
    .then((response: any) => {

      console.log("Fresh Response")
      this.rows = response.content;
      console.log(response)
      this.dtTrigger.next();


      if (this.rows.length > 0) {

        this.vbatch = this.rows[0].batCount

        }


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




  detail(kk) {
   let  userid2 = localStorage.getItem('UserId');
    this.savemode = false;
    this.updatemode = true;
this.row2 = kk;

this.pwalletid = kk.pwalletid
this.pwalletid2= kk.renewalid

localStorage.setItem('Pwallet2' ,this.pwalletid2)
localStorage.setItem('addressesrecord',JSON.stringify( kk));




this.busy =   this.registerapi
// .GetApplicationByDocumentId("7",userid)
 .GetMergerById(this.pwalletid2,userid2)
 .then((response: any) => {
console.log("view  Merger response")

console.log(response)
this.row5=[]
this.row5.push( response.content)


 })
          .catch((response: any) => {

            console.log(response)


          })

this.vshow = true;

this.userid = kk.userid

if (kk.status  =="Approved") {
  this.showstatus=true;
}

else {
  this.showstatus=false;
}

localStorage.setItem('Pwallet' ,kk.pwalletid)

let userid = localStorage.getItem('UserId');

var kk2 =["Search"]


this.busy =   this.registerapi
.GetPreviousComment(kk2,userid,this.pwalletid)
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
    $("#createmodel").modal('show');
    //document.getElementById("openModalButton").click();
   // this.modalRef = this.modalService.show(ref );
  }

  ngOnInit() {
    this.filepath = this.registerapi.GetFilepath2();
   // if (this.registerapi.checkAccess("#/Dashboard/ChangeOfNameBackend"))  {

   // }

   // else {
    //  alert("Access Denied ")

    //  this.router.navigateByUrl('/logout');
   //  return ;
   // }

    this.registerapi.setPage("TrademarkRecordal")

    this.registerapi.VChangeEvent("TrademarkRecordal");

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
    .GetRecordalMerger(userid,Status.Approved)
    .then((response: any) => {

      console.log("RecordalmergerCertificate")
      this.rows = response.content;
      console.log(response)

      this.dtTrigger.next();

      if (this.rows.length > 0) {

      this.vbatch = this.rows[0].batCount

      }


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
