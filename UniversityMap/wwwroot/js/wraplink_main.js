window.addEventListener('DOMContentLoaded', function () {
    var path = document.querySelectorAll("path");
    for (let i = 0; i < path.length; i++) {
        let id = path[i].getAttribute("data-code");
        let building = id.split(' ')[1];
        path[i].setAttribute("onclick", "window.location.href = '/Maps/" + building + "'");
    }
});
