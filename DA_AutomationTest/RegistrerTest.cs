using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace DA_AutomationTest
{
    [TestFixture]
    public class RegistrerTest
    {

        IWebDriver driver;
        static object[] testdata = {

            // string FullName, string Email,string Name ,string Password, string PasswordConfirm, string Question, string Answer, bool Agreement, bool Expected
            new object[] { "Phong", "Generate", "Generate", "123@Abc",  "123@Abc", "What is your favorite colors?", "Red", true, true},
            new object[] { "Phong", "email", "Generate", "123@Abc",  "1234@Abc", "What is your favorite colors?", "Red", true, false},

        };

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(Utility.Driver);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }


        [Test]
        [TestCaseSource("testdata")]
        public void RegisterTest01(string FullName, string Email,string Name ,string Password, string PasswordConfirm, string Question, string Answer ,bool Agreement, bool Expected)
        {
            string url = @"http://20cmtesting3.gear.host/Register.aspx";
            driver.Navigate().GoToUrl(url);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.FindElement(By.Id("FullName")).Clear();
            driver.FindElement(By.Id("FullName")).SendKeys(FullName);

            driver.FindElement(By.Id("Email")).Clear();
            if (Email.Equals("Generate"))
            {
                driver.FindElement(By.Id("Email")).SendKeys(Utility.GenerateEmail());
            }
            else
            {
                driver.FindElement(By.Id("Email")).SendKeys(Email);
            }    

            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys(Name.Equals("Generate") ? Utility.GenerateEmail() : Name);

            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys(Password);

            driver.FindElement(By.Id("ConfirmPassword")).Clear();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys( PasswordConfirm);

            var selectElement = new SelectElement(driver.FindElement(By.Id("Question")));
            selectElement.SelectByValue(Question);

            driver.FindElement(By.Id("Answer")).Clear();
            driver.FindElement(By.Id("Answer")).SendKeys(Answer);

            if (Agreement)
            {
                IWebElement element = driver.FindElement(By.Id("agreement"));
                var jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", element);
            }


            driver.FindElement(By.Id("StepNextButton")).Click();
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            bool temp = false;
            IWebElement tableUserCreate = driver.FindElement(By.Id("CreateUserWizardMember"));
            try
            {
                IWebElement statusSuccess = tableUserCreate.FindElement(By.TagName("h4"));
                if (statusSuccess.Displayed)
                {
                    temp = statusSuccess.Text.Equals("Your account has been created.");
                }
                else
                {
                    temp = false;
                }
            }
            catch (Exception ex)
            {
                temp = false;
            }


            if(temp == Expected)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

    }
}
