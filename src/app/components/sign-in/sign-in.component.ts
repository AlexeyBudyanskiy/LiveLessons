import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '../../models/login';

import { AccountService } from '../../services/account.service';

@Component({
  selector: 'sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: [ './sign-in.component.css' ]
})

export class SignInComponent implements OnInit {
    login: Login;

    constructor(
    private accountService: AccountService) {
     }

    ngOnInit(): void {
    this.login = new Login();
  }

  signIn(){
    this.accountService.createToken(this.login);
  }
 }