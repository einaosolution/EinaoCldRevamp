import { Component, OnInit,Input } from '@angular/core';
import {Router} from '@angular/router';
import {ApiClientService} from '../api-client.service';
import { HttpClient,HttpHeaders } from '@angular/common/http';
declare var $ :any;
@Component({
  selector: 'app-defaultdashboard3',
  templateUrl: './defaultdashboard3.component.html',
  styleUrls: ['./defaultdashboard3.component.css']
})
export class Defaultdashboard3Component implements OnInit {
  trademarkkivcount="0";
  @Input() registerapi2: any;
  @Input()    recordcount : any;
  trademarkcount="0";
  patentcount="0";
  designcount="0";
  randnumber ="0";
  serviceBase = 'http://5.77.54.44/EinaoCldRevamp2/';

  constructor(private router: Router ,private registerapi3 :ApiClientService , private http: HttpClient ) {
this.getData()
    console.log("constructor called")



   }



   getData() {



      localStorage.setItem('ip',"dashboard" );





      var token = localStorage.getItem('access_tokenexpire');

      // const  headers = new  HttpHeaders().set("Authorization", 'Bearer ' + token);
       let  userid = localStorage.getItem('UserId');

       this.registerapi3
       .GetAllApplicationCount(userid)
       .then((response: any) => {

         console.log("All Application Count")
         console.log(response)

       this.trademarkcount = response.result.trademarkcount

       this.patentcount = response.result.patentcount

       this.designcount = response.result.designcount

       //this.randnumber  =this.registerapi3.GetRandomNumber()


         this.trademarkkivcount = response.content


       })
                .catch((response: any) => {

                 console.log("error occurred  sub componet 2")
                 console.log(response)


       })


           this.registerapi3
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









  }

  ngOnInit() {







  }

}
