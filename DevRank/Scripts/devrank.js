(function () {
    var rows = document.querySelectorAll(".leader-row");

    rows.forEach(function (row, index) {
        row.style.animationDelay = (index * 70) + "ms";
        row.classList.add("leader-row-ready");
    });

    var editor = document.querySelector(".solution-editor");
    var counter = document.querySelector("[data-counter]");

    if (editor && counter) {
        var updateCounter = function () {
            counter.textContent = editor.value.length + " caracteres";
        };

        editor.addEventListener("input", updateCounter);
        updateCounter();
    }
})();
