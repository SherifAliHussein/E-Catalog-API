Feature: restaurant admin update item
In order to update item
As a restaurant admin
I want to update item

Scenario: restaurant admin update item
Given I am logged in as a restaurant admin to update item
And I update the current item name with new name
When I click on update item
Then item name will update successfully 

Scenario: restaurant admin update item with empty name
Given I am logged in as a restaurant admin to update item
And I update the current item name with empty name
When I click on update item
Then Missing item name validation message will return for the update item

Scenario: restaurant admin update item with existing name for the category
Given I am logged in as a restaurant admin to update item
And I update the current item name with exist name 
When I click on update item
Then repeated item name validation message will return for the update item


Scenario: restaurant admin update item name with more than 300 characters
Given I am logged in as a restaurant admin to update item 
And I update the current item with long name 
When I click on update item
Then Maximum length for item name validation message will return for the update item

Scenario: restaurant admin update item price less than or equal zero
Given I am logged in as a restaurant admin to update item
And I update the current item price with new price less than or equal zero
When I click on update item
Then item price should be positive validation message will return for the update item

Scenario: restaurant admin update item for category with empty description
Given I am logged in as a restaurant admin to update item 
And I update the current item description with empty 
When I click on update item 
Then Missing item description validation message will return for the update item

