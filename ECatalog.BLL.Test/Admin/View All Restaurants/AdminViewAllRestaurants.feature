Feature: Admin view list of all restaurants
In order to view list of all restaurants
As a admin
I want to view list of all restaurants

Scenario: Admin view list of all restaurants
Given I am logged in as a admin to view list of all restaurants
When I list all restaurants 
Then the list of restaurant will display with the restaurant name, description, restaurant admin info, restaurant type name and status and thumbnail image

