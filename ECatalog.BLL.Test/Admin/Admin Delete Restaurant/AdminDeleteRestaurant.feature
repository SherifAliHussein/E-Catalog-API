Feature: Admin delete restaurant
In order to delete restaurant
As a admin
I want to delete restaurant

Scenario: Admin delete restaurant
Given I am logged in as a admin to delete restaurant
And Select the restaurant to delete
When I delete restaurant 
Then restaurant will be deleted

