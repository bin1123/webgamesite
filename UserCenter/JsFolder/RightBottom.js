function zk_jrkf() { document.getElementById('jdtj_rb').style.display = 'none'; document.getElementById('jrkf_rb').style.display = 'block'; }
function zk_jdtj() { document.getElementById('jrkf_rb').style.display = 'none'; document.getElementById('jdtj_rb').style.display = 'block'; }
function close_fmt() { document.getElementById('all_rb').style.display = 'none'; }

function RightBottomMove() {
    var tips = document.getElementById('all_rb');
    var theTop = 210;
    var old = theTop;
    var tt = 50;
    if (window.innerHeight) {
        pos = window.pageYOffset
    } else if (document.documentElement && document.documentElement.scrollTop) {
        pos = document.documentElement.scrollTop
    } else if (document.body) {
        pos = document.body.scrollTop;
    }
    pos = pos - tips.offsetTop + theTop;
    pos = tips.offsetTop + pos / 10;
    if (pos < theTop) {
        pos = theTop;
    }
    if (pos != old) {
        tips.style.top = pos + "px";
        tt = 10;  //alert(tips.style.top);  
    }
    old = pos;
    setTimeout(RightBottomMove, tt);
}
RightBottomMove();