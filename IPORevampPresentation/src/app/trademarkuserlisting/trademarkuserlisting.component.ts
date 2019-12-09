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
  selector: 'app-trademarkuserlisting',
  templateUrl: './trademarkuserlisting.component.html',
  styleUrls: ['./trademarkuserlisting.component.css']
})
export class TrademarkuserlistingComponent implements OnInit {

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


  }

  onSubmit5(f) {



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




  showcountry(kk) {


  }

  ngOnInit() {

    if (this.registerapi.checkAccess("#/Dashboard/TrademarkApplication"))  {

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



   this.busy =   this.registerapi
    .GetTrademarkUser()
    .then((response: any) => {

      console.log("user Response")
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
