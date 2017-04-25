import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})

export class MainComponent implements OnInit {

  constructor(
    private accountService: AccountService,
    private router: Router) {
  }

  ngOnInit(): void {
    var token = this.accountService.getToken();

    if (!token) {
      this.router.navigate(['/signin'])
    }
  }
}