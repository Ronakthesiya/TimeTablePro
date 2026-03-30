import { TimeTable } from "./CreateTimeTable.js";
import { Branch } from "./Models/Branch.js";
import { Faculty } from "./Models/Faculty.js";
import { Place } from "./Models/Place.js";
import { Student } from "./Models/Student.js";
import { Subject } from "./Models/Subject.js";

let Branchs = await Branch.getBranchs();
let Facultys = await Faculty.getFacultys(); 
let Places = await Place.getPlaces();
let Students = await Student.getStudents();
let Subjects = await Subject.getSubjects();

// display Branch, student, place
let displaysName = ["Branch", "Place"];

// list of Branchs, Students, Places
let displaysValues = [Branchs, Places];

// table hedders for Brach, Student, Places 
let hadders = ["<tr><th>Id</th><th>Branch</th></tr>",
                "<tr><th>Id</th><th>Place</th><th>Type of Place</th></tr>"];

// add the html to branch,student,place tab
for(let i=0;i<2;i++){
    const displaydivs = document.getElementsByClassName("display-"+displaysName[i])[0];
    let rows = "";
    displaysValues[i].forEach(item => rows += item.display()+" ");

    displaydivs.innerHTML = `<table class="display-table">${hadders[i]}${rows}</table>`
}

//display Faculty
for (let i = 0; i < Branchs.length; i++) {
    const displaydivs = document.getElementsByClassName("display-Faculty")[0];
    let rows = "";

    // fetch the name of subject from subject ids in facultys
    Facultys.forEach(item => {
        let sub = new Set();

        Subjects[Branchs[i].id].forEach(std => {
            if(item.subject.includes(std.id))
                sub.add(std.name);
        })

        // display by branchs
        if(Branchs[i].id == item.branch.id){
            rows += item.display(sub)+" "
        }
    });
    displaydivs.innerHTML += `<div class="primary-text">Faculty of ${Branchs[i].name} </div><table class="display-table"><tr><th>Id</th><th>Faculty</th><th>Subjects</th><th>Branch</th></tr>${rows}</table>`
}



//display Subject
for (let i = 0; i < Branchs.length; i++) {
    const displaydivs = document.getElementsByClassName("display-Subject")[0];
    let rows = "";

    //display branch vise
    Subjects[Branchs[i].id].forEach(item => rows += item.display()+" ");
    displaydivs.innerHTML += `<div class="primary-text">Subjects of ${Branchs[i].name} (Sloat Order)</div><table class="display-table"><tr><th>Subject</th><th>Branch</th><th>Place</th></tr>${rows}</table>`
}


// display student
Student.displayStudentList(Branchs,Students);


// sidebar
let selected = "Home";

if(localStorage.getItem("selected"))
    selected = localStorage.getItem("selected");

let tabs = ["Home","Branch","Student","Faculty","Place","Subject","AddBranch"];

const sidebar = document.getElementsByClassName("sidebar")[0];

// sidebar selection and tab change
sidebar.addEventListener("click",function(e){
    sidebar.children[tabs.indexOf(selected)].classList.remove('selected');
    const removeelement = document.getElementsByClassName(selected)[0];
    removeelement.classList.add("remove");

    selected = e.target.innerText;
    
    sidebar.children[tabs.indexOf(selected)].classList.add('selected');
    const addelement = document.getElementsByClassName(selected)[0];
    addelement.classList.remove("remove");
})



// used to store timetable html content on home page
let html = "";

// traverse each branch and create a timetable for that
for (let index = 0; index < Branchs.length; index++) {
    const branch = Branchs[index];

    // store the final timetable for current branch

    let finaltable = [];

    finaltable = await TimeTable.getTimeTable(branch.id);

    function cell(index) {
        if (!finaltable[index]) {
            return `<span class="free-slot">Free Slot</span>`;
        }
    
        return `
            ${finaltable[index].Subject.Name}<br/>
            ${finaltable[index].Faculty.Name}<br/>
            ${finaltable[index].Place.Name}
        `;
    }
    
    html += `
    <div class="primary-text">TimeTable of ${branch.name}</div>
    <table>
        <thead>
            <tr>
                <th>Time</th>
                <th>Monday</th>
                <th>Tuesday</th>
                <th>Wednesday</th>
                <th>Thursday</th>
                <th>Friday</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>7:00 to 8:00</td>
                <td>${cell(0)}</td>
                <td>${cell(6)}</td>
                <td>${cell(12)}</td>
                <td>${cell(18)}</td>
                <td>${cell(24)}</td>
            </tr>
    
            <tr>
                <td>8:00 to 9:00</td>
                <td>${cell(1)}</td>
                <td>${cell(7)}</td>
                <td>${cell(13)}</td>
                <td>${cell(19)}</td>
                <td>${cell(25)}</td>
            </tr>
    
            <tr>
                <td>9:00 to 10:00</td>
                <td>${cell(2)}</td>
                <td>${cell(8)}</td>
                <td>${cell(14)}</td>
                <td>${cell(20)}</td>
                <td>${cell(26)}</td>
            </tr>
    
            <tr>
                <td>10:00 to 10:30</td>
                <td colspan="5">Break</td>
            </tr>
    
            <tr>
                <td>10:30 to 11:30</td>
                <td>${cell(3)}</td>
                <td>${cell(9)}</td>
                <td>${cell(15)}</td>
                <td>${cell(21)}</td>
                <td>${cell(27)}</td>
            </tr>
    
            <tr>
                <td>11:30 to 12:30</td>
                <td>${cell(4)}</td>
                <td>${cell(10)}</td>
                <td>${cell(16)}</td>
                <td>${cell(22)}</td>
                <td>${cell(28)}</td>
            </tr>
    
            <tr>
                <td>12:30 to 1:30</td>
                <td>${cell(5)}</td>
                <td>${cell(11)}</td>
                <td>${cell(17)}</td>
                <td>${cell(23)}</td>
                <td>${cell(29)}</td>
            </tr>
        </tbody>
    </table>
    `;
    
};


$(".time-table").html(html)


// branch ddl
Branchs.forEach(
    branch => $(".AddStudent select")
            .append(`<option value=${branch.id}> ${branch.name} </option>`)
);


$(document).ready(function () {

    // add new student
    $(".AddStudent form").on("submit",async function (e) {
        e.preventDefault(); // Prevent page reload

        var name = $(".AddStudent input").eq(0).val().trim();
        var enrollment = $(".AddStudent input").eq(1).val().trim();
        var branch = $(".AddStudent select").val();

        // Basic validation
        if (name === "" || enrollment === "" || branch === "" || branch === null) {
            alert("Please fill all fields.");
            return;
        }

        await Student.addStudent(name,enrollment,branch);

        Students = await Student.getStudents();

        Student.displayStudentList(Branchs,Students);

        this.reset();
    });



    // add new place
    $(".AddPlace form").on("submit",async function (e) {
        e.preventDefault(); // Prevent page reload

        var name = $(".AddPlace input").eq(0).val().trim();
        var type = $(".AddPlace input").eq(1).val().trim();

        // Basic validation
        if (name === "" || type === "") {
            alert("Please fill all fields.");
            return;
        }

        await Place.addPlace(name,type);

        Places = await Place.getPlaces();
        Place.displayPlaceList(Places);

        this.reset();
    });
});

