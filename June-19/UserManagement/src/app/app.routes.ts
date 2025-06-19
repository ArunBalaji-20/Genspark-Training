import { Routes } from '@angular/router';
import { AddUsers } from './add-users/add-users';
import { SearchUsers } from './search-users/search-users';

export const routes: Routes = [
    {path:'add',component:AddUsers},
    {path:'search',component:SearchUsers}
];
