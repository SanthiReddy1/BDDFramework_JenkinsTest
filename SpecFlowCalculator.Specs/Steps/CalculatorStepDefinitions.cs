using TechTalk.SpecFlow;

namespace SpecFlowCalculator.Specs.Steps
{
    using System;

    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.WaitHelpers;

    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        private IWebDriver driver;

        public CalculatorStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I open BrowserStack sign in page")]
        public void GivenIOpenBrowserStackSignInPage()
        {
            this.driver = new ChromeDriver(@"C:\\SeleniumDrivers");
            this.driver.Manage().Window.Maximize();
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

    }
}
