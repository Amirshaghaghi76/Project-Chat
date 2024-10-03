import { User } from '../../models/user.model';
import { Component, OnInit, inject } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatCommonModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { AccountService } from '../../services/account.service';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    MatToolbarModule, MatMenuModule,
    MatButtonModule, CommonModule, RouterModule,
    MatDividerModule, MatListModule, MatIconModule,
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {
  user: User | null | undefined
  
  constructor(private accountService: AccountService) {

  }
  ngOnInit(): void {
    this.accountService.currntUser$.subscribe({
      next: response => this.user = response
    })
  }

  logout() {
    // console.log('logout is user')
    this.accountService.logoutUser();
  }
}