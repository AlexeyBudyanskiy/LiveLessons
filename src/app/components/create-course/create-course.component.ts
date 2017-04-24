import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Course } from '../../models/course';
import { CreateCourse } from '../../models/create-course';
import { CourseService } from '../../services/course.service';
import myGlobals = require('../../global');

@Component({
  selector: 'create-course',
  templateUrl: './create-course.component.html',
  styleUrls: ['./create-course.component.css']
})

export class CreateCourseComponent{
  createCourse = new CreateCourse();
  host: string;
  errorModel = new CreateCourse();
  errorMessage: string;

  constructor(
    private courseService: CourseService,
    private router: Router) {
    this.host = myGlobals.host;
  }

  addCourse() {
    this.courseService.create(this.createCourse)
      .then(result => {
        //this.router.navigate(['']);
      })
      .catch(error => {
        this.errorMessage = error.value
        this.errorModel = error;
      });
  }
}