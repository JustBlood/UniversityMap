window.addEventListener('DOMContentLoaded', function() {
   var path = document.querySelectorAll("path");
   for(let i = 0; i < path.length; i++)
   {
       let id = path[i].getAttribute("data-code")
       if (id[0] == 'p') {
           path[i].setAttribute("onclick", "window.location.href = '/panorama?tag=" + id + "'")
       }
   }
 });
