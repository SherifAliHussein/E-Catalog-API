Feature: restaurant admin update size
In order to update size
As a restaurant admin
I want to update size

Scenario: restaurant admin update size
Given I am logged in as a restaurant admin to update size
When I update the current menu name with new name
Then size name will update successfully 

Scenario: restaurant admin update size with empty name
Given I am logged in as a restaurant admin to update size
When I update the current menu name with empty name
Then Missing size name validation message will return for the updated size

Scenario: restaurant admin update size with existing name for the restaurant
Given I am logged in as a restaurant admin to update size
When I update the current menu name with exist name 
Then repeated size name validation message will return for the updated size

Scenario: restaurant admin update size name with more than 100 characters
Given I am logged in as a restaurant admin to update size 
When I update the current menu with long name 
Then Maximum length for size name validation message will return for the updated size

Scenario: restaurant admin update size name with less than 3 characters
Given I am logged in as a restaurant admin to update size 
When I update the current menu with short name 
Then Minimum length for size name validation message will return for the updated size

