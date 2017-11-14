Feature: restaurant admin view list of all size
In order to view list of all sizes
As a restaurant admin
I want to view list of all sizes

Scenario: restaurant admin view list of all sizes
Given I am logged in as a restaurant admin to view list of all sizes
When I list all sizes 
Then the list of sizes will display with the name
