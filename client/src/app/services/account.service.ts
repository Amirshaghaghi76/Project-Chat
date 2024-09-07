import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUser } from '../models/registerUser.model';
import { map, Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  registerUser(userInput: RegisterUser): Observable<User> {
    return this.http.post<User>('http://localhost:5000/api/account/register', userInput).pipe(
      map(user => user)
    );
  }
}
