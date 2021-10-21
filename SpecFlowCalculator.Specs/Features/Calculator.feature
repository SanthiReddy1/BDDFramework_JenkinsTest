Feature: BrowserStack

Background: 
Given I open BrowserStack sign in page

@Login
Scenario Outline: Login within multiple users
	Given I enter valid email '<email>'
	And I enter valid password '<password>'
	When I click on Sign in
	Then I should be logged into BrowserStack page

	Examples: 
	| email                       | password    |
	| Sravankumar1719@gmail.com   | Sravan@17   |
	| tulasikasarapu001@gmail.com | Sweety@26   |
	| nagasanthi11@gmail.com      | October2021 |

@Login
Scenario: Login with invalid credentials
	Given I enter valid email 'nagasanthi11@gmail.com'
	And I enter invalid password 'October'
	When I click on Sign in
	Then user should not be logged in with an error message

