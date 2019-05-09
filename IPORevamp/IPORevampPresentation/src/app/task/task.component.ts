import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {
  userform: FormGroup;

  Password: FormControl;
  Name: FormControl;
  Status: FormControl;
  _id :string ;
  edit :boolean = false;
  busy: Promise<any>;
  public rows = [];
  public formSubmitAttempt: boolean;
  varray4 = [{ YearName: 'Pending', YearCode: 'Pending' }, { YearName: 'Finish', YearCode: 'Finish' } ]
  constructor(private fb: FormBuilder,private registerapi :ApiClientService ,private router: Router) { }

  onSubmit11() {

    var AgentsData = {



      _id: this._id,
      name: this.userform.value.Name,
      status: this.userform.value.Status,

    }


    this.busy = this.registerapi
        .UpdateTask(AgentsData)
        .then((response: any) => {
           alert("Task  Updated Successfully")

           this.edit = false;

          this.userform.reset();
          var vemail = localStorage.getItem('username');

          this.busy = this.registerapi
          .GetUserTask(vemail)
          .then((response: any) => {
             console.log("user task")
             this.rows = response.task;
             console.log(response)

          })
                   .catch((response: any) => {
                    alert("Error Occured")
           }
           );

        })
                 .catch((response: any) => {
                  alert("Error Occured")
         }
         );


  }

  onSubmit444 (vemp) {

    if (confirm('Are you sure you want proceed with this action?')) {

      this.busy = this.registerapi
      .DeletTask(vemp._id)
      .then((response: any) => {
        var vemail = localStorage.getItem('username');
        this.busy = this.registerapi
        .GetUserTask(vemail)
        .then((response: any) => {
           console.log("user task")
           this.rows = response.task;
           console.log(response)

        })
                 .catch((response: any) => {
                  alert("Error Occured")
         }
         );


      })
               .catch((response: any) => {
                alert("Error Occured")
       }
       );

  } else {

  }

  }

onSubmit4(vemp) {
  console.log("vemp")
  this._id= vemp._id ;
  (<FormControl> this.userform.controls['Name']).setValue(vemp.name);
  (<FormControl> this.userform.controls['Status']).setValue(vemp.status);
  this.edit = true;
 // console.log(vemp)

  if (confirm('Are you sure you want proceed with this action?')) {
    // Save it!
} else {
    // Do nothing!
}

}
  onSubmit() {


    this.formSubmitAttempt = true;
    var vemail = localStorage.getItem('username');

    var AgentsData = {



      email: vemail,
      name: this.userform.value.Name,
      status: this.userform.value.Status,

    }


    this.busy = this.registerapi
        .CreateTask(AgentsData)
        .then((response: any) => {
           alert("Task  Created Successfully")
          this.formSubmitAttempt = false;

          var vemail = localStorage.getItem('username');
          this.busy = this.registerapi
          .GetUserTask(vemail)
          .then((response: any) => {
             console.log("user task")
             this.rows = response.task;
             console.log(response)

          })
                   .catch((response: any) => {
                    alert("Error Occured")
           }
           );
          this.userform.reset();

        })
                 .catch((response: any) => {
                  alert("Error Occured")
         }
         );

  }

  ngOnInit() {
    var vemail = localStorage.getItem('username');
    this.busy = this.registerapi
    .GetUserTask(vemail)
    .then((response: any) => {
       console.log("user task")
       this.rows = response.task;
       console.log(response)

    })
             .catch((response: any) => {
              alert("Error Occured")
     }
     );
    var vemail = localStorage.getItem('username');
    if (vemail) {

    }
    else {
      alert("Access Denied")
      this.router.navigateByUrl('/home');
      return;
    }
    this.Name = new FormControl('', [
      Validators.required

    ]);

    this.Status = new FormControl('', [
      Validators.required

    ]);







    this.userform = new FormGroup({

      Name: this.Name,

      Status: this.Status ,


    });

  }

}
