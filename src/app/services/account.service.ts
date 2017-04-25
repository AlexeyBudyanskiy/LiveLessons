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
import { Token } from '../models/token'

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

  login(login: Login): Promise<Token> {
    var url = `${this.host}token`
    var headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
    var requestBody = `grant_type=password&username=${login.Email}&password=${login.Password}`

    return this.http.post(url, requestBody, { headers: headers })
      .toPromise()
      .then(response => {
        var token = response.json() as Token;
        this.cookieService.setCookie('Token', token.Token, 1)

        return token;
      })
      .catch(this.handleError);
  }

  logout(): void {
    this.cookieService.deleteCookie("Token");
  }

  register(register: Register) {
    var url = `${this.host}api/account/register`
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append('method', 'post');

    var login = new Login();
    login.Email = register.Email;
    login.Password = register.Password;

    return this.http.post(url, JSON.stringify(register), { headers: headers })
      .toPromise()
      .then(response => {
        this.login(login);
        response.toString()
      })
      .catch(error => {
        var errorObj: any;

        try {
          errorObj = JSON.parse(error._body).ModelState;
        }
        catch (ex) {
          errorObj = { 'value': error._body }
        };

        return Promise.reject(errorObj || error);
      });
  }

  getUser() {
    var url = `${this.host}api/users/me`;
    var token = "bearer " + this.getToken();
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append("Authorization", token);

    var result = this.http.get(url, { headers });

    return result;
  }

  getToken() {
    var token = this.cookieService.getCookie("Token");

    return token;
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);

    var errorObj: any;

    try {
      errorObj = { 'value': JSON.parse(error._body).error_description }
    }
    catch (ex) {
      errorObj = { 'value': error._body }
    };

    return Promise.reject(errorObj || error);
  }



  // createToken(login: Login) {
  //   var request = `grant_type=password&username=${login.Email}&password=${login.Password}`
  //   var url = `${this.host}token`
  //   var headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });

  //   this.http.post(url, request, { headers })
  //     .subscribe(response => this.cookieService.setCookie('Token', response.json().access_token, 3));

  //   //this.location.replaceState('/'); // clears browser history so they can't navigate with back button
  //   this.router.navigate(['']);
  // }
}