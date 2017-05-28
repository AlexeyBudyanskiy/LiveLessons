import { Course } from './course';
import { User } from './user';

export class Appointment {
    Id: number;
    Student: User;
    Course: Course;
    DateTime: Date;
}