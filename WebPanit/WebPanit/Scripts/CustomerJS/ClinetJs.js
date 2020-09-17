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