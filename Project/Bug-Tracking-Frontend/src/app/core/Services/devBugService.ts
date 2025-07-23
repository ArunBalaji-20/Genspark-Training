import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { authService } from "./authService";
import { environment } from "../../../environments/environment";

@Injectable()
export class devBugService
{   
    // token:string="";
    constructor(private http:HttpClient,private authService:authService)
    {
        // this.token = this.authService.getCookie("access_token") ?? "";
        // console.log(this.token)
    }

    getMyBugsAPI()
    {
        const token=this.authService.getCookie("access_token"); 
        return this.http.get(`${environment.apiUrl}/api/v2/BugsManagement/assigned/mybugs`,{
            headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
        }),
            observe:'response'
        })
    }

    getMyBugsByIdAPI(id:number)
    {
        const token=this.authService.getCookie("access_token"); 
        return this.http.get(`${environment.apiUrl}/api/v2/BugsManagement/assigned/mybugs/byId?empId=${id}`,{
            
            headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
        }),
            observe:'response'
        })
    }

    getBugStatusAPI(id:number)
    {
        const token=this.authService.getCookie("access_token"); 
        return this.http.get(`${environment.apiUrl}/api/v1/Bugs/status?BugId=${id}`,{
            headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
        }),
            observe:'response'
        })
    }

    patchBugStatusResolveAPI(id:number)
    {
        const token=this.authService.getCookie("access_token"); 
        return this.http.patch(`${environment.apiUrl}/api/v1/BugsManagement/Resolve?BugId=${id}`,{},{
               headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
        }),
            observe:'response'
        })
    }

    patchBugStatusResolveAdminAPI(id:number)
    {
        const token=this.authService.getCookie("access_token"); 
        return this.http.patch(`${environment.apiUrl}/api/v2/BugsManagement/Resolve/Admin?BugId=${id}`,{},{
               headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
        }),
            observe:'response'
        })
    }

    patchBugStatusInProgressAPI(id:number)
    {
        const token=this.authService.getCookie("access_token");

        return this.http.patch(`${environment.apiUrl}/api/v2/BugsManagement/Update/InProgress?BugId=${id}`,{},{
               headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
        }),
            observe:'response'
        })
    }

    getAvailableDevsAPI()
    {
        const token=this.authService.getCookie("access_token");
        return this.http.get(`${environment.apiUrl}/api/v1/BugsManagement/Available/Devs`,{
                  headers:new HttpHeaders({
                'Authorization': `Bearer ${token}`
        }),
            observe:'response'
        })
    }
}
