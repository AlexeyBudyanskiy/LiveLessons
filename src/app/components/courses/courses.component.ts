import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Course } from '../../models/course';
import { CourseService } from '../../services/course.service';
import myGlobals = require('../../global');

@Component({
  selector: 'courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})

export class CoursesComponent implements OnInit {
  private courses: Course[];
  private host: string;
  private loading: boolean = false;
  private searchString: string;

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

  search(): void {
    this.loading = true;

    if (this.searchString) {
      this.courseService.search(this.searchString).subscribe(courses => {
        this.courses = courses.json();
        this.loading = false;
      });
    }
    else {
      this.getCourses();
    }

  }

  ngOnInit(): void {
    this.getCourses();
  }
}