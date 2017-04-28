import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Course } from '../../models/course';
import { CreateCourse } from '../../models/create-course';
import { CourseService } from '../../services/course.service';
import myGlobals = require('../../global');
import { } from "jquery";

@Component({
  selector: 'create-course',
  templateUrl: './create-course.component.html',
  styleUrls: ['./create-course.component.css']
})

export class CreateCourseComponent {
  createCourse = new CreateCourse();
  host: string;
  errorModel = new CreateCourse();
  errorMessage: string;
  fileInput: any;
  loading: boolean = false;

  constructor(
    private courseService: CourseService,
    private router: Router) {
    this.host = myGlobals.host;
  }

  addCourse() {
    this.addFile().then(() => {
      this.courseService.create(this.createCourse)
      .then(result => {
        //this.router.navigate(['']);
      })
      .catch(error => {
        this.errorMessage = error.value
        this.errorModel = error;
      });
    });
  }

  addFile(){
    this.loading = true;
    let fi = (<any>$('#new_course_photo'))[0];
    if (fi.files && fi.files[0]) {
      let fileToUpload = fi.files[0];
      return this.courseService
        .uploadFile(fileToUpload)
        .toPromise()
        .then(res => {
          this.loading = false;
        });
    }
  }
}