import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { HttpModule }    from '@angular/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent }         from './app.component';
import { CoursesComponent }   from './components/courses/courses.component';
import { CourseDetailComponent }   from './components/course-detail/course-detail.component';
import { AppointmentsComponent }   from './components/appointments/appointments.component';

import { CourseService }      from './services/course.service';
import { AppointmentService }      from './services/appointment.service';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    CoursesComponent,
    CourseDetailComponent,
    AppointmentsComponent   
  ],
  providers: [
    CourseService,
    AppointmentService 
    ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
