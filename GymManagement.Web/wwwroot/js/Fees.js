

$(document).ready(function () {
    $(document).on("click", "#btnGetUserDetail", function (e) {
        var isValidated = true;
        var regNumb = $('#txtRegNumber').val();
        var mobil = $('#txtMobile').val();
        if (regNumb.length <= 0 && mobil.length != 10) {
            alertify.error("Please enter Registration/Mobile Number!");
            isValidated = false;
        }

        if (isValidated) {
            $.ajax({
                type: "GET",
                url: "/User/GetUserFeesData",
                data: { registrationNumber: regNumb, mobile: mobil },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (userView) {
                    if (userView != null && userView.registrationNumber > 0) {
                        $('#txtRegNumber').val(userView.registrationNumber);
                        $("#txtRegNumber").prop('disabled', true);
                        $('#txtMobile').val(userView.mobile);
                        $("#txtMobile").prop('disabled', true);
                        $('#txtName').val(userView.name);
                        $('#txtEndDate').val(userView.endDate);
                        $('#txtPackage').val(userView.package);
                        $('#txtTotal').val(userView.totalAmt);
                        $('#txtBalAmt').val(userView.balanceAmt);
                        $('#txtOrigBal').val(userView.balanceAmt);
                        if (userView.balanceAmt <= 0) {
                            $("#txtPaidAmt").prop('disabled', true);
                        }
                        else {
                            $("#txtPaidAmt").prop('disabled', false);
                        }

                        $("#btnGetUserDetail").prop('disabled', true);
                        $("#btnUpdateFee").prop('disabled', false);
                        $("#btnReset").prop('disabled', false);
                        
                    } else {
                        alertify.error("User not found");
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
    });

    $(document).on("click", "#btnReset", function (e) {
        location.reload();
    });

    $(document).on("click", "#btnUpdateFee", function (e) {
        $.ajax({
            type: "GET",
            url: "/User/GetUserFeesData",
            data: { registrationNumber: regNumb, mobile: mobil },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (userView) {
                console.log(userView);
                if (userView != null && userView.registrationNumber > 0) {
                    $('#txtRegNumber').val(userView.registrationNumber);
                    $("#txtRegNumber").prop('disabled', true);
                    $('#txtMobile').val(userView.mobile);
                    $("#txtMobile").prop('disabled', true);
                    $('#txtName').val(userView.name);
                    $('#txtEndDate').val(userView.endDate);
                    $('#txtPackage').val(userView.package);
                    $('#txtTotal').val(userView.totalAmt);
                    $('#txtBalAmt').val(userView.balanceAmt);
                    $('#txtOrigBal').val(userView.balanceAmt);
                    if (userView.balanceAmt <= 0) {
                        $("#txtPaidAmt").prop('disabled', true);
                    }
                    else {
                        $("#txtPaidAmt").prop('disabled', false);
                    }

                    $("#btnGetUserDetail").prop('disabled', true);
                    $("#btnUpdateFee").prop('disabled', false);
                    $("#btnReset").prop('disabled', false);

                } else {
                    alertify.error("User not found");
                }
            },
            failure: function (response) {
                alertify.error("Some error occured, contact developers");
            },
            error: function (response) {
                alertify.error("Some error occured, contact developers");
            }
        });
    });
});


function ResetRegNumb() {
    document.getElementById("txtRegNumber").value = "";
}

function ResetMobileNumb() {
    document.getElementById("txtMobile").value = "";
}

document.getElementById("txtPaidAmt").addEventListener('keyup', function (event) {

    var paidAmt = document.getElementById("txtPaidAmt").value;
    var origBal = document.getElementById("txtOrigBal").value;

    document.getElementById("txtBalAmt").value = origBal - paidAmt;
});

