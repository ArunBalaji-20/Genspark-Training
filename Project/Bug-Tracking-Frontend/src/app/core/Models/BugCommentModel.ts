
export class BugCommentModel
{
    constructor(
        public comment:string="",
        public commentedOn:string="",
        public bugId:number=0,
        public commenterId:number=0,
        public commenterName:string=""

    )
    {

    }
}





// {
//     "$id": "1",
//     "$values": [
//         {
//             "$id": "2",
//             "comment": "Test comment from postman",
//             "commentedOn": "2025-06-25T06:46:58.59399Z",
//             "bugId": 21,
//             "commenterId": 1,
//             "commenterName": "Arun"
//         },
//         {
//             "$id": "3",
//             "comment": "Test comment2 from postman",
//             "commentedOn": "2025-06-25T06:47:04.796046Z",
//             "bugId": 21,
//             "commenterId": 1,
//             "commenterName": "Arun"
//         },
//         {
//             "$id": "4",
//             "comment": "Hello from tester mike",
//             "commentedOn": "2025-06-25T07:21:21.474322Z",
//             "bugId": 21,
//             "commenterId": 12,
//             "commenterName": "Mike"
//         }
//     ]
// }