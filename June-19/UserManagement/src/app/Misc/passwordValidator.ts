import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function passwordValidator():ValidatorFn
{
    return(control:AbstractControl):ValidationErrors|null=>{
        const value = control.value;
        if(value?.length<6)
            return {lenError:'password is of worng length'}
        return null;

    }
}