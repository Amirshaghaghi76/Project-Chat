import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUser } from '../models/registerUser.model';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { User } from '../models/user.model';
import { LoginUser } from '../models/loginUser';
import { stringify } from 'node:querystring';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private setCurrentUserSourse = new BehaviorSubject<User | null>(null);
  currntUser$ = this.setCurrentUserSourse.asObservable();

  constructor(private http: HttpClient) { }

  registerUser(userInput: RegisterUser): Observable<User> {
    // type NewType = User;

    return this.http.post<User>('http://localhost:5000/api/account/register', userInput).pipe(
      map(userResponse => {
        this.setCurrentUserSourse.next(userResponse)
        localStorage.setItem('user', JSON.stringify(userResponse))
        return userResponse

      })
    );
  }

  loginUser(userInput: LoginUser): Observable<User> {
    return this.http.post<User>('http://localhost:5000/api/account/login', userInput).pipe(
      map(userResponse => {
        this.setCurrentUserSourse.next(userResponse)
        localStorage.setItem('user', JSON.stringify(userResponse))
        return userResponse;
      })
    )
  }
}