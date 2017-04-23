import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import 'rxjs/add/operator/toPromise';

import { Login } from '../models/login';
import { Register } from '../models/register';
import myGlobals = require('../global');
import { CookieService } from './cookie.service';
import { ErrorMessage } from '../models/errormessage';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AccountService {
  host: string;

  constructor(
    private http: Http,
    private cookieService: CookieService,
    private router: Router,
    private location: Location) {
    this.host = myGlobals.host;
  }

  createToken(login: Login) {
    var request = `grant_type=password&username=${login.Email}&password=${login.Password}`
    var url = `${this.host}token`
    var headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });

    this.http.post(url, request, { headers })
      .subscribe(response => this.cookieService.setCookie('Token', response.json().access_token, 3));

    //this.location.replaceState('/'); // clears browser history so they can't navigate with back button
    this.router.navigate(['']);
  }

  register(register: Register, error: ErrorMessage) {
    var url = `${this.host}api/account/register`
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append('method', 'post');

    var login = new Login();
    login.Email = register.Email;
    login.Password = register.Password;

    this.http.post(url, JSON.stringify(register), { headers })
      .subscribe(
        response => {
        if (response.ok) {
          this.createToken(login);
          this.router.navigate(['']);
        }
      }, 
      err => error.Message = err.json().toString()) 
  }

  getUser(){
    var url = `${this.host}api/users/me`;
    var token = this.getToken();
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append("Authorization", token);

    var result = this.http.get(url, { headers });
    
    return result;
  }

  getToken(){
    var token = "bearer " + this.cookieService.getCookie("Token");

    return token;
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }
}