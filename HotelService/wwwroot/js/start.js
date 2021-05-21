

    $(document).ready(function () {
        var current = location.pathname;
        window.$(".nav-tabs li a").each(function () {
            var $this = window.$(this);
            if (current.indexOf($this.attr("href")) !== -1) {
                $this.addClass("active");
            }
        });
    })