import { AjaxModel } from "./AjaxModel.js";

export class AjaxPostModel extends AjaxModel{
    constructor(endpoint,type,data,success,error){
        super(endpoint,type,success,error);
        this.headers = {
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${sessionStorage.getItem("token")}`
        };
        this.data = data;
    }
}