import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { Course } from '../models/course';
import { CreateCourse } from '../models/create-course';
import { AccountService } from '../services/account.service';
import globals = require('../global');

@Injectable()
export class CourseService {
  private headers = new Headers({ 'Content-Type': 'application/json' });
  host: string;

  constructor(private http: Http, private accountService: AccountService) {
    this.host = globals.host;
  }

  getCourses() {
    var url = `${this.host}api/courses`
    var result = this.http.get(url);

    return result;
  }

  getUserCourses() {
    var url = `${this.host}api/courses/my`
    var token = "bearer " + this.accountService.getToken();
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append("Authorization", token);

    var result = this.http.get(url, { headers });

    return result;
  }

  getCourse(id: number) {
    const url = `${this.host}api/courses/${id}`;
    var result = this.http.get(url);

    return result;
  }

  search(searchString: string) {
    var url = `${this.host}api/courses/search?coordX=10&coordY=10&searchString=${searchString}&page=0&itemsPerPage=100`
    var result = this.http.get(url);

    return result;
  }

  create(createCourse: CreateCourse): Promise<string> {
    const url = `${this.host}api/courses`
    var token = "bearer " + this.accountService.getToken();
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append("Authorization", token);

    return this.http
      .post(url, JSON.stringify(createCourse), { headers: headers })
      .toPromise()
      .then(response => response.toString())
      .catch(this.handleError);
  }

  update(updateCourse: CreateCourse): Promise<string> {
    const url = `${this.host}api/courses`
    var token = "bearer " + this.accountService.getToken();
    var headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append("Authorization", token);

    return this.http
      .put(url, JSON.stringify(updateCourse), { headers: headers })
      .toPromise()
      .then(response => response.toString())
      .catch(this.handleError);
  }

  uploadFile(fileToUpload: any) {
    var url = `${this.host}api/courses/image`;
    var token = "bearer " + this.accountService.getToken();
    var headers = new Headers({ 'Authorization': token });
    let input = new FormData();
    input.append("file", fileToUpload);

    return this.http
      .post(url, input, { headers: headers });
  }

  private handleError(error: any): Promise<any> {
    var errorObj: any;

    try {
      errorObj = JSON.parse(error._body).ModelState;
    }
    catch (ex) {
      errorObj = { 'value': error._body }
    };

    return Promise.reject(errorObj || error);
  }
}