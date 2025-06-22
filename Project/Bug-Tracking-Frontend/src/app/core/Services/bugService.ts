import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { authService } from "./authService";

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
    
    return this.http.post("http://localhost:5258/api/v1/Bugs/Report", bugFormData, {
        headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'
    });
}

}

// http://localhost:5258/api/v1/Bugs/Report