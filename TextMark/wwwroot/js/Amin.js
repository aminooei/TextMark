let apparea = document.getElementById('apparea');

apparea.addEventListener("keydown", (e) => {    
   
    //if (!e.repeat)
    //    alert(`Key "${e.key}" pressed  [event: keydown]`);
    //else
    //    alert(`Key "${e.key}" repeating  [event: keydown]`);
    if (e.key == "i" || e.key == "I")
        App.handlers.applyOnclickAnnotation('Internal');
    else if (e.key == "r" || e.key == "R")
        App.handlers.applyOnclickAnnotation('Requirement');
    else if (e.key == "b" || e.key == "B")
        App.handlers.applyOnclickAnnotation('Backlog');
    else { $(".example").attr("contenteditable", false); }
});

apparea.addEventListener("mousedown", (e) => {
    $(".example").attr("contenteditable", true);
});