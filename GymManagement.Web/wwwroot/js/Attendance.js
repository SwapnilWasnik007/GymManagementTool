


function SetCalenderCurrentMonth() {
    var date = new Date();
    document.getElementById("txtMonth").value = date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2);
    SetCalenderData(date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2));
}



var input = document.getElementById("txtRegId");

// Execute a function when the user releases a key on the keyboard
input.addEventListener("keyup", function (event) {
    // Number 13 is the "Enter" key on the keyboard

    var regNumb = $('#txtRegId').val();
    $.ajax({
        type: "GET",
        url: "/Attendance/GetNameByRegNumb",
        data: { regNumber: regNumb },
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: function (userName) {
            console.log(userName);
            document.getElementById("lblName").innerText = userName;
        },
        failure: function (response) {
            alertify.error("Some error occured, contact developers" + response);
        },
        error: function (response) {
            alertify.error("Some error occured, contact developers" + response);
        }
    });


    if (event.keyCode === 13) {
        $.ajax({
            type: "GET",
            url: "/Attendance/AddAttendanceByRegNumb",
            data: { regNumber: regNumb },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    alertify.success(response.message);
                    document.getElementById("lblName").innerText = "________________________";
                    document.getElementById("txtRegId").value = "";
                }
                else {
                    alertify.warning(response.message);
                    document.getElementById("lblName").innerText = "________________________";
                    document.getElementById("txtRegId").value = "";
                }
            },
            failure: function (response) {
                alertify.error("Some error occured, contact developers" + response);
                document.getElementById("lblName").innerText = "________________________";
                document.getElementById("txtRegId").value = "";

            },
            error: function (response) {
                alertify.error("Some error occured, contact developers" + response);
                document.getElementById("lblName").innerText = "________________________";
                document.getElementById("txtRegId").value = "";
            }
        });
    }
});


function ResetCalender() {
    for (i = 1; i <= 42; i++) {
        if (i != "1" && i != "8" && i != "15" && i != "22" && i != "29" && i != "36") {
            document.getElementById(i).removeAttribute("class");
        }
        else {
            document.getElementById(i).setAttribute("class", "sunday");
        }
        document.getElementById(i).innerText = "";
    }
}



function SetCalenderData(value, regNumber) {
    ResetCalender();
    var monthName = GetMonthName(value);
    document.getElementById("lblMonthName").innerText = monthName;
    var startFrom = GetDayName(value);
    var totalDays = GetTotalDaysOfMonth(monthName, value);
    var regNumber = document.getElementById("txtRegNumber").value;
    //SetMonthsDateInCalender(startFrom, totalDays);

    //Get Attendance Data from database
    $.ajax({
        type: "GET",
        url: "/Attendance/GetUserAttendance",
        data: { selectedMonthYear: value, regNumber: regNumber },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (attendances) {
            console.log(attendances.length);

            document.getElementById("lblTotalPresent").innerText = attendances.length;
            document.getElementById("lblTotalAbsent").innerText = totalDays - attendances.length;

            count = 1;
            var sundaysArray = [1, 8, 15, 22, 29, 36];
            for (i = 1; i <= 42; i++) {
                if (i < startFrom || i >= totalDays + startFrom) {
                    //Fill remaining date with sunday color
                    if (sundaysArray.some(e => e === i)) {
                        document.getElementById(i).setAttribute("class", "sunday");
                    }
                    else {
                        document.getElementById(i).setAttribute("class", "invalidDate");
                    }
                }
                else {
                    document.getElementById(i).innerText = count;
                    //change present absent color
                    if (attendances.some(e => e.date === count)) {
                        if (sundaysArray.some(e => e === i)) {
                            document.getElementById(i).setAttribute("class", "present");
                        }
                        else {
                            document.getElementById(i).setAttribute("class", "present");
                        }
                    }
                    else {
                        if (sundaysArray.some(e => e === i)) {
                        }
                        else {
                            document.getElementById(i).setAttribute("class", "absent");
                        }
                    }
                    count++;
                }
            }
        },
        failure: function (response) {
            alertify.error("Some error occured, contact developers");
        },
        error: function (response) {
            alertify.error("Some error occured, contact developers");
        }
    });
}



function SetMonthsDateInCalender(startfrom, totalDays) {
    count = 1;
    var sundaysArray = [1, 8, 15, 22, 29, 36];
    for (i = 1; i <= 42; i++) {

        if (i < startfrom || i >= totalDays + startfrom) {
            //Fill remaining date with sunday color
            if (sundaysArray.some(e => e.foo === i)) {
                document.getElementById(i).setAttribute("class", "sunday");
            }
            else {
                document.getElementById(i).setAttribute("class", "invalidDate");
            }
        }
        else {
            if (sundaysArray.some(e => e.foo === i)) {
                document.getElementById(i).innerText = count++;
                //Do not change color
            }
            else {
                document.getElementById(i).innerText = count++;
                //change present absent color
            }
        }
    }
}

function GetTotalDaysOfMonth(monthName, value) {
    var isLeapYear = leapyear(value);
    switch (monthName) {
        case "January":
        case "March":
        case "May":
        case "July":
        case "August":
        case "October":
        case "December":
            totalDays = 31;
            break;
        case "February":
            if (isLeapYear) {
                totalDays = 29;
            }
            else {
                totalDays = 28;
            }
            break;
        case "April":
        case "June":
        case "September":
        case "November":
            totalDays = 30;
            break;
    }

    return totalDays;
}

function leapyear(monthDate) {
    var date = new Date(monthDate);
    var year = date.getFullYear();
    return (year % 100 === 0) ? (year % 400 === 0) : (year % 4 === 0);
}

function GetMonthName(value) {
    var date = new Date(value);
    var month = new Array();
    month[0] = "January";
    month[1] = "February";
    month[2] = "March";
    month[3] = "April";
    month[4] = "May";
    month[5] = "June";
    month[6] = "July";
    month[7] = "August";
    month[8] = "September";
    month[9] = "October";
    month[10] = "November";
    month[11] = "December";
    return month[date.getMonth()];
}

function GetDayName(value) {
    var d = new Date(value);
    var weekday = new Array(7);
    weekday[0] = "Sunday";
    weekday[1] = "Monday";
    weekday[2] = "Tuesday";
    weekday[3] = "Wednesday";
    weekday[4] = "Thursday";
    weekday[5] = "Friday";
    weekday[6] = "Saturday";

    return d.getDay() + 1;
    //return weekday[d.getDay()];
}




/////// Modal Functions ///////////////
function GetCurrentTdId(selectedDate) {

    var selectedMonthYear = document.getElementById("txtMonth").value;

    document.getElementById("txtId").value = selectedDate + "/" + selectedMonthYear;
}
