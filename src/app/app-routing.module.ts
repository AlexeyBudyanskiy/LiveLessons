import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent }   from './app.component';
import { MainComponent }   from './components/main/main.component';
import { CoursesComponent }   from './components/courses/courses.component';
import { CourseDetailComponent }   from './components/course-detail/course-detail.component';
import { AppointmentsComponent }   from './components/appointments/appointments.component';
import { SignInComponent }   from './components/sign-in/sign-in.component';
import { SignUpComponent }   from './components/sign-up/sign-up.component';

const routes: Routes = [
  { path: '', component: MainComponent },
  { path: 'courses',  component: CoursesComponent },
  { path: 'course/:id',  component: CourseDetailComponent },
  { path: 'appointments',  component: AppointmentsComponent },
  { path: 'signin',  component: SignInComponent },
  { path: 'signup',  component: SignUpComponent }
];

@NgModule({
  imports: [ BrowserModule, RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}