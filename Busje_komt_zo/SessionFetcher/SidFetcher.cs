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

        public SidFetcher()
        {
            var lines = System.IO.File.ReadAllLines("passwords.txt");
            username = lines[0];
            password = lines[1];
        }

        public string GetSid()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("window-size=1200x600");
            options.AddArgument("headless");
            IWebDriver driver =
                new ChromeDriver(@"c:\Users\ruben\Documents\Visual Studio 2017\Projects\Busje_komt_zo\Busje_komt_zo\",
                    options);
            driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, 10);
            driver.Url = "http://nedtrack.nedsoft.nl/";
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

        public String GetItemFromLocalStorage(IJavaScriptExecutor js, String key)
        {
            return (String)js.ExecuteScript($"return window.sessionStorage.getItem('{key}');");
        }

    }
}
