
export class UserAddModel
{
    constructor(public firstName:string="",public lastName:string="",public gender:string="",public age:number=0,public role:string="",public state:string="")
    {}
}

// fetch('https://dummyjson.com/users/add', {method: 'POST',headers: { 'Content-Type': 'application/json' },body: JSON.stringify({firstName: 'Muhammad',lastName: 'Ovi',age: 250,/* other user data */})}).then(res => res.json()).then(console.log);
// Please use the above to add a user. 