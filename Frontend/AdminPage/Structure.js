// define the structure of timetable that create by algorithm
export class Structure{
    // slotInDay = 6;
    // DayInWeek = 5;

    // find day form sloNumber
    static findDay(slotNumber){
        let days = ["Mon", "Tue", "Wed", "Thu", "Fri"]

        return days[slotNumber/6];
    }

    // find start time of slot form sloNumber
    static findStartTime(slotNumber){
        let startTime = ["07:00","08:00","09:00","10:30","11:30","12:30"]

        return startTime[slotNumber%6];
    }

    // find end time of slot form sloNumber
    static findEndTime(slotNumber){
        let endTime = ["08:00","09:00","10:00","11:30","12:30","01:30"]

        return endTime[slotNumber%6];
    }

    // convert boolean available Time array to 1,0 string to store in/get from databse
    static availableToStr(AvailableTime){
        let str = "";

        AvailableTime.forEach(val => {
            str += (val) ? "1" : "0";
        });

        console.log(str);

        return str;
    }
}