using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using XXY.Common.Extends;
using XXY.Configuration;
using XXY.MessageCenter.IBiz;
using XXY.UC.BizEntity;

namespace XXY.MessageCenter {
    public class PageDatas {

        #region Dependency
        [Dependency]
        public Lazy<ICurrentUser> CurrentUserBiz {
            get;
            set;
        }


        #endregion

        #region current user
        public User User {
            get {
                return this.CurrentUserBiz.Value.GetUser();
            }
        }

        public bool IsLogined {
            get {
                return this.CurrentUserBiz.Value.IsLogined;
            }
        }

        #endregion

        public static PageDatas GetInstance() {
            return DependencyResolver.Current.GetService<PageDatas>();
        }

        /// <summary>
        /// 枚举 Description 列表 SelectListItem
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="optionValue"></param>
        /// <param name="optionText"></param>
        /// <param name="useKey"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> EnumDescriptionsListItems(Type enumType, string optionValue = null, string optionText = null, bool useKey = false) {
            var descs = XXY.Common.Extends.EnumHelper.GetDescriptions(enumType);
            var items = descs.Select(kv => new SelectListItem() {
                Text = kv.Value,
                Value = useKey ? kv.Key : ((int)Enum.Parse(enumType, kv.Key, true)).ToString(),
            }).ToList();

            if (optionValue != null || optionText != null) {
                items.Insert(0, new SelectListItem() {
                    Text = optionText ?? "",
                    Value = optionValue ?? ""
                });
            }

            return items;
        }

        public IEnumerable<SelectListItem> EnumNameDescriptionsListItems(Type enumType, string optionValue = null, string optionText = null, bool useKey = false) {
            var descs = XXY.Common.Extends.EnumHelper.GetDescriptions(enumType);
            var items = descs.Select(kv => new SelectListItem() {
                Text = kv.Value,
                Value = kv.Value,
            }).ToList();

            if (optionValue != null || optionText != null) {
                items.Insert(0, new SelectListItem() {
                    Text = optionText ?? "",
                    Value = optionValue ?? ""
                });
            }

            return items;
        }

        public IEnumerable<SelectListItem> AppCodes() {
            return ConfigurationHelper
                .GetSection<SystemsConfig>()
                .Systems.Cast<SystemConfig>()
                .OrderBy(c=>c.AppCode)
                .Select(c => new SelectListItem() {
                    Text = c.AppCode,
                    Value = c.AppCode
                });
        }
    }
}
