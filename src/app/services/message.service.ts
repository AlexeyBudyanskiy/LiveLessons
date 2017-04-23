import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

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

  sendMessage(){

  }
}