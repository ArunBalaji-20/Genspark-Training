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
import { Manage } from './admin/manage/manage';
import { Add } from './admin/add/add';
import { AllBugsStatus } from './shared/all-bugs-status/all-bugs-status';
import { Managebugs } from './developer/managebugs/managebugs';
import { AllBugsAssign } from './admin/all-bugs-assign/all-bugs-assign';
import { ChatList } from './shared/chat-list/chat-list';
import { ViewEmployee } from './admin/view-employee/view-employee';
import { AllBugs } from './admin/all-bugs/all-bugs';
import { ManagebugsAdmin } from './admin/managebugs-admin/managebugs-admin';

export const routes: Routes = [
    {path:'login',component:Login},
    {path:'',component:Landing},
    {path:'about',component:About},
    {path:'chat',component:Chat,canActivate:[authGuard],data: { roles: ['Admin','Dev','Tester']}},


    {path:'admin/dashboard',component:Dashboard,canActivate:[authGuard],data: { roles: ['Admin']}},
    {path:'admin/chatList',component:ChatList,canActivate:[authGuard],data: { roles: ['Admin']}},

    { path: 'admin/bugs',canActivate:[authGuard],data: { roles: ['Admin']},
      children: [
      { path: 'submitted', component:AllBugsAssign  },
      { path: 'report', component: Report },
      { path: 'status', component: AllBugsStatus},
      {path:'assign',component:Assign}
    ]
  },
   {path:'admin/manage/employees',component:Manage,canActivate:[authGuard],data:{roles:['Admin']}},
   {path:'admin/allbugs',component:AllBugs,canActivate:[authGuard],data:{roles:['Admin']}},
   {path:'admin/manage/bugs',component:ManagebugsAdmin,canActivate:[authGuard],data: { roles: ['Admin']}},
   {path:'admin/manage/add',component:Add,canActivate:[authGuard],data:{roles:['Admin']}},
//    /admin/view/employee
  {path:'admin/manage/employees/view',component:ViewEmployee,canActivate:[authGuard],data:{roles:['Admin']}},

  

    {path:'tester/report',component:Report,canActivate:[authGuard],data: { roles: ['Tester']}},
    // {path:'tester/chat',component:Chat,canActivate:[authGuard],data: { roles: ['Tester']}},
    {path:'tester/chatList',component:ChatList,canActivate:[authGuard],data: { roles: ['Tester']}},
    {path:'tester/status',component:AllBugsStatus,canActivate:[authGuard],data:{roles:['Tester']}},


    {path:'developer/myBugs',component:MyBugs,canActivate:[authGuard],data: { roles: ['Dev']}},
    // {path:'developer/chat',component:Chat,canActivate:[authGuard],data: { roles: ['Dev']}},
    {path:'developer/managebugs',component:Managebugs,canActivate:[authGuard],data: { roles: ['Dev']}},
     {path:'developer/chatList',component:ChatList,canActivate:[authGuard],data: { roles: ['Dev']}},

];
