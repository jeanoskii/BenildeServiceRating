$(document).ready(function () {
    $(".btn").click(function () {
        $("#myModal").modal('show');
    });
});

$(document).ready(function () {
    $("#btnYes").click(function () {
        $("#confirmationModal").modal('hide');
    });

    $("#btnNo").click(function () {
        $("#confirmationModal").modal('hide');
    });
});

$(document).ready(function () {
    $("#btnNoAdmin").click(function () {
        $("#confirmationSaveModal").modal('hide');
    });

    $("#btnYesAdmin").click(function () {
        $("#confirmationSaveModal").modal('hide');
    });
});

$(document).ready(function () {
    $("#yesBtn").click(function () {
        $("#deleteCommentModal").modal('hide');
    });

    $("#noBtn").click(function () {
        $("#deleteCommentModal").modal('hide');
    });
});

$(document).ready(function () {
    $("#btnYesLocation").click(function () {
        $("#deleteLocationModal").modal('hide');
    });

    $("#btnNoLocation").click(function () {
        $("#deleteLocationModal").modal('hide');
    });
});


var counter = 0;
var counter2 = 0;
function changeImage() {
    if (counter2 == 0) {
        document.getElementById('happy').src = "images/smileys/hoc.png";
        document.getElementById('sad').src = "images/smileys/snc.png";
        counter2 = 1;
        counter = 0;
    }
    else if (counter2 == 1) {
        counter2 = 0; counter = 0;
        document.getElementById('sad').src = "images/smileys/snc.png";
        document.getElementById('happy').src = "images/smileys/hnc.png";
    }
}

function changeImage2() {
    if (counter == 0) {
        document.getElementById('sad').src = "images/smileys/soc.png";
        document.getElementById('happy').src = "images/smileys/hnc.png";
        counter = 1; counter2 = 0;
    }
    else if (counter == 1) {
        counter = 0; counter2 = 0;
        document.getElementById('sad').src = "images/smileys/snc.png";
        document.getElementById('happy').src = "images/smileys/hnc.png";
    }
}

document.getElementById('mrlTextbox').style.height = "100px";
document.getElementById('mrlTextbox').style.fontSize = "25pt";
document.getElementById('hoursSelection').style.height = "100px";
document.getElementById('SelectRLButton').style.height = "100px";
document.getElementById('addRLButton').style.height = "100px";
