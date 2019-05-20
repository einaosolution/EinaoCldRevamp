import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-email-template',
  templateUrl: './email-template.component.html',
  styleUrls: ['./email-template.component.css']
  ,
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
export class EmailTemplateComponent implements OnInit {

  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  Code: FormControl;
  id:string;
  Description: FormControl;
  Subject: FormControl;
  Sender: FormControl;
  Template: FormControl;
  public rows = [];
  public row2 = [];
  busy: Promise<any>;

  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) { }

  onSubmit() {
    this.submitted= true;
    var userid =parseInt( localStorage.getItem('UserId'));


    if (this.userform.valid) {

      this.spinner.show();

      var kk = {
        EmailName:this.userform.value.Code ,
        EmailSubject:this.userform.value.Subject ,
        EmailSender:this.userform.value.Sender ,
        EmailBody:this.userform.value.Template ,
        CreatedBy:userid,
        EmailCode:this.userform.value.Code



      }



     // this.router.navigate(['/Emailverification']);


      this.registerapi
        .SaveEmailTemplate(kk)
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

  var userid =parseInt( localStorage.getItem('UserId'));

  if (this.userform.valid) {

    this.spinner.show();

    var kk = {
      EmailName:this.userform.value.Code ,
      EmailSubject:this.userform.value.Subject ,
      EmailSender:this.userform.value.Sender ,
      EmailBody:this.userform.value.Template ,
      EmailTemplateId:this.id ,
      CreatedBy:userid ,
      EmailCode:this.userform.value.Code



    }




   // this.router.navigate(['/Emailverification']);


    this.registerapi
      .UpdateEmailTemplate(kk)
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


      } else if (
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

    (<FormControl> this.userform.controls['Code']).setValue(kk.emailName);

    (<FormControl> this.userform.controls['Subject']).setValue(kk.emailSubject);
    (<FormControl> this.userform.controls['Sender']).setValue(kk.emailSender);
    (<FormControl> this.userform.controls['Template']).setValue(kk.emailBody);
  }
  ngOnInit() {

    this.Code = new FormControl('', [
      Validators.required
    ]);

    this.Description = new FormControl('', [

    ]);


    this.Subject = new FormControl('', [
      Validators.required
    ]);

    this.Sender = new FormControl('', [
      Validators.required
    ]);

    this.Template = new FormControl('', [
      Validators.required
    ]);



    this.userform = new FormGroup({

      Code: this.Code,
      Description: this.Description ,
      Subject: this.Subject ,
      Sender: this.Sender ,
      Template: this.Template ,


    });

   // (<FormControl> this.userform.controls['Code']).setValue("<p> Testing </>");
    this.registerapi.setPage("Country")

    this.registerapi.VChangeEvent("Country");

   var userid = localStorage.getItem('UserId');






this.busy =   this.registerapi
.GetAllEmailTemplates(userid)
.then((response: any) => {


  this.row2 = response.content;
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
