
/************************************************
* 功能说明: shrinkPanel plug-in 1.0
* 创建时间: 2013-9-3
*   创建人: lhc
*     描述: 收缩栏处理
/************************************************/
if (typeof cotide == "undefined") {
    cotide = new Object();

}
cotide.shrinkPanel = function () {

    // 全局配置
    var settings = {
        // 收缩栏按钮ID
        panelToggleBtnId: "panel_toggle",
        // 左面板ID
        leftPanelId: "panel",
        // 内容面板ID
        dituContentId: "dituContent"
    };

    // 面板显示/隐藏
    function panelDisplay(isShow) {
        if (isShow == false) {
            var $panelWidth = $('#' + settings.leftPanelId).width();
            var $bodyWidth = $("body").width();
            $('#' + settings.leftPanelId).animate({ 'left': '0px' }, 180);
            $('#' + settings.dituContentId).animate({ 'marginLeft': ($panelWidth - 22) + 'px' }, 180);
            $('#' + settings.dituContentId).animate({ 'width': ($bodyWidth - $panelWidth + 22) + 'px' }, 180);
        } else {
            var $panelWidth = $('#' + settings.leftPanelId).width();
            $('#' + settings.leftPanelId).animate({ 'left': '-' + ($panelWidth - 22) + 'px' }, 180);
            $('#' + settings.dituContentId).animate({ 'width': '100%', 'marginLeft': '0' }, 180);
        }
    }


    return {
        // 初始化
        init: function (isShow) {

            $('#' + settings.panelToggleBtnId).toggle(function () {
                panelDisplay(true);
            }, function () {
                panelDisplay(false);
            });
            if (isShow == false) {
                panelDisplay(false);
            } else {
                panelDisplay(true);
            }
        },
        // 隐藏
        hide: function () {
            panelDisplay(false);
        },
        // 显示 
        show: function () {
            panelDisplay(true);
        }
    };
} ();
