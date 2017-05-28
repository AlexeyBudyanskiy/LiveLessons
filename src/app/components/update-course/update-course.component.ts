import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { Course } from '../../models/course';
import { Coord } from '../../models/coord';
import { CreateCourse } from '../../models/create-course';
import { CourseService } from '../../services/course.service';
import myGlobals = require('../../global');
import { } from "jquery";
declare var $: any;

@Component({
  selector: 'update-course',
  templateUrl: './update-course.component.html',
  styleUrls: ['./update-course.component.css']
})

export class UpdateCourseComponent implements OnInit {
  private updateCourse = new CreateCourse();
  private host: string;
  private errors: any[];
  private errorMessage: string;
  private fileInput: any;
  private loading: boolean = false;
  private subscription: Subscription;

  constructor(
    private courseService: CourseService,
    private router: Router,
    private route: ActivatedRoute) {
    this.host = myGlobals.host;
    this.subscription = route.params.subscribe(params => this.updateCourse.Id = params['id']);
  }

  ngOnInit(): void {
    this.loading = true;
    this.courseService.getCourse(this.updateCourse.Id)
      .subscribe(res => {
        this.updateCourse = res.json();
        this.loading = false;
      });
  }

  editCourse() {
    this.loading = true;
    this.addFile().then(() => {
      this.setPhotoAndCoordsData();
      this.courseService.update(this.updateCourse)
        .then(result => {
          this.loading = false;
          this.router.navigate([`course/${this.updateCourse.Id}`]);
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

    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve();
      }, 500);
    });
  }

  setPhotoAndCoordsData() {
    let fi = (<any>$('#new_course_photo'))[0];
    if (fi.files[0]) {
      this.updateCourse.Photo = fi.files[0].name;
      var coord = new Coord();
      coord.X = 10;
      coord.Y = 10;

      this.updateCourse.Coords = coord;
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