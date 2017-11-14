Feature: restaurant admin update menu
In order to update menu
As a restaurant admin
I want to update menu

Scenario: restaurant admin update menu
Given I am logged in as a restaurant admin to update menu
And I update the current menu name with new name
When I click on update menu
Then menu name will update successfully 

Scenario: restaurant admin update menu with empty name
Given I am logged in as a restaurant admin to update menu
And I update the current menu name with empty name
When I click on update menu
Then Missing menu name validation message will return for the updated menu

Scenario: restaurant admin update menu with existing name for the restaurant
Given I am logged in as a restaurant admin to update menu
And I update the current menu name with exist name 
When I click on update menu
Then repeated menu name validation message will return for the updated menu


Scenario: restaurant admin update menu name with more than 300 characters
Given I am logged in as a restaurant admin to update menu 
And I update the current menu with long name 
When I click on update menu
Then Maximum length for menu name validation message will return for the updated menu

