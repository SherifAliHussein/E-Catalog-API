Feature: restaurant admin add new size
In order to add new size
As a restaurant admin
I want to add new size

Scenario: restaurant admin add new size 
Given I am logged in as a restaurant admin to add new size
And I entered size name
When I click on add size 
Then the size will be added successfully deactivated

Scenario: restaurant admin add new size without entering name
Given I am logged in as a restaurant admin to add new size
And I left size name
When I click on add size 
Then Missing size name validation message will return

Scenario: restaurant admin add new size with existing name for the restaurant
Given I am logged in as a restaurant admin to add new size
And I entered existing size name for the same restaurant 
When I click on add size 
Then repeated size name validation message will return


Scenario: restaurant admin add new size name with less than 3 characters
Given I am logged in as a restaurant admin to add new size
And I entered size name with less than 3 characters
When I click on add size 
Then Minimum length for size name validation message will return

Scenario: restaurant admin add new size name with more than 100 characters
Given I am logged in as a restaurant admin to add new size
And I entered size name with more than 100 characters
When I click on add size 
Then Maximum length for size name validation message will return





