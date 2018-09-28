# Limitations and Opportunities
This challenge was completed in around 12 hrs and since it is a refactoring job it the idea was not to change anything with the service responses and behavior from a client perspective.

## limitations
- There is no authentication
- It should have an automated CI/CD pipeline
- Exception handling is a bit rough
- Dev ConnectionString has hardcoded path. 
## opportunities
- change constructors to use ILoggerFactory instead of ILogger
- Ensure BadResponses are always returned when exceptions happen
- Separate Repository and Controllers so there is one for Product and one for ProductOptions
- Break down into multiple projects
- Create some diagrams 
## review questions
- Should Repository Throw KeyNotfound or should this be done at controller level? 
- Should there be a layer between repository and controller? This doesn't feel big enough for that, but might be something to be tackled 