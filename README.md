# SweetPotatoUI
This is a UI Automation Framework using SpecFlow and Selenium WebDriver.

**What it is**
SweetPotatoUI aims to simplify the implementation and maintenance of UI test automation.  SweetPotatoUI provides a user with the ability to build UI tests using the Gherkin language within SpecFlow feature files.  This let's us abstract the technical implementation of web automation driver software (currently supporting Selenium WebDriver).  This technique also makes the tests more maintainable because they are written in a manner which makes the tests easier to understand for future use.  A very simple example of a complete UI test could look like:

Scenario: Get to Google

	Given I have started the browser 'InternetExplorer' using the driver 'Selenium'
	
	When I Navigate to 'http://www.google.com'
	
	Then I see Page with Title 'Google'
	
Your tests will be more complex than this, of course.  You will probably want to be able to fill elements with data and assert against values/text of elements.  Don't worry, you can do this too.

**Getting started**
This tool runs within Visual Studio, and utilizes open-source dependancies (Nunit, SpecFlow and Selenium).  To get started, follow these steps:

- Start a new Class Library project in Visual Studio.
- Install the SweetPotatoUI Nuget package, called "SweetPotatoUI".
- Include driver executables.  FireFox and PhantomJS drivers are baked in with the framework.  We do not provide all of these executables, but they are available for download and as Nuget package installs.  An example of this would be the Chrome Driver (ChromeDriver.exe).
- Create a settings class which implements the ISweetPotatoSettings interface.  Implement all required members, including:
	- GetBrowserType - This is the browser which you would like to load.  Currently supported options are Chrome, FireFox, InternetExplorer and PhantomJS.  If you are using anything but FireFox and PhantomJS, please make sure you have included these executables.
	- GetDriverType - This tells SweetPotatoUI which driver automation framework you want to utilize.  Currently, only Selenium is supported.
	- GetDriverPath - Provide the path to the drivers you want to use.
	- GetExecutingAssemblyName - Return the name of your project.
	- GetWaitTimeInMilliseconds - Return the amount of time, in milliseconds, you would like to wait for certain events to wait for certain conditions.  e.g. How long would you like to wait for an element to appear on the screen.
	- IsElementHighlighterEnabled - When turned on, SweetPotatoUI will highlight elements on-screen as it interacts with them.
- Create SpecFlow feature file(s) to house your page element table(s).  This will be the means in which you will define the Html elements for use in your tests.  In other words, you will define *how* SweetPotatoUI can find each page element.
- Create SpecFlow feature files to your test scenario.  SweetPotatoUI provides a set of common steps, including data entry and data assertion.  You can also define your own custom steps.
- You will need to initiate the AutomationBrowser using your own simple custom step.  To do this instantiate the AutomationBrowserFactory class and call the CreateBrowser method, supplying your implementation of ISweetPotatoSettings as a parameter.
- Once your steps are defined, simple build and run the SpecFlow tests which get generated.

A sample application is supplied with the source code for SweetPotatoUI.