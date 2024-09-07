import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
  MatButtonModule,
    RouterModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  allUsers: User[] | undefined;

  // constructor(private userService: UserService) { } angular 16
  userService = inject(UserService)

  showAllUsers() {
    this.userService.getAllUsers().subscribe({
      next: users => this.allUsers = users,
      error: err => console.log(err)
    })
  }
}
