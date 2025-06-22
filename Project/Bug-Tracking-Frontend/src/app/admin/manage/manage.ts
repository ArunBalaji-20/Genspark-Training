import { Component, OnInit } from '@angular/core';
import { empManageService } from '../../core/Services/empManageService';
import { EmployeeModel } from '../../core/Models/EmployeeModel';
import { CommonModule } from '@angular/common';

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
    constructor(private empservice:empManageService)
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
          this.getAllEmployees()

        },
        error:(err)=>{
          console.log(err);
        },
        complete:()=>{
          console.log("delete finish")
          this.getAllEmployees();
        }
      })
  }



    
}



