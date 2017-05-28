import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/toPromise';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { Course } from '../../models/course';
import { CreateMessage } from '../../models/create-message';
import { CreateAppointment } from '../../models/create-appointment';
import { CourseService } from '../../services/course.service';
import { AppointmentService } from '../../services/appointment.service';
import { MessageService } from '../../services/message.service';
import { AccountService } from '../../services/account.service';
import globals = require('../../global');

@Component({
  selector: 'course',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})

export class CourseDetailComponent implements OnInit {
  private courses: Course[];
  private course: Course;
  private id: number;
  private subscription: Subscription;
  private host: string;
  private appointment: CreateAppointment;
  private loading: boolean = false;
  private message = new CreateMessage();

  constructor(
    private accountService: AccountService,
    private courseService: CourseService,
    private appointmentService: AppointmentService,
    private messageService: MessageService,
    private route: ActivatedRoute,
    private router: Router) {
    this.subscription = route.params.subscribe(params => this.id = params['id']);
    this.host = globals.host;
  }

  getCourses(): void {
    this.courseService.getCourse(this.id)
      .subscribe(res => {
        this.course = res.json();
        this.appointment = new CreateAppointment();
        this.appointment.CourseId = this.course.Id;
      });
  }

  sendMessage() {
    this.loading = true;
    this.message.DateTime = (new Date);
    this.message.RecieverId = this.course.Teacher.Id;
    this.messageService.sendMessage(this.message)
      .then(() => {
        setTimeout(() => { this.router.navigate([`chat/${this.course.Teacher.Id}`]) }, 500);
        this.loading = false;
      });
  }

  createAppointment() {
    this.appointmentService.create(this.appointment);
  }

  ngOnInit(): void {
    var token = this.accountService.getToken();

    if (!token) {
      this.router.navigate(['/signin'])
    }

    this.getCourses();
  }
}