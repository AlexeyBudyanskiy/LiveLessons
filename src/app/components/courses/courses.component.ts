import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Course } from '../../models/course';
import { CourseService } from '../../services/course.service';
import myGlobals = require('../../global');

@Component({
  selector: 'courses',
  templateUrl: './courses.component.html',
  styleUrls: [ './courses.component.css' ]
})

export class CoursesComponent implements OnInit {
  courses: Course[];
  host: string;
  loading: boolean=false;

  constructor(
    private courseService: CourseService,
    private router: Router) {
      this.host = myGlobals.host;
     }

  getCourses(): void {
    this.loading = true;

    this.courseService.getCourses().subscribe(courses => {
      this.courses = courses.json();
      this.loading = false;
    });
  }

  ngOnInit(): void {
    this.getCourses();
  }
}