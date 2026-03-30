import { Branch } from "./Models/Branch.js";
import { Faculty } from "./Models/Faculty.js";
import { Subject } from "./Models/Subject.js";
import { Place } from "./Models/Place.js";
import { Student } from "./Models/Student.js";

// export let Branchs = [new Branch(1,"CSE"), new Branch(2,"IT"), new Branch(3,"Civil")];
export let Branchs = [new Branch(1,"CSE"), new Branch(2,"IT")];

localStorage.setItem("Branchs",JSON.stringify(Branchs))

export let Students = [new Student("s1",1,"123123123",Branchs[0]),
                new Student("s2",2,"123123123",Branchs[0]),
                new Student("s3",3,"123123123",Branchs[0])];

localStorage.setItem("Students",JSON.stringify(Students))

export let Places = [new Place(1,"A101","class"),
                new Place(2,"A102","class"),
                new Place(3,"A103","class"),
                new Place(4,"A104","class"),
                new Place(5,"A105","class"),
                new Place(6,"A106","class"),
                new Place(7,"A1","audi"),
                new Place(8,"A2","audi"),
                new Place(9,"L1","lab"),
                new Place(10,"L2","lab"),
                new Place(11,"L3","lab"),
                new Place(12,"L4","lab")]

localStorage.setItem("Places",JSON.stringify(Places))

