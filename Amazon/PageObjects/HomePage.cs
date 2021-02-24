using System;
using System.Collections.Generic;
using System.Text;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;

namespace Amazon.PagesObjects
{
    public class HomePage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.Id, Using = "twotabsearchtextbox")]
        public IWebElement txt_SearchBart { get; set; }
        
        [FindsBy(How = How.Id, Using = "nav-search-submit-button")]
        public IWebElement btn_Search { get; set; }
    }
}
