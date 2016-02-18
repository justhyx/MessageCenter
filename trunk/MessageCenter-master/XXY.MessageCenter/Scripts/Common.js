"use strict";

if (window.GlobalDatas == undefined) {
    window.GlobalDatas = { RootCompanyID: 0, Lang: 'zh-CN' };
}


/*重写window.alert方法*/
(function (_) {

    _.NativeAlert = window.alert;

    _.alert = function (message) {
        XXY.ColorBox.Alert("tips", Lang.ALERT_TITLE, message, null, false, null, Lang.CLOSE);
    };


    _.doFunction = function (fun) {
        var args = [], i;
        for (i = 1; i < arguments.length; i++) {
            args.push(arguments[i]);
        }
        return function () {
            fun.apply(null, args);
        };
    };


    _.automap = function (data, type, writeNotExistsProperty, maps, callback, propertyWrap) {
        /// <summary>
        /// 将 Json 对象转换为目标类型的对象
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="type">目标类型</param>
        /// <param name="writeNotExistsProperty">如果目标类型中不存在对应的属性,是否写入,默认不写入</param>
        /// <param name="maps">属性映射列表,如{PropertyA:'PA',PropertyB:'B',...}</param>
        /// <returns type="">目标类型的数据,如果原数据是数组,结果目标类型的数组</returns>
        if (data == null)
            return null;

        if (data instanceof Array) {
            var item, result = [];
            for (var i = 0; item = data[i]; i++) {
                result.push(automap(item, type, writeNotExistsProperty, maps, callback, propertyWrap));
            }
            return result;
        } else if (data instanceof Object) {
            var result = new type();
            for (var k in data) {
                var targetProperty = k;
                var value = data[k];

                if (maps != null && maps[k] != undefined) {
                    if (typeof (maps[k]) == "string") {
                        //if (typeof (propertyWrap) == "function")
                        //    targetProperty = propertyWrap(maps[k])
                        //else
                        targetProperty = maps[k];
                    } else if (typeof (maps[k]) == "function") {
                        //TODO msWriteProfilerMark 是什么?
                        value = automap(value, maps[k], writeNotExistsProperty, null, callback, propertyWrap)
                    }
                } else if (typeof (propertyWrap) == "function") {
                    value = propertyWrap(value, k);
                }

                //不严格等于
                if (result[targetProperty] !== undefined || writeNotExistsProperty) {
                    result[targetProperty] = value;
                }
            }
            if (callback instanceof Function) {
                callback(result, data);
            }
            return result;
        }
    }

    _.ExtendTypeFromJson = function (json, type, propertyWrap) {
        for (var item in json) {
            var value = json[item];

            if (typeof (propertyWrap) == "function")
                type.prototype[item] = propertyWrap(value);
            else
                type.prototype[item] = value;
        }
    }

    _.ExtendConstrctorFromJson = function (json, type, propertyWrap) {

        var __type = type;
        type = function () {
            __type.call(this);


            for (var item in json) {
                var value = json[item];

                if (typeof (propertyWrap) == "function")
                    this[item] = propertyWrap(value, item);
                else
                    this[item] = value;
            }
        }

        return type;
    }


})(window);

(function () {
    $.extend($.expr[':'], {
        regex: function (elem, index, match) {
            var matchParams = match[3].match(/(\w+?),(.+)/);
            var validLabels = /^(data|css):/;
            var attr = {
                method: matchParams[1].match(validLabels) ? matchParams[1].split(':')[1] : 'attr',
                property: matchParams[1]
            };
            var regexOpts = matchParams[2].match(/\/(.+)?\/(\w*)/);
            var regex = new RegExp(regexOpts[1], regexOpts.length == 3 ? regexOpts[2] : "");
            return regex.test(jQuery(elem)[attr.method](attr.property));
        }
    });

    $.fn.smartFloat = function () {
        var position = function (element) {
            var top = element.offset().top, pos = element.css("position");
            $(window).scroll(function () {
                var scrolls = $(this).scrollTop();
                if (scrolls > top) {
                    if (window.XMLHttpRequest) {
                        element.css({ position: "fixed", top: 0 });
                    } else {
                        element.css({ top: scrolls });
                    }
                } else {
                    //element.css({position: pos,top: top});	
                    element.removeAttr("style");
                }
            });
        };
        return $(this).each(function () {
            position($(this));
        });
    };
})();

