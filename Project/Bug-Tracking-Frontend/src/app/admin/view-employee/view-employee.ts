import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { empManageService } from '../../core/Services/empManageService';
import { EmployeeModel } from '../../core/Models/EmployeeModel';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import { BugCommentModel } from '../../core/Models/BugCommentModel';
import { devBugService } from '../../core/Services/devBugService';

@Component({
  selector: 'app-view-employee',
  imports: [],
  templateUrl: './view-employee.html',
  styleUrl: './view-employee.css'
})
export class ViewEmployee implements OnInit{
  empEmail:string="";
  empId:number=0;
  // empDetails:EmployeeModel[]=[];
  empDetails: EmployeeModel = {
  employeeId: 0,
  name: '',
  role: '',
  email: ''
};
  mybugs:BugStatusModel[]=[];
  bugsAssignedToMe:BugStatusModel[]=[];
  latestComments:BugCommentModel[]=[];

  constructor(private router:ActivatedRoute,private empManageService:empManageService,private devBugService:devBugService,
    private empservice:empManageService,private routerRedirect:Router
  )
  {
    this.router.queryParams.subscribe(params=>{
      this.empId=params['id'] || null;
      console.log(this.empId)
      
    })
  }
  ngOnInit(): void {

    this.empManageService.getEmployeeDetailsAPI(this.empId).subscribe({
      next:(res)=>{
        console.log(res)
        // this.empDetails=(res.body as any )?? [];
        this.empDetails = res.body as EmployeeModel;
        console.log(this.empDetails)
      },
      error:(err)=>{
        console.log(err)
      },
      complete:()=>{
        console.log("Completed fetching emp details")
      }
    })

    this.empManageService.getBugsReportedByAPI(this.empId).subscribe({
       next:(res)=>{
        console.log(res)
        this.mybugs=(res.body as any)?.$values ??[];
        console.log(this.mybugs)
      },
      error:(err)=>{
        console.log(err)
      },
      complete:()=>{
        console.log("Completed fetching bug details")
      }
    })

     this.empManageService.getLatestCommentsAPI(this.empId).subscribe({
       next:(res)=>{
        console.log(res)
        this.latestComments=(res.body as any)?.$values ??[];
        // this.mybugs=(res.body as any)?.$values ??[];
        console.log(this.latestComments)
      },
      error:(err)=>{
        console.log(err)
      },
      complete:()=>{
        console.log("Completed fetching latest comment details")
      }
    })

    this.devBugService.getMyBugsByIdAPI(this.empId).subscribe({
      next:(res)=>{
        console.log(res);
        this.bugsAssignedToMe=(res.body as any)?.$values ??[];
        console.log(this.bugsAssignedToMe);
      },
      error:(err)=>{
        console.log(err);
    },
      complete:()=>{
        console.log('completed getbugsassiged to me dev');
      }
    })
    
  }

   confirmDelete(id: number) {
  const confirmed = window.confirm('Are you sure you want to delete this employee?');

  if (confirmed) {
    this.deleteEmployee(id);
  }
}

  deleteEmployee(id:number)
  {
      this.empservice.deleteEmployeeAPI(id).subscribe({
        next:(res)=>{
          console.log(res);
          // this.getAllEmployees()
          window.alert("employee Deleted Successfully")
          this.routerRedirect.navigate(['/admin/manage/employees'])
          console.log('redirected...')

        },
        error:(err)=>{
          console.log(err);
          window.alert("Error while deleting employee")
          this.routerRedirect.navigate(['/admin/manage/employees'])
          console.log('redirected...')
        },
        complete:()=>{
          console.log("delete finish")
          // this.getAllEmployees();
        }
      })
    }

}
