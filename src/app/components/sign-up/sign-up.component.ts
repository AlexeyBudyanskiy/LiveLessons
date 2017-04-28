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
  errors:any[];
  loading: boolean=false;

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
    this.loading = true;
    this.accountService.register(this.register).then(c=>this.loading = false)
      .catch(error => {
        this.readModelError(error);
        this.loading = false
      });
  }

  readModelError(error: any) {
    this.errors = [];
    for (var key in error) {
      for (var val in error[key]) {
        this.errors.push(error[key][val]);
      }
    }
  }
}