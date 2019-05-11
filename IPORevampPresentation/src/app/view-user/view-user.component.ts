import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-view-user',
  templateUrl: './view-user.component.html',
  styleUrls: ['./view-user.component.css']
})
export class ViewUserComponent implements OnInit {
row:any[]=[]
busy: Promise<any>;
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router) { }

  ngOnInit() {

    this.busy = this.registerapi
    .GetUser()
    .then((response: any) => {
    //   alert("User Created Successfully")
     this.row = response.user2
     console.log( this.row )

    })
             .catch((response: any) => {
               console.log(response)
              alert("Error Occured")
     }
     );
  }

}
