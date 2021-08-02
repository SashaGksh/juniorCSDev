1) How to setup project.
Clone the repository to your directory, open the solution, then open the Nuget console and enter the 'update-database' command. This will generate a database with the minimum required dataset.
2) Working with methods
CreateAd - Fill in all the fields of the object. The category and type of advertising must be specified correctly according to the requirements (for example, "HtmlAd").
Enter tags in words separated by a space
For your information: if you enter an invalid link, it will be marked "broken" (according to task 3).
If you leave the tags field empty, the ad will be automatically tagged by default (according to task 4)
GetAdByParam - Set 2 parameters, where type is "Type" / "Category" / "Tags" and Value is the value of the first parameter (for example "Toys" for "Category")
AddCategory - Enter the name of the category you want to create
DeleteAdById - Removes ads by their ID
Methods GetStatistics and GetAd do everything in accordance with the requirements.
Information about additional tasks:
1 - not implemented
2 - not implemented because the logic of viewing ads corresponds to the logic of clicking on it in this context, in my opinion
3 - Implemented, in the code it is marked with a region named 3. Correct links will remain with their name, incorrect ones will have a name "broken"
4 - Implemented, marked in the code with a region named 4. If you leave the tags field empty, then up to 3 tags will be assigned to the advertisement, based on the most frequently encountered words in the ad content
