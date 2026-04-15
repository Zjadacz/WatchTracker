import { Component } from '@angular/core';
import { AuthService } from '@app/core/services/auth.service';
import { Router } from '@angular/router';
import { AbstractControl, FormGroup, FormBuilder, ReactiveFormsModule, Validators, ValidatorFn, ValidationErrors } from '@angular/forms';
import { inject } from '@angular/core';

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

  error = '';

  constructor(
    private auth: AuthService, 
    private router: Router) {}

  register() {

    const { email, password } = this.registerForm.getRawValue();
    this.auth.register(email, password).subscribe({
      next: () => this.router.navigate(['/user/email-sent']),
      error: (err) => {
        this.error = err.error[0].description || 'Something went wrong'
      }
    });
  }
}

export const passwordMatchValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const form = control as FormGroup; 
  const password = form.get('password')?.value;
  const confirmPassword = form.get('confirmPassword')?.value;

  return password === confirmPassword ? null : { passwordMismatch: true };
};
