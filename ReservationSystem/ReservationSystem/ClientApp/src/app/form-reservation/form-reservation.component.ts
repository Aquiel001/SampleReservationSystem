import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';

import {IAngularMyDpOptions, IMyDateModel} from 'angular-mydatepicker';
import {AngularEditorConfig} from '@kolkov/angular-editor';
import {HeaderInfo} from '../../models/HeaderInfo';
import {Reservations} from '../../models/Reservation';
import {Page} from '../../models/PaginatedResponse';
import {Users} from '../../models/User';
import {Router, ActivatedRoute} from '@angular/router';
import {UserTypes} from '../../models/UserTypes';
import {FormBuilder, FormControl, FormGroup, FormsModule, Validators} from '@angular/forms';
import Swal from 'sweetalert2'




@Component({
  selector: 'app-form-reservation',
  templateUrl: './form-reservation.component.html',
  styleUrls: ['./form-reservation.component.scss'],
})
export class FormReservationComponent implements OnInit {
  editorConfig: AngularEditorConfig = {
    editable: true,
    height: '57vh'
  };
  header: HeaderInfo = new HeaderInfo();
  http: HttpClient;
  urlbase: string;
  selectItems = '';
  reservation: Reservations;
  types: UserTypes[];
  form: FormGroup;
  isAnUpdate: boolean;
  localPhoneNumber: string;
   // @ts-ignore
   Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
      toast.addEventListener('mouseenter', Swal.stopTimer)
      toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
  })

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, public router: ActivatedRoute, private formBuilder: FormBuilder) {
    this.reservation = new Reservations();
    http.get(baseUrl + 'reservations/' + this.router.snapshot.paramMap.get('id')).subscribe((result: Page) => {
      this.reservation = new Reservations();
      // @ts-ignore
      this.reservation = result;
      if (this.router.snapshot.paramMap.get('id') === null){

        this.isAnUpdate = false;
      }
      else{

        this.isAnUpdate = true;
      }
    }, error => console.error(error));
    http.get(baseUrl + 'usertype').subscribe((typeresult: Page) => {
      console.log(typeresult);
      // @ts-ignore
      this.types = typeresult;
    }, error => console.error(error));

    this.http = http;
    this.urlbase = baseUrl;
    this.model = null;
    this.updateHeaderInfo();
  }

  htmlContent: string;
  htmlContent1 = '';


  public model: IMyDateModel;
  myDpOptions: IAngularMyDpOptions = {
    dateRange: false,
    dateFormat: 'dd/mm/yyyy'
    // other options are here...
  };
  ngOnInit(): void {
    // this.updateHeaderInfo();
    this.reservation = new Reservations();
    this.htmlContent = '';
    this.form = this.formBuilder.group({
      signature: ['', Validators.required]
    });
    // this.updateHeaderInfo();
    console.log(this.htmlContent);
  }



  onDateChanged($event): void {
    // date selected
    console.log($event.singleDate.formatted);
    this.reservation.birthDate = $event.singleDate.formatted;
  }


  onChange2(event) {
    this.htmlContent = this.form.value.signature;
    // this.reservation.details = this.form.value.signature;
  }
  functionDelete(userName: string): void{
    if (!userName){
      this.Toast.fire({
        icon: 'error',
        html: '<p> User contact can\'t be null</p>'
      });
    }
    else {
      this.http.delete(this.urlbase + 'users/' + userName).subscribe((result: Page) => {
        console.log(result);
        this.Toast.fire({
          icon: 'success',
          html: '<p> User has been deleted</p>'
        });
      }, error => error.status === 404 ? this.Toast.fire({
        icon: 'error',
        html: '<p> User Not Found</p>'
      }) : this.Toast.fire({
        icon: 'error',
        html: '<p> Something went wrong</p>'
      }));
    }
  }
  functionSubmit(reservation?: Reservations): void
  {
    if (this.localPhoneNumber) {
        this.reservation.phoneNumber = this.localPhoneNumber;
      }
    if (this.reservation.contactName === null) {
      this.Toast.fire({
        icon: 'error',
        html: '<p> User contact can\'t be null</p>'
      });
    }
    else if (this.reservation.contactType.description === null){
      this.Toast.fire({
        icon: 'error',
        html: '<p> User type can\'t be null</p>'
      });
    }else if (!this.reservation.phoneNumber){
      this.Toast.fire({
        icon: 'error',
        html: '<p> User phone number can\'t be null</p>'
      });
    }else if(!this.reservation.birthDate) {
      this.Toast.fire({
        icon: 'error',
        html: '<p> User birthdate can\'t be null</p>'
      });
    }
    else {

      this.reservation = reservation;
      this.reservation.details = this.form.value.signature;
      if (!this.isAnUpdate) {
        this.http.put(this.urlbase + 'reservations/add', this.reservation).subscribe((result: Page) => {
        }, error => console.error(error));
        this.isAnUpdate = false;
      }
      else {

        this.http.put(this.urlbase + 'reservations/add/' + this.reservation.id, this.reservation).subscribe((result: Page) => {
        }, error => console.error(error));
      }
    }

  }

  keypress($event): void {

    this.http.get(this.urlbase + 'users/' + this.reservation.contactName).subscribe((result: Page) => {
      // @ts-ignore
      this.reservation = result;
      this.reservation.userId = this.reservation.id;
      this.isAnUpdate = false;
    }, error => console.error(error));

  }
  typeChanged($event?): void{
    this.selectItems = $event.target.value;
    this.reservation.contactType.description = $event.target.value;
    this.reservation.contactType.id = $event.target.selectedIndex;
  }

  updateHeaderInfo(): void {
    this.header.function = ['/'];
    this.header.btnTitle = 'Reservation List';
    this.header.text = 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Beatae consequuntur eaque eligendi eos est eveniet,\n' +
      '      harum in, libero nisi qui quia ratione rem suscipit ullam vel velit veritatis. Consequuntur, sed.';
    this.header.title = 'Create Reservation';
  }

}





