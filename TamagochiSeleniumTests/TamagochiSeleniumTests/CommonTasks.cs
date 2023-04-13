using System.Diagnostics;

namespace TamagochiSeleniumTests;

public static class CommonTasks
{
    public static void LoadDatabase()
    {
        Process process = new Process();
        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.WorkingDirectory = Path.GetFullPath("../../../../../tamagochi-laravel");
        process.StartInfo.Arguments = "/C docker compose exec app php artisan migrate:fresh --seed";
        process.Start();
        process.WaitForExit();
    }

    public static void RegisterUser(string username, string email, string pwd, string pwdConf, ChromeDriver driver)
    {
        IWebElement usernameElement = driver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[1]/input"));
        IWebElement emailElement = driver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[2]/input"));
        IWebElement pwdElement = driver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[3]/input"));
        IWebElement pwdConfElement = driver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[4]/input"));
        
        usernameElement.SendKeys(username);
        emailElement.SendKeys(email);
        pwdElement.SendKeys(pwd);
        pwdConfElement.SendKeys(pwdConf);

        IWebElement button = driver.FindElement(By.XPath("//*[@id=\"register\"]/form/button"));
        button.Click();
    }

    public static void LoginUser(string email, string password, ChromeDriver driver)
    {
        IWebElement emailElement = driver.FindElement(By.XPath("//*[@id=\"login\"]/form/div[1]/input"));
        IWebElement passwordElement = driver.FindElement(By.XPath("//*[@id=\"login\"]/form/div[2]/input"));

        emailElement.SendKeys(email);
        passwordElement.SendKeys(password);

        IWebElement button = driver.FindElement(By.XPath("//*[@id=\"login\"]/form/button"));
        button.Click();
    }

    public static void CreateAnimal(string animalName, ChromeDriver driver)
    {
        driver.FindElement(By.XPath("//*[@id=\"maindiv\"]/form/div/div[1]/input")).SendKeys(animalName);
        driver.FindElement(By.XPath("//*[@id=\"type\"]")).Click();
        
        string selectOptionXPath = "//*[@id=\"type\"]/option[2]";
        WaitForElementLoading(selectOptionXPath, driver);
        driver.FindElement(By.XPath(selectOptionXPath)).Click();
        
        driver.FindElement(By.XPath("//*[@id=\"maindiv\"]/form/button")).Click();
    }
    
    public static void WaitForPageLoading(string url, ChromeDriver driver)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        while (sw.ElapsedMilliseconds <= 20000)
        {
            if (driver.Url == url)
            {
                break;
            }
        }
        sw.Stop();    
    }

    public static void WaitForElementLoading(string xPath, ChromeDriver driver)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        while (sw.ElapsedMilliseconds <= 20000)
        {
            try
            {
                driver.FindElement(By.XPath(xPath));
                break;
            }
            catch
            {
                
            }
        }
        sw.Stop();
    }
    
}