import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Course } from '../../models/course';
import { CourseService }         from '../../services/course.service';

@Component({
  selector: 'courses',
  templateUrl: './courses.component.html',
  styleUrls: [ './courses.component.css' ]
})

export class CoursesComponent implements OnInit {
  courses: Course[];

  constructor(
    private courseService: CourseService,
    private router: Router) { }

  getCourses(): void {
    this.courseService.getCourses().subscribe(courses => this.courses = courses.json());
  }

//   add(name: string): void {
//     name = name.trim();
//     if (!name) { return; }
//     this.courseService.create(name)
//       .then(hero => {
//         this.heroes.push(hero);
//         this.selectedHero = null;
//       });
//   }

//   delete(hero: Hero): void {
//     this.courseService
//         .delete(hero.id)
//         .then(() => {
//           this.heroes = this.heroes.filter(h => h !== hero);
//           if (this.selectedHero === hero) { this.selectedHero = null; }
//         });
//   }

  ngOnInit(): void {
    this.getCourses();
  }

//   onSelect(course: Course): void {
//     this.selectedCourses = course;
//   }

//   gotoDetail(): void {
//     this.router.navigate(['/detail', this.selectedCourse.id]);
//   }
}