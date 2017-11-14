Feature: restaurant admin view list of all side items
In order to view list of all side items
As a restaurant admin
I want to view list of all side items

Scenario: restaurant admin view list of all side items
Given I am logged in as a restaurant admin to view list of all side items
When I list all side items 
Then the list of side items will display with the name and value
