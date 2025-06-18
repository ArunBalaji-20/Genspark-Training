import {  AfterViewInit, Component, OnInit } from '@angular/core';
import { UserService } from '../Services/UserService';
import Chart from 'chart.js/auto';
// import { ChoroplethController, GeoFeature } from 'chartjs-chart-geo';
// import { ChoroplethController, GeoFeature as geoFeature } from 'chartjs-chart-geo';
// Chart.register(ChoroplethController, geoFeature);
import { ChoroplethController, GeoFeature, ColorScale, ProjectionScale } from 'chartjs-chart-geo';
import * as topojson from 'topojson-client';
Chart.register(ChoroplethController, GeoFeature, ColorScale, ProjectionScale);



// Register the choropleth controller and geo feature with Chart.js
// Chart.register(ChoroplethController, GeoFeature);
// Chart.register(ChoroplethController, GeoFeature);

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements OnInit {
  data:any;
  TotalMale:number=0;
  TotalFemale:number=0;

  roleCounts: { [role: string]: number } = {
  admin: 0,moderator: 0,user: 0};

  stateCounts:{[state:string]:number}={};

  constructor(private userService:UserService)
  {
    // this.data=userService.GetAllUsers();
    // console.log(this.data)
  }
ngOnInit(): void {
  this.userService.GetAllUsers().subscribe({
    next: (data) => {
      this.data = data;
      this.TotalMale = this.GetCount("male");
      this.TotalFemale = this.GetCount("female");
      console.log(this.data.users[0].address.state)
      this.GetRoleCounts();
      this.GetStateCounts();
      // console.log(`Total Male: ${this.TotalMale}`);
      // console.log(`Total Female: ${this.TotalFemale}`);
      this.renderGenderChart();
      this.renderRoleChart();
      this.renderStateMapChart();
    },
    error: (err) => {
      console.error('Failed to fetch users', err);
    }
  });
}

GetCount(gender: string): number {
  if (!this.data || !this.data.users) {
    return 0;
  }
  return this.data.users.filter((user: any) => user.gender === gender).length;
}

GetRoleCounts()
{
  if (!this.data || !this.data.users) {
    return 0;
  }
  this.roleCounts["admin"]=this.data.users.filter((user: any) => user.role==="admin").length;
  this.roleCounts["moderator"]=this.data.users.filter((user: any) => user.role==="moderator").length;
  this.roleCounts["user"]=this.data.users.filter((user: any) => user.role==="user").length;
  console.log(this.roleCounts)
  return
}

GetStateCounts() {
  if (!this.data || !this.data.users) {
    return;
  }
  this.stateCounts = {}; 

  this.data.users.forEach((user: any) => {
    const state = user.address.state;
    if (state) {
      if (!this.stateCounts[state]) {
        this.stateCounts[state] = 1;
      } else {
        this.stateCounts[state]++;
      }
    }
  });

  console.log(this.stateCounts); 
}



  renderGenderChart() {
    if (typeof Chart === 'undefined') return;
    const ctx = document.getElementById('genderChart') as HTMLCanvasElement;
    if (!ctx) return;

    new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels: ['Male', 'Female'],
        datasets: [{
          data: [this.TotalMale, this.TotalFemale],
          backgroundColor: ['#36A2EB', '#FF6384'],
        }]
      },
      options: {
        responsive: true,
        plugins: {
          legend: { position: 'bottom' }
        }
      }
    });
  }

  renderRoleChart() {
    if (typeof Chart === 'undefined') return;
    const ctx = document.getElementById('RoleChart') as HTMLCanvasElement;
    if (!ctx) return;

    new Chart(ctx, {
      type: 'bar',
      data: {
        labels: ['Admin', 'Moderator','User'],
        datasets: [{
          data: [this.roleCounts["admin"], this.roleCounts["moderator"],this.roleCounts["user"]],
          backgroundColor: ['#36A2EB', '#FF6384','FF6384'],
        }]
      },
      options: {
        responsive: true,
        plugins: {
          legend: { position: 'bottom' }
        }
      }
    });
  }

 

  renderStateMapChart() {
  const ctx = document.getElementById('StateMapChart') as HTMLCanvasElement;
  if (!ctx) return;

  fetch('https://cdn.jsdelivr.net/npm/us-atlas/states-10m.json')
    .then(res => res.json())
    .then(us => {
      const states = ((topojson.feature(us, us.objects.states) as unknown) as GeoJSON.FeatureCollection).features as any[];

      new Chart(ctx, {
        type: 'choropleth',
        data: {
          labels: states.map(d => d.properties.name),
          datasets: [{
            label: 'User State Count',
            data: states.map(d => ({
              feature: d,
              value: this.stateCounts[d.properties.name] || 0
            })),
          }]
        },
        options: {
          responsive: true,
          plugins: {
            legend: { display: true },
            tooltip: {
            callbacks: {
              label: (context: any) => {
                const val = context.raw.value;
                const stateName = context.raw.feature.properties.name;
                return `${stateName}: ${val}`;
              }
              }
            }
          },
          scales: {
            xy: {
              projection: 'albersUsa'
            }
          }
        }
      });
    });
  }
  

}
