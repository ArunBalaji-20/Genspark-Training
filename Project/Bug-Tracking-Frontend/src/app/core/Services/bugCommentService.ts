import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { authService } from "./authService";
import { environment } from "../../../environments/environment";

@Injectable()
export class bugCommentService
{
    constructor(private http:HttpClient,private authService:authService)
    {}

    getAllCommentsAPI(bugid:number)
    {
         const token=this.authService.getCookie("access_token"); 
        return this.http.get(`${environment.apiUrl}/api/v2/BugComment/GetComments?bugId=${bugid}`,{
            headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'
    });

    }

    postCommentAPI(data:any)
    {
        const token=this.authService.getCookie("access_token"); 
        
        return this.http.post(`${environment.apiUrl}/api/v1/BugComment/Comment`,data,{
              headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'
    });

    }

    deleteCommentAPI(id:number)
    {
        const token=this.authService.getCookie("access_token"); 
        return this.http.delete(`${environment.apiUrl}/api/v2/BugComment/Delete?commentId=${id}`,{
                 headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'
    });
    }
}

// {
//   "comment": "Hello from tester mike",
//   "bugId": 21
// }