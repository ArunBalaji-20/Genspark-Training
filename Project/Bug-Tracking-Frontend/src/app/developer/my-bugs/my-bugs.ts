import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import { devBugService } from '../../core/Services/devBugService';
import { ActivatedRoute, ActivatedRouteSnapshot, Router } from '@angular/router';

@Component({
  selector: 'app-my-bugs',
  imports: [CommonModule],
  templateUrl: './my-bugs.html',
  styleUrl: './my-bugs.css'
})
export class MyBugs implements OnInit{

  isFetching:Boolean=false;
  mybugs:BugStatusModel[]=[];
  pageSize: number = 5;
  currentPage: number = 1;

  constructor(private devBugService:devBugService,private router:Router)
  {}
  ngOnInit(): void {
    this.devBugService.getMyBugsAPI().subscribe({
      next:(data)=>{
        console.log(data)
        this.mybugs=(data.body as any)?.$values ??[];
        
        // this.employees = (res.body as any)?.$values ?? [];
        console.log("from ngonint dev mybugs")
      },
      error:(err)=>{
        console.log(err)
      },
      complete:()=>{
        console.log('completed api dev mybugs')
      }
    })

  }

  updateStatus(id:number)
  {
    console.log(id)
    // this.router.navigateByUrl("/developer/managebugs");
    this.router.navigate(['/developer/managebugs'],{queryParams:{id:`${id}`}})
    console.log("redirected to manage")
  }

   get totalPages(): number {
        return Math.ceil(this.mybugs.length / this.pageSize);
      }
    
    get paginatedList(): BugStatusModel[] {
      const start = (this.currentPage - 1) * this.pageSize;
      return this.mybugs.slice(start, start + this.pageSize);
    }
    
      setPage(page: number) {
        if (page >= 1 && page <= this.totalPages) {
          this.currentPage = page;
        }
      }
}

