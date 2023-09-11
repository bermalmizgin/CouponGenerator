using System;
using System.Collections.Generic;
using System.IO;

namespace CouponGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {


            // Parameteres
            string digits = "ACDEFGHKLMNPRTXYZ234579";
            int couponCount = 1000;
            int digitsCount = 8;
            int randomizer = 4889;

            // Api
            var api = new CouponApi(digits, couponCount, digitsCount, randomizer);

            // Coupons
            var validCoupons = api.GetValidEncodedCoupons();

            // Test validation  (Valid)
            var valid = api.ValidCoupon("9ZXKDTKC");

            // Test validation  (Invalid)
            var invalid = api.ValidCoupon("9ZXKDTKD");

   
            // Coupons Write File
            File.WriteAllLines("coupons.txt", validCoupons.ToArray());


        }


    }
}
