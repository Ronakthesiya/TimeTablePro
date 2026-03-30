$(".logout-btn").click(function () {
    sessionStorage.clear();
    window.location.href = "http://127.0.0.1:5508/TimeTable/Frontend/LandingPage/Landing.html";
});