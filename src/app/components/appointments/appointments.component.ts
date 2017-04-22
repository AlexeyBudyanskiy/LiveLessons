import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Appointment } from '../../models/appointment';
import { AppointmentService }         from '../../services/appointment.service';
import myGlobals = require('../../global');

@Component({
  selector: 'appointments',
  templateUrl: './appointments.component.html',
  styleUrls: [ './appointments.component.css' ]
})

export class AppointmentsComponent implements OnInit {
  appointments: Appointment[];
  host: string;

  constructor(
    private appointmentService: AppointmentService,
    private router: Router) {
      this.host = myGlobals.host;
     }

  getAppointments(): void {
    this.appointmentService.getAppointments().subscribe(appointments => this.appointments = appointments.json());
  }

//   add(name: string): void {
//     name = name.trim();
//     if (!name) { return; }
//     this.courseService.create(name)
//       .then(hero => {
//         this.heroes.push(hero);
//         this.selectedHero = null;
//       });
//   }

//   delete(hero: Hero): void {
//     this.courseService
//         .delete(hero.id)
//         .then(() => {
//           this.heroes = this.heroes.filter(h => h !== hero);
//           if (this.selectedHero === hero) { this.selectedHero = null; }
//         });
//   }

  ngOnInit(): void {
    this.getAppointments();
  }

//   onSelect(course: Course): void {
//     this.selectedCourses = course;
//   }

//   gotoDetail(): void {
//     this.router.navigate(['/detail', this.selectedCourse.id]);
//   }
}