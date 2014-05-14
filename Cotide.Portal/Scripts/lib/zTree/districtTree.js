/************************************************
* 功能说明: districtTree  plug-in 1.0
* 创建时间: 2013-7-24
*   创建人: hcli 
*     描述: 行政区树
/************************************************/
if (typeof cotide == "undefined") {
    cotide = new Object();
}
cotide.DistrictTree = function () {

    // 树对象
    var zTree;

    // 初始化表单配置
    // 全局配置
    var treeSettings = {
        // 获取树数据URL
        getTreeUrl: null,
        // 树面板ID
        treePanelId: "districtPanelTree",
        // 行政区树ID
        districtTreeId: "districtTree"
    };

    // 获取树控件面板
    function getTreePanel() {
        if ($("#" + treeSettings.treePanelId).length <= 0) {
            // 没有树控件面板
            $("body").append("<div id=" + treeSettings.treePanelId +
                " style=\"display:none;width:200px;height:230px;background-color:#fff;border:1px solid #efefef;\"><ul id=\""
                + treeSettings.districtTreeId
                + "\" class=\"ztree\" style=\"margin-top: 3px;\"></ul> </div>");
            return $("#" + treeSettings.treePanelId);
        } else {
            // 有树控件面板
            return $("#" + treeSettings.treePanelId);
        }
    }

    // 加载树
    function loadTree() {
        // 加载树
        var setting = {
            data: {
                key: {
                    name: "Name"
                },
                simpleData: {
                    idKey: "Id"
                }
            },
            check: {
                enable: false
            },
            async: {
                enable: true,
                url: treeSettings.getTreeUrl.urlstamp(),
                type: "get",
                autoParam: ["Id"]
            },
            callback: {
                onAsyncSuccess: zTreeOnAsyncSuccess, //在异步加载完成时调用,
                onClick: zTreeOnClick
            },
            view: {
                dblClickExpand: true,
                autoCancelSelected: true,
                selectedMulti: false
            }
        };
        // 加载成功后
        function zTreeOnAsyncSuccess(event, treeId, treeNode, msg) {

        }

        /* 点击树菜单事件
        * event ---- 事件对象
        * treeId --- 树ID
        * treeNode - 树对象
        */
        function zTreeOnClick(event, treeId, treeNode) { 
            var a = new Array();
            a.push(treeNode.Name);
            while (treeNode.parentTId) {
                treeNode = zTree.getNodeByTId(treeNode.parentTId);
                a.push(treeNode.Name);
            }
            var allTreeNodeStr = a.reverse().join("/"); 
            $("input[districtTree=true][isSel=true]").val(allTreeNodeStr);
            var treePanel = getTreePanel();
            treePanel.hide();
        };
        zTree = $.fn.zTree.init($("#" + treeSettings.districtTreeId), setting);
    }

    return {
        // 初始化行政区控件
        init: function (options) {

            // 初始化参数 
            if (options) {
                $.extend(treeSettings, options);
            }
            // 树控件面板
            var treePanel = getTreePanel();
            // 加载树控件
            loadTree();
            // 获取焦点
            $("input[districtTree=true]")
             .click(function () {
                 $(this).attr("isSel", "true");
                 var nowIndex = $(this).offset();
                 treePanel.css({ position: "absolute", top: nowIndex.top + 30, left: nowIndex.left }).show();
             })
            .focus(function () {
                $(this).attr("isSel", "true");
                var nowIndex = $(this).offset();
                treePanel.css({ position: "absolute", top: nowIndex.top + 30, left: nowIndex.left }).show();
            })
            // 失去焦点
            .blur(function () {
                if (treePanel.attr("isShow") == undefined || treePanel.attr("isShow") == "false") {
                    treePanel.hide();
                    $(this).attr("isSel", "false");
                }
            });
            treePanel.hover(function () {
                treePanel.attr("isShow", "true");
                $(this).attr("isSel", "true");
                treePanel.show();
            }, function () {
                treePanel.attr("isShow", "false");
                $(this).attr("isSel", "false");
                if (treePanel.attr("isShow") == undefined || treePanel.attr("isShow") == "false") {
                    treePanel.hide();
                }
            });

        }
    };
} ();
