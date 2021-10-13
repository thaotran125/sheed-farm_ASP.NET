
$(document).ready(function () {
    $(document).on("#Group_id", "change", function () {
   // $("#Group_id").change(function () {
        var idParent = $("#Group_id").val();
        // alert(idParent);
        $.ajax({
            type: "POST",
            url: "/Admin/Product/getCateChild",
            data: { id: idParent },
            success: function (response) {
                $("#id_cate").empty();
                $("#id_cate").append(response);

            }
        });
    });

    $('#pageSize').change(function () {
        $('#formP').submit();
    })

    $('.pageSizeSearch').change(function () {
        $('#formS').submit();
    })

    $(".viewDetail").click(function () {
        var idPro = $(this).val(); 
        $.ajax({
            url: "/Admin/Product/Detail",
            data: { id: { dataid } },
            success: function (response) {
                $('.#descript').html();
            }
        })
    })

    $("#img").change(function () {
        var input = $(this)[0];
        var a = $(this);
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $(a).next().attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    })
    /////////////////////////////////////////////
   // $(document).on("#addCate", "click", function () {
    $("#addCate").off("click").on( "click", function () {
        $.ajax({
            url: "/Admin/Category/Create",
            type: 'GET',
            contentType: "application/json;charset=UTF-8",
            dataType: "html",
            success: function (result) {
                $('#modal-add-content').html(result);
                $('#modal-add').modal(options);
                $('#modal-add').modal('show');
            }
        })
    })

    //$(document).on("#btnEditCate", "click", function () {
    $(".btnEditCate").off("click").on("click", function () {
        var id = $(this).attr('dataid');

        $.ajax({
            url: "/Admin/Category/Edit/",
            data: { id: id },
            type: 'GET',
            contentType: "application/json;charset=UTF-8",
            success: function (result) {
                $('#modal-edit-content').html(result);
                $('#modal-edit').modal(options);
                $('#modal-edit').modal('show');
            },
            error: function (er) {
                alert(er);
            }
        })
    })

  //  $(document).on("#btnDel", "click", function () {
    $(".btnDel").off("click").on("click", function () {
        var id = $(this).attr('dataid');
        //alert(id);
        $.ajax({
            url: "/Admin/Category/OpenDelete/",
            data: { id: id },
            type: 'GET',
            contentType: "application/json;charset=UTF-8",
            dataType: "html",
            success: function (result) {
                $('#modal-delete-content').html(result);
                $('#modal-delete').modal('show');
            }
        })
    })
    ///////////////////////////////////////////////////////////////////
   // $(document).on("#addCateChild", "click", function () {
    $("#addCateChild").off("click").on("click", function () {
        $.ajax({
            url: "/Admin/CateChild/Create",
            type: 'GET',
            contentType: "application/json;charset=UTF-8",
            dataType: "html",
            success: function (result) {
                $('#modal-add-content').html(result);
                $('#modal-add').modal(options);
                $('#modal-add').modal('show');
            }
        })
    })

   // $(document).on("#EditCateChild", "click", function () {
    $(".EditCateChild").off("click").on("click", function () {
        var id = $(this).attr('dataid');

        $.ajax({
            url: "/Admin/CateChild/Edit/",
            data: { id: id },
            type: 'GET',
            contentType: "application/json;charset=UTF-8",
            success: function (result) {
                $('#modal-edit-content').html(result);
                $('#modal-edit').modal(options);
                $('#modal-edit').modal('show');
            },
            error: function (er) {
                alert(er);
            }
        })
    })

    //$(document).on("#DelCateChild", "click", function () {
    $(".DelCateChild").off("click").on("click", function () {
        var id = $(this).attr('dataid');
        //alert(id);
        $.ajax({
            url: "/Admin/CateChild/OpenDelete/",
            data: { id: id },
            type: 'GET',
            contentType: "application/json;charset=UTF-8",
            dataType: "html",
            success: function (result) {
                $('#modal-delete-content').html(result);
                $('#modal-delete').modal('show');
            }
        })
    })

    setTimeout(function () {
        $('#almsg').fadeOut('slow');
    }, 4000);

    $('.btnEditOrder').click(function () {
        var id = $(this).attr('dataid');
        $.ajax({
            url: "/Admin/Order/OpendEdit/",
            data: { id: id},
            type: 'GET',
            success: function (result) {
                $('#modal-edit-content').html(result);
               
                $('#modal-edit').modal('show');
            }
        })
    })

    //$('.OpenEdit').click(function () {
    //    var page = $(this).attr('dataPage');
    //    var pageSize = $('#pageSize').val();
    //    var id = $(this).attr('dataid');
    //    alert(id+" id "+ page + " size " + pageSize);
    //    $.ajax({
    //        url: "/Admin/Product/OpenEdit/",
    //        data: { id: id, page: page, size: size },
    //        type:"POST",
    //    })
    //})

});
