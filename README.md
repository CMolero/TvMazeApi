
# TvMazeApi

An Api that scrapes TV Maze and stores the TV show and Episodes in a DB.

Code Fulfills the following

* Scrapes the [TV Maze Api](http://www.tvmaze.com/api) for shows and said shows Episodes.
* Populates an **SQL DB** on local machine
* Returns scraped transformed data, from the database, as a Json. 
* Contains two main endpoints shows **api/shows?q=:query** and **api/shows/{showId}/episodes**
  * **api/shows** endpoints uses the scraping service and calls the TV Maze API.
  * **api/shows/{showId}/episodes** retrieves the episodes of a show if it's found in the DB
**NOTE**: I took a cascading approach therefor the Episodes endpoint will always return a 404 if the show doesn't exist in the DB, this prevent loose episodes from existing in the DB.
* Faults and Policies are handle by using Polly

### NuGet packages and similar libraries used:

 - .Net Core 6
 - AutoMapper v11.0.1
 - AutoMapper Dependency Injection v11.0.0
 - NewtonSoft.Json v13.0.1
 - Polly v7.2.3
 - Swashbuckle v6.3.1
 - XUnit v2.4.1


![image](https://user-images.githubusercontent.com/5658513/177415816-34fb3812-62e3-4a9f-b4db-609c15fab5d0.png)
