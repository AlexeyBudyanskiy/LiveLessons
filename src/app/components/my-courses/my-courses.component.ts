import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { Course } from '../../models/course';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'my-courses',
  templateUrl: './my-courses.component.html',
  styleUrls: ['./my-courses.component.css']
})

export class UserCoursesComponent implements OnInit {
  courses: Course[];
  host: string;
  loading: boolean = false;

  constructor(
    private courseService: CourseService,
    private accountService: AccountService,
    private router: Router) { }

  getCourses(): void {
    this.loading = true;

    this.courseService.getUserCourses().subscribe(courses => {
      this.courses = courses.json();
      this.loading = false;
    });
  }

  ngOnInit(): void {
    var token = this.accountService.getToken();

    if (!token) {
      this.router.navigate(['/signin'])
    }

    this.getCourses();
  }

  navigate() {
    this.router.navigate(['courses/new']);
  }
}