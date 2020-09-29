import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {ActivatedRoute, NavigationEnd, Router} from '@angular/router';
import { Location } from '@angular/common';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  public headerLink: string;
  public isNext: boolean;
  title = 'ClientApp';
  form: FormGroup;
  currentUrl = '';
  constructor(fb: FormBuilder, public router: Router, location: Location) {
    this.form = fb.group({
      phone: ['']
    });
    console.log(router.url);

    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        const url = event.urlAfterRedirects;
        this.currentUrl = url;
      }
    });
  }

  toReservationList(): void{
    if (this.currentUrl === '/' || this.currentUrl === ''){
      this.headerLink = 'Create Reservation';
      this.isNext = false;
    }
    else {
      console.log('falso');
      this.headerLink = 'Reservation List';
      this.isNext = true;
    }
  }


  ngOnInit(): void {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        const url = event.urlAfterRedirects;
        this.currentUrl = url;
        this.toReservationList();
      }
    });
  }

}
