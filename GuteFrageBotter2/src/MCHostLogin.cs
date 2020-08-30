using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Cookie = System.Net.Cookie;

namespace GuteFrageBotter2
{
    public class MCHostLogin
    {
        public static String remname = "";
        public static String remval = "";

        public static void refreshSession()
        {
            ChromeOptions opt = new ChromeOptions();
            opt.LeaveBrowserRunning = true;
            opt.AddArgument("--start-maximized");
            ChromeDriver driver = new ChromeDriver(opt);
            WebDriverWait webDriverWait = new WebDriverWait(driver, TimeSpan.Zero);
            driver.Navigate().GoToUrl("https://mc-host24.de/");
            driver.FindElement(By.Id("loginButton")).Click();
            driver.FindElement(By.XPath("//*[@id=\"user-menu\"]/ul/li[3]/a")).Click();

            Thread.Sleep(2000);


            driver.FindElement(By.Id("username")).SendKeys("*******");
            driver.FindElement(By.Id("password")).SendKeys("*******");

            driver.FindElement(By.XPath(" //*[@id=\"anmelden\"]/div/div/div[2]/div/div/div[2]/form/div[3]/button"))
                .Click();
            Thread.Sleep(1000);

            foreach (var cookie in driver.Manage().Cookies.AllCookies)

            {
                if (cookie.Name.Contains("remember_web"))
                {
                    Console.Clear();
                    remname = cookie.Name;
                    remval = cookie.Value;
                }
            }

            
        }


        public static void createMail()
        {
            refreshSession();
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler(){CookieContainer = cookieContainer};

// If you are using .NET Core 3.0+ you can replace `~DecompressionMethods.None` to `DecompressionMethods.All`
            handler.AutomaticDecompression = ~DecompressionMethods.None;

            using (var httpClient = new HttpClient(handler))
            {
                
                using (var request = new HttpRequestMessage(new HttpMethod("POST"),
                    "https://mc-host24.de/json_query/domain/23720/email"))
                {
                    request.Headers.TryAddWithoutValidation("authority", "mc-host24.de");
                    request.Headers.TryAddWithoutValidation("pragma", "no-cache");
                    request.Headers.TryAddWithoutValidation("cache-control", "no-cache");
                    request.Headers.TryAddWithoutValidation("accept", "application/json, text/javascript, */*; q=0.01");
                    request.Headers.TryAddWithoutValidation("x-csrf-token", "UmyduNWRBGYivmDZneFdmTMjAchW7abrWimOkciV");
                    request.Headers.TryAddWithoutValidation("x-requested-with", "XMLHttpRequest");
                    request.Headers.TryAddWithoutValidation("user-agent",
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36");
                    request.Headers.TryAddWithoutValidation("origin", "https://mc-host24.de");
                    request.Headers.TryAddWithoutValidation("sec-fetch-site", "same-origin");
                    request.Headers.Add("cookie", remname + "=" + remval);
                    request.Headers.TryAddWithoutValidation("sec-fetch-mode", "cors");
                    request.Headers.TryAddWithoutValidation("sec-fetch-dest", "empty");
                    request.Headers.TryAddWithoutValidation("referer", "https://mc-host24.de/myservers");
                    request.Headers.TryAddWithoutValidation("accept-language",
                        "de,en-US;q=0.9,en;q=0.8,de-DE;q=0.7,ro;q=0.6");
                    cookieContainer.Add(new Uri("https://mc-host24.de/json_query/domain/23720/email"),new Cookie(remname, remval));
                    Console.Clear();
                   

                    request.Content = new StringContent("email=asdasdafrrrdd&password=rs05082005");
                    request.Content.Headers.ContentType =
                        MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded; charset=UTF-8");

                    var response =  httpClient.SendAsync(request);
                   
                    Thread.Sleep(1000000000);
                }
            }
        }
    }
}