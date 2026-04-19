import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { AuthService } from '@app/core/services/auth.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss',
  imports: [ReactiveFormsModule],
})
export class ResetPasswordComponent {
  private fb = inject(FormBuilder);

  resetPasswordForm = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
  });

  error = '';
  formVisible = true;

  constructor(
    private auth: AuthService,
    private cdRef: ChangeDetectorRef
  ) {}

  resetPassword() {
    if (this.resetPasswordForm.invalid) {
      this.resetPasswordForm.markAllAsTouched();
      return;
    }

    const { email } = this.resetPasswordForm.getRawValue();
    this.auth.resetPassword(email).subscribe({
      next: () => {
        this.formVisible = false;
        this.cdRef.detectChanges();
      },
      error: () => this.error = 'Something went wrong'
    });
  }
}
