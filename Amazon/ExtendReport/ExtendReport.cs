using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static Amazon.Hooks.Hooks;

namespace Amazon
{
	public static class ExtentReport
	{
		#region Variables
		public static string OrganizationID = "";
		public static int counter = 1;
		public static string expectedValue, actualValue, log = "", ActualStep;
		public static void setExpectedValue(string expected) { expectedValue = expected; }
		public static void setActualValue(string Actual) { actualValue = Actual; }
		public static void setActualStep(string ActualSt) { ActualStep = ActualSt; }
		#endregion

		public static string ReportMessage()
		{
			return "Expected Value: " + expectedValue + "<br>" + "Actual Value: " + actualValue;
		}

		public static void LogStep(string message)
		{
			log = " " + message + "<br>";
			extent.AddTestRunnerLogs(log);
		}

		public static void Log(string message)
		{
			log = DateTime.Now.ToString("|" + "dd-MM-yy-HH-mm-ss") + "| " + message + "<br>";
			extent.AddTestRunnerLogs(log);
		}

		public static void ReportStep(string Message, string result = "")
		{
			string text = Message;
			// Add the result variable to the text message if not empty or null
			if (!string.IsNullOrEmpty(result)) text += ": " + result;
			ExtentReport.LogStep("&emsp;" + text);
			Debug.WriteLine(text);
			Console.WriteLine("\t" + text);
		}
	}
}
