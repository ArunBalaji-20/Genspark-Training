import { AbstractControl, ValidationErrors } from '@angular/forms';

export function futureDateValidator(control: AbstractControl): ValidationErrors | null {
  const inputDate = new Date(control.value);
  const today = new Date();

  // Reset time to 00:00:00 for accurate date comparison
  today.setHours(0, 0, 0, 0);

  if (isNaN(inputDate.getTime())) {
    return null; // Let required validator handle empty/invalid inputs
  }

  if (inputDate <= today) {
    return { notFutureDate: true };
  }

  return null;
}
