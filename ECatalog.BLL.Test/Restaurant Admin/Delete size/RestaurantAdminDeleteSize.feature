Feature: restaurant admin delete size
In order to delete size
As a restaurant admin
I want to delete size

Scenario: restaurant admin delete size
Given I am logged in as a restaurant admin to delete size
And I select the size to deleted
When I click on delete size
Then the size will be deleted
