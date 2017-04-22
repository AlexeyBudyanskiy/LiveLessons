import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Appointment } from '../models/appointment';

@Injectable()
export class AppointmentService {
  
  private headers = new Headers({'Content-Type': 'application/json'});
  private appointmentsUrl = 'http://localhost:49558/api/appointments';  // URL to web api

  constructor(private http: Http) { }

  getAppointments(){
    var result = this.http.get(this.appointmentsUrl);
    
    return result;
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