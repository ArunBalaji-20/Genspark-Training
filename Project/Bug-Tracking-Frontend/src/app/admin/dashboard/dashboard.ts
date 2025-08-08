import { Component } from '@angular/core';
import { authService } from '../../core/Services/authService';
import { EmployeeModel } from '../../core/Models/EmployeeModel';
import { empManageService } from '../../core/Services/empManageService';
import { bugService } from '../../core/Services/bugService';
import { BugStatusModel } from '../../core/Models/BugStatusModel';
import Chart from 'chart.js/auto';
import { AvailableDevsResponseModel } from '../../core/Models/AvailableDevsResponseModel';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {

    availDevs:AvailableDevsResponseModel[]=[];
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
    bugCountMap: { [employeeId: number]: number } = {};


    
    constructor(private empservice:empManageService,private bugservice:bugService)
    {}

  ngOnInit(): void {
    this.getAllEmployees();
    this.getBugData();
   
  }
  
isAvailable(employeeId: number): boolean {
  return this.availDevs.some(dev => Number(dev.employeeId) === employeeId);
}
// GetbugsCount(empId:number):number{
//   this.bugservice.getAssignedBugsCountAPI(empId).subscribe({
//     next:(res)=>{
//       console.log(res);
//       console.log('assigned bugs count')
//       return res.body as number;
//     },
//     error:(err)=>{
//       console.error('Error fetching assigned bugs count', err);
//       return 0;
//     }
//   });
//   return 0; // Default return value if the API call fails
// }

GetbugsCount(empId: number): number {
  return this.bugCountMap[empId] ?? 0;
}


  getAllEmployees()
  {
      this.empservice.getAllEmployeesAPI().subscribe({
  next: (res) => {
    console.log('API res', res);
      this.employees = (res.body as any)?.$values ?? [];
       this.totalDev=this.GetEmployeeCount('Dev');
    this.totalTesters=this.GetEmployeeCount('Tester')
    this.renderEmployeeChart();
    this.getAvailEmployees();


    // counting dev assingemnts
    const devs = this.employees.filter(emp => emp.role === 'Dev');

    for (let dev of devs) {
      this.bugservice.getAssignedBugsCountAPI(dev.employeeId).subscribe({
        next: (res) => {
          this.bugCountMap[dev.employeeId] =  Number(res.body ?? 0);
        },
        error: (err) => {
          console.error(`Error loading bug count for ${dev.employeeId}`, err);
          this.bugCountMap[dev.employeeId] = 0;
        }
      });
    }
  },
  error: (err) => {
    console.error('Error fetching employees', err);
    this.isFetching=true;
  }
});
  }

  getAvailEmployees()
  {
    this.empservice.getAvailableEmpAPI().subscribe({
      next:(res)=>{
        console.log(res);
        console.log('avail list')
        this.availDevs=(res.body as any)?.$values ?? [];
        console.log(this.availDevs)

      },
      error:(err)=>{
        console.log(err)
      }
      
    })
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
          },
          title: {
                display: true,
                text: ' Bug Stats',
                // position:'bottom'
            }
        }
      }
    });
  }

  renderEmployeeChart() {
    if (typeof Chart === 'undefined') return;
    const ctx = document.getElementById('EmployeeChart') as HTMLCanvasElement;
    if (!ctx) return;

    new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels: ['Developers', 'Testers'],
        datasets: [{
          data: [this.totalDev, this.totalTesters],
          backgroundColor: ['#36A2EB', '#FF6384'],
        }]
      },
      options: {
        responsive: true,
        plugins: {
          legend: { position: 'bottom' },
          title: {
                display: true,
                text: 'Employee Ratio'
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
