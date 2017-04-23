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

  constructor(
    private courseService: CourseService,
    private router: Router) {
      this.host = myGlobals.host;
     }

  getCourses(): void {
    this.courseService.getCourses().subscribe(courses => this.courses = courses.json());
  }

  ngOnInit(): void {
    this.getCourses();
  }
}