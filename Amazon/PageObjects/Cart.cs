using System;
using System.Collections.Generic;
using System.Text;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;

namespace Amazon.PagesObjects
{
    public class Cart
    {
        private IWebDriver driver;

        [FindsBy(How = How.XPath, Using = "//div[@data-name='Active Items']//div/p")]
        public IWebElement lbl_ItemPrice { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@id='sc-subtotal-amount-activecart']/span")]
        public IWebElement lbl_Subtotal { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@id='sc-subtotal-amount-buybox']/span")]
        public IWebElement lbl_SubtotalBuyBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@id='sc-buy-box-ptc-button']/span")]
        public IWebElement btn_ProceedCheckout { get; set; }

        [FindsBy(How = How.Id, Using = "hlb-view-cart-announce")]
        public IWebElement btn_Cart { get; set; }
    }
}
