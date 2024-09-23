import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../services/account.service';
import { take } from 'rxjs';
import { User } from '../models/user.model';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  inject(AccountService).currntUser$.pipe(take(1)).subscribe({
    next: (currentUser: User | null) => {
      if (currentUser) {
        req = req.clone({
          setHeaders: {
            Authorization: `Beare ${currentUser.token}`
          }
        });
      }
    }
  })
  return next(req);
};