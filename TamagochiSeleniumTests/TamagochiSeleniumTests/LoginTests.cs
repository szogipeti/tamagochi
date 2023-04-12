using System.Diagnostics;

namespace TamagochiSeleniumTests;

[TestClass]
public class LoginTests
{
    private ChromeDriver chromeDriver;
    
    [TestInitialize]
    public void Initialize()
    {
        chromeDriver = new ChromeDriver();
        chromeDriver.Manage().Window.Maximize();
        chromeDriver.Navigate().GoToUrl("http://localhost:8881/#/login");
    }
    private void LoadDatabase()
    {
        Process process = new Process();
        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.WorkingDirectory = Path.GetFullPath("../../../../../tamagochi-laravel");
        process.StartInfo.Arguments = "/C docker compose exec app php artisan migrate:fresh --seed";
        process.Start();
        process.WaitForExit();
    }
    private void OldUser(string user, string email, string pw, string pwc, ChromeDriver driver)
    {
        LoadDatabase();
        chromeDriver.Navigate().GoToUrl("http://localhost:8881/#/register");
        RegisterTests reg = new RegisterTests();
        Thread.Sleep(200);
        reg.RegisterUser(user, email, pw, pwc, chromeDriver);
        reg.WaitForRegister(chromeDriver);

    }
    public void WaitForLogin(ChromeDriver driver)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        while (sw.ElapsedMilliseconds <= 20000)
        {
            if (driver.Url == "http://localhost:8881/#/animals/select")
            {
                break;
            }
        }
        sw.Stop();
    }
    [TestMethod]
    public void LoginNewUser()
    {
        LoadDatabase();
        chromeDriver.Navigate().GoToUrl("http://localhost:8881/#/register");
        RegisterTests reg = new RegisterTests();
        Thread.Sleep(200);
        reg.RegisterUser("kapitany", "kapitany@gmail.com", "kapika", "kapika", chromeDriver);
        reg.WaitForRegister(chromeDriver);

        IWebElement emailElement = chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/form/div[1]/input"));
        IWebElement passwordElement = chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/form/div[2]/input"));

        emailElement.SendKeys("kapitany@gmail.com");
        passwordElement.SendKeys("kapika");

        IWebElement button = chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/form/button"));
        button.Click();
    }
    [TestMethod]
    public void LoginOldUser()
    {
        OldUser("kapibara", "kapibara@gmail.com", "kapipi", "kapipi", chromeDriver);
        IWebElement emailElement = chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/form/div[1]/input"));
        IWebElement passwordElement = chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/form/div[2]/input"));

        emailElement.SendKeys("kapibara@gmail.com");
        passwordElement.SendKeys("kapipi");

        IWebElement button = chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/form/button"));
        button.Click();

    }
    [TestMethod]
    public void TestSuccessfulLogin()
    {
        OldUser("kapibara", "kapibara@gmail.com", "kapipi", "kapipi", chromeDriver);
        IWebElement emailElement = chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/form/div[1]/input"));
        IWebElement passwordElement = chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/form/div[2]/input"));

        emailElement.SendKeys("kapibara@gmail.com");
        passwordElement.SendKeys("kapipi");

        IWebElement button = chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/form/button"));
        button.Click();
        WaitForLogin(chromeDriver);

        Assert.AreEqual("http://localhost:8881/#/animals/select", chromeDriver.Url);
    }

    [TestMethod]
    public void TestEmptyLogin()
    {
        LoadDatabase();
        IWebElement button = chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/form/button"));
        button.Click();

        Assert.AreEqual("http://localhost:8881/#/login", chromeDriver.Url);
        Assert.AreEqual("Az e-mail megadása kötelezõ!", chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/form/div[1]/span")).Text);
        Assert.AreEqual("A jelszó megadása kötelezõ!", chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/form/div[2]/span")).Text);
    }

    [TestCleanup]
    public void Cleanup()
    {
        chromeDriver.Quit();
    }
}