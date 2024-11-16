import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, map, take } from 'rxjs';
import { User } from '../models/user.model';
import { AccountService } from './account.service';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly baseApiUrl: string = environment.apiUrl + 'user/';
  // constructor(private http: HttpClient) { }
  http = inject(HttpClient);
  accountService = inject(AccountService);

  getAllUsers(): Observable<User[] | null> {
    let requestOptions;
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (currentUser: User | null) => {
        if (currentUser) {
          requestOptions = {
            headers: new HttpHeaders({ 'Authorization': `Bearer ${currentUser.token}` })
          }
        }
      }
    });

    return this.http.get<User[]>(this.baseApiUrl).pipe
      (
        map((users: User[]) => {
          if (users)
            return users;

          return null
        })
      )
  }

  getUserById(): Observable<User | null> {
    return this.http.get<User>(this.baseApiUrl + 'u670639c8ea38d8be7d2c49d2').pipe
      (
        map((users: User | null) => {
          if (users)
            return users;

          return null
        })
      )
  }
}
