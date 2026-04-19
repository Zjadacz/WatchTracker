import { Component } from '@angular/core';
import { AuthService } from '@app/core/services/auth.service';
import { RouterLink, Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { inject } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  imports: [ReactiveFormsModule, RouterLink]
})
export class LoginComponent {
  private fb = inject(FormBuilder);

  loginForm = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });
  error = '';

  constructor(
    private auth: AuthService, 
    private router: Router) {}

  login() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const { email, password } = this.loginForm.getRawValue();
    this.auth.login(email, password).subscribe({
      next: () => this.router.navigate(['/']),
      error: () => this.error = 'Wrong email or password'
    });
  }
}
