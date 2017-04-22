import { Coord } from './coord';
import { User } from './user';

export class Course{
    Id: number;
    Name: string;
    Description: string;
    Rate: number;
    Price: number;
    Coords: Coord;
    Teacher: User;
}