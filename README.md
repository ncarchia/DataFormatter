### Introduction
DataFormatter is a Web API for formatting some text fields in the Title Register json file by returning them as structured objects to be more readable.
### Project Support Features
Users can post the Title register json file and get back the same file with the schedule of notices of leases text data as structured, more readable object with following fields:
* RegDateAndPlanRef
* PropertyDescription
* DateOfLeaseAndTerm
* LeaseTitle
* Notes
* SupplementalNote
### Installation Guide
* Clone this [LondonExchange](https://github.com/ncarchia/DataFormatter.git) repository.
### Usage
The Web API application:
* Can be opened and run from any software IDE (Visual Studio, Rider, etc.)
* Can be run from the command line by navigating to the project folder and running the dotnet run command and then by pasting the following Url in the browser: https://localhost:7117/swagger/index.html
* Connect to the API using Postman on port 7152.
### API Endpoints
* POST api/DataFormatter/Data/FormatScheduleOfNoticesOfLeases - To format the Schedule of notices of leases data
### Authors
* [NCarchia](https://github.com/ncarchia)