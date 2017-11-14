Feature: Add new restaurant type
In order to add new restaurant type
As a admin
I want to add new restaurant type

Scenario: Admin add new restaurant type 
Given I am logged in as a admin to add new restaurant type
When I entered restaurant type name 
And I click on add restaurant type 
Then restaurant type will added successfully 

Scenario: Admin add new restaurant type without entering name 
Given I am logged in as a admin to add new restaurant type
When I left restaurant type name 
And I click on add restaurant type 
Then Missing Name validation message will return

Scenario: Admin add new restaurant type with existing name 
Given I am logged in as a admin to add new restaurant type
When I entered existing restaurant type name 
And I click on add restaurant type 
Then Repeated Name validation message will return


Scenario: Admin add new restaurant type with more than 300 characters  
Given I am logged in as a admin to add new restaurant type
When I entered restaurant type name with more than 300 characters
And I click on add restaurant type 
Then Maximum length validation message will return


