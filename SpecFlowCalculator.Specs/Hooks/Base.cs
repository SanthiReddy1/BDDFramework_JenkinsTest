using System;
using System.Collections.Generic;
using System.Text;

namespace SpecFlowCalculator.Specs.Hooks
{
    using BoDi;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    using TechTalk.SpecFlow;

    [Binding]
    public class Base
    {
        private IWebDriver driver = null;

        private readonly IObjectContainer objectContainer;

        public Base(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            Console.WriteLine("Before Feature");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            this.driver = new ChromeDriver(@"C:\\SeleniumDrivers");
            this.objectContainer.RegisterInstanceAs(this.driver);
            this.driver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (this.driver == null)
            {
                this.driver.Close();
            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Console.WriteLine("After Feature");
        }
    }
}
