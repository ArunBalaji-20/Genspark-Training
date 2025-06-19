import { Component, OnInit } from '@angular/core';
import { UserModel } from '../Models/UserModel';
import { UserService } from '../Services/UserService';
import { FormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, of, Subject, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-search-users',
  imports: [FormsModule],
  templateUrl: './search-users.html',
  styleUrl: './search-users.css'
})
export class SearchUsers implements OnInit{
  users: UserModel[] = [];
  searchString:string="";
  searchSubject = new Subject<string>();
  loading:Boolean=true;

  constructor(private userService: UserService) {}

  ngOnInit() {

    this.searchSubject.pipe(
      debounceTime(1000),
      distinctUntilChanged(),
      tap(() => this.loading = true),
      switchMap(query => of(this.userService.getSearchUsers(query))),
      tap(() => this.loading = false)
    ).subscribe({
      next: (data: UserModel[]) => {
        this.users = data;
        console.log(this.users);
      }
    });

      // console.log("ngonit")
  }

   handleSearchUsers() {
 
    this.searchSubject.next(this.searchString);

   
  }
}

