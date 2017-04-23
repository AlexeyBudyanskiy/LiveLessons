import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '../../models/login';
import { Register } from '../../models/register';
import { ErrorMessage } from '../../models/errormessage';

import { AccountService } from '../../services/account.service';

@Component({
  selector: 'sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: [ '../../sign-in/sign-in.component.css' ]
})

export class SignUpComponent implements OnInit {
    register: Register;
    error: ErrorMessage;


    constructor(
    private accountService: AccountService) {
     }

    ngOnInit(): void {
    this.register = new Register();
    this.error = new ErrorMessage();
  }

  signUp(){
    this.accountService.register(this.register);
  }
 }