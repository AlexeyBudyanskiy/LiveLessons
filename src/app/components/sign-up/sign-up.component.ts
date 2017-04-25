import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '../../models/login';
import { Register } from '../../models/register';

import { AccountService } from '../../services/account.service';

@Component({
  selector: 'sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['../../sign-in/sign-in.component.css']
})

export class SignUpComponent implements OnInit {
  register = new Register();
  errorModel: any;
  errorMessage: string;


  constructor(
    private accountService: AccountService,
    private router: Router) {
  }

  ngOnInit(): void {
    var token = this.accountService.getToken();

    if (token) {
      this.router.navigate([''])
    }
  }

  signUp() {
    this.accountService.register(this.register)
      .catch(error => {
        this.errorMessage = error.value
        this.errorModel = error;
      });
  }
}