Feature: restaurant admin update side item
In order to update side item
As a restaurant admin
I want to update side item

Scenario: restaurant admin update side item
Given I am logged in as a restaurant admin to update side item
When I update the current side item name with new name
Then side item name will update successfully 

Scenario: restaurant admin update side item with empty name
Given I am logged in as a restaurant admin to update side item
When I update the current side item name with empty name
Then Missing side item name validation message will return for the updated side item

Scenario: restaurant admin update side item with existing name
Given I am logged in as a restaurant admin to update side item
When I update the current side item name with exist name 
Then repeated side item name validation message will return for the updated side item

Scenario: restaurant admin update side item name with more than 100 characters
Given I am logged in as a restaurant admin to update side item 
When I update the current side item with long name 
Then Maximum length for size name validation message will return for the updated side item

Scenario: restaurant admin update side item name with less than 3 characters
Given I am logged in as a restaurant admin to update side item 
When I update the current side item with short name 
Then Minimum length for size name validation message will return for the updated side item

Scenario: restaurant admin update side item with invalid value
Given I am logged in as a restaurant admin to update side item
When I update the current side item with invalid value
Then Invalid side item value validation message will return for the updated side item