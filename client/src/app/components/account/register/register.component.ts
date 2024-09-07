import { Component, inject } from '@angular/core';
import { AccountService } from '../../../services/account.service';
import { RegisterUser } from '../../../models/registerUser.model';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Router } from '@angular/router';



@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  private accountService = inject(AccountService)
  private fb = inject(FormBuilder)
  private router= inject(Router)

  registerFg = this.fb.group({
    nameCtrl: ['', [Validators.required,]],
    emailCtrl: ['', [Validators.required, Validators.pattern(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$/)]],
    passwordCtrl: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(10)]],
    confrimPasswordCtrl: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(10)]],

  })


  get NameCtrl(): FormControl {
    return this.registerFg.get('nameCtrl') as FormControl
  }
  get EmailCtrl(): FormControl {
    return this.registerFg.get('emailCtrl') as FormControl
  }
  get PasswordCtrl(): FormControl {
    return this.registerFg.get('passwordCtrl') as FormControl
  }
  get ConfrimPasswordCtrl(): FormControl {
    return this.registerFg.get('confrimPasswordCtrl') as FormControl
  }


  register(): void {

    // let user: RegisterUser = { // before add form (to test) 
    //   name: 'ali',
    //   email: 'ali4@a.com',
    //   password: 'ali4256',
    //   confrimPassword: 'ali4256'
    // }
    let user: RegisterUser = {
      name: this.NameCtrl.value,
      email: this.EmailCtrl.value,
      password:this.PasswordCtrl.value,
      confrimPassword:this.ConfrimPasswordCtrl.value
    }

    this.accountService.registerUser(user).subscribe({
      next: user => {

        console.log(user);
        this.router.navigateByUrl('/');
      },
      
    })
  }


}
