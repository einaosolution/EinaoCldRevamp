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
  selector: 'app-user-assignment',
  templateUrl: './user-assignment.component.html',
  styleUrls: ['./user-assignment.component.css']
})
export class UserAssignmentComponent implements OnDestroy ,OnInit {
  @ViewChild('dataTable') table;
  dtOptions:any = {};
  modalRef: BsModalRef;
  dtTrigger: Subject<any> = new Subject();
  dataTable: any;
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  userform2: FormGroup;
  submitted:boolean=false;
  submitted2:boolean=false;
  busy: Promise<any>;
  Code: FormControl;
  RoleId: FormControl;
  UserId: FormControl;
  Firstname: FormControl;
  Lastname: FormControl;
  PhoneNumber: FormControl;
  Occupation: FormControl;
  Role: FormControl;
  id:string;
  Description: FormControl;
  public rows = [];
  public row2 = [];
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }

  onChange(Event) {

  }
  onSubmit() {
    this.submitted= true;
    var table = $('#myTable').DataTable();
    var userid = localStorage.getItem('UserId');
    var  formData = new FormData();

    if (this.userform.valid) {

      this.spinner.show();
      formData.append("RequestedBy",userid);
      formData.append("UserId",this.userform.value.UserId);
      formData.append("RoleId",this.userform.value.RoleId);





     // this.router.navigate(['/Emailverification']);

     this.spinner.show()
      this.registerapi
        .AssignUser(formData)
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

       this.getCountry()

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
  this.submitted2= true;
  var  formData = new FormData();


  var userid = localStorage.getItem('UserId');

  if (this.userform2.valid) {

    this.spinner.show();




    formData.append("UserId",this.id);
    formData.append("RoleId",this.userform2.value.Role);
    formData.append("Firstname",this.userform2.value.Firstname);
    formData.append("RequestedBy",userid);
    formData.append("Lastname",this.userform2.value.Lastname);
    formData.append("PhoneNumber",this.userform2.value.PhoneNumber);
    formData.append("Occupation",this.userform2.value.Occupation);



   // this.router.navigate(['/Emailverification']);


    this.registerapi
      .UpdateUserFunction(formData)
      .then((response: any) => {
        this.spinner.hide();

        this.submitted=false;
        $("#createmodel2").modal('hide');
        Swal.fire(
          'Record Updated Succesfully ',
          '',
          'success'
        )
     //  this.router.navigate(['/Emailverification']);
     table.destroy();
     this.getCountry();
     this.userform2.reset();


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
        .DeleteUser(emp.id,userid)
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
     this. getCountry();

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

  //this.savemode = false;
  //this.updatemode = true;
  this.id = kk.id ;

  (<FormControl> this.userform2.controls['Firstname']).setValue(kk.firstName);

  (<FormControl> this.userform2.controls['Lastname']).setValue(kk.lastName);
  (<FormControl> this.userform2.controls['PhoneNumber']).setValue(kk.phoneNumber);
  (<FormControl> this.userform2.controls['Occupation']).setValue(kk.Occupation);
  (<FormControl> this.userform2.controls['Role']).setValue(kk.rolesId);
  $("#createmodel2").modal('show');
  //document.getElementById("openModalButton").click();
 // this.modalRef = this.modalService.show(ref );
}

showcountry2() {




  (<FormControl> this.userform.controls['RoleId']).setValue("");

  (<FormControl> this.userform.controls['UserId']).setValue("");
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
  getrole(roleid) {
    var vrole = "";
    //this.row2

    for (var i=0; i<this.row2.length; i++){
      if (this.row2[i].roleId ==roleid)  {
        vrole =this.row2[i].title


      }

    }


    return vrole
  }
  getCountry() {

   var userid = localStorage.getItem('UserId');
   this.busy =   this.registerapi
   .GetUser2()
   .then((response: any) => {

     console.log("Response")
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

  ngOnInit() {

    if (this.registerapi.checkAccess("#/Dashboard/AssignUser"))  {

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

    this.RoleId = new FormControl('', [
      Validators.required
    ]);

    this.UserId= new FormControl('', [
      Validators.required
    ]);

    this.Firstname= new FormControl('', [
      Validators.required
    ]);

    this.Lastname= new FormControl('', [
      Validators.required
    ]);

    this.PhoneNumber= new FormControl('', [

    ]);

    this.Occupation= new FormControl('', [

    ]);
    this.Role= new FormControl('', [

    ]);


    this.Description = new FormControl('', [

    ]);



    this.userform = new FormGroup({

      RoleId: this.RoleId,
      UserId: this.UserId,


    });

    this.userform2 = new FormGroup({

      Firstname: this.Firstname,
      Lastname: this.Lastname,
      PhoneNumber: this.PhoneNumber,
      Occupation: this.Occupation,
      Role :this.Role


    });

   // (<FormControl> this.userform.controls['Code']).setValue("<p> Testing </>");
    this.registerapi.setPage("Security")

    this.registerapi.VChangeEvent("Security");

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
    .GetUser2()
    .then((response: any) => {
      this.spinner.hide();
      console.log("Response")
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