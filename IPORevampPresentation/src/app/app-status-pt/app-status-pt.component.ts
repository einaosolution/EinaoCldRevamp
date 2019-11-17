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
  selector: 'app-app-status-pt',
  templateUrl: './app-status-pt.component.html',
  styleUrls: ['./app-status-pt.component.css']
})
export class AppStatusPtComponent  implements OnDestroy ,OnInit {
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
  Role2 : FormControl;
  public rows = [];
  public row2 = [];
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }

  onSubmit() {
    this.submitted= true;
    var table = $('#myTable').DataTable();
    var userid =parseInt( localStorage.getItem('UserId'));

    let obj = this.rows.find(o => o.statusDescription.toUpperCase() === this.userform.value.Description.toUpperCase());


    if (obj) {

    //let obj2 = obj.find(o => o.departmentId.toUpperCase() === this.userform.value.Department.toUpperCase());
    if (obj.roleId == this.userform.value.Role2) {
     (<FormControl> this.userform.controls['Description']).setValue("");

     Swal.fire(
       "Description Already Exist",
       '',
       'error'
     )
    }

   }


    if (this.userform.valid) {

      this.spinner.show();

      var kk = {

        StatusDescription:this.userform.value.Description ,
        RoleId:this.userform.value.Role2  ,
        CreatedBy:userid


      }



     // this.router.navigate(['/Emailverification']);

     this.spinner.show()
      this.registerapi
        .SavePTApplicationStatus(kk)
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

       this.getAllTm()

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


valuechange(een ) {
  //  alert(this.userform.value.email);
   // this.userform.value.email ="aa@ya.com";




  }


onSubmit4() {
  var table = $('#myTable').DataTable();
  this.submitted= true;

  var userid =parseInt( localStorage.getItem('UserId'));

  if (this.userform.valid) {

    this.spinner.show();

    var kk = {

      StatusDescription:this.userform.value.Description ,
      RoleId:this.userform.value.Role2  ,
      Id:this.id ,
      CreatedBy:userid


    }



   // this.router.navigate(['/Emailverification']);


    this.registerapi
      .UpdatePTApplicationStatus(kk)
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
     this.getAllTm() ;

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

getrole2(roleid) {
  var vrole = "";
  //this.row2

  for (var i=0; i<this.row2.length; i++){
    if (this.row2[i].roleId ==roleid)  {
      vrole =this.row2[i].title


    }

  }


  return vrole
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
        .DeletePTApplicationStatus(emp.id,userid)
        .then((response: any) => {
          this.spinner.hide();
          console.log("Response")

          console.log(response)
          $("#createmodel").modal('hide');
          Swal.fire(
            'Record Deleted  Succesfully ',
            '',
            'success'
          )

          table.destroy();
          this.getAllTm() ;


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



  (<FormControl> this.userform.controls['Description']).setValue(kk.statusDescription);
  (<FormControl> this.userform.controls['Role2']).setValue(kk.roleId);
  $("#createmodel").modal('show');
  //document.getElementById("openModalButton").click();
 // this.modalRef = this.modalService.show(ref );
}

showcountry2() {

  this.savemode = true;
  this.updatemode = false;




  (<FormControl> this.userform.controls['Description']).setValue("");
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

  getAllTm() {
    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
    .GetAllPTApplicationStatus(userid)
    .then((response: any) => {

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

  ngOnInit() {

    if (this.registerapi.checkAccess("#/Dashboard/AppStatusPt"))  {

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

    ]);

    this.Description = new FormControl('', [
      Validators.required
    ]);

    this.Role2 = new FormControl('', [
      Validators.required
    ]);





    this.userform = new FormGroup({

      Code: this.Code,
      Description: this.Description ,
      Role2:this.Role2


    });

   // (<FormControl> this.userform.controls['Code']).setValue("<p> Testing </>");
    this.registerapi.setPage("Setup")

    this.registerapi.VChangeEvent("Setup");

   var userid = localStorage.getItem('UserId');


   this.busy =   this.registerapi
   .GetAllRoles(userid)
   .then((response: any) => {
     this.spinner.hide();
     console.log("Response")
     this.row2 = response.content;
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

   this.busy =   this.registerapi
    .GetAllPTApplicationStatus(userid)
    .then((response: any) => {

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
