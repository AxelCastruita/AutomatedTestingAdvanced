using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.pageObjects.ReportPortal
{
    internal class rpLoginPage
    {
        private IWebDriver _driver;

        public rpLoginPage(IWebDriver driver) {

            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

    }
}
