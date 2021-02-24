using System;
using System.Collections.Generic;
using System.Text;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;

namespace Amazon.PagesObjects
{
    class CheckOrder
    {
        private IWebDriver driver;

        [FindsBy(How = How.XPath, Using = "//div[@class='a-row quantity-block']/a")]
        public IWebElement lnk_ChangeQty { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@class='dynamic-quantity-update']/a[2]")]
        public IWebElement lnk_Delete { get; set; }

        [FindsBy(How = How.XPath, Using = "//h1[@class='a-spacing-mini a-spacing-top-base']")]
        public IWebElement lbl_EmptyCart { get; set; }

    }
}
