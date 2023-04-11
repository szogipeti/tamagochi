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

    public void RegisterUser(string username, string email, string pwd, string pwdConf)
    {
        IWebElement usernameElement = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[1]/input"));
        IWebElement emailElement = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[2]/input"));
        IWebElement pwdElement = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[3]/input"));
        IWebElement pwdConfElement = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[4]/input"));
        
        usernameElement.SendKeys(username);
        emailElement.SendKeys(email);
        pwdElement.SendKeys(pwd);
        pwdConfElement.SendKeys(pwdConf);

        IWebElement button = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/button"));
        button.Click();
    }

    public void WaitForRegister()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        while (sw.ElapsedMilliseconds <= 20000)
        {
            if (chromeDriver.Url == "http://localhost:8881/#/login")
            {
                break;
            }
        }
        sw.Stop();
    }

    public void WaitForInvalidRegister()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        while (sw.ElapsedMilliseconds <= 20000)
        {
            try
            {
                chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[5]/div/p"));
                break;
            }
            catch
            {
                
            }
        }
        sw.Stop();
    }

    [TestMethod]
    public void TestSuccessfulRegister()
    {
        LoadDatabase();

        RegisterUser("gipszjakab", "gipszjakab@gmail.com", "jelszo123", "jelszo123");
        
        WaitForRegister();

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
        RegisterUser("gipszjakab", "gipszjakab134", "jelszo123", "jelszo123");

        Assert.AreEqual("http://localhost:8881/#/register", chromeDriver.Url);
        Assert.AreEqual("Az e-mail formátuma nem megfelelő!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[2]/span")).Text);
    }

    [TestMethod]
    public void TestNotMatchingPasswordRegister()
    {
        RegisterUser("gipszjakab", "gipszjakab@gmail.com", "jelszo123", "jelszo1234");
        
        IWebElement button = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/button"));
        button.Click();

        Assert.AreEqual("http://localhost:8881/#/register", chromeDriver.Url);
        Assert.AreEqual("A jelszó megerősítésének meg kell egyeznie a jelszóval!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[4]/span")).Text);
    }

    [TestMethod]
    public void TestUsedUsernameRegister()
    {
        LoadDatabase();
        
        RegisterUser("gipszjakab", "gipszjakab@gmail.com", "jelszo123", "jelszo123");
        WaitForRegister();
        
        chromeDriver.Navigate().GoToUrl("http://localhost:8881/#/register");
        RegisterUser("gipszjakab", "gipszjakab2@gmail.com", "jelszo123", "jelszo123");
        
        WaitForInvalidRegister();
        
        Assert.AreEqual("A felhasználónév már használatban van!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[5]/div/p")).Text);
    }

    [TestMethod]
    public void TestUsedEmailRegister()
    {
        LoadDatabase();
        
        RegisterUser("gipszjakab", "gipszjakab@gmail.com", "jelszo123", "jelszo123");
        WaitForRegister();
        
        chromeDriver.Navigate().GoToUrl("http://localhost:8881/#/register");
        RegisterUser("gipszjakab2", "gipszjakab@gmail.com", "jelszo123", "jelszo123");
        
        WaitForInvalidRegister();

        Assert.AreEqual("Az e-mail már használatban van!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[5]/div/p")).Text);

    }
    
    [TestCleanup]
    public void Cleanup()
    {
        chromeDriver.Quit();
    }
}