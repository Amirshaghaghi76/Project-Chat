import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  // constructor(private http: HttpClient) { }
  http = inject (HttpClient)

  getAllUsers(): Observable<User[]> {

    return this.http.get<User[]>('http://localhost:5000/api/user').pipe(
      map(users => {
        return users;
      })
    )
  }
}
