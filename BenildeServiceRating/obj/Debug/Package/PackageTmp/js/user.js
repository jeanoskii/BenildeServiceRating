var counter = 0;
var counter2 = 0;
function changeImage() {
        if (counter2 == 0) {
            document.getElementById('happy').src = "../css/images/smileys/hoc.png";
            document.getElementById('sad').src = "../css/images/smileys/snc.png";
            document.getElementById('HappyOrSad').value = "happy";
            counter2 = 1;
            counter = 0;
        }
        else if (counter2 == 1) {
            counter2 = 0; counter = 0;
            document.getElementById('sad').src = "../css/images/smileys/snc.png";
            document.getElementById('happy').src = "../css/images/smileys/hnc.png";
            document.getElementById('HappyOrSad').value = null;
        }
}

function changeImage2() {
        if (counter == 0) {
            document.getElementById('sad').src = "../css/images/smileys/soc.png";
            document.getElementById('happy').src = "../css/images/smileys/hnc.png";
            document.getElementById('HappyOrSad').value = "sad";
            counter = 1; counter2 = 0;
        }
        else if (counter == 1) {
            counter = 0; counter2 = 0;
            document.getElementById('sad').src = "../css/images/smileys/snc.png";
            document.getElementById('happy').src = "../css/images/smileys/hnc.png";
            document.getElementById('HappyOrSad').value = null;
        }

    }

function resetImage() {
    counter = 0; counter2 = 0;
    document.getElementById('sad').src = "../css/images/smileys/snc.png";
    document.getElementById('happy').src = "../css/images/smileys/hnc.png";
}
