Feature: restaurant admin add new menu for restaurant
In order to add new menu
As a restaurant admin
I want to add new menu

Scenario: restaurant admin add new menu for restaurant 
Given I am logged in as a restaurant admin to add new menu for restaurant
And I entered menu name
When I click on add menu 
Then the menu will be added successfully deactivated

Scenario: restaurant admin add new menu for restaurant without entering name
Given I am logged in as a restaurant admin to add new menu for restaurant
And I left menu name
When I click on add menu 
Then Missing menu name validation message will return

Scenario: restaurant admin add new menu with existing name for the restaurant
Given I am logged in as a restaurant admin to add new menu 
When I entered existing menu name for the same restaurant 
And I click on add menu
Then repeated menu name validation message will return


Scenario: restaurant admin add new menu name with more than 300 characters
Given I am logged in as a restaurant admin to add new menu 
When I entered menu name with more than 300 characters
And I click on add menu 
Then Maximum length for menu name validation message will return





