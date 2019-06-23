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
  selector: 'app-fee-list',
  templateUrl: './fee-list.component.html',
  styleUrls: ['./fee-list.component.css']
})
export class FeeListComponent implements OnDestroy ,OnInit {
  @ViewChild('dataTable') table;
  dtOptions:any = {};
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  busy: Promise<any>;
  Code: FormControl;
  id:string;
  Description: FormControl;
  QtCode: FormControl;
  Category: FormControl;
  InitAmount: FormControl;
 TechAmount: FormControl;
 varray4 = [ {  YearName: 'Trademark', YearCode: 'tm' }  ,{  YearName: 'Patent', YearCode: 'pt' },{  YearName: 'Design', YearCode: 'ds' }  ]
  public rows = [];
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }

  onSubmit() {
    this.submitted= true;
    var table = $('#myTable').DataTable();
    var userid =parseInt( localStorage.getItem('UserId'));


    if (this.userform.valid) {

      this.spinner.show();

      var kk = {
        ItemCode:this.userform.value.Code ,
        ItemName:this.userform.value.Description ,
        QTCode:this.userform.value.QtCode ,
        Description:this.userform.value.Description ,
        init_amt:this.userform.value.InitAmount ,
        TechnologyFee:this.userform.value.TechAmount ,
        Category:this.userform.value.Category,
        FeeId:0 ,
        CreatedBy:userid


      }



     // this.router.navigate(['/Emailverification']);

     this.spinner.show()
      this.registerapi
        .SaveFeeList(kk)
        .then((response: any) => {
          this.spinner.hide();

          this.submitted=false;
          Swal.fire(
            'Record Saved Succesfully ',
            '',
            'success'
          )
       //  this.router.navigate(['/Emailverification']);

       this.userform.reset();
       $("#createmodel").modal('hide');
       table.destroy();

       this.getFeeList()

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



onSubmit4() {
  var table = $('#myTable').DataTable();
  this.submitted= true;

  var userid =parseInt( localStorage.getItem('UserId'));

  if (this.userform.valid) {

    this.spinner.show();

    var kk = {
      ItemCode:this.userform.value.Code ,
      ItemName:this.userform.value.Description ,
      QTCode:this.userform.value.QtCode ,
      Description:this.userform.value.Description ,
      init_amt:this.userform.value.InitAmount ,
      TechnologyFee:this.userform.value.TechAmount ,
      Category:this.userform.value.Category,
      FeeId:this.id ,
      CreatedBy:userid


    }




   // this.router.navigate(['/Emailverification']);


    this.registerapi
      .UpdateFeeList(kk)
      .then((response: any) => {
        this.spinner.hide();

        this.submitted=false;
        $("#createmodel").modal('hide');
        Swal.fire(
          'Record Updated Succesfully ',
          '',
          'success'
        )
     //  this.router.navigate(['/Emailverification']);
     table.destroy();
     this.getFeeList()
     this.userform.reset();


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

  onSubmit2() {
    this.savemode = true;
    this.updatemode = false;
    this.userform.reset();

  }

  onSubmit5(emp) {
    var userid =localStorage.getItem('UserId');
    var table = $('#myTable').DataTable();

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
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!',
      reverseButtons: true
    }).then((result) => {
      if (result.value) {

          this.registerapi
        .DeleteFeeList(emp.id,userid)
        .then((response: any) => {
          this.spinner.hide();
          console.log("Response")
          this.rows = response.content;
          console.log(response)
          $("#createmodel").modal('hide');
          Swal.fire(
            'Record Deleted  Succesfully ',
            '',
            'success'
          )

          table.destroy();
     this.getFeeList();

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
      } else if (
        // Read more about handling dismissals
        result.dismiss === Swal.DismissReason.cancel
      ) {

      }
    })
  }
showcountry(kk) {

  this.savemode = false;
  this.updatemode = true;
  this.id = kk.id ;



  (<FormControl> this.userform.controls['Code']).setValue(kk.itemCode);

  (<FormControl> this.userform.controls['Description']).setValue(kk.description);
  (<FormControl> this.userform.controls['QtCode']).setValue(kk.qtCode);
  (<FormControl> this.userform.controls['Category']).setValue(kk.category);
  (<FormControl> this.userform.controls['InitAmount']).setValue(kk.init_amt);
  (<FormControl> this.userform.controls['TechAmount']).setValue(kk.technologyFee);
  $("#createmodel").modal('show');
  //document.getElementById("openModalButton").click();
 // this.modalRef = this.modalService.show(ref );
}

showcountry2() {

  this.savemode = true;
  this.updatemode = false;


  (<FormControl> this.userform.controls['Code']).setValue("");

  (<FormControl> this.userform.controls['Description']).setValue("");
  (<FormControl> this.userform.controls['QtCode']).setValue("");
  (<FormControl> this.userform.controls['Category']).setValue("");
  (<FormControl> this.userform.controls['InitAmount']).setValue("");
  (<FormControl> this.userform.controls['TechAmount']).setValue("");
  $("#createmodel").modal('show');
  //document.getElementById("openModalButton").click();
 // this.modalRef = this.modalService.show(ref );
}
  onSubmit3(kk) {
    this.savemode = false;
    this.updatemode = true;
    this.id = kk.id ;

    (<FormControl> this.userform.controls['Code']).setValue(kk.code);

    (<FormControl> this.userform.controls['Description']).setValue(kk.name);
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getCountry() {

   var userid = localStorage.getItem('UserId');
   this.busy =   this.registerapi
   .GetCountry("true",userid)
   .then((response: any) => {
     this.spinner.hide();
     console.log("Response")
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

  getFeeList() {
    var userid = localStorage.getItem('UserId');



    this.busy =   this.registerapi
      . GetAllFeeLists(userid)
    .then((response: any) => {
      this.spinner.hide();
      console.log("FeeList Response")
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

  valuechange(een ) {
    //  alert(this.userform.value.email);
     // this.userform.value.email ="aa@ya.com";
     let obj = this.rows.find(o => o.itemCode.toUpperCase() === this.userform.value.Code.toUpperCase());

     if (obj) {
      (<FormControl> this.userform.controls['Code']).setValue("");

      Swal.fire(
        "itemCode Already Exist",
        '',
        'error'
      )
     }




    }


  ngOnInit() {

    if (this.registerapi.checkAccess("#/Dashboard/FeeList"))  {

    }

    else {
      alert("Access Denied ")

      this.router.navigateByUrl('/logout');
      return ;
    }


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

    this.QtCode = new FormControl('', [
      Validators.required
    ]);

    this.Category = new FormControl('', [
      Validators.required
    ]);

    this.InitAmount = new FormControl('', [
      Validators.required
    ]);

    this.TechAmount = new FormControl('', [
      Validators.required
    ]);






    this.userform = new FormGroup({

      Code: this.Code,
      Description: this.Description ,
      QtCode: this.QtCode ,
      Category: this.Category ,
      InitAmount: this.InitAmount,
      TechAmount: this.TechAmount,


    });

   // (<FormControl> this.userform.controls['Code']).setValue("<p> Testing </>");
    this.registerapi.setPage("Setup")

    this.registerapi.VChangeEvent("Setup");

   var userid = localStorage.getItem('UserId');



   this.busy =   this.registerapi
    . GetAllFeeLists(userid)
    .then((response: any) => {
      this.spinner.hide();
      console.log("FeeList Response")
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
