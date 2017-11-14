Feature: restaurant admin add new item for category
In order to add new item
As a restaurant admin
I want to add new item

Scenario: restaurant admin add new item for category 
Given I am logged in as a restaurant admin to add new item for category
And I entered item name, price and description and select the image
When I click on add item
Then the item will be added successfully

Scenario: restaurant admin add new item for category without entering name
Given I am logged in as a restaurant admin to add new item for category
And I left item name
When I click on add item
Then Missing item name validation message will return

Scenario: restaurant admin add new item with existing name for the category
Given I am logged in as a restaurant admin to add new item for category
And I entered existing item name for the same category
When I click on add item
Then repeated item name validation message will return


Scenario: restaurant admin add new item name with more than 300 characters
Given I am logged in as a restaurant admin to add new item for category
And I entered item name with more than 300 characters
When I click on add item
Then Maximum length for item name validation message will return

Scenario: restaurant admin add new item for category without entering description
Given I am logged in as a restaurant admin to add new item for category
And I left item description
When I click on add item
Then Missing item description validation message will return

Scenario: restaurant admin add new item with price less than or equal zero
Given I am logged in as a restaurant admin to add new item for category 
And I entered item name, description and image and price less than or equal zero 
When I click on add item
Then item price should be positive validation message will return

