

alertify.set('notifier', 'position', 'top-right');


//Setting current date to start date
function OnloadFunct() {
    var date = new Date();
    var result = date.toLocaleDateString("fr-CA",
        {
            year: "numeric",
            month: "2-digit",
            day: "2-digit",
        });
    document.getElementById("txtStartDate").value = result;
}

function convertDecimal(value) {
    if (value.indexOf(".") == -1) {
        value = value + '.00';
    }
    var totalAmt = document.getElementById("txtTotal").value
    document.getElementById("txtPaidAmt").value = value;
    document.getElementById("txtBalAmt").value = totalAmt - value;
}

function UpdateEndDateOnStartDate(selectedDate) {
    var e = document.getElementById("drpPackage");
    var text = e.options[e.selectedIndex].text;

    var date = new Date(selectedDate);

    switch (text) {
        case "Yearly":
            date.setMonth(date.getMonth() + 12)
            var result = date.toLocaleDateString("fr-CA",
                {
                    year: "numeric",
                    month: "2-digit",
                    day: "2-digit",
                });
            document.getElementById("txtEndDate").value = result;
            break;
        case "Half Yearly":
            date.setMonth(date.getMonth() + 6)
            var result = date.toLocaleDateString("fr-CA",
                {
                    year: "numeric",
                    month: "2-digit",
                    day: "2-digit",
                });
            document.getElementById("txtEndDate").value = result;
            break;
        case "Quaterly":
            date.setMonth(date.getMonth() + 3)
            var result = date.toLocaleDateString("fr-CA",
                {
                    year: "numeric",
                    month: "2-digit",
                    day: "2-digit",
                });
            document.getElementById("txtEndDate").value = result;
            break;
        case "Monthly":
            date.setMonth(date.getMonth() + 1)
            var result = date.toLocaleDateString("fr-CA",
                {
                    year: "numeric",
                    month: "2-digit",
                    day: "2-digit",
                });
            document.getElementById("txtEndDate").value = result;
            break;
    }
}

function UpdateBatch(selectedbatch) {

    document.getElementById("txtBatch").value = selectedbatch;
}

function UpdateTotalAmount(packageAmount) {
    //Get paid amount
    var paidAmount = document.getElementById("txtPaidAmt").value;
    if (packageAmount != -1) {
        //Set Total Amount
        document.getElementById("txtTotal").value = packageAmount;

        //Set Balance Amount
        document.getElementById("txtBalAmt").value = packageAmount - paidAmount;

        //Enable Start Date dropdown
        document.getElementById("txtStartDate").disabled = false;

        //Set Current Date in start Date
        var date = new Date(document.getElementById("txtStartDate").value);

        //Set End Date
        var e = document.getElementById("drpPackage");
        var text = e.options[e.selectedIndex].text;

        document.getElementById("txtPackageValue").value = text;

        switch (text) {
            case "Yearly":
                date.setMonth(date.getMonth() + 12)
                var result = date.toLocaleDateString("fr-CA",
                    {
                        year: "numeric",
                        month: "2-digit",
                        day: "2-digit",
                    });
                document.getElementById("txtEndDate").value = result;
                break;
            case "Half Yearly":
                date.setMonth(date.getMonth() + 6)
                var result = date.toLocaleDateString("fr-CA",
                    {
                        year: "numeric",
                        month: "2-digit",
                        day: "2-digit",
                    });
                document.getElementById("txtEndDate").value = result;
                break;
            case "Quaterly":
                date.setMonth(date.getMonth() + 3)
                var result = date.toLocaleDateString("fr-CA",
                    {
                        year: "numeric",
                        month: "2-digit",
                        day: "2-digit",
                    });
                document.getElementById("txtEndDate").value = result;
                break;
            case "Monthly":
                date.setMonth(date.getMonth() + 1)
                var result = date.toLocaleDateString("fr-CA",
                    {
                        year: "numeric",
                        month: "2-digit",
                        day: "2-digit",
                    });
                document.getElementById("txtEndDate").value = result;
                break;
        }
    }
    else {
        //Reset Total Amount
        document.getElementById("txtTotal").value = '0.00';
        //Set Balance Amount
        document.getElementById("txtBalAmt").value = 0.00 - paidAmount;
        //Disable Start Date
        document.getElementById("txtStartDate").disabled = true;
    }
}


$(document).ready(function () {
    $(document).on("click", "#btnRegisterUser", function (e) {

        ///////////////// VALIDATION //////////////////////

        //Create User model Json
        var isValidated = true;
        var obj = {};
        obj.Name = $("#txtName").val();
        obj.Mobile = $("#txtMobile1").val();
        obj.Batch = $("#txtBatch").val();
        obj.Address = $("#txtAddress").val();
        obj.Package = $("#txtPackageValue").val();
        obj.StartDate = $("#txtStartDate").val();
        obj.EndDate = $("#txtEndDate").val();
        obj.TotalAmt = $("#txtTotal").val();
        obj.PaidAmt = $("#txtPaidAmt").val();
        obj.BalanceAmt = $("#txtBalAmt").val();

        if (obj.Name.length <= 0) {
            alertify.error("Please enter name!");
            isValidated = false;
        }
        else
            if (obj.Mobile.length != 10) {
                alertify.error("Mobile must be 10 digit!");
                isValidated = false;
            } else
                if (obj.Address.length <= 0) {
                    alertify.error("Please enter address!");
                    isValidated = false;
                } else
                    if (obj.Batch <= 0) {
                        alertify.error("Please enter batch!");
                        isValidated = false;
                    } else
                        if (obj.Package.length <= 0) {
                            alertify.error("Please select package!");
                            isValidated = false;
                        } else
                            if (obj.PaidAmt.length <= 0) {
                                var res = confirm("Paid Amount is missing. Do you want to proceed");
                                isValidated = res;
                                obj.PaidAmt = 0;
                            }

        if (isValidated) {
            var jsonStr = JSON.stringify(obj);
            $.ajax({
                url: '/User/RegisterUser',
                type: 'POST',
                data: {
                    data: JSON.stringify(obj)
                },
                success: function (response) {
                    if (response == true) {
                        alertify.success("User registered successfully");
                    }
                    else {
                        alertify.error("Failed to register user");
                    }
                },
                error: function () {
                }
            });
        }
    });
});


function HideError() {
    //document.getElementById("divErrorResult").setAttribute("hidden");
    document.getElementById("divErrorResult").hidden = true;
}