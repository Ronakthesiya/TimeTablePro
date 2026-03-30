import { AjaxModel } from "../../JS/AjaxModel.js";
import { AjaxPostModel } from "../../JS/AjaxPostModel.js";
import { CallAPI } from "../../JS/CallAPI.js";
import { Branch } from "./Branch.js";
import { Student } from "./Student.js";

export class Subject {
    constructor(name, id, branch, place, perWeekClass) {
        this.name = name;
        this.id = id;
        this.branch = branch;
        this.place = place;
        this.perWeekClass = perWeekClass;
    }

    display(){
        return `<tr>
            <td>${this.name}</td>
            <td>${this.branch.name}</td>
            <td>${this.place}</td>
        </tr>`;
    }

    static async getSubjects(){

        return new Promise((resolve,reject)=>{

            let getModel = new AjaxModel(
                "Subject",
                "get",
                function (res) {
                    let list = res.data;

                    let Subjects = {};

                    list.forEach(element=>{
                        let branch = new Branch(element.Branch.Id,element.Branch.Name);
                        let subject = new Subject(element.Name,element.Id,branch,element.PlaceType,element.PerWeekClass);    
                        if(!Subjects[branch.id]){
                            Subjects[branch.id] = [];
                        }
                        Subjects[branch.id].push(subject)
                    })

                    resolve(Subjects);
                },
                function (xhr, status, error) {
                    console.log(status);
                    console.log(error);
                    
                    reject(error);
                }
            )

            CallAPI.get(getModel)

            // $.ajax({
            //     url: "https://localhost:44311/api/Subject",
            //     type: "get",
            //     success: function (res) {
            //         let list = res.data;

            //         let Subjects = {};

            //         list.forEach(element=>{
            //             let branch = new Branch(element.Branch.Id,element.Branch.Name);
            //             let subject = new Subject(element.Name,element.Id,branch,element.PlaceType,element.PerWeekClass);    
            //             if(!Subjects[branch.id]){
            //                 Subjects[branch.id] = [];
            //             }
            //             Subjects[branch.id].push(subject)
            //         })

            //         resolve(Subjects);
            //     },
            //     error: function (xhr, status, error) {
            //         console.log(status);
            //         console.log(error);
                    
            //         reject(error);
            //     }
            // });
        })

    }

    static async addSubjects(subjects,branchId){
        console.log(subjects);

        let subs = [];

        subjects.forEach(subject => {
            subs.push({
                Name:subject.name , 
                BranchId: branchId,
                PlaceType: subject.place,
                PerWeekClass: 1
            })
        });

        return new Promise((resolve,reject)=>{
            let postmodel = new AjaxPostModel(
                "Subject",
                "post",
                JSON.stringify(subs),
                function (res) {
                    let ids = res.data;
                    console.log(ids);

                    resolve(ids);
                },
                function (xhr, status, error) {
                    console.log(status);
                    console.log(error);
                    
                    reject(error);
                }
            )

            CallAPI.post(postmodel);

            // $.ajax({
            //     url: "https://localhost:44311/api/Subject",
            //     type: "post",
            //     headers: {
            //         'Content-Type': 'application/json'
            //     },
            //     data: JSON.stringify(subs),
            //     success: function (res) {
            //         let ids = res.data;
            //         console.log(ids);

            //         resolve(ids);
            //     },
            //     error: function (xhr, status, error) {
            //         console.log(status);
            //         console.log(error);
                    
            //         reject(error);
            //     }
            // });
        });
    }
}
