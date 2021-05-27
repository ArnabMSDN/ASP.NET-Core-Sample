// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function ($) {

    var id = null;

    function Index() {
        var $this = this;
        function initialize() {

            $(".popup").on('click', function (e) {
                id = $(this).closest('tr').children('td:first').text();
                EditModelPopup(this);
            });

            $(".delete").on('click', function (e) {
                id = $(this).closest('tr').children('td:first').text();
                DeleteModelPopup(this);
            });

            function EditModelPopup(reff) {
                var url = $(reff).data('url');
                url = url + '/' + id;
                $.get(url).done(function (data) {
                    debugger;
                    $('#edit-employee').find(".modal-dialog").html(data);
                    $('#edit-employee > .modal', data).modal("show");
                });
            }

            function DeleteModelPopup(reff) {
                var url = $(reff).data('url');
                url = url + '/' + id;
                $.get(url).done(function (data) {
                    debugger;
                    $('#delete-employee').find(".modal-dialog").html(data);
                    $('#delete-employee > .modal', data).modal("show");
                });
            }
        }

        $this.init = function () {
            initialize();
        };
    }

    $(function () {
        var self = new Index();
        self.init();
    });

}(jQuery)); 
