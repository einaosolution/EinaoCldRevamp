import { Component, OnInit , TemplateRef,ViewChild  } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, style, animate, transition } from '@angular/animations';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-menu',
  templateUrl: './create-menu.component.html',
  styleUrls: ['./create-menu.component.css']
})
export class CreateMenuComponent implements OnInit {
  modalRef: BsModalRef;
  savemode:boolean = true;
  updatemode:boolean = false;
  userform: FormGroup;
  submitted:boolean=false;
  busy: Promise<any>;
  Code: FormControl;
  id:string;
  Description: FormControl;
  icon : FormControl;
  url : FormControl;
  parent : FormControl;
  public rows = [];

  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService ,private modalService: BsModalService) { }
  close(emp,template) {
    console.log(template)
    template.hide()
  // this.modalService.hide(template);
  // this.modalone.hide()
  }

  onSelectionChange(emp) {
  //  alert(emp.id)

  (<FormControl> this.userform.controls['parent']).setValue(emp.id);
  }

  openModal(template: TemplateRef<any>) {
    var userid = localStorage.getItem('UserId');
    this.busy =   this.registerapi
    . GetMenuParentId("0",userid)
    .then((response: any) => {

      this.modalRef = this.modalService.show(template );
      console.log("Response")
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

  }


  onSubmit() {
    this.submitted= true;
    var userid =parseInt( localStorage.getItem('UserId'));


    if (this.userform.valid) {

      this.spinner.show();

      var kk = {
        Name:this.userform.value.Code ,
        Icon:this.userform.value.icon ,
        Url:this.userform.value.url ,
        ParentId:this.userform.value.parent ,
        CreatedBy:userid



      }



     // this.router.navigate(['/Emailverification']);

     this.spinner.show()
      this.registerapi
        .SaveMenu(kk)
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
       (<FormControl> this.userform.controls['parent']).setValue("0");


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
      CountryCode:this.userform.value.Code ,
      CountryName:this.userform.value.Description ,
      CountryId:this.id ,
      CreatedBy:userid



    }



   // this.router.navigate(['/Emailverification']);


    this.registerapi
      .SaveCountry(kk)
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

  onSubmit3(kk) {
    this.savemode = false;
    this.updatemode = true;
    this.id = kk.id ;

    (<FormControl> this.userform.controls['Code']).setValue(kk.code);

    (<FormControl> this.userform.controls['Description']).setValue(kk.name);
  }
  ngOnInit() {

    this.Code = new FormControl('', [
      Validators.required
    ]);

    this.Description = new FormControl('', [

    ]);

    this.icon  = new FormControl('', [

    ]);

    this.url  = new FormControl('', [

    ]);


    this.parent  = new FormControl('', [

    ]);




    this.userform = new FormGroup({

      Code: this.Code,
      Description: this.Description ,
      icon: this.icon ,
      url: this.url ,
      parent: this.parent ,


    });

    (<FormControl> this.userform.controls['parent']).setValue("0");

   // (<FormControl> this.userform.controls['Code']).setValue("<p> Testing </>");
    this.registerapi.setPage("Security")

    this.registerapi.VChangeEvent("Security");










  }
}
