
// DataTables + Charts.js
$(document).ready(function () {
    $(".data-table").each(function (_, table) {
        $(table).DataTable({
            columnDefs: [         
                {
                    orderable: false,
                    className: 'select-checkbox',
                    targets: 0
                },
                { orderable: false, targets: "non-orderable" }
            ],
            select: {
                style: 'os',
                selector: 'td:first-child'
            },
            order: [[0, 'asc']],
            searching: true,
            select: true,
            pageLength: 5,
            lengthMenu: [5, 10, 20, 50, 100]
        });
    });
});

// ModalView
showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal("show");
        }
    });
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $(".view-table").html(res.html);
                    $("#form-modal .modal-body").html('');
                    $("#form-modal .modal-title").html('');
                    $("#form-modal").modal('hide');
                    $(document).ready(function () {
                        $(".data-table").each(function (_, table) {
                            $(table).DataTable({
                                columnDefs: [
                                    {
                                        orderable: false,
                                        className: 'select-checkbox',
                                        targets: 0
                                    },
                                    { orderable: false, targets: "non-orderable" }
                                ],
                                select: {
                                    style: 'os',
                                    selector: 'td:first-child'
                                },
                                order: [[1, 'asc']],
                                searching: true,
                                select: true,
                                pageLength: 5,
                                lengthMenu: [5, 10, 20, 50, 100]
                            });
                        });
                    });
                } else
                    $("#form-modal .modal-body").html(res.html);
            },
            error: function (err) {
                console.log(err);
            }
        });
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex);
    }
}

// Sidebar
document.addEventListener("DOMContentLoaded", function (event) {

    const showNavbar = (toggleId, navId, bodyId, headerId) => {
        const toggle = document.getElementById(toggleId),
            nav = document.getElementById(navId),
            bodypd = document.getElementById(bodyId),
            headerpd = document.getElementById(headerId)

        // Validate that all variables exist
        if (toggle && nav && bodypd && headerpd) {
            toggle.addEventListener('click', () => {
                // show navbar
                nav.classList.toggle('bar_show')
                // change icon
                toggle.classList.toggle('bx-x')
                // add padding to body
                bodypd.classList.toggle('body-pd')
                // add padding to header
                headerpd.classList.toggle('body-pd')
            })
        }
    }

    showNavbar('header-toggle', 'nav-bar', 'body-pd', 'header')

    /*===== LINK ACTIVE =====*/
    const linkColor = document.querySelectorAll('.nav_link')
    linkColor.forEach(l => l.addEventListener('click', colorLink))

    function colorLink() {
        if (linkColor) {
            linkColor.forEach(l => l.classList.remove('active'))
            this.classList.add('active')
        }
    }


    // Your code to run since DOM is loaded and ready
});