Feature: Admin update restaurant 
In order to update restaurant
As a admin
I want to update restaurant

Scenario: Admin update restaurant
Given I am logged in as a admin to update restaurant
And select restaurant to update
When I update restaurant name with new restaurant name 
Then restaurant name will be changed

Scenario: Admin leaves restaurant name empty
Given I am logged in as a admin to update restaurant
When I update restaurant name with empty restaurant name 
Then Missing restaurant name validation message will return for updated restaurant

Scenario: Admin entered existing restaurant name 
Given I am logged in as a admin to update restaurant
When I update restaurant name with existing restaurant name 
Then repeated restaurant name validation message will return for updated restaurant

