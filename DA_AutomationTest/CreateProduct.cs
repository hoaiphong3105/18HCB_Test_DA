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
    public class CreateProduct
    {

        IWebDriver driver;
        static object[] testdata = {

            // string AdminUser, string AdminPass, string ProName, string Category, string Brand, string Sumary, string Description, string Price, string OldPrice, string StartTime, string EndTime, string Image, bool Expected
            new object[] { "admin", "admin-123", "Pro1", "Household", "Lorem", "Summary" ,"Description", "100", "150", "", "" , @"C:\Users\hoaip\Downloads\4J3NpUW.png", true},
            new object[] { "admin", "admin-123", "Pro1", "Household", "Lorem", "Summary" ,"Description", "100", "150", "", "" , @"", false},
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
        public void CreateProductTest01(string AdminUser, string AdminPass, string ProName, string Category, string Brand, string Sumary, string Description, string Price, string OldPrice, string StartTime, string EndTime, string Image, bool Expected)
        {
            string url = @"http://20cmtesting3.gear.host/Admin/ProductForm.aspx";
            driver.Navigate().GoToUrl(url);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            // Đăng nhập
            if (!driver.Url.Equals(url))
            {
                driver.FindElement(By.Id("UserName")).Clear();
                driver.FindElement(By.Id("UserName")).SendKeys(AdminUser);
                driver.FindElement(By.Id("Password")).Clear();
                driver.FindElement(By.Id("Password")).SendKeys(AdminPass);
                driver.FindElement(By.Id("LoginButton")).Click();

                WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                if (!driver.Url.Equals(url))
                {
                    Assert.Fail();
                }
            }

            // create product

            // Name
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxName")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxName")).SendKeys(ProName);

            // Category
            var selectElement = new SelectElement(driver.FindElement(By.Id("ContentPlaceHolderContent_DropDownListCategory")));
            selectElement.SelectByText(Category);

            // Brand
            var selectElement2 = new SelectElement(driver.FindElement(By.Id("ContentPlaceHolderContent_DropDownListBrand")));
            selectElement2.SelectByText(Brand);

            // Sumary
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxSummary")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxSummary")).SendKeys(Sumary);

            // Description
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxDescription")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxDescription")).SendKeys(Description);

            // Price
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxPrice")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxPrice")).SendKeys(Price);

            // Old Price
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxOldPrice")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxOldPrice")).SendKeys(OldPrice);

            // Start Offer Datetime
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxStartOfferDatetime")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxStartOfferDatetime")).SendKeys(StartTime);

            // End Offer Datetime
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxEndOfferDatetime")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolderContent_TextBoxEndOfferDatetime")).SendKeys(EndTime);

            // Image 
            if (!string.IsNullOrWhiteSpace(Image))
            {
                IWebElement element = driver.FindElement(By.Id("FileUploadImage"));
                element.SendKeys(Image);
            }

            // Click
            driver.FindElement(By.Id("ContentPlaceHolderContent_ButtonSave")).Click();
            WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            bool result = false;
            if (driver.Url.Equals("http://20cmtesting3.gear.host/Admin/Products.aspx"))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            if (result == Expected)
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
