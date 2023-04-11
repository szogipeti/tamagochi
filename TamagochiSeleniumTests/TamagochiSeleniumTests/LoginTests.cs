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
    [TestMethod]
    public void TestMethod1()
    {
        
    }

    [TestCleanup]
    public void Cleanup()
    {
        
    }
}