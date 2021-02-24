using System;
using System.Collections.Generic;
using System.Text;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;

namespace Amazon.PagesObjects
{
    public class SearchResult
    {
        private IWebDriver driver;

        [FindsBy(How = How.XPath, Using = "//*[@data-index=1]//a/div/img")]
        public IWebElement img_FirstItem { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@data-index=1]//span[@class='a-price']")]
        public IWebElement Price { get; set; }

    }
}
