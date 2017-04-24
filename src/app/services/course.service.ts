import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Course } from '../models/course';
import { CreateCourse } from '../models/create-course';
import { AccountService } from '../services/account.service';

@Injectable()
export class CourseService {
  private headers = new Headers({'Content-Type': 'application/json'});
  private coursesUrl = 'http://localhost:49558/api/courses';  // URL to web api

  constructor(private http: Http, private accountService: AccountService) { }

  getCourses(){
    var result = this.http.get(this.coursesUrl);
    
    return result;
  }

  getCourse(id: number){
    const url = `${this.coursesUrl}/${id}`;

    //var result = this.http.get(url);
    var result = this.http.get(url);
    return result;
  }

      create(createCourse: CreateCourse): Promise<string> {
        const url = `${this.coursesUrl}`;
        var token = this.accountService.getToken();
        var headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append("Authorization", token);

        return this.http
            .post(url, JSON.stringify(createCourse), {headers: headers})
            .toPromise()
            .then(response => response.toString())
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);

        var errorObj : any;

        try{
            errorObj = JSON.parse(error._body);
        }
        catch(ex){
            errorObj = { 'value': error._body }
        };

        return Promise.reject(errorObj || error);
    }

  // delete(id: number): Promise<void> {
  //   const url = `${this.coursesUrl}/${id}`;
  //   return this.http.delete(url, {headers: this.headers})
  //     .toPromise()
  //     .then(() => null)
  //     .catch(this.handleError);
  // }

  // create(name: string): Promise<Course> {
  //   return this.http
  //     .post(this.coursesUrl, JSON.stringify({name: name}), {headers: this.headers})
  //     .toPromise()
  //     .then(res => res.json().data as Course)
  //     .catch(this.handleError);
  // }

  // update(hero: Course): Promise<Course> {
  //   const url = `${this.coursesUrl}/${hero.Id}`;
  //   return this.http
  //     .put(url, JSON.stringify(hero), {headers: this.headers})
  //     .toPromise()
  //     .then(() => hero)
  //     .catch(this.handleError);
  // }

  // private handleError(error: any): Promise<any> {
  //   console.error('Can`t get courses', error); // for demo purposes only
  //   return Promise.reject(error.message || error);
  // }
}