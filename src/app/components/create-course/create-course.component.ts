import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Course } from '../../models/course';
import { Coord } from '../../models/coord';
import { CreateCourse } from '../../models/create-course';
import { CourseService } from '../../services/course.service';
import myGlobals = require('../../global');
import { } from "jquery";
declare var $: any;

@Component({
  selector: 'create-course',
  templateUrl: './create-course.component.html',
  styleUrls: ['./create-course.component.css']
})

export class CreateCourseComponent {
  createCourse = new CreateCourse();
  host: string;
  errors: any[];
  errorMessage: string;
  fileInput: any;
  loading: boolean = false;

  constructor(
    private courseService: CourseService,
    private router: Router) {
    this.host = myGlobals.host;
  }

  addCourse() {
    this.loading = true;
    this.addFile().then(() => {
      let fi = (<any>$('#new_course_photo'))[0];
      if(fi.files[0]){
        this.createCourse.Photo = fi.files[0].name;
        var coord = new Coord();
        coord.X = 10;
        coord.Y  = 10;

        this.createCourse.Coords = coord;
      }
      this.courseService.create(this.createCourse)
        .then(result => {
          this.loading = false;
          this.router.navigate(['']);
        })
        .catch(error => {
          this.readModelError(error);
          this.loading = false;
        });
    });
  }

  addFile() {
    let fi = (<any>$('#new_course_photo'))[0];
    if (fi.files && fi.files[0]) {
      let fileToUpload = fi.files[0];
      return this.courseService
        .uploadFile(fileToUpload)
        .toPromise();
    }
  }

  readModelError(error: any) {
    this.errors = [];
    for (var key in error) {
      for (var val in error[key]) {
        this.errors.push(error[key][val]);
      }
    }
  }
}