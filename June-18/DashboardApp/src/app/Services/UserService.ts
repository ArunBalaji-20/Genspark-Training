import { BehaviorSubject, Observable } from "rxjs";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { UserAddModel } from "../Models/UserAddModel";


@Injectable()
export class UserService
{
    private http = inject(HttpClient);
   
    AddUsers(user: UserAddModel) {
    
        return this.AddUsersAPI(user);
}

    AddUsersAPI(user:UserAddModel)
    {
        return this.http.post("https://dummyjson.com/users/add",user);
    }

    GetAllUsers()
    {
        return this.http.get("https://dummyjson.com/users")
    }
}