import { Component, inject, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AccountService } from '../../../services/account.service';
import { Router } from '@angular/router';
import { LoginUser } from '../../../models/loginUser';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatButtonModule, MatFormFieldModule, FormsModule, ReactiveFormsModule, MatInputModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private accountService = inject(AccountService)
  private fb = inject(FormBuilder)
  private router = inject(Router)

  loginFg = this.fb.group({
    emailCtrl: ['', [Validators.required, Validators.pattern(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$/)]],
    passwordCtrl: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(10)]]
  })

  get EmailCtrl(): FormControl {
    return this.loginFg.get('emailCtrl') as FormControl
  }
  get PasswordCtrl(): FormControl {
    return this.loginFg.get('passwordCtrl') as FormControl
  }

  login(): void {
    let user: LoginUser = {
      email: this.EmailCtrl.value,
      password: this.PasswordCtrl.value
    }
    this.accountService.loginUser(user).subscribe({
      next: user => {
        console.log(user)
        this.router.navigateByUrl('/')
      }
    })

  }
}
