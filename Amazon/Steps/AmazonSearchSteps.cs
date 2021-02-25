using Amazon.Helpers;
using Amazon.PagesObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.IO;
using TechTalk.SpecFlow;


namespace Amazon
{
    [Binding]
    public class AmazonSearchSteps
    {
        #region Variables
        private IWebDriver _driver;
        private WebDriverWait wait;
        private HomePage firstpage;
        private SearchResult resultpage;
        private DetailsPage details;
        private Cart cartdetails;
        private CheckOrder checkout;
        private Login authentication;
        private decimal uniqueprice;
        private JObject user, enviroment;
        private HelpFunctions cast;
        private SeleniumWaits sleep;
        #endregion
        #region Constructor
        public AmazonSearchSteps(IWebDriver driver)
        {
            _driver = driver;
            //Json file reader for data
            using (StreamReader file = File.OpenText(Path.Combine(Environment.CurrentDirectory, "Data.json")))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                user = (JObject)JToken.ReadFrom(reader);
            }
            //Json config reader
            using (StreamReader file = File.OpenText(Path.Combine(Environment.CurrentDirectory, "Config.json")))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                enviroment = (JObject)JToken.ReadFrom(reader);
            }
            //Abstract class
            cast = new HelpFunctions();
            //Interface
            sleep = new SeleniumWaits();
        }
        #endregion

        [Given(@"Web browser is open")]
        public void GivenWebBrowserIsOpen()
        {            
            _driver.Manage().Window.Maximize();
            //implicit wait
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        }

        [Given(@"'(.*)' page is loaded")]
        public void GivenPageIsLoaded(string page)
        {

            _driver.Navigate().GoToUrl((string)enviroment["Env"]);
            sleep.DeathTime(1000);
            //Explicit wait
            IWebElement firstResult = wait.Until(e => e.FindElement(By.Id("nav-logo-sprites")));
        }

        [When(@"User search for ""(.*)""")]
        public void WhenUserSearchForSamsungGalaxyNote(string brand)
        {
            firstpage = new HomePage();
            PageFactory.InitElements(_driver, firstpage);
            firstpage.txt_SearchBart.SendKeys(brand);
            firstpage.btn_Search.Click();
        }

        [When(@"Clicks on the first result")]
        public void WhenClicksOnTheFirstResult()
        {
            resultpage = new SearchResult();
            PageFactory.InitElements(_driver, resultpage);
            uniqueprice = cast.StringToInt(resultpage.Price.Text);
            resultpage.img_FirstItem.Click();
        }

        [When(@"Item is added to cart")]
        public void WhenItemIsAddedToCart()
        {
            details = new DetailsPage();
            PageFactory.InitElements(_driver, details);
            //Compare the price 
            Assert.AreEqual(uniqueprice, cast.StringToInt(details.lbl_OurPrice.Text));
            Assert.AreEqual(uniqueprice, cast.StringToInt(details.lbl_buyboxPrice.Text));
            details.btn_AddtoCart.Click();
            //Try catch acts funky due combination of explicit and explicit wait
            try
            {
                IWebElement cartSideSheet = wait.Until(e => e.FindElement(By.Id("attach-sidesheet-view-cart-button")));
                cartSideSheet.Click();
            }
            catch (Exception a)
            {
                IWebElement cartSideSheet = wait.Until(e => e.FindElement(By.Id("hlb-view-cart-announce")));
                cartSideSheet.Click();
            };
        }

        [When(@"Navigates to the cart")]
        public void WhenNavigatesToTheCart()
        {
            cartdetails = new Cart();
            PageFactory.InitElements(_driver, cartdetails);
            // Asserts
            Assert.AreEqual(uniqueprice, cast.StringToInt(cartdetails.lbl_ItemPrice.Text));
            Assert.AreEqual(uniqueprice, cast.StringToInt(cartdetails.lbl_Subtotal.Text));
            Assert.AreEqual(uniqueprice, cast.StringToInt(cartdetails.lbl_SubtotalBuyBox.Text));
        }

        [When(@"Clicks on checkout")]
        public void WhenClicksOnCheckout()
        {
            //Json read data
            string mail = (string)user["user"]["Email"];
            string password = (string)user["user"]["Password"];
            cartdetails.btn_ProceedCheckout.Click();
            authentication = new Login();
            PageFactory.InitElements(_driver, authentication);
            authentication.txt_Mailbox.SendKeys(mail);
            authentication.btn_Continue.Click();
            authentication.txt_Password.SendKeys(password);
            authentication.btn_SignIn.Click();
        }

        [Then(@"User deletes the items")]
        public void ThenUserDeletesTheItems()
        {
            checkout = new CheckOrder();
            PageFactory.InitElements(_driver, checkout);
            checkout.lnk_ChangeQty.Click();
            checkout.lnk_Delete.Click();
            Assert.AreEqual("Tu carrito de Amazon está vacío.", checkout.lbl_EmptyCart.Text);
        }
    }
}
