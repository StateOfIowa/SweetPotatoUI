Feature: GetToGoogleWithNoCode

Scenario: Get to Google

	Given I have started the browser 'InternetExplorer' using the driver 'Selenium'
	
	Given 'GooglePageElements' Page Elements have been loaded

	When I Navigate to 'http://www.google.com'
	
	Then I see Page with Title 'Google'
	
	Then 'GooglePage' has Displayed Elements
		| ElementAlias |
		| SearchBox    |

	When I enter 'Des Moines' into the 'SearchBox' element of the 'GooglePage' page

	When I tab away from Element 'SearchBox' of the 'GooglePage' page

	When I fill Element of the 'GooglePage' page
		| ElementAlias | Data      |
		| SearchBox    | Denver    |
		| SearchBox    | Omaha     |

	#When I scroll to the 'SearchButton' element of the 'GooglePage'

	When I click the 'SearchButton' element of the 'GooglePage' page

	Then I see Page with Title 'Omaha - Google Search'

	Then 'GooglePage' has elements that contain Value
		| ElementAlias | Value |
		| SearchBox    | Omaha |

	Then 'GooglePage' has Enabled Elements 
		| ElementAlias |
		| SearchBox    |

	Then 'GoogleResultsPage' has elements that contain Text
		| ElementAlias | Text       |
		| ResultStats  | About      |

	Then 'GooglePage' has elements with Exact Value
		| ElementAlias | Value |
		| SearchBox    | Omaha |