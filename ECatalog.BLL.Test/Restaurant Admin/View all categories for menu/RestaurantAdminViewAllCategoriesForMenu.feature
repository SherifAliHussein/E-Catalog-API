Feature: restaurant admin view list of all categories for menu
In order to view list of all categories for menu
As a restaurant admin
I want to view list of all categories for menu

Scenario: restaurant admin view list of all categories for menu
Given I am logged in as a restaurant admin to view list of all categories for menu
When I list all categories 
Then the list of categories will display with the name, description, status, image thumbnail and list of all items for this category
