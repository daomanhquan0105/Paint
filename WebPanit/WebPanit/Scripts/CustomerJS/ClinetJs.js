function Dropdown_category() {
    $(".input-group .dropdown-menu").toggleClass('show');
}
function Toggle_MenuMobile() {
    $("#menu-mobile").show();
    $("#menu-mobile").addClass('show');
    $("#menu-mobile").fadeIn();
}
function CloseMenuMobile() {
    $("#menu-mobile").hide();
    $("#menu-mobile").removeClass('show')
}

function SendEmail() {
    if ($("#formSendEmail .form-control").length > 0) {
        var formData = $("#formSendEmail").serialize();
        $.ajax({
            type: "Post",
            url: "/Home/SendEmail",
            data: formData,
            success: function (res) {
                if (res) {
                    alert("Cảm ơn bạn đã gửi thông tin cho chúng tôi!!");
                    window.location.reload();
                }
                else {
                    alert("Hãy nhập đúng định dạng email!");
                }
            }
        });
    }
    else {
        alert("Hãy nhập email của bạn!");
        return false;
    }
}
