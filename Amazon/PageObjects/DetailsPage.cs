using System;
using System.Collections.Generic;
using System.Text;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;

namespace Amazon.PagesObjects
{
    public class DetailsPage
    {
        private IWebDriver driver;

        [FindsBy(How = How.Id, Using = "priceblock_ourprice")]
        public IWebElement lbl_OurPrice { get; set; }

        [FindsBy(How = How.Id, Using = "price_inside_buybox")]
        public IWebElement lbl_buyboxPrice { get; set; }

        [FindsBy(How = How.Id, Using = "add-to-cart-button")]
        public IWebElement btn_AddtoCart { get; set; }
    }
}
