import { TimeTable } from "../AdminPage/CreateTimeTable.js";

let selected = "Home"
let tabs = ["Home","Profile"];

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
const branchid = Number(sessionStorage.getItem("bid"));

// store the final timetable for current branch
let finaltable = [];

finaltable = await TimeTable.getTimeTable(branchid);

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
    <div class="primary-text">TimeTable </div>
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



$(".time-table").html(html)