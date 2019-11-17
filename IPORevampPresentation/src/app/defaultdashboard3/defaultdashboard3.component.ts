import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {ApiClientService} from '../api-client.service';
@Component({
  selector: 'app-defaultdashboard3',
  templateUrl: './defaultdashboard3.component.html',
  styleUrls: ['./defaultdashboard3.component.css']
})
export class Defaultdashboard3Component implements OnInit {
  trademarkkivcount="0";
  trademarkcount="0";
  patentcount="0";
  designcount="0";
  randnumber ="0";

  constructor(private router: Router ,private registerapi :ApiClientService  ) {


   }


  trademark() {
    this.router.navigateByUrl('/Dashboard/NewApplication');
  }

  trademarkkiv() {
    this.router.navigateByUrl('/Dashboard/UserKiv');
  }

  patent() {
    this.router.navigateByUrl('/Patent/NewPatent');
  }

  design() {
    this.router.navigateByUrl('/Design/newDesign');
  }

  ngAfterViewInit() {

    console.log("child view called ")



    let  userid = localStorage.getItem('UserId');

    console.log("user id " + userid)

    this.registerapi
.GetUserKivApplicationCount(userid)
.then((response: any) => {

  console.log("kiv count")

  console.log(response)
  this.trademarkkivcount = response.content

 // this.randnumber  =this.registerapi.GetRandomNumber()


})
         .catch((response: any) => {

          console.log("error occurred  sub componet 1")
          console.log(response)


})


this.registerapi
.GetAllApplicationCount(userid)
.then((response: any) => {

  console.log("All Application Count")

this.trademarkcount = response.result.trademarkcount

this.patentcount = response.result.patentcount

this.designcount = response.result.designcount

//this.randnumber  =this.registerapi.GetRandomNumber()


//  this.trademarkkivcount = response.content


})
         .catch((response: any) => {

          console.log("error occurred  sub componet 2")
          console.log(response)


})


  }

  ngOnInit() {

  }

}
