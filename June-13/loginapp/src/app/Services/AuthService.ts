import { Injectable } from "@angular/core";
import { LoginModel } from "../Models/LoginModel";


// @Injectable()
@Injectable({
  providedIn: 'root'
})
export class AuthService{
     private dummyUsers: LoginModel[] = [
    { email: 'admin@gmail.com', password: 'admin123'}
  ];

  constructor() {}

  validateUser(email: string, password: string): LoginModel | null {
    return this.dummyUsers.find(
      u => u.email === email && u.password === password
    ) || null;
  }
}