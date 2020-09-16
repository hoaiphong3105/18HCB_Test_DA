using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_AutomationTest
{
    public static class Utility
    {
        public static string Domain = @"http://20cmtesting3.gear.host";
        public static string Driver = @"C:\Users\hoaip\Downloads\chromedriver_win32";

        public static string GenerateEmail()
        {
            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(100000);
            string email = "username" + randomInt.ToString() + "@gmail.com";

            return email;
        }
    }
}