(function () {

    Array.prototype.indexOf2 = function (ele) {
        /// <summary>
        /// 非严格等于
        /// IE下 数组没有 indexOf
        /// jquery.inArray 和 其它浏览器的 indexOf 是严格等于的
        /// </summary>
        /// <param name="ele"></param>

        for (var i = 0, length = this.length; i < length; i++) {
            if (this[i] == ele) {
                return i;
            }
        }
        return -1;
    };

    Array.prototype.contains = function (element) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == element) {
                return true;
            }
        }
        return false;
    };

    Array.prototype.intersection = function (arr) {
        var t1 = this, t2 = arr;
        if (t1.length > t2.length) {
            var t = t1;
            t1 = t2;
            t2 = t;
        }
        var reg = new RegExp("(," + t2.join(",)|(,") + ",)", "g");
        var ma = ("," + t1.join(",,") + ",").match(reg);
        var result = [];
        if (ma != null) {
            for (var i = 0; i < ma.length; i++) {
                result.push(ma[i].substr(1, ma[i].length - 2));
            }
        }
        return result.distinct();
    };

    Array.prototype.distinct = function () {
        /// <summary>
        /// 只接受数字和字符串数组的 distinct
        /// jquery 的 unique 在IE下对非DOM数组不起作用
        /// </summary>

        var o = {};
        for (var i = 0; i < this.length; i++) {
            var t = typeof (this[i]);
            if (t == "number" || t == "string") {
                o[this[i]] = 1;
            } else {
                throw new Exception("不支持非字母或数字数组的 Distinct");
            }
        }

        var arr = [];
        for (var oo in o) {
            arr.push(oo);
        }

        return arr;
    };


    String.prototype.GetLength = function () { return this.replace(/[^\x00-\xff]/g, 'xx').length; };
    String.prototype.trim = function () { return this.replace(/(^\s*)|(\s*$)/g, ""); };
    String.prototype.repeat = function (n) {
        var arr = new Array(n + 1);
        return arr.join(this);
    };
    String.prototype.padLeft = function (n, s) {
        // IE 不接受 substar 的第一个参数为负数的
        //return (s.repeat(n) + this).substr(-n);
        if ("000".substr(-2) != "000")
            return (s.repeat(n) + this).substr(-n);
        else
            return (s.repeat(n) + this).match(new RegExp("([\\s\\S]{" + n + "}$)"))[0];
    };
    String.prototype.padRight = function (n, s) {
        return (this + s.repeat(n)).substring(0, n);
    };

    String.prototype.toDateExact = function (fmt, defaultDate) {
        /// <summary>
        /// 按格式转换成日期
        /// </summary>
        /// <param name="fmt">日期格式</param>
        /// <param name="defaultDate"></param>

        defaultDate = defaultDate == undefined ? null : defaultDate;

        var ps = [];
        fmt = fmt.replace(/((yyyy)|(yy)|(MM)|(M)|(dd)|(d)|(HH)|(H)|(hh)|(h)|(mm)|(m)|(ss)|(s))/g, function (k) {
            ps.push(k.split("")[0]);
            if (k.length == 1) {
                return "(\\d{1,2})";
            } else {
                return "(\\d{" + k.length + "})";
            }
        });

        var year = 1, month = 1, day = 1, hour = 1, second = 1, minute = 1;

        var reg = new RegExp(fmt);
        var ms = this.match(reg);
        if (ms != null) {
            for (var i = 1; i < ms.length; i++) {
                var p = ps[i - 1];
                var v = ms[i];
                switch (p) {
                    case "y":
                        year = v;
                        break;
                    case "M":
                        month = parseInt(v) - 1;
                        break;
                    case "d":
                        day = v;
                        break;
                    case "H":
                        hour = v;
                        break;
                    case "m":
                        minute = v;
                        break;
                    case "s":
                        second = v;
                        break;
                }
            }

            // 因为25点,会把日期加1, 13月会把年加1,等, 所以比较字符串,如果年月日相等,则是一个有效的日期
            var d = new Date(year, month, day, hour, minute, second);
            var d2 = new Date(year, month, day);
            return (minute < 60 && hour <= 23 && d2.toString("yyyy/MM/dd") == d.toString("yyyy/MM/dd")) ? d : defaultDate;
        } else {
            return defaultDate;
        }
    };

    String.prototype.toDate = function (defaultDate) {
        /// <summary>
        /// 将给定的字符串转换为日期, defaultDate 默认可以不填,返回 null
        /// </summary>
        /// <param name="defaultDate" type="Date">转换失败时,要返回的默认值</param>
        /// <returns type="Date"></returns>
        if (defaultDate != null && !(defaultDate instanceof Date)) {
            defaultDate = defaultDate.toString().toDate();
        }

        if (defaultDate == undefined)
            defaultDate = null;

        var date = null;
        var arr = new Array();
        if (this.indexOf("-") != -1) {
            arr = this.toString().split("-");
        } else if (this.indexOf("/") != -1) {
            arr = this.toString().split("/");
        } else {
            return defaultDate;
        }

        if (arr.length != 3)
            return defaultDate;

        //yyyy-mm-dd || yyyy/mm/dd
        if (arr[0].length == 4) {
            date = new Date(arr[0], arr[1] - 1, arr[2]);
            if (date.getFullYear() == arr[0] && date.getMonth() == arr[1] - 1 && date.getDate() == arr[2]) {
                return date;
            }
        }
        //dd-mm-yyyy || dd/mm/yyyy
        if (arr[2].length == 4) {
            date = new Date(arr[2], arr[1] - 1, arr[0]);
            if (date.getFullYear() == arr[2] && date.getMonth() == arr[1] - 1 && date.getDate() == arr[0]) {
                return date;
            }
        }
        //mm-dd-yyyy || mm/dd/yyyy
        if (arr[2].length == 4) {
            date = new Date(arr[2], arr[0] - 1, arr[1]);
            if (date.getFullYear() == arr[2] && date.getMonth() == arr[0] - 1 && date.getDate() == arr[1]) {
                return date;
            }
        }

        return defaultDate;
    };

    String.prototype.isDate = function () {
        /// <summary>
        /// 给定的字符串是否可以转换为日期
        /// </summary>
        /// <returns type=""></returns>
        return this.toDate() != null;
    };

    String.prototype.asMoney = function (decimalDigits, defaultValue) {
        /// <summary>
        /// 数字转换为金额表示
        /// </summary>
        /// <param name="decimalDigits">小数位</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns type=""></returns>
        if (defaultValue == null)
            defaultValue = "";

        if (isNaN(decimalDigits)) {
            decimalDigits = 2;
        }

        var reg = /^([+-]?)(\d*)\.?(\d*)$/;

        if (!reg.test(this.trim()))
            return defaultValue;

        var match = this.trim().match(reg);
        if (match[2] + match[3] == "")
            return defaultValue;

        var part3 = match[3];
        if (part3.length < decimalDigits) {
            part3 = part3 + "0".repeat(decimalDigits - match[3].length);
        } else {
            part3 = part3.substring(0, decimalDigits);
        }

        var sign = match[1];
        if (sign != "-")
            sign = "";

        if (match[2] != "")
            return sign + match[2].split('').reverse().join('').replace(/(.{3}(?=.))/g, "$1,").split('').reverse().join('') + (part3 != "" ? "." + part3 : "");
        else
            return sign + "0." + part3;
    };

    String.prototype.format = function () {
        var str = this;
        var args = arguments;
        str = str.replace(/\{(\d+)\}/g, function () {
            var idx = arguments[1];
            return args[parseInt(idx)];
        });
        return str;
    }


    String.prototype.HtmlEncode = function () {
        var s = "";
        if (this == null || this.length == 0) return "";
        s = this.replace(/&/g, "&amp;");
        s = s.replace(/</g, "&lt;");
        s = s.replace(/>/g, "&gt;");
        s = s.replace(/ /g, "&nbsp;");
        s = s.replace(/\'/g, "&#39;");
        s = s.replace(/\"/g, "&quot;");
        s = s.replace(/\n/g, "<br/>");
        return s;
    }


    String.prototype.HtmlDecode = function () {
        var s = "";
        if (this == null || this.length == 0) return "";
        s = this.replace(/&amp;/g, "&");
        s = s.replace(/&lt;/g, "<");
        s = s.replace(/&gt;/g, ">");
        s = s.replace(/&nbsp;/g, " ");
        s = s.replace(/&#39;/g, "\'");
        s = s.replace(/&quot;/g, "\"");
        s = s.replace(/<br\/>/g, "\n");
        return s;
    }

    var __dateToString__ = Date.prototype.toString;
    /*仅支持以下格式：MM２长度月，yyyy四长度年，yy２长度年，dd２长日期, t / T AM or PM (小写返回小，大写返回大写)
	HH 2长度小时(24) hh2长度小时（12进制） mm 2长度分钟,ss 2长度秒
	M 1和2长度月, d 1或２长度日期 m,h,s 同上
	*/
    Date.prototype.toString = function (fmt) {
        var date = this;
        if (!fmt)
            return __dateToString__.apply(this);

        var month = date.getMonth() + 1;
        var year = date.getFullYear();

        fmt = fmt.replace(/((yyyy)|(yy)|(MM)|(M)|(dd)|(d)|(HH)|(H)|(hh)|(h)|(mm)|(m)|(ss)|(s)|(T)|(t))/g, function (key) {
            //var leng = ma.length;
            switch (key) {
                case "yyyy":
                    return year;
                    break;
                case "yy":
                    return year % 100;
                    break;
                case "MM":
                    return month.toString().padLeft(2, "0");
                    break;
                case "M":
                    return month;
                    break;
                case "dd":
                    return date.getDate().toString().padLeft(2, "0");
                    break;
                case "d":
                    return date.getDate();
                    break;
                case "HH":
                    return date.getHours().toString().padLeft(2, "0");
                    break;
                case "H":
                    return date.getHours();
                    break;
                case "hh":
                    return (date.getHours() % 12).toString().padLeft(2, "0");
                    break;
                case "h":
                    return date.getHours() % 12;
                    break;
                case "mm":
                    return date.getMinutes().toString().padLeft(2, "0");
                    break;
                case "m":
                    return date.getMinutes();
                    break;
                case "ss":
                    return date.getSeconds().toString().padLeft(2, "0");
                    break;
                case "s":
                    return date.getSeconds();
                    break;
                case "t":
                    return date.getHours() > 11 ? "pm" : "am";
                    break;
                case "T":
                    return date.getHours() > 11 ? "PM" : "AM";
                    break;
                default:
                    return key;
            }
        });
        return fmt;
    };

    Date.prototype.getWeekOfYear = function (sundayAsFirst) {
        /// <summary>
        /// <param name='sundayAsFirst'>周日做为第一天,默认 false</param>
        /// </summary>
        var year = this.getFullYear();
        var sDate = new Date(year, 0, 1);//年的第一天
        var day = sDate.getDay();//年的第一天 在周中的第几天

        if (sundayAsFirst)
            day++;

        sDate = new Date(sDate - day * 86400000);//周的第一天的日期

        return Math.ceil((this - sDate) / 86400000 / 7);
    }

    Date.prototype.add = function (datePart, v) {
        /// <summary>
        /// 返回新值，並且不改變舊值 datePart yy:year, m:month, d: date,h:hour, mi:minute, s:second, ms:毫秒
        /// </summary>
        /// <param name="datePart"></param>
        /// <param name="v"></param>
        /// <returns type=""></returns>
        var reg = /^((yy)|(m)|(d)|(h)|(mi)|(s)|(ms))$/i;
        if (!reg.test(datePart)) {
            throw new Error("Invalid argument datePart, yy:year, m:month, d: date,h:hour, mi:minute, s:second, ms:millisecond");
        }

        if (isNaN(v)) {
            return this;
        }

        // Date 是地址引用
        var tmp = new Date(this.valueOf());

        var key = datePart.match(reg)[0].toUpperCase();
        var vv = this.valueOf();
        switch (key) {
            case "YY":
                vv = tmp.setFullYear(this.getFullYear() + v);
                break;
            case "M":
                vv = tmp.setMonth(this.getMonth() + v);
                break;
            case "D":
                vv = tmp.setDate(this.getDate() + v);
                break;
            case "H":
                vv = tmp.setHours(this.getHours() + v);
                break;
            case "MI":
                vv = tmp.setMinutes(this.getMinutes() + v);
                break;
            case "S":
                vv = tmp.setSeconds(this.getSeconds() + v);
                break;
            case "MS":
                vv = tmp.setMilliseconds(this.getMilliseconds() + v);
                break;
        }
        return new Date(vv);
    };
})();


var CookieHelper = {};
(function ($) {

    $.getExpires = function (y, m, d, h, i, s, ms) {
        var date = new Date();
        y = isNaN(y) ? date.getFullYear() : y;
        m = isNaN(m) ? date.getMonth() : m - 1;
        d = isNaN(d) ? date.getDate() : d;

        h = isNaN(h) ? date.getHours() : h;
        i = isNaN(i) ? date.getMinutes() : i;
        s = isNaN(s) ? date.getSeconds() : s;
        ms = isNaN(ms) ? date.getMilliseconds() : ms;

        return new Date(y, m, d, h, i, s, ms).toUTCString();
    }

    $.getExpiresByUTCString = function (UTCString) {
        var s = new Date(UTCString).toUTCString();
        if (s == 'NaN' || s == 'Invalid Date')
            return null; // IE,Opera NaN , FF,Safari Invalid Date;
        else
            return s;
    }


    $.set = function (k, v, expires, path, domain, secure) {
        var cookie = k + '=' + encodeURIComponent(v);

        if (expires) cookie += ";expires=" + expires;
        if (path) cookie += ";path=" + path;
        if (domain) cookie += ";domain=" + domain;
        if (secure) cookie += ";secure";
        document.cookie = cookie;
    }

    $.get = function (k) {
        var cks = document.cookie.split(';');
        var t;
        for (var i = 0; i < cks.length; i++) {
            t = cks[i].split('=');
            if (k == t[0].trim()) return decodeURIComponent(t[1]);
        }
    }

    $.remove = function (k) {
        $.set(k, '', $.getExpires(new Date().getFullYear() - 1));
    }

    $.empty = function () {
        var cks = document.cookie.split(';');
        var t;
        for (var i = 0; i < cks.length; i++) {
            $.remove(cks[i].split('=')[0].trim());
        }
    }
})(CookieHelper);

var ImageHelper = {};
(function (_) {

    _.Reload = function (oldImg) {
        var src = oldImg.src;
        var img = new Image();
        img.src = src + (src.indexOf("?") == -1 ? "?" : "&") + "tmp=" + (new Date()).valueOf().toString(32);
        img.width = oldImg.width;
        img.height = oldImg.height;
        $(oldImg).parent().append(img);
        $(oldImg).remove();
        img.onclick = doFunction(ImageHelper.Reload, img);
    }

})(ImageHelper);


var HtmlHelper = {};
(function (o) {
    o.EnableSubmit = function () {
        $(":submit").each(function (idx, item) {
            var txt = $(item).attr("__txt");
            if (item.tagName == "BUTTON")
                $(item).attr("disabled", false).html(txt);
            else if (item.tagName == "INPUT")
                $(item).attr("disabled", false).val(txt);
        });
        $(document).unbind(".cancel");
    };

    o.DisableSubmit = function () {
        $(":submit").each(function (idx, item) {
            if (item.tagName == "BUTTON")
                $(item).attr({ "__txt": $(item).html(), disabled: true })
					.html(Lang.IN_PROCESS);
            else if (item.tagName == "INPUT")
                $(item).attr({ "__txt": $(item).val(), disabled: true })
					.val(Lang.IN_PROCESS);
        });
        $(document).bind("keydown.cancel", function (e) {
            if (e.keyCode == 27) {
                o.EnableSubmit();
            }
        });
    };

    $(window).bind("beforeunload", function () {
        o.DisableSubmit();
    }).bind("unload", function () {
        o.EnableSubmit();
    });

    $('a').on('click', function (e) {
        //$('a').live('click', function (e) {
        var jslinkstart = "javascript:";
        var thisLinkHref = $(this).attr('href') || "";
        var thisLinkJs = thisLinkHref.substring(jslinkstart.length);
        if (thisLinkHref) {
            var thisLinkHrefStart = thisLinkHref.substring(0, jslinkstart.length);
            if (thisLinkHrefStart == jslinkstart) {
                e.preventDefault();
                eval(thisLinkJs);
            }
        }
    });

    o.SetRequired = function (obj) {
        var mySpan = obj.closest("div").find("span[class=help-block]");
        obj.attr("data-val-required", mySpan.html() + LangHelper[LangHelper.Lang].FieldNotNull);
        mySpan.html(mySpan.html() + "<span class='red'>*</span>");
    };


})(HtmlHelper);


var ValidationHelper = {};
(function (o) {

    o.Invalidate = function (form, validator) {

    };

    var Helper = function (form) {

        var setting = null;
        var errorPlacement = null;
        var success = null;

        this.ready = function (mode) {
            $(document).ready(function () {
                try {
                    var obj = $.data(form, 'validator');
                    if (obj) {
                        var validator = $.data(form, 'validator')

                        validator.showLabel = function (element, message) {
                            if (message)
                                mode3_error(message, element);
                            else
                                mode3_success(null, element)
                        }

                        setting = validator.settings;
                        setting.ignore = "input[type='hidden']";
                        errorPlacement = setting.errorPlacement;
                        success = setting.success;
                        setting.errorPlacement = $.proxy(mode3_error, form);
                        setting.success = $.proxy(mode3_success, form);
                        //setting.showErrors = function (errorMap, errorList) {
                        //    var err = null;
                        //    for (var i = 0; err = errorList[i]; i++) {
                        //        mode3_error(err.message, err.element);
                        //    }
                        //}

                        $(form).unbind("invalid-form.validate")
							.bind("invalid-form.validate", function () {
							    o.Invalidate(this, arguments[1]);
							});
                    }

                } catch (e) {
                }
            });
        };

        var mode3_error = function (error, inputElement) {
            $(inputElement).parent().addClass("has-error");
            $(inputElement).tooltip('destroy');
            $(inputElement).tooltip({ title: error, html: true });
        };

        var mode3_success = function (error, inputElement) {
            if (inputElement.className.indexOf("input-validation-error") > -1) {
                $(inputElement).tooltip('destroy');
                $(inputElement).parent().removeClass("has-error");
                $("span[for='" + inputElement.name + "']").parent().parent().remove();
            }
        };
    };

    $("form").each(function (idx, form) {
        var helper = new Helper(form);
        helper.ready();
    });

    var checkDate = function (value, element, params) {
        var fmt = params["format"] || "";
        if (fmt.trim() != "") {
            return this.optional(element) || value.toDateExact(fmt) != null;
        } else
            return this.optional(element) || value.toDate() != null;
    };

    var mustChecked = function (value, element) {
        return $(element).is(':checked');
    };

    var dateRange = function (value, element, params) {
        if (this.optional(element)) {
            return true;
        }

        var startDate = (params["min"] || "0001/1/1").toDate(); //$(element).attr("data-val-dateRange-min").toDate();
        var endDate = (params["max"] || "9999/12/31").toDate(); //$(element).attr("data-val-dateRange-max").toDate();
        var format = params["format"];
        var enteredDate = format == "" ? value.toDate() : value.toDateExact(format);

        return ((startDate <= enteredDate) && (enteredDate <= endDate));
    };

    var requiredIf = function (value, element, params) {
        var prefix = getModelPrefix(element.name);
        var fullDependencyName = appendModelPrefix(params["dependency"], prefix);
        var dependency = $(element.form).find(":input[name='" + fullDependencyName + "']");
        if (dependency.length == 0)
            return true;

        var dependencyValue = params["dependencyvalue"];

        var acturalValue = null;
        if (dependency.attr("type") == "checkbox") {
            //acturalValue = dependency.is(":checked") ? dependency.val().toLowerCase() : null;
            //mvc 的 checkbox 只有 true 和 false 两个值
            acturalValue = dependency.is(":checked") ? "true" : "false";
        } else {
            acturalValue = dependency.val().toLowerCase();
        }

        var dvs = null;
        eval("dvs = " + dependencyValue);
        if (dvs.indexOf2(acturalValue) >= 0) {
            return $.validator.methods.required.call(this, value, element, params);
        }

        return true;
    };


    var mRemote = function (value, element, param) {
        //自定义 remote

        if (this.optional(element)) {
            return "dependency-mismatch";
        }

        var previous = this.previousValue(element),
			validator, data;

        if (!this.settings.messages[element.name]) {
            this.settings.messages[element.name] = {};
        }
        previous.originalMessage = this.settings.messages[element.name].remote;
        this.settings.messages[element.name].remote = previous.message;

        param = typeof param === "string" && { url: param } || param;

        var flag = true;
        var additionValues = {};
        for (var a in param.data) {
            additionValues[a] = param.data[a]();
            flag = flag && ((previous.old != null && previous.old[a] == additionValues[a]) || false);
        }

        if (flag)
            return previous.valid;

        //if (previous.old === value) {
        //    return previous.valid;
        //}
        //previous.old = value;
        previous.old = additionValues;
        validator = this;
        this.startRequest(element);
        data = {};
        data[param.paramName] = value;
        $.ajax($.extend(true, {
            url: param,
            mode: "abort",
            port: "validate" + element.name,
            dataType: "json",
            data: data,
            context: validator.currentForm,
            success: function (response) {
                var valid = response === true || response === "true",
					errors, message, submitted;

                validator.settings.messages[element.name].remote = previous.originalMessage;
                if (valid) {
                    submitted = validator.formSubmitted;
                    validator.prepareElement(element);
                    validator.formSubmitted = submitted;
                    validator.successList.push(element);
                    delete validator.invalid[element.name];
                    validator.showErrors();
                } else {
                    errors = {};
                    message = response || validator.defaultMessage(element, "remote");
                    errors[element.name] = previous.message = $.isFunction(message) ? message(value) : message;
                    validator.invalid[element.name] = true;
                    validator.showErrors(errors);
                }
                previous.valid = valid;
                validator.stopRequest(element, valid);
            }
        }, param));
        return "pending";
    }

    var compareWith = function (value, element, params) {
        if (this.optional(element))
            return true;

        var prefix = getModelPrefix(element.name);
        var fullCompareWithPropName = appendModelPrefix(params["to"], prefix);
        //var compareWith = $(":input[name='" + fullCompareWithPropName + "']");
        //优化
        var compareWith = element.form[fullCompareWithPropName];
        if (!compareWith)
            return true;

        var opts = { Gt: ">", GtOrEqual: ">=", Lt: "<", LtOrEqual: "<=", Equal: "==", NotEqual: "!=" };
        var opt = opts[params["opt"]];
        var compareValue = compareWith.value;

        var v1, v2;
        if (compareWith.getAttribute("data-val-date")) {
            var fmt = compareWith.getAttribute("data-val-date-format");
            v2 = fmt != undefined ? compareValue.toDateExact(fmt) : compareValue.toDate();

            fmt = element.getAttribute("data-val-date-format");
            v1 = fmt != undefined ? value.toDateExact(fmt) : value.toDate();

            if (v1 == null || v2 == null)
                return true;
        } else if (compareWith.getAttribute("data-val-number")) {
            v1 = new Number(value);
            v2 = new Number(compareValue);

            if (isNaN(v1) || isNaN(v2))
                return true;
        }

        var result = null;
        eval("result = v1" + opt + "v2");

        return result;
    }

    var getModelPrefix = function (fieldName) {
        return fieldName.substr(0, fieldName.lastIndexOf(".") + 1);
    };

    var appendModelPrefix = function (value, prefix) {
        if (value.indexOf("*.") === 0) {
            value = value.replace("*.", prefix);
        }
        return value;
    };

    var dateAdapter = function (options) {
        options.rules['date'] = options.params;
        options.messages['date'] = Lang.INVALID_DATE;
    };

    var dateRangeAdapter = function (options) {
        options.rules['dateRange'] = options.params;
        options.messages['dateRange'] = options.message;
    };

    var mustCheckedAdapter = function (options) {
        options.rules['mustChecked'] = options.params;
        options.messages['mustChecked'] = options.message;
    };

    var requiredIfAdapter = function (options) {
        options.rules["requiredIf"] = options.params;
        options.messages["requiredIf"] = options.message;
    };

    var splitAndTrim = function (value) {
        return value.replace(/^\s+|\s+$/g, "").split(/\s*,\s*/g);
    }

    var escapeAttributeValue = function (value) {
        // As mentioned on http://api.jquery.com/category/selectors/
        return value.replace(/([!"#$%&'()*+,./:;<=>?@\[\\\]^`{|}~])/g, "\\$1");
    }

    var mRemoteAdapter = function (options) {
        var value = {
            url: options.params.url,
            type: options.params.type || "GET",
            data: {},
            paramName: options.params.paramname || ""
        },
			prefix = getModelPrefix(options.element.name);

        $.each(splitAndTrim(options.params.additionalfields || options.element.name), function (i, fieldName) {
            var paramName = appendModelPrefix(fieldName, prefix);
            value.data[paramName] = function () {
                //优化
                //return $(options.form).find(":input").filter("[name='" + escapeAttributeValue(paramName) + "']").val();
                var ele = options.form.getElementsByTagName(escapeAttributeValue(paramName));
                return ele != null ? ele.value : null;
            };
        });

        //setValidationValues(options, "mRemote", value);

        options.rules["mRemote"] = value;
        if (options.message) {
            options.messages["mRemote"] = options.message;
        }
    };

    var compareWithAdapter = function (options) {
        options.rules["compareWith"] = options.params;
        options.messages["compareWith"] = options.message;
    }

    if ($.validator != undefined) {
        var ms = $.validator.messages;
        ms["email"] = Lang.INVALID_EMAIL;
        ms["date"] = Lang.INVALID_DATE;

        $.validator.addMethod("date", checkDate);
        $.validator.addMethod("dateRange", dateRange);
        $.validator.addMethod('requiredIf', requiredIf);
        $.validator.addMethod('mustChecked', mustChecked);
        $.validator.addMethod('mRemote', mRemote);
        $.validator.addMethod('compareWith', compareWith);

        //$.validator.unobtrusive.adapters["date", { name: dateAdapter , params: ['format'] , adapt: dateAdapter }];
        $.validator.unobtrusive.adapters.add("date", ["format"], dateAdapter);
        $.validator.unobtrusive.adapters.add("dateRange", ['min', 'max', 'format'], dateRangeAdapter);
        $.validator.unobtrusive.adapters.add('requiredIf', ['dependency', 'dependencyvalue'], requiredIfAdapter);
        $.validator.unobtrusive.adapters.add('mustchecked', [], mustCheckedAdapter);
        $.validator.unobtrusive.adapters.add("mRemote", ["url", "type", "additionalfields", "paramname"], mRemoteAdapter);
        $.validator.unobtrusive.adapters.add('compareWith', ['to', 'opt'], compareWithAdapter);
    }

    o.ManualCheck = function () {
        $("form").each(function (idx, form) {
            var helper = new Helper(form);
            helper.ready();
        });
    };

    o.ResetValidator = function () {
        //jquery.validate 的 validate 方法,大概在31行,会判断当前表单是 data('validator')是不是存在,如果存在,就不会用新的.
        $("form").each(function (idx, form) {
            $(form).data('validator', null);
            $.validator.unobtrusive.parse(form);
            new Helper(form).ready();
        })

    }

})(ValidationHelper);


var PaginationHelper = {};
(function (p) {
    p.Set = function (name, page) {
        var hp = $(":hidden[name='" + name + "']");
        hp.val(page);
        hp.parents("form").submit();
        return false;
    }

    $(":submit").on("click", function () {
        $(":hidden[data-pager='true']").val("");
    });
})(PaginationHelper)



$(".sidebarCloser").on("click", function () {
    $(".sidebar").switchClass("", "sidebarMini");
    $(".mainContent").switchClass("", "mainContentMax");
    $(".sidebarMini").switchClass("sidebarMini", "");
    $(".mainContentMax").switchClass("mainContentMax", "");
});

var IndexPageHelper = {};
(function (i) {

    i.HistorySetted = false;

    i.SetHistory = function () {
        //页面加载完成时才做下面这些动作
        $(function () {
            i.HistorySetted = true;
            //不要试图动态创建这个 hidden input , 动态创建的, 回退时,无法还原
            //如果动态创建, 回退的时候, 是得不到这个hidden input 的, 每次都会 appendTo
            //if ($("#historyIndex").length == 0)
            //    $("<input type='hidden' id='historyIndex' />").appendTo(document.body);

            if ($("#historyIndex").val() == "") {
                //新页面会执行这里
                var str = history.length + "|" + location.pathname;
                $("#historyIndex").val(str);
                CookieHelper.set("historyIndex", str, null, "/");
            } else {
                //回退页面会执行这里
                //CookieHelper.set("historyIndex", $("#historyIndex").val(), null, "/");
            }
        });
    }


    i.GoToIndex = function (defaultHref) {
        var tmp = (CookieHelper.get("historyIndex") || "|").split("|");
        var idx = tmp[0];
        var path = tmp[1];
        var backCount = history.length - idx;
        if (backCount > 0 && path.indexOf(defaultHref) >= 0)
            history.go(-backCount);
        else {
            location.href = defaultHref;
        }
    };

    $("a[data-xxy-action]").on("click", function () {
        var action = $(this).attr("data-xxy-action");
        if (confirm(Lang.CONFIRM_ACTION.format(action))) {
            //location.href = this.href;
            var form = $(this).parents("form")[0];
            form.action = this.href;
            form.submit();
        }
        return false;
    });

})(IndexPageHelper);

var HtmlUtil = {
    /*1.用浏览器内部转换器实现html转码*/
    htmlEncode: function (html) {
        //1.首先动态创建一个容器标签元素，如DIV
        var temp = document.createElement("div");
        //2.然后将要转换的字符串设置为这个元素的innerText(ie支持)或者textContent(火狐，google支持)
        (temp.textContent != undefined) ? (temp.textContent = html) : (temp.innerText = html);
        //3.最后返回这个元素的innerHTML，即得到经过HTML编码转换的字符串了
        var output = temp.innerHTML;
        temp = null;
        return output;
    },
    /*2.用浏览器内部转换器实现html解码*/
    htmlDecode: function (text) {
        //1.首先动态创建一个容器标签元素，如DIV
        var temp = document.createElement("div");
        //2.然后将要转换的字符串设置为这个元素的innerHTML(ie，火狐，google都支持)
        temp.innerHTML = text;
        //3.最后返回这个元素的innerText(ie支持)或者textContent(火狐，google支持)，即得到经过HTML解码的字符串了。
        var output = temp.innerText || temp.textContent;
        temp = null;
        return output;
    }
};



var ToolbarHelper = {};
(function (t) {

    t.Enabled = true;

    var init = function () {
        $("tr").find("td:last > .btn-group").wrap("<div class='_toolbar_'><div class='inner'></div></div>");
        $("tr").find("td:last ._toolbar_").hide();
        $("tr").has("td:last .btn-group")
			.mouseover(function () {
			    if ($(this).find("td:last .btn-group").children().length == 0)
			        return;

			    var pos = $(this).position();
			    var tb = $(this).find("td:last ._toolbar_");
			    var h = $(this).outerHeight()
			    tb.css({
			        height: h,
			        top: pos.top
			    })
				.show();
			})
			.mouseleave(function () {
			    $(this).find("td:last ._toolbar_").hide();
			});
        ;
    }

    $(document).ready(function () {
        if (t.Enabled)
            init();
    });

})(ToolbarHelper);