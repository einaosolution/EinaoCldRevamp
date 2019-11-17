import { Component, OnInit } from '@angular/core';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';
import Swal from 'sweetalert2' ;
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html',
  styleUrls: ['./error-page.component.css']
})
export class ErrorPageComponent implements OnInit {

  constructor(private registerapi :ApiClientService ,private router: Router ,private route: ActivatedRoute ) { }

  ngOnInit() {
    const errormessage: string = this.route.snapshot.queryParamMap.get('error');

    if (errormessage) {

      Swal.fire(
        errormessage,
        '',
        'error'
      )

    }
  }

}
