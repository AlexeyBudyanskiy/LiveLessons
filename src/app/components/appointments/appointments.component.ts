import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { Appointment } from '../../models/appointment';
import { AppointmentService } from '../../services/appointment.service';
import globals = require('../../global');

@Component({
  selector: 'appointments',
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.css']
})

export class AppointmentsComponent implements OnInit {
  private appointments: Appointment[];
  private loading: boolean = false;
  host: string;

  constructor(
    private appointmentService: AppointmentService,
    private router: Router,
    private accountService: AccountService) {
    this.host = globals.host;
  }

  getAppointments(): void {
    this.loading = true;
    this.appointmentService.get().then(appointments => {
      this.appointments = appointments.json();

      this.loading = false;
    });
  }

  deleteAppointment(id: number) {
    this.loading = true;
    this.appointmentService.remove(id).then(() => this.router.navigate(['']))
      .then(() => {
        setTimeout(() => { this.router.navigate([`appointments`]) }, 500);
        this.loading = false;
      });

  }

  ngOnInit(): void {
    var token = this.accountService.getToken();

    if (!token) {
      this.router.navigate(['/signin'])
    }

    this.getAppointments();
  }
}