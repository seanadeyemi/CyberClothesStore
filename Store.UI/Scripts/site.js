$(function () {
    $(".dropdown").hover(
        function () {
            $('.dropdown-menu', this).stop(true, true).fadeIn("fast");
            $(this).toggleClass('open');
            $('b', this).toggleClass("caret caret-up");
        },
        function () {
            $('.dropdown-menu', this).stop(true, true).fadeOut("fast");
            $(this).toggleClass('open');
            $('b', this).toggleClass("caret caret-up");
        });

    $('#close').click(
        function () {

            $('#error').hide('fade');
        }
    );

    
   // var wr = $(".menu-wrapper");
   //// var l = $("#list");
   // wr.height(0);

    $('.dropdown').click(function () {
        //  $(this).siblings(".submenu").toggleClass('hide');
        var w = $(this).siblings(".menu-wrapper");


        if (!w.hasClass('open')) {
            $(".menu-wrapper").removeClass('open');
            $(".menu-wrapper").height(0);
            //$("i.expand").toggleClass("fa-minus fa-plus");
           
        }

        

        $('i.expand', this).toggleClass("fa-plus fa-minus");
        //var l = $(this).siblings(".submenu");//$('.submenu', this);
      
        if (w.hasClass('open')) {
            w.removeClass('open');
            w.height(0);
        } else {
            w.addClass('open');
            w.height(60);//l.outerHeight(true)//80
        }

     
        

    });



    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.img-upload').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $(".imgInp").change(function () {
        readURL(this);
    });


    $(".submenu li a,td a").click(function (e) {
       
        e.preventDefault(); // prevent default link button redirect behaviour     
        var url = $(this).attr("href");

        
        //$('#renderbody').load(url);


        $.ajax({
            async: true,
            url: url,
            success: function (data) {
                $("#renderbody").html(data);
            }
        })

    });
    $(document).on("change", ".imgInp", function () {

        readURL(this);
    });

    $(document).on("click", "#contentPager a, .linker", function () {
       

        $.ajax({
            url: $(this).attr("href"),
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#renderbody').html(result);
            }
        });
        return false;
    });

    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $(".sidebar").toggleClass("active");
        //$("#main-wrapper").toggleClass("active");
        $("#right-pane").toggleClass("col-md-offset-2");


        $(".coll").toggleClass("fa-chevron-left fa-chevron-right");
    });


   function navigate(href) {


        $.ajax({
            url: href,
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#renderbody').html(result);
            }
        });
       
    }

});