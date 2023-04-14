using System.Diagnostics;

namespace TamagochiSeleniumTests;

[TestClass]
public class CreateAnimalTests
{
    private ChromeDriver chromeDriver;
    [TestInitialize]
    public void Initialize()
    {
        chromeDriver = new ChromeDriver();
        chromeDriver.Manage().Window.Maximize();
        chromeDriver.Navigate().GoToUrl("http://localhost:8881/#/register");

        string username = "gipszjakab";
        string email = "gipszjakab@gmail.com";
        string pwd = "jelszo123";
        
        CommonTasks.LoadDatabase();
        CommonTasks.RegisterUser(username, email, pwd, pwd, chromeDriver);
        CommonTasks.WaitForPageLoading("http://localhost:8881/#/login", chromeDriver);
        
        CommonTasks.LoginUser(email, pwd, chromeDriver);
        CommonTasks.WaitForPageLoading("http://localhost:8881/#/animals/select", chromeDriver);
    }

    [TestMethod]
    public void TestSuccessfulAnimalCreation()
    {
        string animalName = "Gombóc";
        CommonTasks.CreateAnimal(animalName, chromeDriver);
        CommonTasks.WaitForPageLoading("http://localhost:8881/#/", chromeDriver);
        Assert.AreEqual("http://localhost:8881/#/", chromeDriver.Url);

        string animalNameXPath = "//*[@id=\"app\"]/div/div/div[1]/div/h3";
        CommonTasks.WaitForElementLoading(animalNameXPath, chromeDriver);
        Assert.AreEqual(animalName + " adatai", chromeDriver.FindElement(By.XPath(animalNameXPath)).Text);
    }

    [TestMethod]
    public void TestEmptyCreation()
    {
        chromeDriver.FindElement(By.XPath("//*[@id=\"maindiv\"]/form/button")).Click();
        Assert.AreEqual("A név kitöltése kötelező!", chromeDriver.FindElement(By.XPath("//*[@id=\"maindiv\"]/form/div/div[1]/span")).Text);
        Assert.AreEqual("Az állat kiválasztása kötelező!", chromeDriver.FindElement(By.XPath("//*[@id=\"maindiv\"]/form/div/div[2]/span")).Text);
    }

    [TestMethod]
    public void TestLongNameCreation()
    {
        string animalName = "nagyonnagyonhosszuallatnev";
        CommonTasks.CreateAnimal(animalName, chromeDriver);
        Assert.AreEqual("A név maximum 25 karakter hosszú lehet!", chromeDriver.FindElement(By.XPath("//*[@id=\"maindiv\"]/form/div/div[1]/span")).Text);
    }

    [TestCleanup]
    public void Cleanup()
    {
        chromeDriver.Quit();
    }
}