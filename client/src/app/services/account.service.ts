import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUser } from '../models/registerUser.model';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { User } from '../models/user.model';
import { LoginUser } from '../models/loginUser';
import { environment } from '../../environments/environment.development';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private readonly baseApiUrl: string = environment.apiUrl +'account/'
  private setCurrentUserSourse = new BehaviorSubject<User | null>(null);
  currentUser$ = this.setCurrentUserSourse.asObservable();

  constructor(private http: HttpClient) { }

  registerUser(userInput: RegisterUser): Observable<User | null> {
    // type NewType = User;

    return this.http.post<User>(this.baseApiUrl + 'register', userInput).pipe(
      map(userResponse => {
        if (userResponse) {
          this.setCurrentUser(userResponse) // The code is cleaner (after delete line 24)

          return userResponse;
        } //false

        return null;
      })
    );
  }

  loginUser(userInput: LoginUser): Observable<User | null> {
    return this.http.post<User>(this.baseApiUrl + 'login', userInput).pipe(
      map(userResponse => {
        if (userResponse) {
          // this.setCurrentUserSourse.next(userResponse) before add line 36
          this.setCurrentUser(userResponse) // The code is cleaner (after delete line 35)
          // localStorage.setItem('user', JSON.stringify(userResponse)) 

          return userResponse;
        } //false

        return null;
      })
    );
  }

  setCurrentUser(user: User): void {
    this.setCurrentUserSourse.next(user)
    localStorage.setItem('user', JSON.stringify(user)) // The code is cleaner (after delete line 26 and 38 )
  }

  logoutUser(): void {
    this.setCurrentUserSourse.next(null);
    localStorage.removeItem('user')
  }
}