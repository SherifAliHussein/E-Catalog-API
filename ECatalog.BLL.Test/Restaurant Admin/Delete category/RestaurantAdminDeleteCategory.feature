Feature: restaurant admin delete category
In order to delete category
As a restaurant admin
I want to delete category

Scenario: restaurant admin delete category
Given I am logged in as a restaurant admin to delete category
And Select the category to delete
When I delete category
Then category will be deleted
