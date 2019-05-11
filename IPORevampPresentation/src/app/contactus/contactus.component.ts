import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FormGroup, FormControl } from '@angular/forms';
import {ApiClientService} from '../api-client.service';
import {Router} from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser'

@Component({
  selector: 'app-contactus',
  templateUrl: './contactus.component.html',
  styleUrls: ['./contactus.component.css']
})
export class ContactusComponent implements OnInit {

  vdescription :any;
  vshow:boolean = false;
  vurl :any;
  busy: Promise<any>;
  constructor(private registerapi :ApiClientService ,private router: Router ,public sanitizer: DomSanitizer) { }

  ngOnInit() {

    this.busy =     this.registerapi
    .GetMap()
    .then((response: any) => {
console.log("response")
console.log(response)

var display_address =response.firstResult.location.display_address[0] + response.firstResult.location.display_address[1] +  response.firstResult.location.display_address[2] ;
this.vdescription =this.sanitizer.bypassSecurityTrustResourceUrl( display_address);
this.vshow = true;

//this.vurl ="https://maps.google.com/maps?q=" + display_address + "&t=&z=13&ie=UTF8&iwloc=&output=embed"
//this.vurl =this.sanitizer.bypassSecurityTrustResourceUrl("https://maps.google.com/maps?q= 1000 Chastain Rd Ste 2901 Kennesaw, GA 30144 &t=&z=13&ie=UTF8&iwloc=&output=embed")

this.vurl =this.sanitizer.bypassSecurityTrustResourceUrl("https://maps.google.com/maps?q="+ display_address+ "&t=&z=13&ie=UTF8&iwloc=&output=embed") ;



    })
             .catch((response: any) => {
              alert("Error Occured")
              console.log(response)
     }
     );
  }

}
