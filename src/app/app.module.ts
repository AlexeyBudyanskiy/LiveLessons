import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { MainComponent } from './components/main/main.component';
import { CoursesComponent } from './components/courses/courses.component';
import { CourseDetailComponent } from './components/course-detail/course-detail.component';
import { AppointmentsComponent } from './components/appointments/appointments.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { CreateCourseComponent } from './components/create-course/create-course.component';
import { UserCoursesComponent } from './components/my-courses/my-courses.component';
import { ConversationsComponent } from './components/conversations/conversations.component';
import { ChatComponent } from './components/chat/chat.component';

import { CourseService } from './services/course.service';
import { AppointmentService } from './services/appointment.service';
import { AccountService } from './services/account.service';
import { CookieService } from './services/cookie.service';
import { MessageService } from './services/message.service';


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
    SignUpComponent,
    CreateCourseComponent,
    UserCoursesComponent,
    ConversationsComponent,
    ChatComponent
  ],
  providers: [
    CourseService,
    AppointmentService,
    AccountService,
    CookieService,
    MessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
