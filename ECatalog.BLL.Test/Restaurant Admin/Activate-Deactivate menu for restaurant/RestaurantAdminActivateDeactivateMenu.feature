Feature: restaurant admin activate/deactivate menu 
In order to activate/deactivate menu 
As a restaurant admin
I want to activate/deactivate menu

Scenario: restaurant admin activate menu 
Given I am logged in as a restaurant admin to activate/deactivate menu 
When I activate the selected menu 
Then the menu will be activated

Scenario: restaurant admin activate menu that hasn’t activated category
Given I am logged in as a restaurant admin to activate/deactivate menu
When I activate the selected menu that has no activated category
Then menu hasn’t activated category validation message will return

Scenario: restaurant admin deactivate menu 
Given I am logged in as a restaurant admin to activate/deactivate menu
When I deactivate the selected menu
Then menu will be deactivate




