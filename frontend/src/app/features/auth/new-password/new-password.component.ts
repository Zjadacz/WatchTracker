import { Component, inject, ChangeDetectorRef, OnInit } from '@angular/core';
import { AuthService } from '@app/core/services/auth.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { passwordMatchValidator } from '@app/core/validators/passwordMatchValidator';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-new-password',
  templateUrl: './new-password.component.html',
  styleUrl: './new-password.component.scss',
  imports: [ReactiveFormsModule],
})
export class NewPasswordComponent implements OnInit {
  private fb = inject(FormBuilder);

  newPasswordForm = this.fb.nonNullable.group(
    {
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    },
    {
      validators: [passwordMatchValidator],
    },
  );

  error = '';
  formVisible = true;
  userId = '';
  token = '';

  constructor(
    private auth: AuthService,
    private cdRef: ChangeDetectorRef,
    private route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.route.queryParamMap.subscribe((params) => {
      this.userId = params.get('userId') ?? '';
      this.token = params.get('token') ?? '';
      if (!this.token || !this.userId) {
        this.error = 'Wrong password reset link.';
      }
    });
  }

  newPassword() {
    if (this.newPasswordForm.invalid) {
      this.newPasswordForm.markAllAsTouched();
      return;
    }

    const { password } = this.newPasswordForm.getRawValue();
    this.auth.newPassword(this.userId, password, this.token).subscribe({
      next: () => {
        this.formVisible = false;
        this.cdRef.detectChanges();
      },
      error: (err) => {
        this.error = err.error || 'Error confirming account.';
        this.cdRef.markForCheck();
      },
    });
  }
}
