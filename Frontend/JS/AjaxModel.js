export class AjaxModel{
    constructor(endpoint,type,success,error){
        this.url = "https://localhost:44311/api/" + endpoint;
        this.type = type;
        this.success = success;
        this.error = function (xhr, status, error) {
            console.log(xhr.status);
            console.log(xhr);
            alert(xhr.responseJSON);  

            if(xhr.status == "401"){
                window.location.href = "http://127.0.0.1:5508/TimeTable/Frontend/LandingPage/Landing.html"
            }
        };
        this.headers = {
            "Authorization": `Bearer ${sessionStorage.getItem("token")}`
        };
    }
}

