Feature: restaurant admin view list of all items for category
In order to view list of all items for category
As a restaurant admin
I want to view list of all items for category

Scenario: restaurant admin view list of all items for category
Given I am logged in as a restaurant admin to view list of all items for category
When I list all items 
Then the list of items will display with the name, description, image and price
