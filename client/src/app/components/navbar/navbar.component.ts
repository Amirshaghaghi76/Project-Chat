import { User } from '../../models/user.model';
import { Component, OnInit, inject } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatCommonModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { AccountService } from '../../services/account.service';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    MatToolbarModule, MatMenuModule, MatListModule,
    MatButtonModule, CommonModule, RouterModule,
    MatDividerModule, MatListModule, MatIconModule,
    MatToolbarModule, MatIconModule, MatDividerModule
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {
  user: User | null | undefined
  // user: Observable<User | null> | undefined
  constructor(private accountService: AccountService, private router: Router) {

  }
  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: response => this.user = response
    })
    // this.user$ = this.accountService.currentUser$;
  }

  logout() {
    // console.log('logout is user')
    this.accountService.logoutUser();
    this.router.navigateByUrl('account/login')
  }
}