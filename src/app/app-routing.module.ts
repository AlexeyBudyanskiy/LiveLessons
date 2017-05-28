import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { MainComponent } from './components/main/main.component';
import { CoursesComponent } from './components/courses/courses.component';
import { CourseDetailComponent } from './components/course-detail/course-detail.component';
import { AppointmentsComponent } from './components/appointments/appointments.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { CreateCourseComponent } from './components/create-course/create-course.component';
import { UpdateCourseComponent } from './components/update-course/update-course.component';
import { UserCoursesComponent } from './components/my-courses/my-courses.component';
import { ConversationsComponent } from './components/conversations/conversations.component';
import { ChatComponent } from './components/chat/chat.component';

const routes: Routes = [
  { path: '', component: MainComponent },
  { path: 'courses/new', component: CreateCourseComponent },
  { path: 'courses/update/:id', component: UpdateCourseComponent },
  { path: 'courses/my', component: UserCoursesComponent },
  { path: 'courses', component: CoursesComponent },
  { path: 'course/:id', component: CourseDetailComponent },
  { path: 'appointments', component: AppointmentsComponent },
  { path: 'signin', component: SignInComponent },
  { path: 'signup', component: SignUpComponent },
  { path: 'conversations', component: ConversationsComponent },
  { path: 'chat/:id', component: ChatComponent },
];

@NgModule({
  imports: [BrowserModule, RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }