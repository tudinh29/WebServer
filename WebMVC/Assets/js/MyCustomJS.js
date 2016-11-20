function openNav() {
    document.getElementById("main").style.marginLeft = "11%";
    document.getElementById("mySidenav").style.width = "10%";
    document.getElementById("mySidenav").style.display = "block";
    document.getElementById("openNav").style.display = 'none';
    document.getElementById("sidenavFilter").style.border = "2px solid #a1a1a1";
}

function closeNav() {
    document.getElementById("main").style.marginLeft = "0%";
    document.getElementById("mySidenav").style.display = "none";
    document.getElementById("openNav").style.display = "inline-block";
    document.getElementById("mySidenav").style.transition = "0.5s";

}