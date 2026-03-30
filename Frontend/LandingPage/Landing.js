import { AjaxModel } from "../JS/AjaxModel.js";
import { AjaxPostModel } from "../JS/AjaxPostModel.js";
import { CallAPI } from "../JS/CallAPI.js";

const loginform = document.querySelector(".dark-bg");
document.querySelector(".login-btn").addEventListener("click",()=>{

  loginform.classList.toggle("d-none");
})

document.querySelector(".login-page").addEventListener("click",(e)=>{
  e.stopImmediatePropagation();
})

document.querySelector(".dark-bg").addEventListener("click",()=>{
  loginform.classList.toggle("d-none");
})

let formbtn = document.querySelector(".submit-form");

formbtn.addEventListener("click",(e)=>{
  e.preventDefault();

  let username = document.querySelector("#username").value.trim();
  let email = document.querySelector("#email").value.trim();
  let password = document.querySelector("#password").value;

  if(username==null || username == "" || email==null || email == "" || password==null || password==""){
    alert("all feilds are required");
    return;
  }

  $(".submit-form").prop('disabled', true).text('Login...');

  let loginpost = new AjaxPostModel(
      "login",
      "POST",
      JSON.stringify({ Username: username, Email: email, Password: password }),
      function (res,textStatus,jqXHR) {
        console.log(res);
        console.log(textStatus);
        console.log(jqXHR.getAllResponseHeaders());
        if (res.token && res.role) {
          sessionStorage.setItem("token", res.token);
          sessionStorage.setItem("role", res.role);
          sessionStorage.setItem("id", res.id);
          sessionStorage.setItem("bid", res.bid);
          sessionStorage.setItem("name", username);
  
          if(res.role)
            window.location.href = `http://127.0.0.1:5508/TimeTable/Frontend/${res.role}Page/Home.html`;
        } else {
          alert("Login failed. Please try again.");
        }
      },
      function (xhr, status, error) {
        console.log(status);
        console.log(xhr.status);
        console.log(xhr);
  
        alert("Login failed. Please check your credentials and try again.");
  
        $(".submit-form").prop('disabled', false).text('Login');
      }
  );

  CallAPI.post(loginpost);
})