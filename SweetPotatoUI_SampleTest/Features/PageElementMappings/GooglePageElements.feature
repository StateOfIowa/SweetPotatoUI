Feature: GooglePageElements

Background:
	Given Page Elements Are Loaded
		| PageName          | Alias         | Id          | Name | LinkText | TagName | CssSelector | XPath             |
		| GooglePage        | SearchBox     |             |      |          |         |             | //*[@id="lst-ib"] |
		| GooglePage        | SearchButton  |             | btnG |          |         |             |                   |
		| GoogleResultsPage | SearchResults | res         |      |          |         |             |                   |
		| GoogleResultsPage | ResultStats   | resultStats |      |          |         |             |                   |