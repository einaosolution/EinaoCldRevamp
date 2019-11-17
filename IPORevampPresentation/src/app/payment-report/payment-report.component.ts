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
import {formatDate} from '@angular/common';



import { map } from 'rxjs/operators';

import 'datatables.net'
import 'datatables.net-dt'

@Component({
  selector: 'app-payment-report',
  templateUrl: './payment-report.component.html',
  styleUrls: ['./payment-report.component.css']
})
export class PaymentReportComponent implements OnInit {

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
  start_date:string =""
  end_date:string ="" ;
  fee:string =""
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
  uploads ;
  dataid ;

  varray4 = [{ YearName: 'Trademark', YearCode: 'tm' }, {  YearName: 'Patent', YearCode: 'pt' }  , {  YearName: 'Design', YearCode: 'ds' } ]


  vshow :boolean = false;
  filepath:any ;
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }

requerytransaction(trans) {

      const swalWithBootstrapButtons = Swal.mixin({
          customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
          },
          buttonsStyling: false,
        })

            swalWithBootstrapButtons.fire({
              title: 'Are You Sure  ,Proceed to Requery Transaction  ',
              text: "",
              type: 'warning',
              showCancelButton: true,
              confirmButtonText: 'Yes, Proceed!',
              cancelButtonText: 'No, cancel!',
              reverseButtons: true
            }).then((result) => {
              if (result.value) {


                  this.busy =  this.registerapi
      .RemitaTransactionRequeryPayment(trans.rrr)
      .then((response: any) => {






}).catch((response: any) => {

                 console.log(response)




                Swal.fire(
                  response.error.message,
                  '',
                  'error'
                )

             //  this. setpaytype()

   })



                  } else if (
                // Read more about handling dismissals
                result.dismiss === Swal.DismissReason.cancel
              ) {

              }

 })

}
  onSubmit() {

    var userid = localStorage.getItem('UserId');

    this.busy =   this.registerapi
   .GetAllPayment(userid,formatDate(this.start_date, 'yyyy-MM-dd', 'en'),formatDate(this.end_date, 'yyyy-MM-dd', 'en'),this.fee)
   .then((response: any) => {

    var table = $('#myTable').DataTable();


    table.destroy();

     this.rows = response.content;
     console.log("payment response")
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

valuechange(een ) {




  }


onSubmit4() {

}

onChange( deviceValue) {
  console.log(deviceValue);
  this.appdescription = deviceValue
}

onSubmit22(data) {





}
onSubmit20(f) {


}

  onSubmit2(f) {


  }

  onSubmit5(f) {




  }


  onSubmit3(kk) {

  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getallApplication2() {

  }




  showdetail(kk) {



  }

  ngOnInit() {


    if (this.registerapi.checkAccess("#/Reports/PaymentReport"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }

    this.registerapi.setPage("ApplicationReport")

    this.registerapi.VChangeEvent("ApplicationReport");

    this.filepath = this.registerapi.GetFilepath2();

   // formatDate(this.userform.value.DateofBirth, 'yyyy-mm-dd', 'en')


  //  this.registerapi.setPage("PatentSearch")

   // this.registerapi.VChangeEvent("PatentSearch");

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




  var userid = localStorage.getItem('UserId');

  this.busy =   this.registerapi
   .GetAllPayment(userid,"","" ,"")
   .then((response: any) => {


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
