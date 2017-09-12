using System;
using System.IO;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Chrome;

namespace SessionFetcher
{
    public class SidFetcher : ISessionGetter
    {
        private readonly string username;
        private readonly string password;
        private readonly string chromedriver;
        private readonly string siteUrl;
        
        public SidFetcher(string user, string pw, string chromedriver, string siteUrl)
        {
            username = user;
            password = pw;
            this.chromedriver = chromedriver;
            this.siteUrl = siteUrl;
        }

        public string GetSid()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("window-size=1200x600");
           // options.AddArgument("headless");
           IWebDriver driver =
                new ChromeDriver(chromedriver,
                    options);
            driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, 10);
            driver.Url = siteUrl;
            driver.Navigate();

            Console.WriteLine("Displayed:" + driver.FindElement(By.Id("user")));
            var executor = driver as IJavaScriptExecutor;
            executor.ExecuteScript($"document.getElementById('passw').setAttribute('value', '{password}')");
            executor.ExecuteScript($"document.getElementById('user').value = '{username}';");
            driver.FindElement(By.Id("submit")).Click();
            Console.WriteLine(driver.Url);

            String SID = GetItemFromLocalStorage(executor, "wialon_sid");
            driver.Quit();
            return SID;
        }

        private String GetItemFromLocalStorage(IJavaScriptExecutor js, String key)
        {
            return (String)js.ExecuteScript($"return window.sessionStorage.getItem('{key}');");
        }

    }
}
