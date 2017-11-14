Feature: restaurant admin delete menu
In order to delete menu
As a restaurant admin
I want to delete menu

Scenario: restaurant admin delete menu
Given I am logged in as a restaurant admin to delete menu
And I select the menu to deleted
When I click on delete menu
Then the menu will be deleted
