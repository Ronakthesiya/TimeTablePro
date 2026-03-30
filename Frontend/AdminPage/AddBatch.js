import { Branch } from "./Models/Branch.js";
import { Subject } from "./Models/Subject.js";
import { Faculty } from "./Models/Faculty.js";
import { TimeTable } from "./CreateTimeTable.js";
import { AjaxPostModel } from "../JS/AjaxPostModel.js";
import { CallAPI } from "../JS/CallAPI.js";

const subjects = [];
const faculty = [];

$("#addSubject").on("click",addSubject);
$("#addFaculty").on("click",addFaculty);

// add subject in list subjects
function addSubject() {
    const name = subject_name.value.trim();
    const place = subject_place.value.trim();

    if (!name || !place) return;

    subjects.push({ name, place });

    // also add subject to UI
    const row = document.createElement("tr");
    row.innerHTML = `
        <td>${subjects.length}</td>
        <td>${name}</td>
        <td>${place}</td>
    `;
    subjectTableBody.appendChild(row);

    const option = document.createElement("option");
    option.value = subjects.length-1;
    option.textContent = name;
    faculty_subject.appendChild(option);

    subject_name.value = "";
    subject_place.value = "";
}

// add faculty to faculty list
function addFaculty() {
    const name = faculty_name.value.trim();
    let subject = "";
    let subjectsId = [];

    for(let i=0;i<faculty_subject.selectedOptions.length;i++){
        subject += faculty_subject.selectedOptions[i].textContent+"<br>";
        subjectsId.push(faculty_subject.selectedOptions[i].value)
    }

    if (!name || !subject) return;

    for (let i = faculty_subject.selectedOptions.length - 1; i >= 0; i--) {
        faculty_subject.selectedOptions[i].remove();  
    }
      

    faculty.push({ name, subjectsId });


    // add to UI
    const row = document.createElement("tr");
    row.innerHTML = `
        <td>${faculty.length}</td>
        <td>${name}</td>
        <td>${subject}</td>
    `;
    facultyTableBody.appendChild(row);

    faculty_name.value = "";
}

// handle branch submit 
document.getElementById("branchForm").addEventListener("submit",async function (e){
    e.preventDefault();

    const data = {
        branch: branch_name.value,
        subjects,
        faculty
    };

    if(!data.branch){
        alert("Enter Branch");
        return;
    }

    if(!data.subjects.length){
        alert("Enter at least one subject");
        return;
    }

    if(!data.faculty.length || faculty_subject.length){
        alert("each subjects need to asign a faculty");
        return;
    }

    // add branch into database
    let branchId = await Branch.addBranch(data.branch);

    // add all subjects into database
    let subjectIds = await Subject.addSubjects(subjects, branchId);

    // add all faculty into database
    let facultyIds = await Faculty.addFaculty(faculty, branchId);


    // crate the list of faculty vise subjects Id
    let facultysubjects = [];

    for(let i=0;i<faculty.length;i++){
        faculty[i].subjectsId.forEach((subId) => {
            facultysubjects.push({
                FacultyId: facultyIds[i],
                SubjectId: subjectIds[subId]
            });
        });
    }

    // add faculty vise subject to databse
    let postModel = new AjaxPostModel(
        "FacultySubject",
        "post",
        JSON.stringify(facultysubjects),
        function (res) {
            console.log(res.data);
        },
        function (xhr, status, error) {
            console.log(status);
            console.log(error);
        }
    )

    CallAPI.post(postModel);

    await TimeTable.createAndStore(branchId);

    window.location.reload();
});