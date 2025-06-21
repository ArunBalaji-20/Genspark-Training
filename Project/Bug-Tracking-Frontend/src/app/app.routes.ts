import { Routes } from '@angular/router';
import { Login } from './Home/login/login';
import { Landing } from './Home/landing/landing';
import { About } from './Home/about/about';
import { Dashboard } from './admin/dashboard/dashboard';
import { Report } from './shared/report/report';
import { MyBugs } from './developer/my-bugs/my-bugs';
import { Assign } from './admin/assign/assign';
import { Status } from './shared/status/status';
import { Chat } from './shared/chat/chat';
import { Resolve } from './developer/resolve/resolve';
import { authGuard } from './auth-guard';

export const routes: Routes = [
    {path:'login',component:Login},
    {path:'',component:Landing},
    {path:'about',component:About},

    {path:'admin/dashboard',component:Dashboard,canActivate:[authGuard],data: { roles: ['Admin']}},
    {path:'admin/chat',component:Chat,canActivate:[authGuard],data: { roles: ['Admin']}},

    { path: 'admin/bugs',canActivate:[authGuard],data: { roles: ['Admin']},
      children: [
      { path: 'assign', component:Assign  },
      { path: 'report', component: Report },
      { path: 'status', component: Status}
    ]
  },

    {path:'tester/report',component:Report,canActivate:[authGuard],data: { roles: ['Tester']}},
    {path:'tester/chat',component:Chat,canActivate:[authGuard],data: { roles: ['Tester']}},


    {path:'developer/myBugs',component:MyBugs,canActivate:[authGuard],data: { roles: ['Dev']}},
    {path:'developer/chat',component:Chat,canActivate:[authGuard],data: { roles: ['Dev']}},
    {path:'developer/resolve',component:Resolve,canActivate:[authGuard],data: { roles: ['Dev']}}

];
