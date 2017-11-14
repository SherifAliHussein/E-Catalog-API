Feature: restaurant admin activate/deactivate category 
In order to activate/deactivate category 
As a restaurant admin
I want to activate/deactivate category

Scenario: restaurant admin activate category 
Given I am logged in as a restaurant admin to activate/deactivate category 
When I activate the selected category 
Then the category will be activated

Scenario: restaurant admin activate category that hasn’t items
Given I am logged in as a restaurant admin to activate/deactivate category 
When I activate the selected category that has no items
Then category hasn’t items validation message will return

Scenario: restaurant admin deactivate category 
Given I am logged in as a admin to activate/deactivate category 
When I deactivate the selected category
Then category will be deactivate
