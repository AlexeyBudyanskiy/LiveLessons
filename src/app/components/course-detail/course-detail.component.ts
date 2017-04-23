import 'rxjs/add/operator/switchMap';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Params } from '@angular/router';
import { Location } from '@angular/common';
import {Subscription} from 'rxjs/Subscription';

import { Course } from '../../models/course';
import { CreateAppointment } from '../../models/create-appointment';
import { CourseService } from '../../services/course.service';
import { AppointmentService } from '../../services/appointment.service';

import 'rxjs/add/operator/toPromise';
import myGlobals = require('../../global');

@Component({
  selector: 'course',
  templateUrl: './course-detail.component.html',
  styleUrls: [ './course-detail.component.css' ]
})

export class CourseDetailComponent implements OnInit {
  courses: Course[];
  public course: Course;
  private id: number;
  private subscription: Subscription;
  host: string;
  appointment: CreateAppointment;

  constructor(
    private courseService: CourseService,
    private appointmentService: AppointmentService,
    private route: ActivatedRoute,
    private location: Location) { 
      this.subscription = route.params.subscribe(params=>this.id=params['id']);
      this.host = myGlobals.host;
    }

  getCourses(): void {
    this.courseService.getCourse(this.id)
    .subscribe(res => {
      this.course = res.json();
      this.appointment = new CreateAppointment();
      this.appointment.CourseId = this.course.Id;
    });
  }

  createAppointment(){
    this.appointmentService.create(this.appointment);
  }

  ngOnInit(): void {
    this.getCourses();
    //this.course = this.courses[0];
    //this.courseService.getCourse(this.id).subscribe(course => this.course = course.json().data[0]);
    //console.log(this.course);
  }

//   onSelect(course: Course): void {
//     this.selectedCourses = course;
//   }

//   gotoDetail(): void {
//     this.router.navigate(['/detail', this.selectedCourse.id]);
//   }
}