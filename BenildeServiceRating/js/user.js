var counter = 0;
var counter2 = 0;
function changeImage() {
        if (counter2 == 0) {
            document.getElementById('happy').src = "../css/images/smileys/hoc.png";
            document.getElementById('sad').src = "../css/images/smileys/snc.png";
            document.getElementById('HappyOrSad').value = "happy";
            document.getElementById('Answer').value = document.getElementById("lblPositiveAnswer").innerHTML;
            counter2 = 1;
            counter = 0;
        }
        else if (counter2 == 1) {
            counter2 = 0; counter = 0;
            document.getElementById('sad').src = "../css/images/smileys/snc.png";
            document.getElementById('happy').src = "../css/images/smileys/hnc.png";
            document.getElementById('HappyOrSad').value = null;
            document.getElementById('Answer').value = null;
        }
}

function changeImage2() {
        if (counter == 0) {
            document.getElementById('sad').src = "../css/images/smileys/soc.png";
            document.getElementById('happy').src = "../css/images/smileys/hnc.png";
            document.getElementById('HappyOrSad').value = "sad";
            document.getElementById('Answer').value = document.getElementById("lblNegativeAnswer").innerHTML;
            counter = 1; counter2 = 0;
        }
        else if (counter == 1) {
            counter = 0; counter2 = 0;
            document.getElementById('sad').src = "../css/images/smileys/snc.png";
            document.getElementById('happy').src = "../css/images/smileys/hnc.png";
            document.getElementById('HappyOrSad').value = null;
            document.getElementById('Answer').value = null;
        }

    }

function resetImage() {
    counter = 0; counter2 = 0;
    document.getElementById('sad').src = "../css/images/smileys/snc.png";
    document.getElementById('happy').src = "../css/images/smileys/hnc.png";
}
