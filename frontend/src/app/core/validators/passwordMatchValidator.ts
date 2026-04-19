import { AbstractControl, FormGroup, ValidatorFn, ValidationErrors } from '@angular/forms';

export const passwordMatchValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const form = control as FormGroup; 
  const password = form.get('password')?.value;
  const confirmPassword = form.get('confirmPassword')?.value;

  return password === confirmPassword ? null : { passwordMismatch: true };
};