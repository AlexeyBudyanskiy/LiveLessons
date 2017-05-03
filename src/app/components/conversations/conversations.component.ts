import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { User } from '../../models/user';
import { MessageService } from '../../services/message.service';

@Component({
    selector: 'conversations',
    templateUrl: './conversations.component.html',
    styleUrls: ['./conversations.component.css']
})

export class ConversationsComponent implements OnInit {
    private users: User[];
    private host: string;
    private loading: boolean = false;

    constructor(
        private accountService: AccountService,
        private messageService: MessageService,
        private router: Router) {
    }

    getConversationUsers(): void {
        this.loading = true;

        this.messageService.getConversationsUsers().subscribe(users => {
            this.users = users.json();
            this.loading = false;
        });
    }

    ngOnInit(): void {
        var token = this.accountService.getToken();

        if (!token) {
            this.router.navigate(['/signin'])
        }

        this.getConversationUsers();
    }
}