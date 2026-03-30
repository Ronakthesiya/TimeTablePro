import { AjaxModel } from "../../JS/AjaxModel.js";
import { AjaxPostModel } from "../../JS/AjaxPostModel.js";
import { CallAPI } from "../../JS/CallAPI.js";
import { Structure } from "../Structure.js";
import { Branch } from "./Branch.js";

export class Faculty {
    constructor(name, id, branch, subject, isAvalable) {
        this.name = name;
        this.id = id;
        this.branch = branch;
        this.subject = subject;
        this.isAvalable = isAvalable;
    }
    
    display(subs){

        let displaysub = [];

        for (const sub of subs) {
            displaysub.push(sub)
        }
        return `<tr>
            <td>${this.id}</td>
            <td>${this.name}</td>
            <td>${displaysub.join("<br>")}</td>
            <td>${this.branch.name}</td>
        </tr>`;
    }

    static async getFacultys(){

        return new Promise((resolve,reject)=>{

            let getmodel = new AjaxModel(
                "Faculty",
                "get",
                function (res) {

                    let list = res.data;
                    let facultys = [];
    
                    list.forEach(element => {
                        let branch = new Branch(element.Branch.Id,element.Branch.Name);
                        let faculty = new Faculty(
                            element.Name,
                            element.Id,
                            branch,
                            element.Subjects.map(function (sub) {
                                return sub.Id;
                            }),
                            element.AvailableTime);    
                        facultys.push(faculty)
                    });

                    resolve(facultys);
                },
                function (xhr, status, error) {
                    console.log(status);
                    console.log(error);
                    
                    reject(error);
                }
            )

            CallAPI.get(getmodel);

            // $.ajax({
            //     url: "https://localhost:44311/api/Faculty",
            //     type: "get",
            //     success: function (res) {

            //         let list = res.data;
            //         let facultys = [];
    
            //         list.forEach(element => {
            //             let branch = new Branch(element.Branch.Id,element.Branch.Name);
            //             let faculty = new Faculty(
            //                 element.Name,
            //                 element.Id,
            //                 branch,
            //                 element.Subjects.map(function (sub) {
            //                     return sub.Id;
            //                 }),
            //                 element.AvailableTime);    
            //             facultys.push(faculty)
            //         });

            //         resolve(facultys);
            //     },
            //     error: function (xhr, status, error) {
            //         console.log(status);
            //         console.log(error);
                    
            //         reject(error);
            //     }
            // });
        })

    }

    static async addFaculty(facultys,branchId){
        let facs = [];

        facultys.forEach(faculty => {
            facs.push({
                Name: faculty.name, 
                BranchId: branchId,
                AvailableTime: "000000000000000000000000000000"
            })
        });

        return new Promise((resolve,reject)=>{

            let postmodel = new AjaxPostModel(
                "Faculty",
                "post",
                JSON.stringify(facs),
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
            //     url: "https://localhost:44311/api/Faculty",
            //     type: "post",
            //     headers: {
            //         'Content-Type': 'application/json'
            //     },
            //     data: JSON.stringify(facs),
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

    static async updateFacultyAvailable(facultys,branchId){
        let data = [];

        facultys.forEach(facs => {
            if(facs.branch.id == branchId){
                data.push({
                    Id: facs.id,
                    AvailableTime: Structure.availableToStr(facs.isAvalable)
                })
            }
        })

        return new Promise((resolve,reject)=>{
            let patchModel = new AjaxPostModel(
                "Faculty/AvailableTime",
                "PATCH",
                JSON.stringify(data),
                function (res) {
                    let cnt = res.data;
                    console.log(cnt);

                    resolve(cnt);
                },
                function (xhr, status, error) {
                    console.log(status);
                    console.log(error);
                    
                    reject(error);
                }
            )

            CallAPI.post(patchModel);

            // $.ajax({
            //     url: "https://localhost:44311/api/Faculty/AvailableTime",
            //     type: "PATCH",
            //     headers: {
            //         'Content-Type': 'application/json'
            //     },
            //     data: JSON.stringify(data),
            //     success: function (res) {
            //         let cnt = res.data;
            //         console.log(cnt);

            //         resolve(cnt);
            //     },
            //     error: function (xhr, status, error) {
            //         console.log(status);
            //         console.log(error);
                    
            //         reject(error);
            //     }
            // });
        })
    }
}