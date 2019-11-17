import { Component, OnInit ,OnDestroy } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';
import { Subject } from 'rxjs';
declare var $;
@Component({
  selector: 'app-create-role',
  templateUrl: './create-role.component.html',
  styleUrls: ['./create-role.component.css'] ,
  animations: [
    trigger(
      'enterAnimation', [
        transition(':enter', [
          style({ transform: 'translateX(100%)', opacity: 0 }),
          animate('500ms', style({ transform: 'translateX(0)', opacity: 1 }))
        ]),
        transition(':leave', [
          style({ transform: 'translateX(0)', opacity: 1 }),
          animate('500ms', style({ transform: 'translateX(100%)', opacity: 0 }))
        ])
      ]
    )
  ]
})
export class CreateRoleComponent implements OnInit,OnDestroy {
  dtOptions:any = {};

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
  public rows = [];
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) { }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  onSubmit() {
    this.submitted= true;
    var userid =parseInt( localStorage.getItem('UserId'));

    var table = $('#myTable').DataTable();
    if (this.userform.valid) {

      this.spinner.show();

      var kk = {
        Title:this.userform.value.Code ,
        Description:this.userform.value.Description ,
        CreatedBy:userid



      }



     // this.router.navigate(['/Emailverification']);

     this.spinner.show()
      this.registerapi
        .SaveRole(kk)
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
       this.getRoles()

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

showcountry2() {

  this.savemode = true;
  this.updatemode = false;


  (<FormControl> this.userform.controls['Code']).setValue("");

  (<FormControl> this.userform.controls['Description']).setValue("");
  $("#createmodel").modal('show');
  //document.getElementById("openModalButton").click();
 // this.modalRef = this.modalService.show(ref );
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
      .DeleteRole(emp.roleId,userid)
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
        this.getRoles();

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
  this.id = kk.roleId ;

  (<FormControl> this.userform.controls['Code']).setValue(kk.title);

  (<FormControl> this.userform.controls['Description']).setValue(kk.description);
  $("#createmodel").modal('show');
  //document.getElementById("openModalButton").click();
 // this.modalRef = this.modalService.show(ref );
}

onSubmit4() {
  this.submitted= true;
  var table = $('#myTable').DataTable();
  var userid =parseInt( localStorage.getItem('UserId'));

  if (this.userform.valid) {

    this.spinner.show();

    var kk = {
      Title:this.userform.value.Code ,
      Description:this.userform.value.Description ,
      CreatedBy:userid ,
      RoleId :this.id



    }



   // this.router.navigate(['/Emailverification']);


    this.registerapi
      .EditRole(kk)
      .then((response: any) => {
        this.spinner.hide();

        this.submitted=false;
        Swal.fire(
          'Record Updated Succesfully ',
          '',
          'success'
        )
     //  this.router.navigate(['/Emailverification']);

     this.userform.reset();
     $("#createmodel").modal('hide');
     table.destroy();
     this.getRoles()


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

  getRoles() {
    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
    .GetAllRoles(userid)
    .then((response: any) => {
      this.spinner.hide();
      console.log("Response")
      this.rows = response.content;
      this.dtTrigger.next();
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

  onSubmit3(kk) {
    this.savemode = false;
    this.updatemode = true;
    this.id = kk.id ;

    (<FormControl> this.userform.controls['Code']).setValue(kk.code);

    (<FormControl> this.userform.controls['Description']).setValue(kk.name);
  }
  ngOnInit() {

    if (this.registerapi.checkAccess("#/Dashboard/Role"))  {

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



    this.userform = new FormGroup({

      Code: this.Code,
      Description: this.Description ,


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
     this.rows = response.content;
     this.dtTrigger.next();
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

}
