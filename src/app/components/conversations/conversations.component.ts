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
    private currentUser: User;
    private host: string;
    private loading: boolean = false;

    constructor(
        private accountService: AccountService,
        private messageService: MessageService,
        private router: Router) {
        this.accountService.getUser().then(res => this.currentUser = res.json());
    }

    getConversationUsers(): void {
        this.loading = true;

        this.messageService.getConversationsUsers().subscribe(users => {
            this.users = users.json();
            this.filterUsers();

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

    filterUsers() {
        for (var i = 0; i < this.users.length; i++) {
            if (this.users[i].Id == this.currentUser.Id) {
                this.users.splice(i, 1);
            }
        }
    }
}