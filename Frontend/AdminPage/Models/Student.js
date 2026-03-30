import { AjaxModel } from "../../JS/AjaxModel.js";
import { AjaxPostModel } from "../../JS/AjaxPostModel.js";
import { CallAPI } from "../../JS/CallAPI.js";
import { Branch } from "./Branch.js";

export class Student {
    constructor(name, id, enrollment, branch) {
        this.name = name;
        this.id = id;
        this.enrollment = enrollment;
        this.branch = branch;
    }

    display(){
        return `<tr>
            <td>${this.id}</td>
            <td>${this.name}</td>
            <td>${this.enrollment}</td>
            <td>${this.branch.name}</td>
        </tr>`;
    }

    static displayStudentList(Branchs,Students){
        const displaydivs = document.getElementsByClassName("display-Student")[0];
        displaydivs.innerHTML = "";
        for (let i = 0; i < Branchs.length; i++) {
            
            let rows = "";
        
            //display branch vise
            Students.forEach(item => {
        
                // display by branchs
                if(Branchs[i].id == item.branch.id){
                    rows += item.display()+" "
                }
            });
            displaydivs.innerHTML += `<div class="primary-text">Students of ${Branchs[i].name} </div>
            <table class="display-table"><tr><th>Id</th><th>Student</th><th>Enrollment</th><th>Brach</th></tr>${rows}</table>`
        }
    }

    static async getStudents(){
        return new Promise((resolve,reject)=>{
            
            let getModel = new AjaxModel(
                "Student",
                "get",
                function (res) {
                    let list = res.data;
                    let Students = [];

                    list.forEach(element => {
                        let branch = new Branch(element.Branch.Id,element.Branch.Name);
                        let student = new Student(element.Name,element.Id,element.Enrollment,branch);    
                        Students.push(student)
                    });

                    resolve(Students);
                },
                function (xhr, status, error) {
                    console.log(status);
                    console.log(error);
                    
                    reject(error);
                }
            )

            CallAPI.get(getModel)
        
        })
    }

    static async addStudent(name,enrollment,branchId){
        return new Promise((resolve,reject)=>{
            let postModel = new AjaxPostModel(
                "Student",
                "post",
                JSON.stringify({Name: name,Enrollment: enrollment,BranchId: branchId}),
                function (res) {
                    resolve(res.data);
                },
                function (xhr, status, error) {
                    console.log(status);
                    console.log(error);
                    
                    reject(error);
                }
            );

            CallAPI.post(postModel)
        });
    }
}