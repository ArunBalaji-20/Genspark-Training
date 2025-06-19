import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function RoleValidator(forbiddenRoles: string[]): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = (control.value || '').toLowerCase();
    return forbiddenRoles.includes(value)
      ? { forbiddenRole: { value: control.value } }
      : null;
  };
}