using TechTalk.SpecFlow;

namespace SpecFlowCalculator.Specs.Steps
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.WaitHelpers;

    using SpecFlowCalculator.Specs.Utils;

    using TechTalk.SpecFlow.Assist;

    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private IWebDriver driver = null;

        private readonly ScenarioContext scenarioContext;

        public CalculatorStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.driver = driver;
        }

        [Given(@"I open BrowserStack sign in page")]
        public void GivenIOpenBrowserStackSignInPage()
        {
            this.driver.Navigate().GoToUrl("https://www.browserstack.com/users/sign_in");
        }

        [Given(@"I enter valid email '(.*)'")]
        public void GivenIEnterValidEmail(string email)
        {
            this.driver.FindElement(By.Id("user_email_login")).SendKeys(email);
        }

        [Given(@"I enter valid password '(.*)'")]
        public void GivenIEnterValidPassword(string password)
        {
            this.driver.FindElement(By.Id("user_password")).SendKeys(password);
        }

        [When(@"I click on Sign in")]
        public void WhenIClickOnSignIn()
        {
            this.driver.FindElement(By.Id("user_submit")).Click();
        }

        [Then(@"I should be logged into BrowserStack page")]
        public void ThenIShouldBeLoggedIntoBrowserStackPage()
        {
            WebDriverWait wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("framework__list-container__header")));
            string text = this.driver.FindElement(By.ClassName("framework__list-container__header")).Text;
            Assert.IsTrue(text.Contains("Welcome"));
            this.driver.Close();
        }

        [Given(@"I enter invalid password '(.*)'")]
        public void GivenIEnterInvalidPassword(string password)
        {
            this.driver.FindElement(By.Id("user_password")).SendKeys(password);
        }

        [Then(@"user should not be logged in with an error message")]
        public void ThenUserShouldNotBeLoggedInWithAnErrorMessage()
        {
            string errormessage = this.driver.FindElement(By.XPath("//input[@id='user_password']/following-sibling::div/div/span")).Text;
            Assert.AreEqual("Invalid password", errormessage);
            this.driver.Close();
        }

        [Given(@"I am practicing doc string feature")]
        public void GivenIAmPracticingDocStringFeature(string multilineText)
        {

        }

        [Given(@"I am practicing data table feature using Dictionary")]
        public void GivenIAmPracticingDataTableFeatureUsingDictionary(Table table)
        {
            var credentials = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                credentials.Add(row[0], row[1]);
            }

            foreach (var key in credentials.Keys)
            {
                var username = key;
                var pwd = credentials["key"];
                Console.WriteLine(username + "  " + pwd);
            }
        }


        [Given(@"I am practicing data table feature using CreateInstance")]
        public void GivenIAmPracticingDataTableFeatureUsingCreateInstance(Table table)
        {
            // Instead of (string username, string pwd), we can create a class with getters and setters for username, pwd
            //var cred = table.CreateInstance<(string username, string pwd)>(); 
            var credentials = table.CreateInstance<Credentials>();
            var username = credentials.Username;
            var pwd = credentials.Password;
            Console.WriteLine(username, pwd);
        }
    }
}
