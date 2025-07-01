import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { authService } from "./authService";

@Injectable()
export class bugCommentService
{
    constructor(private http:HttpClient,private authService:authService)
    {}

    getAllCommentsAPI(bugid:number)
    {
         const token=this.authService.getCookie("access_token"); 
        return this.http.get(`http://localhost:5258/api/v2/BugComment/GetComments?bugId=${bugid}`,{
            headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'
    });

    }

    postCommentAPI(data:any)
    {
        const token=this.authService.getCookie("access_token"); 
        
        return this.http.post('http://localhost:5258/api/v1/BugComment/Comment',data,{
              headers: new HttpHeaders({
            'Authorization': `Bearer ${token}`
        }),
        observe: 'response'
    });

    }

    deleteCommentAPI(id:number)
    {
        const token=this.authService.getCookie("access_token"); 
        return this.http.delete(`http://localhost:5258/api/v2/BugComment/Delete?commentId=${id}`,{
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