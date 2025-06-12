const userData = {
    name: "Arun Balaji",
    email: "arun@gmail.com",
    age: 22
};

const outputList = document.querySelector(".output ul");
const title=document.querySelector(".output h2")

function displayUser(user,func) {
    outputList.innerHTML = ""; 
    title.innerHTML="";
    const { name, email, age } = user;

    title.innerHTML=func;
    outputList.innerHTML += `<li>Name: ${name}</li>`;
    outputList.innerHTML += `<li>Email: ${email}</li>`;
    outputList.innerHTML += `<li>Age: ${age}</li>`;
}

//callback

function getUserWithCallback(callback, func) {
    setTimeout(() => {
        callback(userData,func);
    }, 1000); 
}

document.getElementById("Callback").addEventListener("click", () => {
    getUserWithCallback(displayUser,"From Callback");
});

//promise
function getUserWithPromise() {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve(userData);
        }, 1000);
    });
}

document.getElementById("Promise").addEventListener("click", () => {
    getUserWithPromise().then((data) => {
        displayUser(data, "From Promise");
    });
});

//async
async function getUserAsync() {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve(userData);
        }, 1000);
    });
}

document.getElementById("Async").addEventListener("click", async () => {
    const user = await getUserAsync();
    displayUser(user,"From Async/Await");
});
