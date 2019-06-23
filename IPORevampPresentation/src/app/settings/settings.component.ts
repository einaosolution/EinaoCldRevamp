import { Component, OnInit ,OnDestroy} from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';
import { idLocale } from 'ngx-bootstrap';
import { Subject } from 'rxjs';

declare var $;

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css'],
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
export class SettingsComponent implements OnDestroy, OnInit {
  dtOptions:any = {};
  dtTrigger: Subject<any> = new Subject();
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  busy: Promise<any>;
  Code: FormControl;
  id:string;
  Description: FormControl;
  Description2: FormControl;

  public rows = [];
  public row2  = [];
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) { }

  onSubmit() {
    this.submitted= true;
    var table = $('#myTable').DataTable();


    if (this.userform.valid) {

      this.spinner.show();
      var userid =parseInt( localStorage.getItem('UserId'));

      var kk = {
        SettingCode:this.userform.value.Code ,
        ItemName:this.userform.value.Description ,
        ItemValue:this.userform.value.Description2 ,
        CreatedBy:userid ,


      }



     // this.router.navigate(['/Emailverification']);

     this.spinner.show()
      this.registerapi
        .SaveSetting(kk)
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

       this.getallsetting();

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
  this.submitted= true;
  var table = $('#myTable').DataTable();
  var userid = localStorage.getItem('UserId');

  if (this.userform.valid) {

    this.spinner.show();

    var kk = {
      SettingCode:this.userform.value.Code ,
      ItemName:this.userform.value.Description ,
      ItemValue:this.userform.value.Description2 ,
      SettingId:this.id,
      CreatedBy:userid ,


    }




   // this.router.navigate(['/Emailverification']);


    this.registerapi
      .UpdateSetting(kk)
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
     this.getallsetting();

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
        .DeleteSetting(emp.id,userid)
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
        this.getallsetting();



        })
                 .catch((response: any) => {
                  this.spinner.hide();
                   console.log(response)


                  Swal.fire(
                    response.error.message,
                    '',
                    'error'
                  )

      })} else if (
        // Read more about handling dismissals
        result.dismiss === Swal.DismissReason.cancel
      ) {

      }
    })
  }

  onSubmit3(kk) {
    this.savemode = false;
    this.updatemode = true;
    this.id = kk.id ;

    (<FormControl> this.userform.controls['Code']).setValue(kk.settingCode);

    (<FormControl> this.userform.controls['Description']).setValue(kk.itemName);
    (<FormControl> this.userform.controls['Description2']).setValue(kk.itemValue);
  }

  showcountry2() {

    this.savemode = true;
    this.updatemode = false;


    (<FormControl> this.userform.controls['Code']).setValue("");

    (<FormControl> this.userform.controls['Description']).setValue("");
    (<FormControl> this.userform.controls['Description2']).setValue("");
    $("#createmodel").modal('show');
    //document.getElementById("openModalButton").click();
   // this.modalRef = this.modalService.show(ref );
  }
  showcountry(kk) {

    this.savemode = false;
    this.updatemode = true;
    this.id = kk.id ;

    (<FormControl> this.userform.controls['Code']).setValue(kk.settingCode);

    (<FormControl> this.userform.controls['Description']).setValue(kk.itemName);
    (<FormControl> this.userform.controls['Description2']).setValue(kk.itemValue);
    $("#createmodel").modal('show');
    //document.getElementById("openModalButton").click();
   // this.modalRef = this.modalService.show(ref );
  }

  valuechange(een ) {
    //  alert(this.userform.value.email);
     // this.userform.value.email ="aa@ya.com";
     let obj = this.row2.find(o => o.settingCode.toUpperCase() === this.userform.value.Code.toUpperCase());

     if (obj) {
      (<FormControl> this.userform.controls['Code']).setValue("");

      Swal.fire(
        "Code Already Exist",
        '',
        'error'
      )
     }




    }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  getallsetting() {

   var userid = localStorage.getItem('UserId');


   this.busy =   this.registerapi
   .GetAllSettings(userid)
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

    if (this.registerapi.checkAccess("#/Dashboard/Settings"))  {

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

    this.Description2 = new FormControl('', [
      Validators.required
    ]);



    this.userform = new FormGroup({

      Code: this.Code,
      Description: this.Description ,
      Description2: this.Description2 ,


    });

   // (<FormControl> this.userform.controls['Code']).setValue("<p> Testing </>");
    this.registerapi.setPage("Setup")

    this.registerapi.VChangeEvent("Setup");

   var userid = localStorage.getItem('UserId');


   this.busy =   this.registerapi
   .GetAllSettings(userid)
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
