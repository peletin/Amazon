using System;
using System.Collections.Generic;
using System.Text;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;

namespace Amazon.PagesObjects
{
    class Login
    {
        private IWebDriver driver;

        [FindsBy(How = How.Id, Using = "continue")]
        public IWebElement btn_Continue { get; set; }

        [FindsBy(How = How.Id, Using = "signInSubmit")]
        public IWebElement btn_SignIn { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='email']")]
        public IWebElement txt_Mailbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        public IWebElement txt_Password { get; set; }

    }
}
