Feature: Admin activate/deactivate restaurant 
In order to activate/deactivate restaurant 
As a admin
I want to activate/deactivate restaurant 

Scenario: Admin activate restaurant 
Given I am logged in as a admin to activate/deactivate restaurant 
When I activate the selected restaurant 
Then the restaurant will be activated

Scenario: Admin activate restaurant that hasn’t activated menu
Given I am logged in as a admin to activate/deactivate restaurant 
When I activate the selected restaurant that has no activated menu
Then restaurant hasn’t activated menu validation message will return

Scenario: Admin deactivate restaurant 
Given I am logged in as a admin to activate/deactivate restaurant 
When I deactivate the selected restaurant
Then restaurant will be deactivate



