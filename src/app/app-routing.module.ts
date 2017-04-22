import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { CoursesComponent }   from './components/courses/courses.component';
import { CourseDetailComponent }   from './components/course-detail/course-detail.component';
import { AppointmentsComponent }   from './components/appointments/appointments.component';
import { AppComponent }   from './app.component';

const routes: Routes = [
  //{ path: '', component: AppComponent },
  { path: 'courses',  component: CoursesComponent },
  { path: 'course/:id',  component: CourseDetailComponent },
  { path: 'appointments',  component: AppointmentsComponent },
];

@NgModule({
  imports: [ BrowserModule, RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}