import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Appointment } from '../models/appointment';
import { CreateAppointment } from '../models/create-appointment';
import myGlobals = require('../global');
import { CookieService } from '../services/cookie.service';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Injectable()
export class AppointmentService {

  private headers = new Headers({ 'Content-Type': 'application/json' });
  private host: string;
  constructor(
    private http: Http,
    private cookieService: CookieService,
    private accountService: AccountService,
    private router: Router) {
    this.host = myGlobals.host;
  }

  get() {
    var url = `${this.host}api/appointments/my`
    var token = "bearer " + this.accountService.getToken();
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append("Authorization", token);

    return this.http.get(url, { headers }).toPromise();
  }

  remove(id: number) {
    var url = `${this.host}api/appointments/${id}`
    var token = "bearer " + this.accountService.getToken();
    var headers = new Headers();
    headers.append("Authorization", token);

    return this.http.delete(url, { headers }).toPromise();
  }

  create(createAppointment: CreateAppointment) {
    var url = `${this.host}api/appointments`
    var token = "bearer " + this.accountService.getToken();
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append("Authorization", token);

    this.accountService.getUser().then(user => {
      createAppointment.StudentId = user.json().Id;

      this.http.post(url, JSON.stringify(createAppointment), { headers })
        .subscribe(response => {
          if (response.ok) {
            this.router.navigate([`appointments`]);
          }
        });
    })
  }
}