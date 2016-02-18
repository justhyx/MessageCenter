var Selector = {};
(function (sel) {
    function BaseSelector() {
        var s = this;

        //
        s.TargetCompanyID = GlobalDatas.RootCompanyID;

        //自动完成 URL
        s.AutocompleteUrl = "";
        //选择器 URL
        s.SelectorUrl = "";
        //选择器已选项的匹配项
        s.SelectorKey = "ID";
        //Autocomplete 宽度，
        //如果默认宽度，请赋为 null, 否则请给一个正整数
        s.AutocompleteWidth = null;

        ////选择器已选择数据
        //s.SelectorChoicedDatas = [];

        var ChoicedDatas = null;

        //选择器标题
        s.SelectorTitle = Lang.CHOICE;
        //选择器宽度
        s.SelectorWidth = 800;
        //选择高度
        s.SelectorHeight = 500;

        s.SelectorFor = null;
        s.SelectorForAssist = null;

        //数据
        var data = {};

        s.SetData = function (key, value) {
            data[key] = value;
        }

        s.RemoveData = function (key) {
            delete data[key];
        }

        var SelectorChoicedKeys = function () {
            /// <summary>
            /// 已选择的数据
            /// 只取 Key 标记的字段, 通知选择器哪些 Key 已选中
            /// </summary>
            var result = [];

            if (ChoicedDatas != null)
                for (var k in ChoicedDatas) {
                    result.push(k);
                }

            return result;
        };

        s.SetChoicedDatas = function (datas) {
            ///<summary>
            /// 设置选中的数据, 设置之前,请先设置 SelectorKey
            ///</summary>
            ///<param name="datas" type="Array"></param>
            if (s.SelectorKey == null || s.SelectorKey == "")
                throw new Error("请设置 SelectorKey");

            ChoicedDatas = {};
            if (datas != null) {
                var t;
                for (var i = 0; t = datas[i]; i++) {
                    ChoicedDatas[t[s.SelectorKey]] = t;
                }
            }
        }

        s.AddChoicedData = function (data) {
            if (ChoicedDatas != null)
                ChoicedDatas[data[s.SelectorKey]] = data;
        }

        s.RemoveChoicedData = function (data) {
            if (ChoicedDatas != null)
                delete ChoicedDatas[data[s.SelectorKey]];
        }

        s.AutocompleteRenderFormat = function (item) {
            /// <summary>
            /// 自动完成的渲染格式
            /// </summary>
            /// <remark>
            /// eg: return "<a>" + item.Code + "<span class='badge pull-right'>" + item.NationNameCn + "</span></a>"
            /// </remark>
            /// <param name="item"></param>

            return "<a>" + Lang.NOT_SET_FORMAT + "</a>"
        }

        var autocompleteRender = function (ul, item) {
            /// <summary>
            /// 自定完成的数据渲染
            /// </summary>
            /// <param name="ul"></param>
            /// <param name=" item"></param>
            return $("<li>")
                .data("item.autocomplete", item)
                .append(s.AutocompleteRenderFormat(item))
                .appendTo(ul);
        };

        s.AutocompleteRenderContainer = null;

        var autoCompleteSource = function (request, response) {
            data.q = request.term;
            data.cmpID = s.TargetCompanyID;
            $.ajax({
                url: s.AutocompleteUrl,
                dataType: "jsonp",
                jsonpCallback: "callback",////////
                //data: {
                //    q: request.term,
                //    cmpID: s.TargetCompanyID
                //},
                data: data,
                cache: false
            }).done(function (data) {
                response(data);
            }).fail(function (httpRequest, textStatus, errorThrown) {
                if (console && console.log)
                    console.log(errorThrown);
            });
        };

        s.OnChoice = function (data, ele, assistEle, isSelector) {
            /// <summary>
            /// 当选中时
            /// </summary>
            /// <param name="data">选中的数据</param>
            /// <param name="isSelector">是否是选择器的 OnChange</param>
            /// <param name="ele"></param>
            /// <param name="assistEle"></param>
        };

        s.OnUnChoice = function (data, ele) {
            /// <summary>
            /// 取消选中时
            /// </summary>
            /// <param name="data">取消选中的数据</param>
            /// <param name="ele"></param>
        };

        s.BindAutoComplete = function (path) {
            /// <summary>
            /// Autocomplete 绑定
            /// </summary>
            /// <param name="path">jQuery 选择路径</param>

            $(path).each(function (idx, ele) {
                var data = $(ele).autocomplete({
                    source: autoCompleteSource,
                    select: function (event, ui) {
                        var sF = $("#" + $(ele).attr("data-xxy-selector-for"));
                        if (sF.length > 0)
                            s.OnChoice(ui.item, sF[0], ele);
                        else
                            s.OnChoice(ui.item, ele)
                        return false;
                    }
                })
                .data("ui-autocomplete");
                var a = data.menu;
                data._renderItem = autocompleteRender;
                if (s.RenderContainer instanceof Function)
                    data._renderMenu = s.AutocompleteRenderContainer;

                if (s.AutocompleteWidth != null && !isNaN(s.AutocompleteWidth)) {
                    data._resizeMenu = function () {
                        this.menu.element.outerWidth(s.AutocompleteWidth);
                    }
                }
            });
        }


        s.BindSelector = function (path) {
            /// <summary>
            /// 选择器绑定
            /// </summary>
            /// <param name="path">jQuery 选择的对象</param>

            if (s.SelectorDialog instanceof Function) {
                $(path).unbind("click.Selector");
                //$(document.body).unbind("click.Selector");//清理老的选择器事件
                //$(path).on("click.Selector", s.SelectorDialog);
                $(document.body).on("click.Selector", path, s.SelectorDialog);
            }
        }

        var observers = [];
        s.AddObserver = function (o) {
            /// <summary>
            /// 选择器
            /// </summary>
            /// <param name="o"></param>
            observers.push(o);
            o.Notify(SelectorChoicedKeys());
        };

        s.Notify = function () {
            /// <summary>
            /// 选择器
            /// </summary>
            var o;
            var choiced = SelectorChoicedKeys();
            for (var i = 0; o = observers[i]; i++) {
                try {
                    o.Notify(choiced);
                } catch (e) {
                }
            }
        };

        var reg = /^(id|name|type|value|class|data-val-number)$/i;
        //COPY属性
        $("[data-xxy-selector]").each(function (idx, ele) {

            var assist = $("[name='" + ele.name + "_assist']");
            var attr;
            for (var i = 0; attr = ele.attributes[i]; i++) {
                if (reg.test(attr.name))
                    continue;

                assist.attr(attr.name, attr.value);
            }

        });


        s.SelectorDialog = function () {
            /// <summary>
            /// 选择器的弹出框
            /// </summary>
            if (s.BeforeOpenDialog(this) === false) {
                return;
            }
            var url = s.SelectorUrl + (s.SelectorUrl.indexOf("?") > 0 ? "&" : "?") + "cmpID=" + s.TargetCompanyID;
            for (var k in data) {
                url = url + "&" + k + "=" + data[k];
            }
            XXY.ColorBox.AutoPage(s.SelectorTitle || Lang.CHOICE, s.SelectorWidth || 800, s.SelectorHeight || 500, url, null);
        };

        s.BeforeOpenDialog = function (obj) {
            /// <summary>
            /// 如果返回 false，不会打开弹出框。 其它任何值，或不返回都会打弹出框
            /// </summary>
            /// <param name="obj"></param>
            var forName = $(obj).attr("data-xxy-selector-for");
            if (forName != undefined) {


                s.SelectorFor = $("[name='" + forName + "']");
                s.SelectorForAssist = $("#" + forName.replace(".", "_") + "_assist");
                if (s.SelectorForAssist.length == 0)
                    s.SelectorForAssist = $("[name='" + forName + "_assist']");
            }
            //添加点回调事件
            if (s.BeforeOpenDialogCallBack != null) {
                s.BeforeOpenDialogCallBack(obj);
            }

        };
    }


    sel.GetBaseSelector = function () {
        /// <summary>
        /// 扩展, 已使外部可使用, 但是外部不能修改
        /// </summary>
        return BaseSelector;
    }

    sel.SelectorDialog = {};
    (function (s) {

        s.Init = function (selector, disRowClick) {

            if (selector) {
                selector.AddObserver(s);
                //默认点击行为选中
                if (!disRowClick) {
                    $("tr").on("click", function () {
                        var cbx = $(this).find("input:checkbox");
                        cbx.click();
                    }).css({ "cursor": "pointer" });
                }
                $("input:checkbox").on("click", function (event) {
                    event.stopPropagation();
                    if (window.parent) {
                        var choice = $(this).prop("checked");
                        var data = window.parent.$.parseJSON($(this).attr("data-xxy-info"));

                        if (choice) {
                            selector.OnChoice(data, selector.SelectorFor, selector.SelectorForAssist, true)
                            selector.AddChoicedData(data);
                        } else {
                            selector.OnUnChoice(data, selector.SelectorFor, selector.SelectorForAssist, true);
                            selector.RemoveChoicedData(data);
                        }
                    }
                });
            }
        }

        s.Notify = function (choiced) {
            $("input:checkbox").prop("checked", false);
            for (var i = 0; i < choiced.length; i++) {
                $("#cbx_" + choiced[i]).prop("checked", true);
            }
        };

    })(sel.SelectorDialog);

})(Selector);