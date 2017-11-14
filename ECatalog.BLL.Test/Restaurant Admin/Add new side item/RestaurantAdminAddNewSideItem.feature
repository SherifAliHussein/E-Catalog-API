Feature: restaurant admin add new side item
In order to add new side item
As a restaurant admin
I want to add new side item

Scenario: restaurant admin add new side item 
Given I am logged in as a restaurant admin to add new side item
And I entered side item name and value
When I click on add side item 
Then the side item will be added successfully deactivated

Scenario: restaurant admin add new side item without entering name
Given I am logged in as a restaurant admin to add new side item
And I left side item name
When I click on add side item 
Then Missing side item name validation message will return

Scenario: restaurant admin add new side item with existing name 
Given I am logged in as a restaurant admin to add new side item
And I entered existing side item name 
When I click on add side item 
Then repeated side item name validation message will return


Scenario: restaurant admin add new side item name with less than 3 characters
Given I am logged in as a restaurant admin to add new side item
And I entered side item name with less than 3 characters
When I click on add side item 
Then Minimum length for side item name validation message will return

Scenario: restaurant admin add new side item name with more than 100 characters
Given I am logged in as a restaurant admin to add new side item
And I entered side item name with more than 100 characters
When I click on add side item 
Then Maximum length for side item name validation message will return

Scenario: restaurant admin add new side item without entering value
Given I am logged in as a restaurant admin to add new side item
And I left side item value
When I click on add side item 
Then Missing side item value validation message will return

Scenario: restaurant admin add new side item with invalid number for value
Given I am logged in as a restaurant admin to add new side item
And I entered side item name and invalid number for value
When I click on add side item 
Then Invalid side item value validation message will return



