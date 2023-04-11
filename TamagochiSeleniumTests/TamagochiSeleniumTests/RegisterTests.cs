
using System.ComponentModel.Design;
using System.Diagnostics;

namespace TamagochiSeleniumTests;

[TestClass]
public class RegisterTests
{
    private ChromeDriver chromeDriver;
    
    [TestInitialize]
    public void Initialize()
    {
        chromeDriver = new ChromeDriver();
        chromeDriver.Manage().Window.Maximize();
        chromeDriver.Navigate().GoToUrl("http://localhost:8881/#/register");
        Thread.Sleep(200);
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

    [TestMethod]
    public void TestSuccessfulRegister()
    {
        LoadDatabase();

        IWebElement username = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[1]/input"));
        IWebElement email = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[2]/input"));
        IWebElement pwd = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[3]/input"));
        IWebElement pwdConf = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[4]/input"));
        
        username.SendKeys("gipszjakab");
        email.SendKeys("gipszjakab@gmail.com");
        pwd.SendKeys("jelszo123");
        pwdConf.SendKeys("jelszo123");

        IWebElement button = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/button"));
        button.Click();
        
        Thread.Sleep(10000);
        
        Assert.AreEqual("http://localhost:8881/#/login", chromeDriver.Url);
        Assert.AreEqual("Bejelentkezés", chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/h3")).Text);
    }

    [TestMethod]
    public void TestRegisterEmpty()
    {
        IWebElement button = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/button"));
        button.Click();
        
        Assert.AreEqual("http://localhost:8881/#/register", chromeDriver.Url);
        Assert.AreEqual("A felhasználónév megadása kötelező!",  chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[1]/span")).Text);
        Assert.AreEqual("Az e-mail megadása kötelező!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[2]/span")).Text);
        Assert.AreEqual("A jelszó megadása kötelező!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[3]/span")).Text);
        Assert.AreEqual("A jelszó megerősítésének megadása kötelező!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[4]/span")).Text);
    }
    
    [TestMethod]
    public void TestInvalidEmailRegister()
    {
        IWebElement username = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[1]/input"));
        IWebElement email = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[2]/input"));
        IWebElement pwd = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[3]/input"));
        IWebElement pwdConf = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[4]/input"));
        
        username.SendKeys("gipszjakab");
        email.SendKeys("gipszjakab134");
        pwd.SendKeys("jelszo123");
        pwdConf.SendKeys("jelszo123");
        
        IWebElement button = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/button"));
        button.Click();
        
        Assert.AreEqual("http://localhost:8881/#/register", chromeDriver.Url);
        Assert.AreEqual("Az e-mail formátuma nem megfelelő!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[2]/span")).Text);
    }

    [TestMethod]
    public void TestNotMatchingPasswordRegister()
    {
        IWebElement username = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[1]/input"));
        IWebElement email = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[2]/input"));
        IWebElement pwd = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[3]/input"));
        IWebElement pwdConf = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[4]/input"));
        
        username.SendKeys("gipszjakab");
        email.SendKeys("gipszjakab134");
        pwd.SendKeys("jelszo123");
        pwdConf.SendKeys("jelszo1234");
        
        IWebElement button = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/button"));
        button.Click();

        Assert.AreEqual("http://localhost:8881/#/register", chromeDriver.Url);
        Assert.AreEqual("A jelszó megerősítésének meg kell egyeznie a jelszóval!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[4]/span")).Text);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        chromeDriver.Quit();
    }
}