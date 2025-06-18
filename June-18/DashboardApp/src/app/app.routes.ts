import { Routes } from '@angular/router';
import { Dashboard } from './dashboard/dashboard';
import { Addusers } from './addusers/addusers';

export const routes: Routes = [
    {path:'dashboard',component:Dashboard},
    {path:'add',component:Addusers}
];
