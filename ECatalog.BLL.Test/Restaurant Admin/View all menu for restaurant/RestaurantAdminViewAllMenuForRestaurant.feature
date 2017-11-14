Feature: restaurant admin view list of all menus for restaurant
In order to view list of all menus for restaurant 
As a restaurant admin
I want to view list of all menus for restaurant

Scenario: restaurant admin view list of all menus for restaurant
Given I am logged in as a restaurant admin to view list of all menus for restaurant
When I list all menus for restaurant 
Then the list of menu will display with the menu name, description, status and list of all categories for this menu
