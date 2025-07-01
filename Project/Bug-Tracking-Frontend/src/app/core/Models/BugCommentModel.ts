
export class BugCommentModel
{
    constructor(
        public commentId:number=0,
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
//   "$id": "1",
//   "$values": [
//     {
//       "$id": "2",
//       "commentId": 10,
//       "comment": "Test comment from postman",
//       "commentedOn": "2025-06-25T06:46:58.59399Z",
//       "bugId": 21,
//       "commenterId": 1,
//       "commenterName": "Arun"
//     },
//     {
//       "$id": "3",
//       "commentId": 11,
//       "comment": "Test comment2 from postman",
//       "commentedOn": "2025-06-25T06:47:04.796046Z",
//       "bugId": 21,
//       "commenterId": 1,
//       "commenterName": "Arun"
//     },
//     {
//       "$id": "4",
//       "commentId": 13,
//       "comment": "Hello from tester mike",
//       "commentedOn": "2025-06-25T07:21:21.474322Z",
//       "bugId": 21,
//       "commenterId": 12,
//       "commenterName": "Mike"
//     },
//     {
//       "$id": "5",
//       "commentId": 14,
//       "comment": "hello from frontend",
//       "commentedOn": "2025-06-25T13:53:41.896648Z",
//       "bugId": 21,
//       "commenterId": 1,
//       "commenterName": "Arun"
//     },
//     {
//       "$id": "6",
//       "commentId": 15,
//       "comment": "hello once again",
//       "commentedOn": "2025-06-25T13:54:47.457944Z",
//       "bugId": 21,
//       "commenterId": 1,
//       "commenterName": "Arun"
//     },
//     {
//       "$id": "7",
//       "commentId": 18,
//       "comment": "hiii",
//       "commentedOn": "2025-06-25T13:59:31.257785Z",
//       "bugId": 21,
//       "commenterId": 1,
//       "commenterName": "Arun"
//     },
//     {
//       "$id": "8",
//       "commentId": 19,
//       "comment": "hola",
//       "commentedOn": "2025-06-26T09:12:00.765276Z",
//       "bugId": 21,
//       "commenterId": 1,
//       "commenterName": "Arun"
//     },
//     {
//       "$id": "9",
//       "commentId": 21,
//       "comment": "hello from dev",
//       "commentedOn": "2025-06-27T09:17:34.507943Z",
//       "bugId": 21,
//       "commenterId": 4,
//       "commenterName": "balaDev"
//     }
//   ]
// }