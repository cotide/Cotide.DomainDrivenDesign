/************************************************
* 功能说明: Javascript Code 1.0
* 创建时间: 2013-7-24
*   创建人: 贺隽
*     描述: Javascript 扩展库
/************************************************/
/*------------------------------------------------String 命名空间扩展-----------------------------------------*/
/*获取GUID*/
String.guid = function () {
    var S4 = function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}
/*字符串format*/
String.format = function (fmt) {
    var params = arguments;
    var pattern = /{{|{[1-9][0-9]*}|\x7B0\x7D/g;
    return fmt.replace(
		        pattern,
		        function (p) {
		            if (p == "{{") return "{";
		            return params[parseInt(p.substr(1, p.length - 2), 10) + 1]
		        }
	        );
}
/*添加时间戳*/
String.prototype.urlstamp = function () {
    var template = "{0}?_t={1}";
    if (this.indexOf("?") != -1) {
        template = "{0}&_t={1}";
    }
    return String.format(template, this, Date.parse(new Date()));
}
/*添加url参数*/
String.prototype.addurlpara = function (name, value) {
    var template = "{0}?{1}={2}";
    if (this.indexOf("?") != -1) {
        template = "{0}&{1}={2}";
    }
    return String.format(template, this, name, value);
}
/*判断字符串是否包含指定的字符*/
String.isContent = function (str, tag) {
    if (str.indexOf(tag) >= 0) {
        return true;
    }
    else {
        return false;
    }
}

/* 制保留2位小数，如：2，会在2后面补上00.即2.00  */
String.prototype.toDecimal2 = function () {
    var f = parseFloat(this);
    if (isNaN(f)) {
        return false;
    }
    var f = Math.round(this * 100) / 100;
    var s = f.toString();
    var rs = s.indexOf('.');
    if (rs < 0) {
        rs = s.length;
        s += '.';
    }
    while (s.length <= rs + 2) {
        s += '0';
    }
    return s;
};

/* 字符串截取，中文按两个字节处理 */
String.prototype.sub = function (n) {
    var r = /[^\x00-\xff]/g;
    if (this.replace(r, "mm").length <= n) return this;
    var m = Math.floor(n / 2);
    for (var i = m; i < this.length; i++) {
        if (this.substr(0, i).replace(r, "mm").length >= n) {
            return this.substr(0, i) + "...";
        }
    }
    return this;
}; 

/* 字符串TrimStart处理 */
String.prototype.trimStart = function (trimStr) {
    if (!trimStr) { return this; }
    var temp = this;
    while (true) {
        if (temp.substr(0, trimStr.length) != trimStr) {
            break;
        }
        temp = temp.substr(trimStr.length);
    }
    return temp;
};
/* 字符串TrimEnd处理 */
String.prototype.trimEnd = function (trimStr) {
    if (!trimStr) { return this; }
    var temp = this;
    while (true) {
        if (temp.substr(temp.length - trimStr.length, trimStr.length) != trimStr) {
            break;
        }
        temp = temp.substr(0, temp.length - trimStr.length);
    }
    return temp;
};
/* 字符串Trim处理 */
String.prototype.trim = function (trimStr) {
    var temp = trimStr;
    if (!trimStr) { temp = " "; }
    return this.trimStart(temp).trimEnd(temp);
};
