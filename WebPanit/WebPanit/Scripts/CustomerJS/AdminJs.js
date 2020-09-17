var leftMenuProfile = $.cookie("left_menu_profile");
if (leftMenuProfile == null) leftMenuProfile = "";
var arrMenu = leftMenuProfile.split('|');
for (var i = 0; i < arrMenu.length; i++) {
    $("#left_menu_profile li[data-id='" + arrMenu[i] + "'] a.root").addClass("expand");
    $("#left_menu_profile li[data-id='" + arrMenu[i] + "'] div").show();
}
$(".left_menu_profile li .root").click(function () {
    $(this).parent().find(".sub").slideToggle(400);
    $(this).parent().find(".root").toggleClass("expand");
    var strTemp = $(this).parent().attr("data-id") + "|";
    leftMenuProfile = leftMenuProfile.replace(strTemp, "");
    if ($(this).hasClass("expand")) {
        leftMenuProfile = leftMenuProfile + strTemp;
    }
    $.cookie("left_menu_profile", leftMenuProfile);
});