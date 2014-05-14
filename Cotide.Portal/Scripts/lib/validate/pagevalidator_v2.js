/*
使用例子：
<input type="text"  name="tttt" id="tttt" 
                    focusMsg="用户名/邮箱/手机2"
                    isEmpty="true" 
                    description="用户名/邮箱/手机"  
                    emptyMsg="用户名不能为空" 
                    min="5"
                    max="20"
                    rangeMsg="用户范围为5-20位字符"
                    regex = "/^[0-9]{3,5}$/" 
                    regexMsg="正则匹配错误：应该为0-9 3-5位字符的值"
                    validator="true" 
 /> 
            
 <div id="ttttTip">AAAAAA</div>   
<select emptyMsg="请选择你想吃的东西"   
        description="你想吃什么呢？" 
        focusMsg="你到底想吃什么啊~哥！" 
        validator="true" 
        id="objSel"
    name="objSel">
<option value="">-请选择-</option>
<option value="1"> 蛋糕</option>
<option value="2"> 甜点</option>
<option value="3"> 猪肠粉</option>
</select>

 <div id="objSelTip"></div>
//------------------------------------------------------------------- 
//系统名称：验证控件脚本
//文件名称：pagevalidator_v2.js
//模块名称：PageValidator 2.0 
//模块编号：
//作　　者：LHC
//完成日期：2010/8/29 14:34:00
//功能说明：
//-----------------------------------------------------------------
//修改记录：改造客户端验证控件脚本
//修改人：  
//修改时间：
//修改内容：
//------------------------------------------------------------------- 
*/ 
/* 验证控件主体 */
(function ($) {
    // 全局配置
    var settings = {
        // 错误提示样式名
        msgErrorClassName: "msgError",
        // 正确提示样式名
        msgRightClassName: "msgOK",
        // 默认提示样式名
        msgDefaultClassName: "msgNormal",
        // 鼠标移开提示
        msgFocusClassName: "msgOnFocus",
        // 异步读取数据样式名
        msgAjaxClassName: "msgAjaxing",
        // 提示方式 alter : 弹出框提示 show : 显示提示 默认为show
        showType: "show",
        // 鼠标移开提示属性名
        onFocusMsgAttr: "focusMsg",
        // 默认提示属性名
        defaultMsgAttr: "description",
        // 非空验证属性名
        isEmptyAttr: "isEmpty",
        // 非空验证提示属性名
        emptyMsgAttr: "emptyMsg",
        // 最少值长度属性名
        minAttr: "min",
        // 最大值长度属性名
        maxAttr: "max",
        // 范围验证提示属性名
        rangeMsgAttr: "rangeMsg",
        // 正则验证属性名
        regexAttr: "regex",
        // 正则提示属性名
        regexMsgAttr: "regexMsg",
        // 对比值控件ID
        compareElemIdAttr: "compareElemId",
        // 对比值控件提示
        compareElemMsgAttr: "compareElemMsg",
        // AJAX属性名
        ajaxAttr: "ajax",
        // 验证促发事件 blur: 失去焦点  
        validatorEvent: "blur",
        // 信息提示内容后缀
        tipSuffix: "Tip"
    };

    // 表单验证
    $.fn.formValidator = function (options) {

        if (options) {
            $.extend(settings, options);
        };
        // 表单对象
        var $from = this;
        // 需要验证的控件
        var $checkElement = $from.find("[validator=true]");
        // 表单验证结果  
        if ($checkElement.length > 0) {
            // 表单
            $from.submit(function () {
                // 验证结果
                var $isPass = true;
                // 标记是否通过
                var isNoPass = false;
                $checkElement.each(function () {
                    var obj = $(this);
                    if (settings.showType == "alter") {
                        if ($isPass) {
                            $isPass = startValidator($from, obj, $isPass);
                        }
                    } else {
                        $isPass = startValidator($from, obj, $isPass);
                    }
                    if (!$isPass && !isNoPass) {
                        isNoPass = true;
                        obj.focus();
                    }
                });
                return $isPass;
            });
            // 进行控件验证
            $checkElement.each(function (i) {
                var obj = $(this);
                var objDefaultMsg = obj.attr(settings.defaultMsgAttr);
                if (objDefaultMsg != undefined) {
                    showMessage($from, obj[0].name, "Default", objDefaultMsg);
                    if (settings.showType == "alter") {
                        obj.bind("blur", function () {
                            showMessage($from, obj[0].name, "Default", objDefaultMsg);
                        });
                    }
                };
                var objFocusMsg = obj.attr(settings.onFocusMsgAttr);
                if (objFocusMsg != undefined) {
                    obj.bind("focus", function () {
                        $(".validation-summary-errors").fadeOut();
                        showMessage($from, obj[0].name, "Focus", objFocusMsg);
                    });
                };
                if (settings.showType != "alter") {
                    obj.bind(settings.validatorEvent, function () {
                        return startValidator($from, obj);
                    });
                }

            });
        };
    };


    // 表单验证直接返回结果
    $.fn.formValidatorResult = function (options) {
        
        if (options) {
            $.extend(settings, options);
        };
        // 表单对象
        var $from = this;
        // 需要验证的控件
        var $checkElement = $from.find("[validator=true]");
        // 表单验证结果  
        if ($checkElement.length > 0) {

            // 验证结果
            var $isPass = true;
            // 标记是否通过
            var isNoPass = false;
            $checkElement.each(function () {
                var obj = $(this);
                if (settings.showType == "alter") {
                    if ($isPass) {
                        $isPass = startValidator($from, obj, $isPass);
                    }
                } else {
                    
                    $isPass = startValidator($from, obj, $isPass);
                }
                if (!$isPass && !isNoPass) {
                    isNoPass = true;
                    obj.focus();
                }
            });
            return $isPass;
        } else {
            return true;
        }
    };

    //////////////////////// 方法 //////////////////////// 
    // 开始验证
    // form ： 当前表单对象
    // obj: 需要验证的对象
    // 是否忽略
    var startValidator = function (form, obj, isPass) {
        
        // 验证
        var validatorResult = true;
        // 进行非空验证 
        var isEmptyAttr = "true";
        if (obj.attr(settings.isEmptyAttr) == undefined) {
            isEmptyAttr = "false";
        } else {
            isEmptyAttr = obj.attr(settings.isEmptyAttr);
        }
        if (isEmptyAttr != "true") {
            var emptyMsg = obj.attr(settings.emptyMsgAttr);
            validatorResult = checkIsEmpty(form, obj, emptyMsg);
        }
        // 如果有值，继续进行其他校验
        if (obj.val() != "") {
            isEmptyAttr = "false";
        }

        if (getElemType(obj) == "INPUT") {
            // 进行范围验证 
            if (isEmptyAttr != "true" && validatorResult) {
                var rangeMsg = obj.attr(settings.rangeMsgAttr);
                var minValue = obj.attr(settings.minAttr);
                var maxValue = obj.attr(settings.maxAttr);
                if (minValue || maxValue) {
                    validatorResult = checkLenRange(form, obj, rangeMsg, minValue, maxValue);
                }
            }
            // 进行正则验证
            if (isEmptyAttr != "true" && validatorResult) {
                var regex = obj.attr(settings.regexAttr);
                if (regex) {
                    var regexMsg = obj.attr(settings.regexMsgAttr);
                    validatorResult = checkRegex(form, obj, regex, regexMsg);
                }
            }

            // 进行比较验证
            if (isEmptyAttr != "true" && validatorResult) {
                var compareElem = obj.attr(settings.compareElemIdAttr);
                if (compareElem) {
                    compareElem = $("#" + compareElem);
                    var compareMsg = obj.attr(settings.compareElemMsgAttr);
                    validatorResult = checkCompareElem(from, obj, compareElem, compareMsg);
                }
            }

            // 进行AJAX验证
            if (isEmptyAttr != "true" && validatorResult) {
                var ajaxUrl = obj.attr(settings.ajaxAttr);
                if (ajaxUrl) {
                    // validatorResult = undefined;
                    showMessage(form, obj[0].name, "Loadding");
                    var value = $.trim(obj.val());
                    $.ajax({
                        url: ajaxUrl + "?value=" + value,
                        type: "GET",
                        dataType: "json",
                        cache: false,
                        success: function (data) {
                            if (data) {
                                if (data.IsRight) {
                                    showMessage(from, obj[0].name, "Right");
                                } else {
                                    showMessage(from, obj[0].name, "Error", data.Msg);
                                }
                            } else {
                                showMessage(from, obj[0].name, "Error", "访问超时，请稍后重试");
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            showMessage(from, obj[0].name, "Error", "访问超时，请稍后重试");
                            isPass = false;
                            return isPass;
                        }
                    });
                }

            }

        }

        // 输出验证结果
        if (validatorResult) {
            showMessage(form, obj[0].name, "Right");
        } else {
            isPass = false;
        }
        return isPass;
    };

    // 非空验证
    // elem  ------ 元素对象 
    // msg -------- 提示消息
    var checkIsEmpty = function (from, elem, msg) {

        var obj = elem;
        if (!(elem instanceof jQuery)) {
            obj = $(elem);
        }
        if (obj.val() == "") {
            if (settings.showType == "show") {
                showMessage(from, obj[0].name, "Error", msg);
            } else {
                alert(msg);
            }
            return false;
        };
        return true;
    };

    // 值对比
    var checkCompareElem = function (from, elem, compareElem, msg) {
        var obj = elem;
        if (!(elem instanceof jQuery)) {
            obj = $(elem);
        }
        var obj2 = compareElem;
        if (!(compareElem instanceof jQuery)) {
            obj2 = $(compareElem);
        }

        if ($.trim(obj.val()) != $.trim(obj2.val())) {
            if (settings.showType == "show") {
                showMessage(from, obj[0].name, "Error", msg);
            } else {
                alert(msg);
            }
            return false;
        }
        return true;
    };

    // 长度验证
    var checkLenRange = function (from, elem, msg, min, max) {
        var obj = elem;
        if (!(elem instanceof jQuery)) {
            obj = $(elem);
        }
        var objValue = obj.val();
        if (min != undefined && min != null) {
            if (objValue.length < min) {
                if (settings.showType == "show") {
                    showMessage(from, obj[0].name, "Error", msg);
                } else {
                    alert(msg);
                }
                return false;
            }
        }
        if (max != undefined && min != null) {
            if (objValue.length > max) {
                if (settings.showType == "show") {
                    showMessage(from, obj[0].name, "Error", msg);
                } else {
                    alert(msg);
                }
                return false;
            }
        }
        return true;
    };


    // 正则验证
    var checkRegex = function (form, elem, regex, msg) {
        var obj = elem;
        if (!(elem instanceof jQuery)) {
            obj = $(elem);
        }
        try {
            if (!eval(regex).test($.trim(obj.val()))) {
                if (settings.showType == "show") {
                    showMessage(form, obj[0].name, "Error", msg);
                    return false;
                } else {
                    alert(msg);
                    return false;
                }
            }
            return true;
        }
        catch (err) {
            alert("正则表达式格式错误!");
            return false;
        }
    };

    // 获取文本框的类型 
    var getElemType = function (elem) {
        var obj = elem;
        if (!(elem instanceof jQuery)) {
            obj = $(elem);
        }
        return obj[0].tagName;
    };

    // 显示验证提示  
    // form -------- 表单对象
    // elemId ------ 元素对象ID
    // 消息类型 ---- Error 错误提示，Right 正确提示 Default 默认提示  Focus 鼠标移开提示 Loadding AJAX验证加载
    // 提示内容  
    var showMessage = function (form, elemId, msgType, msg) {
        var tipObj = form.find("#" + elemId + settings.tipSuffix);
        if (tipObj[0]) {
            // 错误提示
            if (msgType == "Error") {
                tipObj.html("&nbsp;");
                tipObj.html(msg);
                tipObj.removeClass();
                tipObj.addClass(settings.msgErrorClassName);
                tipObj.show();
            }
            // 正确提示
            else if (msgType == "Right") {
                tipObj.html("&nbsp;");
                tipObj.html(msg);
                tipObj.removeClass();
                tipObj.addClass(settings.msgRightClassName);
                tipObj.hide();
            }
            else if (msgType == "Default") {
                tipObj.html("&nbsp;");
                tipObj.html(msg);
                tipObj.removeClass();
                tipObj.addClass(settings.msgDefaultClassName);
                tipObj.show();
            } else if (msgType == "Focus") {
                tipObj.html("&nbsp;");
                tipObj.html(msg);
                tipObj.removeClass();
                tipObj.addClass(settings.msgFocusClassName);
                tipObj.show();
            } else if (msgType == "Loadding") {
                tipObj.html("");
                tipObj.html("数据读取中，请稍后...");
                tipObj.removeClass();
                tipObj.addClass(settings.msgAjaxClassName);
                tipObj.show();
            }
        }
    };
})(jQuery);
 