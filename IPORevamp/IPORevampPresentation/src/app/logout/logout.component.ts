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

this.registerapi.settoken("");




    this.router.navigateByUrl('/home');
  }

}
