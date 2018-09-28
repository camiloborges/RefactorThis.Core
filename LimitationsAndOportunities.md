# Limitations and Opportunities
This challenge was completed in around 12 hrs and since it is a refactoring job it the idea was not to change anything with the service responses and behavior from a client perspective.

## limitations
- There is no authentication
- It should have an automated CI/CD pipeline
- Exception handling is a bit rough

## opportunities
- change constructors to use ILoggerFactory instead of ILogger
- Ensure BadResponses are always returned when exceptions happen
- Separate Repository and Controllers so there is one for Product and one for ProductOptions
- 