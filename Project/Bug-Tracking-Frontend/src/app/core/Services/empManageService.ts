import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { authService } from "./authService";
import { EmployeeModel } from "../Models/EmployeeModel";

@Injectable()
export class empManageService
{
    private http=inject(HttpClient)

    constructor(private authService:authService)
    {}

    getAllEmployeesAPI()
    {
        const token=this.authService.getCookie('access_token')
        return this.http.get<EmployeeModel[]>('http://localhost:5258/api/v1/EmployeeManagement/GetAll',{
            headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'
    }
        )
    }

    deleteEmployeeAPI(id:number)
    {
        const token=this.authService.getCookie('access_token');
        return this.http.delete(`http://localhost:5258/api/v1/EmployeeManagement/Delete?EmployeeId=${id}`,{
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
        return this.http.post('http://localhost:5258/api/v1/Authentication/register',data,{
             headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
            }),

        
        observe:'response'
        })

    }
}
