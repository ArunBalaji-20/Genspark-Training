import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { authService } from "./authService";
import { EmployeeModel } from "../Models/EmployeeModel";
import { environment } from "../../../environments/environment";

@Injectable()
export class empManageService
{
    private http=inject(HttpClient)

    constructor(private authService:authService)
    {}

    getAllEmployeesAPI()
    {
        const token=this.authService.getCookie('access_token')
        return this.http.get<EmployeeModel[]>(`${environment.apiUrl}/api/v1/EmployeeManagement/GetAll`,{
            headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'
    }
        )
    }

    getAvailableEmpAPI()
    {
         const token=this.authService.getCookie('access_token');

        return this.http.get(`${environment.apiUrl}/api/v1/BugsManagement/Available/Devs`,{
             headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
            }),

        observe:'response'
        })
    }

    deleteEmployeeAPI(id:number)
    {
        const token=this.authService.getCookie('access_token');
        return this.http.delete(`${environment.apiUrl}/api/v1/EmployeeManagement/Delete?EmployeeId=${id}`,{
            headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
            }),

        
        observe:'response'
        }
    )
    }

    addEmployeeAPI(data:any)
    {
        const token=this.authService.getCookie('access_token');
        return this.http.post(`${environment.apiUrl}/api/v1/Authentication/register`,data,{
             headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
            }),

        
        observe:'response'
        })

    }

    getEmployeeDetailsAPI(id:number)
    {
        const token=this.authService.getCookie('access_token');
        return this.http.get(`${environment.apiUrl}/api/v2/EmployeeManagement/GetEmployee?EmployeeId=${id}`,{
            headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
            }),
            observe:'response'
        })
    }

    getBugsReportedByAPI(id:number)
    {

        const token=this.authService.getCookie('access_token');
        return this.http.get(`${environment.apiUrl}/api/v2/Bugs/Get/reportedBy?empId=${id}`,{
             headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
            }),
            observe:'response'
        });
    }

    getLatestCommentsAPI(id:number)
    {
        const token=this.authService.getCookie('access_token');
        return this.http.get(`${environment.apiUrl}/api/v2/BugComment/GetComments/latest?empId=${id}`,{
            headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
            }),
            observe:'response'
        });
    }
}
