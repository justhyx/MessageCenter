using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XXY.MessageCenter.Email {
    public static class Helper {

        public static readonly Regex ParseReg = new Regex(@"((?<name>\w+)\s*<\s*)?(?<address>\w+@\w+\.\w+)(\s*>)?");

        public static IEnumerable<MailAddress> ToMailAddress(this string str) {
            var mas = ParseReg.Matches(str);
            foreach (Match ma in mas)
                yield return Extract(ma);
        }

        private static MailAddress Extract(Match ma) {

            var address = ma.Groups["address"].Value.Trim();
            var name = ma.Groups["name"].Value.Trim();
            if (string.IsNullOrWhiteSpace(name))
                name = address.Split('@')[0].Trim();

            return new MailAddress(address, name);
        }

    }
}
