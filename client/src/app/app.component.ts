import { Component, inject, OnInit, PLATFORM_ID } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./components/navbar/navbar.component";
import { json } from 'stream/consumers';
import { User } from './models/user.model';
import { AccountService } from './services/account.service';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'client';
  platformId = inject(PLATFORM_ID);
  // constructor() {
  //   // this.getLocalStorageCurrentValues();
  // }
  private accountService = inject(AccountService)

  ngOnInit(): void {
    try { this.getLocallStorageCurrentValue(); }
    catch (error) { }
  }

  getLocallStorageCurrentValue(): void {
    let userString: string | null = null;

    if (isPlatformBrowser(this.platformId)) {
      console.log(this.platformId)
      userString = localStorage.getItem('user');
    }

    if (userString) {
      const user: User = JSON.parse(userString)

      this.accountService.setCurrentUser(user);
    }
  }

}
