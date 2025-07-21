import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";

@Injectable()
export class UploadService
{
    private http=inject(HttpClient);
    constructor()
    {}

    getlist()
    {
        return this.http.get('http://localhost:5046/api/Videos',{
            observe:'response'
        });
    }

    uploadVideo(file:any)
    {
        return this.http.post('http://localhost:5046/api/Videos/upload',file,{
            observe:'response'
        });
    }
}