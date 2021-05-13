Payment Gateway my solution

In order to create the database connection string should be changed in appsettings.json
Run "dotnet ef database update" from the database project

I tried to focus mostly on getting the project structure in a good state where each component can be maintained extended and tested.
Therefore I rely heavily on interfaces and dependency injection.
I feel there are many options to extend and make my solution more professional given the time and the effort.
For example by saving card details more securely so they can't be hacked. Or by adding authentication and authorization.
I really enjoyed the challenge overall :)


- 