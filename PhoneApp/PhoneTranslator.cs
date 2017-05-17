using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneApp
{
    public class PhoneTranslator
    {
        const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string Numbers = "22233344455566677778889999";

        public string ToNumber(string alfaPhoneNumber)
        {
            var numericPhoneNumber = new StringBuilder();
            if (string.IsNullOrWhiteSpace(alfaPhoneNumber)) return alfaPhoneNumber;

            foreach (var c in alfaPhoneNumber)
            {
                var idx = Letters.IndexOf(c);
                if(idx >= 0)
                {
                    numericPhoneNumber.Append(Numbers[idx]);
                }
                else
                {
                    numericPhoneNumber.Append(c);
                }
            }
            return numericPhoneNumber.ToString();
        }

    }
}