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

    [TestMethod]
    public void TestSuccessfulRegister()
    {
        CommonTasks.LoadDatabase();

        CommonTasks.RegisterUser("gipszjakab", "gipszjakab@gmail.com", "jelszo123", "jelszo123", chromeDriver);
        
        CommonTasks.WaitForPageLoading("http://localhost:8881/#/login", chromeDriver);

        Assert.AreEqual("http://localhost:8881/#/login", chromeDriver.Url);
        Assert.AreEqual("Bejelentkezés", chromeDriver.FindElement(By.XPath("//*[@id=\"login\"]/h3")).Text);
    }

    [TestMethod]
    public void TestEmptyRegister()
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
        CommonTasks.RegisterUser("gipszjakab", "gipszjakab134", "jelszo123", "jelszo123", chromeDriver);

        Assert.AreEqual("http://localhost:8881/#/register", chromeDriver.Url);
        Assert.AreEqual("Az e-mail formátuma nem megfelelő!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[2]/span")).Text);
    }

    [TestMethod]
    public void TestNotMatchingPasswordRegister()
    {
        CommonTasks.RegisterUser("gipszjakab", "gipszjakab@gmail.com", "jelszo123", "jelszo1234", chromeDriver);
        
        IWebElement button = chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/button"));
        button.Click();

        Assert.AreEqual("http://localhost:8881/#/register", chromeDriver.Url);
        Assert.AreEqual("A jelszó megerősítésének meg kell egyeznie a jelszóval!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[4]/span")).Text);
    }

    [TestMethod]
    public void TestUsedUsernameRegister()
    {
        CommonTasks.LoadDatabase();
        
        CommonTasks.RegisterUser("gipszjakab", "gipszjakab@gmail.com", "jelszo123", "jelszo123", chromeDriver);
        CommonTasks.WaitForPageLoading("http://localhost:8881/#/login", chromeDriver);
        
        chromeDriver.Navigate().GoToUrl("http://localhost:8881/#/register");
        CommonTasks.RegisterUser("gipszjakab", "gipszjakab2@gmail.com", "jelszo123", "jelszo123", chromeDriver);
        
        CommonTasks.WaitForElementLoading("//*[@id=\"register\"]/form/div[5]/div/p", chromeDriver);
        
        Assert.AreEqual("A felhasználónév már használatban van!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[5]/div/p")).Text);
    }

    [TestMethod]
    public void TestUsedEmailRegister()
    {
        CommonTasks.LoadDatabase();
        
        CommonTasks.RegisterUser("gipszjakab", "gipszjakab@gmail.com", "jelszo123", "jelszo123", chromeDriver);
        CommonTasks.WaitForPageLoading("http://localhost:8881/#/login", chromeDriver);
        
        chromeDriver.Navigate().GoToUrl("http://localhost:8881/#/register");
        CommonTasks.RegisterUser("gipszjakab2", "gipszjakab@gmail.com", "jelszo123", "jelszo123", chromeDriver);
        
        CommonTasks.WaitForElementLoading("//*[@id=\"register\"]/form/div[5]/div/p", chromeDriver);

        Assert.AreEqual("Az e-mail már használatban van!", chromeDriver.FindElement(By.XPath("//*[@id=\"register\"]/form/div[5]/div/p")).Text);

    }
    
    [TestCleanup]
    public void Cleanup()
    {
        chromeDriver.Quit();
    }
}