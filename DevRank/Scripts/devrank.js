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

    var body = document.body;
    var openMenu = document.querySelector("[data-mobile-open]");
    var closeMenuItems = document.querySelectorAll("[data-mobile-close], .drawer-nav a, .drawer-cta a");

    if (openMenu) {
        openMenu.addEventListener("click", function () {
            body.classList.add("mobile-menu-open");
        });
    }

    closeMenuItems.forEach(function (item) {
        item.addEventListener("click", function () {
            body.classList.remove("mobile-menu-open");
        });
    });

    var copyButton = document.querySelector("[data-copy-share]");
    var shareInput = document.querySelector("[data-share-url]");
    var copyFeedback = document.querySelector("[data-copy-feedback]");

    if (copyButton && shareInput) {
        copyButton.addEventListener("click", function () {
            shareInput.select();
            document.execCommand("copy");

            if (copyFeedback) {
                copyFeedback.textContent = "Link copiado para a area de transferencia";
                copyFeedback.classList.add("is-copied");
            }
        });
    }
})();
