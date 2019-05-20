import { Component, OnInit } from '@angular/core';
import {User2} from '../Model/User2';
import {ApiClientService} from '../api-client.service';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';

import {Router} from '@angular/router';
import {ActivatedRoute} from "@angular/router";
import Swal from 'sweetalert2' ;
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-user-security',
  templateUrl: './user-security.component.html',
  styleUrls: ['./user-security.component.css']
})
export class UserSecurityComponent implements OnInit {


User:User2[];
public rows = [];
public row2 = [];



  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router,private route: ActivatedRoute,private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.registerapi.setPage("Country")

    this.registerapi.VChangeEvent("Country");

    this.registerapi
    .GetUserDetail()
    .then((response: any) => {
      this.rows = response;

     // this.User = response.content;


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



this.registerapi
.GetMenu()
.then((response: any) => {
  this.row2 = response;
  console.log("response.content")
  console.log(response)
 // this.User = response.content;


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
