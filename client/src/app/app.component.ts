import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./components/navbar/navbar.component";
import { json } from 'stream/consumers';
import { User } from './models/user.model';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'client';
  // constructor() {
  //   // this.getLocalStorageCurrentValues();
  // }
  private accountService = inject(AccountService)

  ngOnInit(): void {
    try { this.getLocallStorageCurrentValue(); }
    catch (error) {}
  }

  getLocallStorageCurrentValue(): void {
    const userString: string | null = localStorage.getItem('user');

    if (userString) {
      const user: User = JSON.parse(userString)

      this.accountService.setCurrentUser(user);
    }
  }

}
