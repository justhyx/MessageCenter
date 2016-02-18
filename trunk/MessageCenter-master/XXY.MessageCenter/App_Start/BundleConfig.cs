using System.Web;
using System.Web.Optimization;

namespace XXY.MessageCenter {
    public class BundleConfig {

        public static readonly string NewSelector = "~/bundles/NewSelector";
        public static readonly string DateTimePicker = "~/bundles/DateTimePicker";
        public static readonly string DateTimePickerCss = "~/bundles/DateTimePickerCss";
        public static readonly string Knockout = "~/bundles/knockout";
        public static readonly string Respond = "~/bundles/respond";
        public static readonly string Common = "~/bundles/common";
        public static readonly string Css = "~/Content/css";
        public static readonly string Modernizr = "~/bundles/modernizr";
        public static readonly string CKEditor = "~/bundles/ckeditor";


        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle(Modernizr).Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            bundles.Add(new ScriptBundle(Common).Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.signalR-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.colorbox.js",
                "~/Scripts/Common.js"
                ));


            bundles.Add(new StyleBundle(Css).Include(
                "~/Content/bootstrap.css",
                "~/Content/themes/base/jquery-ui.css",
                "~/Content/colorbox.css",
                "~/Content/site.css"));

            bundles.Add(new ScriptBundle(Respond).Include("~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle(Knockout).Include("~/Scripts/knockout-{version}.js"));
            bundles.Add(new ScriptBundle(NewSelector).Include("~/Scripts/NewSelector.js"));
            bundles.Add(new ScriptBundle(DateTimePicker).Include("~/Scripts/jquery.datetimepicker.js"));
            bundles.Add(new StyleBundle(DateTimePickerCss).Include("~/Content/jquery.datetimepicker.css"));
            bundles.Add(new ScriptBundle(CKEditor).Include("~/ckeditor/ckeditor.js"));
        }
    }
}
