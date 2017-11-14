Feature: admin add new restaurant 
In order to add new restaurant
As a admin
I want to add new restaurant

Scenario: admin add new restaurant
Given I am logged in as a admin to add new restaurant
When I entered restaurant name and description and logo and select restaurant type and username for restaurant admin and password
And I click on add restaurant
Then the restaurant will be added successfully deactivated

Scenario: admin add new restaurant without entering restaurant name
Given I am logged in as a admin to add new restaurant
When I left restaurant name and select restaurant type 
And I click on add restaurant
Then Missing restaurant name validation message will return

Scenario: admin add new restaurant with existing name 
Given I am logged in as a admin to add new restaurant 
When I entered existing restaurant name 
And I click on add restaurant 
Then repeated restaurant name validation message will return


Scenario: admin add new restaurant name with more than 300 characters
Given I am logged in as a admin to add new restaurant 
When I entered restaurant name with more than 300 characters
And I click on add restaurant 
Then Maximum length for restaurant name validation message will return

Scenario: admin add new restaurant without entering restaurant description
Given I am logged in as a admin to add new restaurant
When I left restaurant description and select restaurant type 
And I click on add restaurant
Then Missing restaurant description validation message will return


Scenario: admin add new restaurant without entering username for restaurant admin
Given I am logged in as a admin to add new restaurant
When I left username for restaurant admin 
And I click on add restaurant
Then Missing admin username validation message will return

Scenario: admin add new restaurant without entering password for restaurant admin
Given I am logged in as a admin to add new restaurant
When I left password for restaurant admin 
And I click on add restaurant
Then Missing admin password validation message will return

Scenario: admin add new restaurant with existing username for restaurant admin
Given I am logged in as a admin to add new restaurant 
When I entered existing username for restaurant admin
And I click on add restaurant 
Then repeated username for restaurant admin validation message will return

Scenario: admin add new restaurant with short password for restaurant admin
Given I am logged in as a admin to add new restaurant
When I entered short password for restaurant admin 
And I click on add restaurant
Then Minimum password length 8 characters validation message will return

Scenario: admin add new restaurant with long password for restaurant admin
Given I am logged in as a admin to add new restaurant
When I entered long password for restaurant admin 
And I click on add restaurant
Then Maximum password length 25 characters validation message will return

