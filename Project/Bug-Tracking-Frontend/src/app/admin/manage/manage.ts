import { Component, OnInit } from '@angular/core';
import { empManageService } from '../../core/Services/empManageService';
import { EmployeeModel } from '../../core/Models/EmployeeModel';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manage',
  imports: [CommonModule],
  templateUrl: './manage.html',
  styleUrl: './manage.css'
})
export class Manage implements OnInit{
    // employees:EmployeeModel=new EmployeeModel();
    employees: EmployeeModel[] = []; 
    isFetching:boolean=false;
    pageSize: number = 5;
    currentPage: number = 1;
    constructor(private empservice:empManageService,private router:Router)
    {}

  ngOnInit(): void {
   this.getAllEmployees();

  }

  getAllEmployees()
  {
     this.empservice.getAllEmployeesAPI().subscribe({
  next: (res) => {
    console.log('API res', res);
     this.employees = (res.body as any)?.$values ?? [];
  },
  error: (err) => {
    console.error('Error fetching employees', err);
    this.isFetching=true;
  }
});
  }



  handleView(id:number)
  {
    console.log(id);
    this.router.navigate(['/admin/manage/employees/view'],{queryParams:{id:`${id}`}})
    console.log('redirected...')
  }

   get totalPages(): number {
      return Math.ceil(this.employees.length / this.pageSize);
    }
  
    get paginatedEmployees(): EmployeeModel[] {
      const start = (this.currentPage - 1) * this.pageSize;
      return this.employees.slice(start, start + this.pageSize);
    }
  
    setPage(page: number) {
      if (page >= 1 && page <= this.totalPages) {
        this.currentPage = page;
      }
    }

    
}



