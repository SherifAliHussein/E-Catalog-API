Feature: restaurant admin update category
In order to update category
As a restaurant admin
I want to update category

Scenario: restaurant admin update category
Given I am logged in as a restaurant admin to update category
And I update the current category name with new name
When I click on update category
Then category name will update successfully 

Scenario: restaurant admin update category with empty name
Given I am logged in as a restaurant admin to update category
And I update the current category name with empty name
When I click on update category
Then Missing category name validation message will return for the updated category

Scenario: restaurant admin update category with existing name for the menu
Given I am logged in as a restaurant admin to update category
And I update the current category name with exist name 
When I click on update category
Then repeated category name validation message will return for the updated category

Scenario: restaurant admin update category name with more than 300 characters
Given I am logged in as a restaurant admin to update category 
And I update the current category with long name 
When I click on update category
Then Maximum length for category name validation message will return for the updated category
