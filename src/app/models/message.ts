import { Course } from './course';
import { User } from './user';

export class Message{
        Id: number;
        Sender:User;
        Reciever: User;
        DateTime: Date;
        Text: string;
        Course: Course;
}