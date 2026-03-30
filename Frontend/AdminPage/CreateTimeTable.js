import { Structure } from "./Structure.js";
import { Faculty } from "./Models/Faculty.js";
import { Place } from "./Models/Place.js";
import { Subject } from "./Models/Subject.js";
import { AjaxPostModel } from "../JS/AjaxPostModel.js";
import { CallAPI } from "../JS/CallAPI.js";
import { AjaxModel } from "../JS/AjaxModel.js";


export class TimeTable{

    // use to store the timetable
    static finalresult = [];

    // recursive function book places,faculty for specific subject ans branch 
    static async createTimeTable(subjectId,cntWeek,sloats,subjects,places,facultys){
        // stop the current subject if perWeekclass is assinged
        if(subjects[subjectId].perWeekClass == cntWeek){
            // move to next subject
            subjectId++;

            //if all subject is complated
            if(subjectId>=subjects.length){
                for (let i = 0; i < sloats.length; i++) {
                    this.finalresult.push(sloats[i]);
                }

                return true;
            }

            // call for next subject
            await this.createTimeTable(subjectId,0,sloats,subjects,places,facultys)
            return true;
        }

        // base condition to avoide infinite recursion
        if(subjectId>=subjects.length){
            return true;
        }

        // get the faculty for cur subject
        let facultysForSubject = TimeTable.findFacultyForSubject(subjects[subjectId],facultys);
        // get the place for cur subject place
        let placesForSubject = TimeTable.findPlacesForSubject(subjects[subjectId],places);

        // got to all sloat and find empty sloat
        for(let sloat=0;sloat<30;sloat++){
            if(!sloats[sloat]){
                //places
                for(let place=0;place<placesForSubject.length;place++){
                    if(!placesForSubject[place].isAvalable[sloat]){
                        //faculty
                        for (let faculty = 0; faculty < facultysForSubject.length; faculty++) {
                            if(!facultysForSubject[faculty].isAvalable[sloat]){
                                
                                // book the sloat, faculty, place
                                sloats[sloat] = [subjects[subjectId],facultysForSubject[faculty],placesForSubject[place]];
                                facultysForSubject[faculty].isAvalable[sloat] = true;
                                placesForSubject[place].isAvalable[sloat] = true;

                                if(await this.createTimeTable(subjectId,cntWeek+1,sloats,subjects,places,facultys)){
                                    return true;
                                }

                                // free the sloat, faculty, place
                                sloats[sloat] = null;
                                facultysForSubject[faculty].isAvalable[sloat] = false;
                                placesForSubject[place].isAvalable[sloat] = false;
                            }
                        }
                    }
                }
            }
        }
    }


    // function to find facultys for cur subject 
    static findFacultyForSubject(subject, facultys){
        let FacultyList = [];

        facultys.forEach(faculty => {
            for (let i = 0; i < faculty.subject.length; i++) {
                const sub = faculty.subject[i];
                if(sub == subject.id && faculty.branch.name == subject.branch.name){
                    FacultyList.push(faculty);
                    break;
                }
            }
        });

        return FacultyList;
    }

    // function to find places for cur subject 

    static findPlacesForSubject(subject, places){
        let PlacesList = [];

        for (let i = 0; i  < places.length; i++) {
            // console.log(places[i].type, subject.place);
            if(places[i].type == subject.place){
                PlacesList.push(places[i]);
            }
        }

        // console.log("places" , PlacesList);
        return PlacesList;
    }

    // create timetable and store to db
    static async createAndStore(branchId){
        
        let Facultys = await Faculty.getFacultys(); 
        let Places = await Place.getPlaces();
        let Subjects = await Subject.getSubjects();

        // call algo to create timeteble
        await TimeTable.createTimeTable(0,0,[],Subjects[branchId],Places,Facultys);

        // chech if timetable is created or not
        if(TimeTable.finalresult.length <= 0){
            alert("faculty or places more required");   
            console.log("table is not possible");
            return;
        }

        // add timetable into this array
        let finaltable = [];
        await TimeTable.finalresult.forEach(ele => finaltable.push(ele));

        // store time table in database
        await TimeTable.storeTimeTable(finaltable,branchId);

        // update the availability of faculty and plae based on this time table 
        await Faculty.updateFacultyAvailable(Facultys,branchId);
        await Place.updatePlaceAvailable(Places);

        this.finalresult = [];
    }

    // call api and store timetable in database
    static async storeTimeTable(timetable,branchId){
        let slots = [];
        
        for(let i=0;i<timetable.length;i++){
            slots.push({
                BranchId: branchId,
                SlotNumber: i,
                DayOfWeek: Structure.findDay(i),
                TimeStart: Structure.findStartTime(i),
                TimeEnd: Structure.findEndTime(i),
                SubjectId: timetable[i][0].id,
                FacultyId: timetable[i][1].id,
                PlaceId: timetable[i][2].id
            })
        }

        return new Promise((resolve,reject)=>{

            let postModel = new AjaxPostModel(
                "Slot",
                "post",
                JSON.stringify(slots),
                function (res) {
                    console.log(res.data);
                    resolve(res.data);
                },
                function (xhr, status, error) {
                    console.log(status);
                    console.log(error);
                    
                    reject(error);
                }
            )

            CallAPI.post(postModel);

            // $.ajax({
            //     url: "https://localhost:44311/api/Slot",
            //     type: "post",
            //     headers: {
            //         'Content-Type': 'application/json'
            //     },
            //     data: JSON.stringify(slots),
            //     success: function (res) {
            //         console.log(res.data);
            //         resolve(res.data);
            //     },
            //     error: function (xhr, status, error) {
            //         console.log(status);
            //         console.log(error);
                    
            //         reject(error);
            //     }
            // });
        })
    }

    // get the time table from database
    static async getTimeTable(branchId){
        return new Promise((resolve,reject)=>{

            let getModel = new AjaxModel(
                `Slot/branch/${branchId}`,
                "get",
                function (res) {
                    let slots = res.data;
                    resolve(slots);
                },
                function (xhr, status, error) {
                    console.log(status);
                    console.log(error);
                    
                    reject(error);
                }
            )

            CallAPI.get(getModel);
            
            // $.ajax({
            //     url: `https://localhost:44311/api/Slot/branch/${branchId}`,
            //     type: "get",
            //     success: function (res) {
            //         let slots = res.data;
            //         resolve(slots);
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



