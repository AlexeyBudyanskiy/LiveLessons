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

  getAppointments() {
    var url = `${this.host}api/appointments`
    var result = this.http.get(url);

    return result;
  }

  create(createAppointment: CreateAppointment) {
    var url = `${this.host}api/appointments`
    var token = this.accountService.getToken();
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append("Authorization", token);

    this.accountService.getUser().subscribe(user => {
      createAppointment.StudentId = user.json().Id;

      this.http.post(url, JSON.stringify(createAppointment), { headers })
        .subscribe(response => {
          if (response.ok) {
            this.router.navigate([`appointments`]);
          }
        });
    })
  }

  // getCourse(id: number): Promise<Course> {
  //   const url = `${this.appointmentsUrl}/${id}`;
  //   return this.http.get(url)
  //     .toPromise()
  //     .then(response => response.json().data as Course)
  //     .catch(this.handleError);
  // }

  // delete(id: number): Promise<void> {
  //   const url = `${this.appointmentsUrl}/${id}`;
  //   return this.http.delete(url, {headers: this.headers})
  //     .toPromise()
  //     .then(() => null)
  //     .catch(this.handleError);
  // }

  // create(name: string): Promise<Course> {
  //   return this.http
  //     .post(this.appointmentsUrl, JSON.stringify({name: name}), {headers: this.headers})
  //     .toPromise()
  //     .then(res => res.json().data as Course)
  //     .catch(this.handleError);
  // }

  // update(hero: Course): Promise<Course> {
  //   const url = `${this.appointmentsUrl}/${hero.Id}`;
  //   return this.http
  //     .put(url, JSON.stringify(hero), {headers: this.headers})
  //     .toPromise()
  //     .then(() => hero)
  //     .catch(this.handleError);
  // }

  // private handleError(error: any): Promise<any> {
  //   console.error('Can`t get courses', error); // for demo purposes only
  //   return Promise.reject(error.message || error);
  // }
}