import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  subscription: any;
  constructor(private registerapi :ApiClientService) {

    this.subscription = this.registerapi.getNavChangeEmitter()
    .subscribe(item => this.selectedNavItem(item));
  }

  selectedNavItem(item) {

    window.location.reload();
  }

  ngOnInit() {
  }

}
