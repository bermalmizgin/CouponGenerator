using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CouponGenerator
{
    public class CouponApi
    {
        
        private readonly string digits;
        private readonly int couponCount;
        private readonly int digitCount;
        private readonly int randomValue;

        public CouponApi(string digits, int couponCount, int digitCount, int rendomizer)
        {
            this.digits = digits;
            this.couponCount = couponCount;
            this.digitCount = digitCount;
            this.randomValue = rendomizer;
        }


        public bool ValidCoupon(string encodedCoupon)
        {
            return GetValidEncodedCoupons().Exists(a => a == encodedCoupon);
        }


        public List<string> GetValidEncodedCoupons()
        {
            var combinationCount = GetCombinationCount();
            var endPoint = GetEndPoint(combinationCount, couponCount+ randomValue);


            var couponScale = endPoint / (couponCount + randomValue);
            List<string> list = new List<string>();

            for (int i = 0; i < couponCount; i++)
            {
                endPoint -= couponScale;
                var converted = ConvertFromDecimal(endPoint, digits.ToCharArray());

                list.Add(converted);
            }

            return list;
        }

     
        private double GetCombinationCount()
        {
            return Math.Pow(digits.Length, digitCount);
        }
        public double GetEndPoint(double value, double count)
        {
            return value - value % count;
        }

        private string ConvertFromDecimal(double decimalNumber, char[] encodedDigits)
        {
            int baseValue = encodedDigits.Length;
            string customDigits = "";

            while (decimalNumber > 0)
            {
                int remaining = (int)(decimalNumber % baseValue);
                char karakter = encodedDigits[remaining];
                customDigits = karakter + customDigits;
                decimalNumber /= baseValue;
            }

            if (customDigits.Length > 8)
            {
                customDigits = customDigits.Substring(customDigits.Length - 8, 8);
            }
            else if (customDigits.Length < 8)
            {
                customDigits = customDigits.PadLeft(digitCount, encodedDigits.First());
            }

            return customDigits;
        }
    }
}
