import { AjaxModel } from "../../JS/AjaxModel.js";
import { AjaxPostModel } from "../../JS/AjaxPostModel.js";
import { CallAPI } from "../../JS/CallAPI.js";
import { Structure } from "../Structure.js";

export class Place {
    constructor(id, name, type,isAvalable) {
        this.id = id;
        this.name = name;
        this.type = type;
        this.isAvalable = isAvalable;
    }
    
    display(){
        return `<tr>
            <td>${this.id}</td>
            <td>${this.name}</td>
            <td>${this.type}</td>
        </tr>`;
    }
    
    static displayPlaceList(places){
        const displaydivs = document.getElementsByClassName("display-Place")[0];
        let rows = "";
        places.forEach(item => rows += item.display()+" ");

        displaydivs.innerHTML = `<table class="display-table"><tr><th>Id</th><th>Place</th><th>Type of Place</th></tr>${rows}</table>`
    }

    static async getPlaces(){

        return new Promise((resolve,reject)=>{
            let getModel = new AjaxModel(
                "Place",
                "get",
                function (res) {
                    let list = res.data;
                    let places = [];

                    list.forEach(element => {
                        places.push(new Place(element.Id,element.Name,element.Type,element.AvailableTime))    
                    });
    
                    resolve(places);
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

    static async updatePlaceAvailable(Places){
        let data = [];

        Places.forEach(place => {
            data.push({
                Id: place.id,
                AvailableTime: Structure.availableToStr(place.isAvalable)
            })
        })

        return new Promise((resolve,reject)=>{
            let patchModel =  new AjaxPostModel(
                "Place/AvailableTime",
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

        })
    }

    static async addPlace(name,type){
        return new Promise((resolve,reject)=>{
            let postModel =  new AjaxPostModel(
                "Place",
                "post",
                JSON.stringify({Name:name,Type:type}),
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

            CallAPI.post(postModel);

        })
    }
}