Feature: restaurant admin delete item
In order to delete item
As a restaurant admin
I want to delete item

Scenario: restaurant admin delete item
Given I am logged in as a restaurant admin to delete item
And Select the item to delete
When I delete item
Then item will be deleted