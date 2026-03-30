import { AjaxModel } from "../../JS/AjaxModel.js";
import { AjaxPostModel } from "../../JS/AjaxPostModel.js";
import { CallAPI } from "../../JS/CallAPI.js";

export class Branch {
    constructor(id,name) {
        this.id = id;
        this.name = name;
    }

    display(){
        return `<tr>
            <td>${this.id}</td>
            <td>${this.name}</td>
        </tr>`;
    }

    static async getBranchs() {

        return new Promise((resolve, reject) => {
            let getmodel = new AjaxModel(
                "Branch",
                "get",
                function (res) {
                    let list = res.data;
                    let branchs = [];
    
                    list.forEach(element => {
                        branchs.push(new Branch(element.Id, element.Name));
                    });
    
                    resolve(branchs);
                },
                function (xhr, status, error) {
                    console.log(status);
                    console.log(error);
                    
                    reject(error);
                }
            )
            CallAPI.get(getmodel);
        });
    }


    static async getBranch(branchId) {

        return new Promise((resolve, reject) => {
            let getmodel = new AjaxModel(
                `Branch/${branchId}`,
                "get",
                function (res) {
                    let branch = res.data;
                    resolve(branch);
                },
                function (xhr, status, error) {
                    console.log(status);
                    console.log(error);
                    
                    reject(error);
                }
            )
            CallAPI.get(getmodel);
        });
    }
    

    static async addBranch(branch){
        return new Promise((resolve,reject)=>{

            let postbranch = new AjaxPostModel(
                "Branch",
                "post",
                JSON.stringify({Name:branch}),
                function (res) {
                    let newbranch = res.data;
                    console.log(newbranch);
                    resolve(newbranch.Id);
                },
                function (xhr, status, error) {
                    console.log(status);
                    console.log(error);
                    
                    reject(error);
                }
            )

            CallAPI.post(postbranch);

            // $.ajax({
            //     url: "https://localhost:44311/api/Branch",
            //     type: "post",
            //     data: {Name:branch},
            //     success: function (res) {
            //         let newbranch = res.data;
            //         console.log(newbranch);
            //         resolve(newbranch.Id);
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