// branch id vise subject list 
export let Subjects = {
    1 : 
    [
       new Subject("WD LAB", 6, Branchs[0], "lab", 1),
       new Subject("JS LAB", 2, Branchs[0], "lab", 1),
       new Subject("OOP", 3, Branchs[0], "class", 1),
       new Subject("C# LAB", 8, Branchs[0], "lab", 1),
       new Subject("C# LAB", 8, Branchs[0], "lab", 1),
       new Subject("OOP LAB", 4, Branchs[0], "lab", 1),
       new Subject("JS", 1, Branchs[0], "class", 1),
       new Subject("C# LAB", 8, Branchs[0], "lab", 1),
       new Subject("WD LAB", 6, Branchs[0], "lab", 1),
       new Subject("JS LAB", 2, Branchs[0], "lab", 1),
       new Subject("OOP LAB", 4, Branchs[0], "lab", 1),
       new Subject("C#", 7, Branchs[0], "class", 1),
       new Subject("OOP", 3, Branchs[0], "class", 1),
       new Subject("WD", 5, Branchs[0], "class", 1),
       new Subject("C#", 7, Branchs[0], "class", 1),
       new Subject("JS", 1, Branchs[0], "class", 1),
       new Subject("C#", 7, Branchs[0], "class", 1),
       new Subject("WD", 5, Branchs[0], "class", 1),
       new Subject("JS", 1, Branchs[0], "class", 1),
       new Subject("WD LAB", 6, Branchs[0], "lab", 1),
       new Subject("OOP LAB", 4, Branchs[0], "lab", 1),
       new Subject("C#", 7, Branchs[0], "class", 1),
       new Subject("OOP", 3, Branchs[0], "class", 1),
       new Subject("C#", 7, Branchs[0], "class", 1),
       new Subject("JS LAB", 2, Branchs[0], "lab", 1),
       new Subject("WD", 5, Branchs[0], "class", 1),
       new Subject("OOP LAB", 4, Branchs[0], "lab", 1),
       new Subject("JS", 1, Branchs[0], "class", 1),
       new Subject("JS", 1, Branchs[0], "class", 1),
       new Subject("C# LAB", 8, Branchs[0], "lab", 1)
   ],
    2 : 
    [
        new Subject("WDD LAB", 6, Branchs[1], "lab", 1),
        new Subject("JSS LAB", 2, Branchs[1], "lab", 1),
        new Subject("OOPP", 3, Branchs[1], "class", 1),
        new Subject("C## LAB", 8, Branchs[1], "lab", 1),
        new Subject("C## LAB", 8, Branchs[1], "lab", 1),
        new Subject("OOPP LAB", 4, Branchs[1], "lab", 1),
        new Subject("JSS", 1, Branchs[1], "class", 1),
        new Subject("C## LAB", 8, Branchs[1], "lab", 1),
        new Subject("WDD LAB", 6, Branchs[1], "lab", 1),
        new Subject("JSS LAB", 2, Branchs[1], "lab", 1),
        new Subject("OOPP LAB", 4, Branchs[1], "lab", 1),
        new Subject("C##", 7, Branchs[1], "class", 1),
        new Subject("OOPP", 3, Branchs[1], "class", 1),
        new Subject("WDD", 5, Branchs[1], "class", 1),
        new Subject("C##", 7, Branchs[1], "class", 1),
        new Subject("JSS", 1, Branchs[1], "class", 1),
        new Subject("C##", 7, Branchs[1], "class", 1),
        new Subject("WDD", 5, Branchs[1], "class", 1),
        new Subject("JSS", 1, Branchs[1], "class", 1),
        new Subject("WDD LAB", 6, Branchs[1], "lab", 1),
        new Subject("OOPP LAB", 4, Branchs[1], "lab", 1),
        new Subject("C##", 7, Branchs[1], "class", 1),
        new Subject("OOPP", 3, Branchs[1], "class", 1),
        new Subject("C##", 7, Branchs[1], "class", 1),
        new Subject("JSS LAB", 2, Branchs[1], "lab", 1),
        new Subject("WDD", 5, Branchs[1], "class", 1),
        new Subject("OOPP LAB", 4, Branchs[1], "lab", 1),
        new Subject("JSS", 1, Branchs[1], "class", 1),
        new Subject("JSS", 1, Branchs[1], "class", 1),
        new Subject("C## LAB", 8, Branchs[1], "lab", 1)
    ]
    // 3:[
    //     new Subject("WDD LAB", 6, Branchs[2], "lab", 1),
    //     new Subject("JSS LAB", 2, Branchs[2], "lab", 1),
    //     new Subject("OOPP", 3, Branchs[2], "class", 1),
    //     new Subject("C## LAB", 8, Branchs[2], "lab", 1),
    //     new Subject("C## LAB", 8, Branchs[2], "lab", 1),
    //     new Subject("OOPP LAB", 4, Branchs[2], "lab", 1),
    //     new Subject("JSS", 1, Branchs[2], "class", 1),
    //     new Subject("C## LAB", 8, Branchs[2], "lab", 1),
    //     new Subject("WDD LAB", 6, Branchs[2], "lab", 1),
    //     new Subject("JSS LAB", 2, Branchs[2], "lab", 1),
    //     new Subject("OOPP LAB", 4, Branchs[2], "lab", 1),
    //     new Subject("C##", 7, Branchs[2], "class", 1),
    //     new Subject("OOPP", 3, Branchs[2], "class", 1),
    //     new Subject("WDD", 5, Branchs[2], "class", 1),
    //     new Subject("C##", 7, Branchs[2], "class", 1),
    //     new Subject("JSS", 1, Branchs[2], "class", 1),
    //     new Subject("C##", 7, Branchs[2], "class", 1),
    //     new Subject("WDD", 5, Branchs[2], "class", 1),
    //     new Subject("JSS", 1, Branchs[2], "class", 1),
    //     new Subject("WDD LAB", 6, Branchs[2], "lab", 1),
    //     new Subject("OOPP LAB", 4, Branchs[2], "lab", 1),
    //     new Subject("C##", 7, Branchs[2], "class", 1),
    //     new Subject("OOPP", 3, Branchs[2], "class", 1),
    //     new Subject("C##", 7, Branchs[2], "class", 1),
    //     new Subject("JSS LAB", 2, Branchs[2], "lab", 1),
    //     new Subject("WDD", 5, Branchs[2], "class", 1),
    //     new Subject("OOPP LAB", 4, Branchs[2], "lab", 1),
    //     new Subject("JSS", 1, Branchs[2], "class", 1),
    //     new Subject("JSS", 1, Branchs[2], "class", 1),
    //     new Subject("C## LAB", 8, Branchs[2], "lab", 1)
    // ]
}

localStorage.setItem("Subjects",JSON.stringify(Subjects))


export let Facultys = [new Faculty("f1",1,Branchs[0],[1,2]),
                new Faculty("f2",2,Branchs[0],[3,4]),
                new Faculty("f3",3,Branchs[0],[5,6]),
                new Faculty("f4",4,Branchs[0],[7,8]),
                new Faculty("f5",1,Branchs[1],[1,2]),
                new Faculty("f6",2,Branchs[1],[3,4]),
                new Faculty("f7",3,Branchs[1],[5,6]),
                new Faculty("f8",4,Branchs[1],[7,8])
                // new Faculty("f9",1,Branchs[2],[1,2]),
                // new Faculty("f10",2,Branchs[2],[3,4]),
                // new Faculty("f11",3,Branchs[2],[5,6]),
                // new Faculty("f12",4,Branchs[2],[7,8]),
            ]

localStorage.setItem("Facultys",JSON.stringify(Facultys))

