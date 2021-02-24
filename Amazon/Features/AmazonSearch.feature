Feature: Amazon Search
	Simple calculator for adding two numbers

@mytag
Scenario Outline: Amazon Search
	Given Web browser is open
	And 'Amazon' page is loaded
	When User search for "<Model>"
	And Clicks on the first result
	And Item is added to cart 
	And Navigates to the cart
	And Clicks on checkout
	Then User deletes the items

Examples:
|Model|
| Samsung Galaxy Note 20   |
| Samsung Galaxy S20 FE 5G |