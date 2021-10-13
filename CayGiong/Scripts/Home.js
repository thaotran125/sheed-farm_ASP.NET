$(document).ready(function () {

      $(document).on("click", ".addCart", function (e) {
   // $(".addCart").click(function (e) {
       //   alert($(this).attr("dataPro"));

        $.ajax({
            url: "/Cart/AddCart",
            data: { idPro: $(this).attr("dataPro") },
            success: function (response) {
                $("#QuantityMycart").text(response);
            }
        });
        e.preventDefault();
    });


    $(document).on("click", "#minus", function () {
        //$(".btn-minus").click(function () {
       //  alert($(this).attr("data-minus"));
        $.ajax({
            url: "/Cart/removeCart",
            data: { idPro: $(this).attr("data-minus") },
            success: function (response) {
                $("#tableCart").html(response);
            }
        })
    });

    $(document).on("click", "#plus", function (e) {
        //$(".btn-plus").click(function () {
       //  alert($(this).attr("data-plus"));
        $.ajax({
            url: "/Cart/UpdateCart",
            data: { idPro: $(this).attr("data-plus") },
            success: function (response) {
                $("#tableCart").html(response);
            }
        })
        e.preventDefault();
    });

    $(document).on("click", ".removeCart", function (e) {
        $.ajax({
            url: "/Cart/removeProductCart",
            data: { idPro: $(this).attr("data-remove") },
            success: function (response) {
                $("#tableCart").html(response);
            }
        })
        e.preventDefault();
    });


    $(document).on("click", "#minus-detail", function (e) {
        var sl = $("#quantity-detail").val();
       // alert(sl);
        $.ajax({
            url: "/Product/MinusQuantity",
            data: { quantity: sl },
            success: function (response) {
                $("#quantity-detail").val(response);
            }
        });
        e.preventDefault();
    });

    $(document).on("click", "#plus-datail", function (e) {
        var sl = $("#quantity-detail").val();
     //   alert(sl);
        $.ajax({
            url: "/Product/PlusQuantity",
            data: { quantity: sl },
            success: function (response) {
                $("#quantity-detail").val(response);
            }
        });
        e.preventDefault();
    });

    $(document).on("click", ".cartDetail", function (e) {
        var sl = $("#quantity-detail").val();
      //  alert(sl);
        $.ajax({
            url: "/Cart/CartDetail",
            data: { quantity: sl, idPro: $(this).attr("dataPro") },
            success: function (response) {
                $("#QuantityMycart").text(response);
            }
        });
        e.preventDefault();
    });

    $(document).on("click", ".BuyNow", function (e) {
        var sl = $("#quantity-detail").val();
        //  alert(sl);
        $.ajax({
            url: "/Cart/BuyNow",
            data: { idPro: $(this).attr("dataPro"), quantity: sl  },
            
        });
        e.preventDefault();
    });

    $('.show-submenu').click(function () {
        $(this).collapse({ toggle: true });
    })

    document.addEventListener("DOMContentLoaded", function () {
        // make it as accordion for smaller screens
        if (window.innerWidth < 992) {

            // close all inner dropdowns when parent is closed
            document.querySelectorAll('.navbar .dropdown').forEach(function (everydropdown) {
                everydropdown.addEventListener('hidden.bs.dropdown', function () {
                    // after dropdown is hidden, then find all submenus
                    this.querySelectorAll('.submenu').forEach(function (everysubmenu) {
                        // hide every submenu as well
                        everysubmenu.style.display = 'none';
                    });
                })
            });

            document.querySelectorAll('.dropdown-menu a').forEach(function (element) {
                element.addEventListener('click', function (e) {
                    let nextEl = this.nextElementSibling;
                    if (nextEl && nextEl.classList.contains('submenu')) {
                        // prevent opening link if link needs to open dropdown
                        e.preventDefault();
                        if (nextEl.style.display == 'block') {
                            nextEl.style.display = 'none';
                        } else {
                            nextEl.style.display = 'block';
                        }

                    }
                });
            })
        }
        // end if innerWidth
    });

});