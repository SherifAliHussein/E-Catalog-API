Feature: restaurant admin add new category for menu
In order to add new category
As a restaurant admin
I want to add new category

Scenario: restaurant admin add new category for menu 
Given I am logged in as a restaurant admin to add new category for menu
And I entered category name and select the image
When I click on add category
Then the category will be added successfully deactivated

Scenario: restaurant admin add new category for menu without entering name
Given I am logged in as a restaurant admin to add new category for menu
And I left category name
When I click on add category
Then Missing category name validation message will return

Scenario: restaurant admin add new category with existing name for the menu
Given I am logged in as a restaurant admin to add new category for menu
When I entered existing category name for the same menu
And I click on add category
Then repeated category name validation message will return

Scenario: restaurant admin add new category name with more than 300 characters
Given I am logged in as a restaurant admin to add new category for menu
When I entered category name with more than 300 characters
And I click on add category
Then Maximum length for category name validation message will return
