import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RegisterComponent } from '../register/register.component';
import { User } from 'src/app/shared/models/User';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UsersService } from 'src/app/shared/services/users.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginObj: User;
  loginForm: FormGroup;

  constructor(private api: UsersService, private form: FormBuilder, private router: Router) {}

  ngOnInit(): void {
    this.loginForm = this.form.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });

    this.loginObj = new User();
  }

  login(event: FormGroup): void {
    this.loginObj = {
      userId: 0,
      username: '',
      emailId: event.value.email,
      password: event.value.password,
      roleId: 0,
      access: '',
    };

    this.api.loginUser(this.loginObj).subscribe({
      next: (res: { message: string }) => {
        alert(res.message);
        this.router.navigate(['/projects']);
      },
      error: (err: Error) => {
        console.log('error');
      },
      complete: () => {
        console.log('complete');
      },
    });
  }
}
