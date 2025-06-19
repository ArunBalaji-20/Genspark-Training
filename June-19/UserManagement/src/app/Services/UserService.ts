import { Injectable } from "@angular/core";
import { UserModel } from "../Models/UserModel";
import { BehaviorSubject, Observable } from "rxjs";

@Injectable()
export class UserService {
  private usersSubject = new BehaviorSubject<UserModel[]>([]);
  users$ = this.usersSubject.asObservable();

  constructor() {
    const saved = localStorage.getItem('users');
    if (saved) {
      this.usersSubject.next(JSON.parse(saved));
    }
  }

  addUser(user: UserModel) {
    const current = this.usersSubject.value;
    const updated = [...current, user];
    this.usersSubject.next(updated);
    localStorage.setItem('users', JSON.stringify(updated));
  }

  getUsersSnapshot(): UserModel[] {
    return this.usersSubject.value;
  }

getSearchUsers(query: string): UserModel[] {
  console.log("from service")

  const q = query.trim().toLowerCase();
  return this.usersSubject.value.filter(user =>
    user.email.toLowerCase().includes(q) ||
    user.role.toLowerCase().includes(q)
  );

}

}