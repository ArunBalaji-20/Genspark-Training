import { Component } from '@angular/core';
import { authService } from '../../core/Services/authService';
import { EmployeeModel } from '../../core/Models/EmployeeModel';
import { empManageService } from '../../core/Services/empManageService';
import { bugService } from '../../core/Services/bugService';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {

  
    employees: EmployeeModel[] = []; 
    bugs:BugStatusModel[]=[];
    isFetching:boolean=false;
    totalDev:number=0;
    totalTesters:number=0;
    InProgress:number=0;
    Submitted:number=0;
    Resolved:number=0;
    Assigned:number=0;
    TotalBugs:number=0;
    constructor(private empservice:empManageService,private bugservice:bugService)
    {}

  ngOnInit(): void {
    this.getAllEmployees();
    this.getBugData();
   
  }
  
  getAllEmployees()
  {
      this.empservice.getAllEmployeesAPI().subscribe({
  next: (res) => {
    console.log('API res', res);
      this.employees = (res.body as any)?.$values ?? [];
       this.totalDev=this.GetEmployeeCount('Dev');
    this.totalTesters=this.GetEmployeeCount('Tester')
  },
  error: (err) => {
    console.error('Error fetching employees', err);
    this.isFetching=true;
  }
});
  }

  getBugData()
  {
    this.bugservice.getAllBugStatusAPI().subscribe({
            next: (data: any) => {
              this.bugs=(data.body.$values as BugStatusModel[]);
              this.InProgress=this.bugs.filter(b=>b.status==='InProgress').length;
              this.Submitted=this.bugs.filter(b=>b.status==='Submitted').length;
              this.Assigned=this.bugs.filter(b=>b.status==='Assigned').length;
              this.Resolved=this.bugs.filter(b=>b.status==='Resolved').length;
              this.TotalBugs=this.bugs.length;
              console.log(this.TotalBugs)
              this.renderStatusChart()
            },
            error: (err) => {
              console.log(err);
            }
          });
  }

   renderStatusChart() {
    if (typeof Chart === 'undefined') return;
    const ctx = document.getElementById('StatusChart') as HTMLCanvasElement;
    if (!ctx) return;

    new Chart(ctx, {
      type: 'bar',
      data: {
        labels: ['Submitted', 'Assigned','InProgress','Resolved'],
        datasets: [{
          data: [this.Submitted, this.Assigned,this.InProgress,this.Resolved],
          backgroundColor: ['#db0d0d', '#0d22db','#eba309','#09eb27'],
          
        }]
      },
      options: {
        responsive: true,
        plugins: {
          legend: { position: 'bottom' ,
            display:false
          }
        }
      }
    });
  }


  GetEmployeeCount(role: string): number
  {
    const res= this.employees.filter(emp => emp.role === role).length;
    console.log(res);
    return res;
  }


}
