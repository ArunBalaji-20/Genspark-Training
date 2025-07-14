import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { authService } from "./authService";
import { environment } from "../../../environments/environment";

@Injectable()
export class bugService
{
    private http=inject(HttpClient);
    constructor(private authService:authService)
    {}

    // submitBugReportAPI(bug:any)
    // {
    //     return this.http.post("http://localhost:5258/api/v1/Bugs/Report",bug)
    // }

    submitBugReportAPI(bugFormData: FormData) {
    const token=this.authService.getCookie("access_token"); 
    // console.log(token);
    
    return this.http.post(`${environment.apiUrl}/api/v1/Bugs/Report`, bugFormData, {
        headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'
    });
}

    getAllBugStatusAPI()
    {
        const token=this.authService.getCookie("access_token"); 
        return this.http.get(`${environment.apiUrl}/api/v1/Bugs/ReportedBugs`,{
             headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'

        });
    }

    getAllBugsAssignedListAPI()
    {
        const token=this.authService.getCookie("access_token"); 

         return this.http.get(`${environment.apiUrl}/api/v1/BugsManagement/Get/Assigned/Lists`,{
             headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'

        });
    }

    getAllBugsInSubmittedState()
    {
        const token=this.authService.getCookie("access_token"); 
        return this.http.get(`${environment.apiUrl}/api/v2/Bugs/Get/InSubmittedState`,{
              headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'

        });
    }

    assignBugToDevAPI(data:any)
    {
        const token=this.authService.getCookie("access_token"); 
        return this.http.post(`${environment.apiUrl}/api/v1/BugsManagement/Assign`,data,{
             headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'

        });
    }
}

// http://localhost:5258/api/v1/Bugs/Report