import { Component, ChangeDetectorRef } from '@angular/core';
import { AuthService } from '@app/core/services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { inject } from '@angular/core';
import { passwordMatchValidator } from '@app/core/validators/passwordMatchValidator';
import { UserError } from '@app/core/models/user-error.model';

@Component({
  selector: 'app-login',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
  imports: [ReactiveFormsModule]
})
export class RegisterComponent {
  private fb = inject(FormBuilder);

  registerForm = this.fb.nonNullable.group(
    {
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    }, {
      validators: [passwordMatchValidator]
    }
  );

  errors: UserError[] = [];

  constructor(
    private auth: AuthService, 
    private cdRef: ChangeDetectorRef,
    private router: Router) {}

  register() {

    const { email, password } = this.registerForm.getRawValue();
    this.auth.register(email, password).subscribe({
      next: () => this.router.navigate(['/user/email-sent']),
      error: (err) => {
        this.errors = err.error as UserError[];
        this.cdRef.detectChanges();
      }
    });
  }
}
