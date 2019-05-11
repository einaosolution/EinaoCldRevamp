import { Component, OnInit,Output,EventEmitter } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import {ApiClientService} from '../api-client.service';

import {Router} from '@angular/router';
import {
  trigger,
  state,
  style,
  animate,
  transition,
  query,
} from '@angular/animations';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: [ '../../assets/css/style.min.css']

})
export class HeaderComponent implements OnInit {
  subscription: any;
  constructor(private registerapi :ApiClientService ) {

    this.subscription = this.registerapi.getNavChangeEmitter()
    .subscribe(item => this.selectedNavItem(item));
   }

   selectedNavItem(item) {

    window.location.reload();
  }

  ngOnInit() {
  }

}
