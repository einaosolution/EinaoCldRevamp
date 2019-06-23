import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {ApiClientService} from '../api-client.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(private router: Router,private registerapi :ApiClientService) { }

  ngOnInit() {
    var userid = localStorage.getItem('UserId');
    this.registerapi.LogoutUser(userid)
    .then((response: any) => {



    })
             .catch((response: any) => {


})

this.registerapi.settoken("");

localStorage.setItem('username', "");


    this.router.navigateByUrl('/login');
  }

}
