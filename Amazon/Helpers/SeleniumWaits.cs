using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Amazon.Helpers
{
    class SeleniumWaits : Wait
    {
        public void DeathTime(int duration)
        {
            Thread.Sleep(duration);
        }
    }
}
