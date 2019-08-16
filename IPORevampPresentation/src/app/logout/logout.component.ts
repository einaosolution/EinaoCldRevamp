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
    localStorage.removeItem('firstLoad');
    this.registerapi.VChangeEvent("Logout");
    this.registerapi.settoken("");
    localStorage.setItem('username', "");

    this.router.navigateByUrl('/login');
  }

}
