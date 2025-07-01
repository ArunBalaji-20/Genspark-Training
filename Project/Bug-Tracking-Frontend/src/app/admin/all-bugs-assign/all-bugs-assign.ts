import { Component, OnInit } from '@angular/core';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import { CommonModule } from '@angular/common';
import { bugService } from '../../core/Services/bugService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-all-bugs-assign',
  imports: [CommonModule],
  templateUrl: './all-bugs-assign.html',
  styleUrl: './all-bugs-assign.css'
})
export class AllBugsAssign implements OnInit {
  mybugs:BugStatusModel[]=[];
  isFetching:Boolean=false;

  constructor(private bugService:bugService,private router:Router)
  {

  }

  ngOnInit(): void {
    this.bugService.getAllBugsInSubmittedState().subscribe({
      next:(data)=>{
        console.log(data)
        this.mybugs=(data.body as any)?.$values ??[];
      },
      error:(err)=>{
        console.log(err)
      },
      complete:()=>{
        console.log("completed getallbugsstateinsubmitted")
      }

    })

  }
  AssignBug(id:number)
  {
    console.log(id)
    this.router.navigate(['/admin/bugs/assign'],{queryParams:{id:`${id}`}})
    console.log("redirected ..")
  }

}

