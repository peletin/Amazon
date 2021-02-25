namespace Amazon.Hooks
{
    using AventStack.ExtentReports;
    using AventStack.ExtentReports.Gherkin.Model;
    using AventStack.ExtentReports.Reporter;
    using OpenQA.Selenium;
    using System;
    using System.Diagnostics;
    using TechTalk.SpecFlow;

    [Binding]
    public class Hooks
    {
        public IWebDriver Driver;
        public static ExtentReports extent;
        public static ExtentTest FeautureName, Scenario;
        public static ExtentHtmlReporter htmlReporter;

        public Hooks(IWebDriver driver)
        {
            Driver = driver;
        }

        [BeforeTestRun]
        public static void Initialize()
        {
            //Search for Chromedriver.exe runing task, If present then end it                        
            Process[] ChromeDriverProcess = Process.GetProcessesByName("chromedriver");
            foreach (var chromeDrivProc in ChromeDriverProcess)
            {
                chromeDrivProc.Kill();
            }
            //create HTML Report and Report Location
            htmlReporter = new ExtentHtmlReporter(".\\ExtentReport\\Report" + "__" + DateTime.Now.ToString("MMM-dd-yyyy__HH-mm-ss") + ".html");
            //Attach Report to reporter                          
            extent = new ExtentReports();
            //Reporter Started
            extent.AttachReporter(htmlReporter);
        }

        [BeforeFeature(Order = 0)]
        public static void BeforeFeatore()
        {
            FeautureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            //Extent Report
            ScenarioContext.Current["id"] = ExtentReport.counter;
            Scenario = FeautureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title + "<br/>" + "Test Case: " + ScenarioContext.Current["id"] + "");
            //Add Scenario Title to EXternal Report
            ExtentReport.Log("Scenario: " + ScenarioContext.Current.ScenarioInfo.Title);
            //Add Test Case number to EXternal Report
            ExtentReport.Log("Test Case: " + ScenarioContext.Current["id"] + "");
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            ExtentReport.LogStep("<br>===================================</br>");
            //get the count for script
            ExtentReport.counter = ExtentReport.counter + 1;
            ScenarioContext.Current["id"] = ExtentReport.counter;
        }

        [AfterStep]
        public static void AfterStepweb()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            //Assert Passed
            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                {
                    Scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "When")
                {
                    Scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                }

                else if (stepType == "Then")
                {
                    Scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Pass(ExtentReport.ReportMessage());
                }
            }
            //Assert Failed
            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                {
                    Scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                    ExtentReport.Log("Error: " + ScenarioContext.Current.TestError.Message + "<br>");
                }
                else if (stepType == "When")
                {
                    Scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                    ExtentReport.Log("Error: " + ScenarioContext.Current.TestError.Message + "<br>");
                }
                else if (stepType == "Then")
                {
                    Scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                    ExtentReport.Log("Error: " + ScenarioContext.Current.TestError.Message + "<br>");
                }
            }
        }

        [AfterScenario]
        public void AfterScenarios()
        {
            Driver.Quit();
            extent.Flush();
        }
    }
}
