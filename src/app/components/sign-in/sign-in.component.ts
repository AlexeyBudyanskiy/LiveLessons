import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '../../models/login';

import { AccountService } from '../../services/account.service';

@Component({
  selector: 'sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})

export class SignInComponent implements OnInit {
  login: Login;
  errorModel = new Login();
  errorMessage: string;
  loading: boolean=false;

  constructor(
    private accountService: AccountService,
    private router: Router) {
  }

  ngOnInit(): void {
    this.login = new Login();
    var token = this.accountService.getToken();

    if (token) {
      this.router.navigate([''])
    }
  }

  signIn() {
    this.loading = true;
    this.accountService.login(this.login)
      .then(token => this.router.navigate(['']))
      .catch(error => {
        this.errorMessage = error.value
        this.errorModel = error;
        this.loading = false;
      });
  }
}