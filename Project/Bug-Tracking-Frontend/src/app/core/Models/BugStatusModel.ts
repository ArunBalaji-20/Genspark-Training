


export class BugStatusModel {
    constructor(
        public bugId:number=0,
        public bugName: string = "",
        public description: string = "",
        public screenshot: string = "",
        public cvssScore: number = 0,
        public submittedOn:string="",
        public resolvedAt:string="",
        public status:string="",
        public submittedById:number=0
    ) {}
}

// "bugId": 1,
//             "bugName": "SQL Injection",
//             "description": "Time based Sql Injection found in dashboard",
//             "screenshot": "/Users/presidio/Documents/Repo/june6-project/Bug-Tracking-System/BugTrackingAPI/screenshots/bf7eae7a-9216-4f68-9337-9ec934b1a881.png",
//             "cvssScore": 9,
//             "submittedOn": "2025-06-09T14:34:14.673572Z",
//             "resolvedAt": "2025-06-09T17:51:52.385667Z",
//             "status": "Resolved",
//             "submittedById": 1