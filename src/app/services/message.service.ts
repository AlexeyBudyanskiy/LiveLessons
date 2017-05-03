import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Message } from '../models/message';
import { CreateMessage } from '../models/create-message';
import myGlobals = require('../global');
import { AccountService } from './account.service';
import { ErrorMessage } from '../models/errormessage';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class MessageService {
  host: string;

  constructor(
    private http: Http,
    private accountService: AccountService) {
    this.host = myGlobals.host;
  }

  sendMessage(message: CreateMessage) {
    var url = `${this.host}api/messages`
    var token = "bearer " + this.accountService.getToken();
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append('method', 'post');
    headers.append("Authorization", token);

    return this.accountService.getUser()
      .then(user => {
        message.SenderId = user.json().Id;
      }).then(() => {
        this.http
          .post(url, JSON.stringify(message), { headers: headers })
          .toPromise();
      });
  }

  getMessages(id: number) {
    var url = `${this.host}api/messages/user/${id}`;
    var token = "bearer " + this.accountService.getToken();
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append("Authorization", token);

    return this.http.get(url, { headers }).toPromise();
  }
  getConversationsUsers() {
    var url = `${this.host}api/messages/users`;
    var token = "bearer " + this.accountService.getToken();
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append("Authorization", token);

    var result = this.http.get(url, { headers });

    return result;
  }
}