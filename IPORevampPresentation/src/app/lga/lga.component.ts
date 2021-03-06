import { Component, OnInit ,OnDestroy} from '@angular/core';
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
  selector: 'app-lga',
  templateUrl: './lga.component.html',
  styleUrls: ['./lga.component.css'] ,
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
export class LgaComponent implements OnDestroy, OnInit {
  dtOptions:any = {};
  dtTrigger: Subject<any> = new Subject();
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  Code: FormControl;
  Code2: FormControl;
  Description: FormControl;
  Country: FormControl;
  busy: Promise<any>;
  id:string;
  public rows = [];
  public row2 = [];
  public row3 = [];
  public row = [];
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) { }

  onSubmit() {
    this.submitted= true;
    var table = $('#myTable').DataTable();

    let obj = this.row2.find(o => o.lgaName.toUpperCase() === this.userform.value.Description.toUpperCase());


    if (obj) {

    //let obj2 = obj.find(o => o.departmentId.toUpperCase() === this.userform.value.Department.toUpperCase());
    if (obj.stateId== this.userform.value.Code) {
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
      var userid =parseInt( localStorage.getItem('UserId'));
      var kk = {
        StateId:this.userform.value.Code ,
        LGAName:this.userform.value.Description ,
        CreatedBy:userid,
        CountryId:this.userform.value.Country



      }



     // this.router.navigate(['/Emailverification']);


      this.registerapi
        .SaveLGA(kk)
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
       this.getalllga();

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

onSubmit11() {

  this.submitted= true;
  var table = $('#myTable').DataTable();
  var userid =parseInt( localStorage.getItem('UserId'));

  if (this.userform.valid) {

    this.spinner.show();

    var kk = {
      StateId:this.userform.value.Code ,
      LGAName:this.userform.value.Description ,
      LGAId:this.id ,
      CreatedBy:userid ,
      CountryId:this.userform.value.Country




    }



   // this.router.navigate(['/Emailverification']);


    this.registerapi
      .UpdateLGA(kk)
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

     this.getalllga();

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

showcountry(kk) {

  this.savemode = false;
  this.updatemode = true;
  this.id = kk.id ;

  (<FormControl> this.userform.controls['Code']).setValue(kk.stateId);
  (<FormControl> this.userform.controls['Country']).setValue(kk.countryId);

  (<FormControl> this.userform.controls['Description']).setValue(kk.lgaName);
  $("#createmodel").modal('show');
  //document.getElementById("openModalButton").click();
 // this.modalRef = this.modalService.show(ref );
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

reload() {
  window.location.reload();
}
onSubmit3(emp) {
  this.savemode = false;
  this.updatemode = true;
  this.id = emp.id ;

  (<FormControl> this.userform.controls['Code']).setValue(emp.stateId);

  (<FormControl> this.userform.controls['Description']).setValue(emp.lgaName);


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
      .DeleteLGA(emp.id,userid)
      .then((response: any) => {
        this.spinner.hide();
        console.log("Response")


        this.submitted=false;
        Swal.fire(
          'Record Deleted  Succesfully ',
          '',
          'success'
        )
        console.log(response)
      //  this.reload()
      table.destroy();
       this.getalllga();



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

  onSubmit2() {
    this.savemode = true;
    this.updatemode = false;
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getalllga() {
    var userid = localStorage.getItem('UserId');
this.busy =   this.registerapi
.GetAllLGAs(userid)
.then((response: any) => {


  this.row2 = response.content;
  this.dtTrigger.next();


  console.log(response)



})
         .catch((response: any) => {

           console.log(response)


          Swal.fire(
            response.error.message,
            '',
            'error'
          )
})
  }

  ngOnInit() {

    if (this.registerapi.checkAccess("#/Dashboard/Lga"))  {

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

    this.Country = new FormControl('', [
      Validators.required
    ]);



    this.userform = new FormGroup({

      Code: this.Code,

      Description: this.Description ,
      Country:this.Country


    });


    this.registerapi.setPage("Setup")

    this.registerapi.VChangeEvent("Setup");
    var userid = localStorage.getItem('UserId');






  this.busy =   this.registerapi
  .GetAllStates(userid)
  .then((response: any) => {


    this.rows = response.content;
    console.log(response)



  })
           .catch((response: any) => {

             console.log(response)


            Swal.fire(
              response.error.message,
              '',
              'error'
            )
})


this.registerapi
.GetCountry("true",userid)
.then((response: any) => {

 console.log("Response2")
 this.row = response.content;
 console.log(response)



})
        .catch((response: any) => {

          console.log(response)


         Swal.fire(
           response.error.message,
           '',
           'error'
         )

})

this.busy =   this.registerapi
.GetAllLGAs(userid)
.then((response: any) => {


  this.row2 = response.content;

  this.dtTrigger.next();
  console.log(response)



})
         .catch((response: any) => {

           console.log(response)


          Swal.fire(
            response.error.message,
            '',
            'error'
          )
})



}

}
