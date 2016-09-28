Feature: GetToGoogle

Scenario: Get to Google
	Given I have started the browser 'Chrome' using the driver 'Selenium'
	
	Given 'GooglePageElements' Page Elements have been loaded

	Given I have Navigated to Google
	
	Then I see Page with Title 'Google'
	
	When I enter 'Des Moines' into the 'SearchBox'
	When I click element 'SearchButton'

	Then I see Page with Title 'Des Moines - Google Search'

	Then 'GooglePage' has elements that contain Value
		| ElementAlias | Value      |
		| SearchBox    | Des Moines |

	Then 'GoogleResultsPage' has elements that contain Text
		| ElementAlias | Text       |
		| ResultStats  | About      |