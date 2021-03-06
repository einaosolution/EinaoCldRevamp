import { Component, OnInit,ViewChild,OnDestroy } from '@angular/core';
import {ApiClientService} from '../api-client.service';

import {Status} from '../Status';
import {DataStatus} from '../DataStatus';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";

import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';
import { Subject } from 'rxjs';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';


import { map } from 'rxjs/operators';

import 'datatables.net'
import 'datatables.net-dt'


declare var $;


import * as jspdf from 'jspdf';

import html2canvas from 'html2canvas';

import Swal from 'sweetalert2' ;

@Component({
  selector: 'app-design-registra-certificate',
  templateUrl: './design-registra-certificate.component.html',
  styleUrls: ['./design-registra-certificate.component.css']
})
export class DesignRegistraCertificateComponent implements OnInit {

  @ViewChild('dataTable') table;
  @ViewChild("fileInput") fileInput;
  dtOptions:any = {};
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  public coapplicant  = [];
  public Status = Status;
  public DataStatus = DataStatus;
  dataTable: any;
  savemode:boolean = true;
  updatemode:boolean = false;
  showrefusal:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  busy: Promise<any>;
  model: any = {};
  Code: FormControl;
  id:string;
  pwalletid:string ;
  appdescription:string ;
  appcomment3:string ;
  appcomment4:string ;
  appcomment2:string ;
  Description: FormControl;
  public rows = [];
  public row2
  public row3 = [];
  public row4  = [];
  public row5  = [];
  public row6  = [];
  public row7  = [];
  row22:any;
  row33:any;
  appuser:any;
  filepath:any ;


  vshow :boolean = false;
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }

  onSubmit() {
    $("#createmodel2").modal('show');

}

onSubmit22() {
  $("#createmodel4").modal('show');

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

senddata (dd:FormData) {

  $(document).scrollTop(0);
 this.registerapi.SendAttachmentRefusal(dd)
.then((response: any) => {


//  alert("success")


//   this.msgs = [];
//  this.msgs2 = [];
//    this.msgs2.push({severity:'success', summary:'Success', detail:'Registered  Successfully,Confirmation email sent to you email '});

   // this.router.navigateByUrl('/');
})
         .catch((response: any) => {
console.log(response)
var  ppr = response.json() ;

$(document).scrollTop(0);
//  alert("error")
 }
 );
}

onSubmit8(){


  // var doc = new jspdf('p', 'mm', "a4");
   var data2 = new FormData();

  // alert( pfile +  " 1")
  var userid =localStorage.getItem('UserId');

  data2.append('userid' ,this.appuser);
  var d = new Date()
  data2.append('message' ,"Refusal Letter  Generated on " + d);
   var AgentsData = {

     email: this.appuser


 };

 //console.log(AgentsData)

 data2.append("RegisterBindingModel", JSON.stringify(AgentsData));
 var self = this;



   html2canvas(document.getElementById('report')).then(function(canvas) {
    // alert(self)

    const context = canvas.getContext('2d');
    context.scale(2, 2);
    context['dpi'] = 144;
    context['imageSmoothingEnabled'] = false;
    context['mozImageSmoothingEnabled'] = false;
    context['oImageSmoothingEnabled'] = false;
    context['webkitImageSmoothingEnabled'] = false;
    context['msImageSmoothingEnabled'] = false;

    var doc = new jspdf('p', 'pt', [canvas.width, canvas.height]);
    // var img = canvas.toDataURL("image/png");
    var img = canvas.toDataURL("image/png", 1.0);

  //  var doc = new jsPDF();


  //  var doc = new jsPDF('p', 'mm', "a4");

 // doc.setFont("courier");

  var width = doc.internal.pageSize.width;
  var height = doc.internal.pageSize.height;



 // doc.addImage(img, 'JPEG', 0, 0, width, height);
 doc.addImage(img, 'JPEG', 0, 0, width,  canvas.height);



   var pdf = doc.output('blob');
   console.log(pdf)


 data2.append('FileUpload' , pdf);

 //formData.append("FileUpload", fileToUpload);

 self.senddata(data2)

 self.showrefusal =false;



 });
 }

sendrefusal(pwalletid)  {

  this.registerapi
  .GetDesignApplicationById(pwalletid)
  .then((response: any) => {

console.log("design data")



    this.row22 = response.content;

    console.log(response.content)

   this.appuser =response.content.userid



 this.busy =   this.registerapi
 .GetUserById(this.appuser)
 .then((response: any) => {

   console.log("Response Result")
   this.row33= response;
   console.log(this.row33)


   var self = this;

   this.showrefusal = true;

   $( document ).ready(function() {

    self.onSubmit8();

    });







 })
          .catch((response: any) => {

            console.log(response)

})




    this.vshow = true;
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

  onSubmit55(f) {
    this.submitted = true;







   if (!this.appcomment4) {

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
   formData.append("comment",this.appcomment4);
   formData.append("description",this.appdescription);
   formData.append("fromstatus",this.Status.Fresh);
   formData.append("tostatus",this.Status.Refused);
   formData.append("fromDatastatus",this.DataStatus.Acceptance);
   formData.append("toDatastatus",this.DataStatus.Examiner);
   formData.append("userid",userid);


   this.busy =  this.registerapi
   .SaveDesignFreshAppHistory(formData)
   .then((response: any) => {

     this.submitted=false;
     this.sendrefusal(this.pwalletid)



  //  this.router.navigate(['/Emailverification']);

   this.showrefusal =false;
  $("#createmodel4").modal('hide');
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


  onSubmit3(kk) {

  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getallApplication2() {

    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
    .GetDesignRegistraFreshapplication(userid)
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

//this.sendrefusal(this.pwalletid)

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




    if (this.registerapi.checkAccess("#/Design/DesignCertificatePayment"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }

    this.registerapi.setPage("RegistraDesign")

    this.registerapi.VChangeEvent("RegistraDesign");

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
    .GetDesignRegistraFreshapplication(userid)
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
