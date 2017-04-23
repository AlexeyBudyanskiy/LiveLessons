import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { HttpModule }    from '@angular/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent }         from './app.component';
import { MainComponent }   from './components/main/main.component';
import { CoursesComponent }   from './components/courses/courses.component';
import { CourseDetailComponent }   from './components/course-detail/course-detail.component';
import { AppointmentsComponent }   from './components/appointments/appointments.component';
import { SignInComponent }   from './components/sign-in/sign-in.component';
import { SignUpComponent }   from './components/sign-up/sign-up.component';

import { CourseService }      from './services/course.service';
import { AppointmentService }      from './services/appointment.service';
import { AccountService }      from './services/account.service';
import { CookieService }      from './services/cookie.service';


@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    MainComponent,
    CoursesComponent,
    CourseDetailComponent,
    AppointmentsComponent,
    SignInComponent,
    SignUpComponent
  ],
  providers: [
    CourseService,
    AppointmentService ,
    AccountService,
    CookieService
    ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
