# Install instruction
## clone https://github.com/kadranga/HtmlAnalysis.git
## Execute DBScript.sql
## Update HtmlAnalysis.Api connection string data source
## Open project solution folder on command prompt
## Execute Run-App.bat
	I create this bat file to run project but something went wrong with SSL.
	Please do n't use this option
	
## Locally run solution
### api => eg: https://localhost:44343/index.html
	Expose two end points, post url and get top 100 words
	Get stored words and count from database with page index and number of rows to display on the page
### update port on the Admin.cshtml and Index.cshtml urls for request to api
### update policies on HtmlAnalysis.api start class
### web ui => eg: https://localhost:44383/
	Home page top 100 words, Need to enter validate url (no validation is implemented)
	Admin page list saved words from the database
	

# Technologies 
## .net core 3.1
## word cloud => jqcloud (http://mistic100.github.io/jQCloud/demo.html)
## HtmlAgilityPack to extracts words from the DOM (https://html-agility-pack.net/)
## For Data layer => Daper and inline sql
