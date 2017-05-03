import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../models/user';
import { CreateMessage } from '../../models/create-message';
import { Message } from '../../models/message';
import { Location } from '@angular/common';
import { MessageService } from '../../services/message.service';
import { Subscription } from 'rxjs/Subscription';
import { AccountService } from '../../services/account.service';

@Component({
    selector: 'chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.css']
})

export class ChatComponent implements OnInit {
    private messages: Message[];
    private message = new CreateMessage();
    private loading: boolean = false;
    private subscription: Subscription;
    private companionId: number;

    constructor(
        private messageService: MessageService,
        private cdRef: ChangeDetectorRef,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location,
        private accountService: AccountService) {
        this.subscription = route.params.subscribe(params => this.companionId = params['id']);
    }

    getMessages(id: number): void {
        this.loading = true;

        this.messageService.getMessages(id).then(messages => {
            this.messages = messages.json();
            this.loading = false;
        });
    }

    sendMessage() {
        this.loading = true;
        this.message.DateTime = (new Date);
        this.message.RecieverId = this.companionId;
        this.messageService.sendMessage(this.message)
            .then(() => {
                var newMessage = new Message();
                newMessage.Reciever = this.messages[0].Reciever;
                newMessage.Sender = this.messages[0].Sender;
                newMessage.Text = this.message.Text;

                this.messages.unshift(newMessage);
                this.loading = false;
            });
    }

    ngOnInit(): void {
        var token = this.accountService.getToken();

        if (!token) {
            this.router.navigate(['/signin'])
        }
        this.getMessages(this.companionId);
    }
}