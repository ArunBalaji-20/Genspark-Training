import { AbstractControl, ValidationErrors } from '@angular/forms';

export function passwordMatchValidator(group: AbstractControl): ValidationErrors | null {
  const pass = group.get('pass')?.value;
  const confirmPass = group.get('confirmPass')?.value;
  return pass === confirmPass ? null : { matchError: true };
}