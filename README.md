# GameLibrary-MVC-Application
An MVC application created for SPC's ASP.NET Programming with C# course. The purpose of this project is to give users a platform to share and explore video game reviews in an organized and interactive way. Users will be able to submit new games to the library/database, write their own reviews, or browse through existing titles to see what others have said about games they may be interested in.

## 📅 Project Roadmap

| Week | Concept | Feature | Goal | Acceptance Criteria | Evidence in README.md | Test Plan |
|------|----------|----------|------|----------------------|------------------------|------------|
| 10 | Modeling | Create `Game` and `Review` entities | App can store games and allow users to leave reviews. | [ ] Games table created <br> [ ] Reviews table created <br> [ ] Relationship works | Implemented code; README write-up; screenshots as needed | Run migration; verify DB tables exist |
| 11 | Separation of Concerns / DI | Add `IGameService` to calculate average rating | Move logic out of controller into a service. | [ ] Service registered in DI <br> [ ] Controller uses constructor injection <br> [ ] Average rating returned correctly | Implemented code; README write-up; screenshots as needed | Call endpoint; verify average ratings display properly |
| 12 | CRUD | Add Create/Edit forms for games and reviews | Users can add or edit games, and optionally post reviews. | [ ] Form displays <br> [ ] Validation messages show <br> [ ] Changes save to DB | Implemented code; README write-up; screenshots as needed | Add new game, edit it, post review, confirm DB update |
| 13 | Diagnostics | Add `/healthz` endpoint | App reports if the games database is reachable. | [ ] Healthy when DB up <br> [ ] Unhealthy when DB down | Implemented code; README write-up; screenshots as needed | Stop DB and hit `/healthz` |
| 14 | Logging | Log every new review submission | Record structured logs whenever users submit or edit reviews. | [ ] Log message created <br> [ ] Contains game ID, review ID, and user info | Implemented code; README write-up; screenshots as needed | Submit/edit a review; verify structured log output |
| 15 | Stored Procedures | Call SP: Top 5 highest-rated games | Display leaderboard of games by average review rating. | [ ] SP executes <br> [ ] Results displayed | Implemented code; README write-up; screenshots as needed | Run SP in app and DB; compare results |
| 16 | Deployment | Deploy app to Azure App Service | Make the application accessible in the cloud with production settings. | [ ] App Service created <br> [ ] App builds and runs on Azure <br> [ ] `/healthz` reachable <br> [ ] One functional path works | Implemented code; README write-up; screenshots as needed | Visit public URL; confirm health endpoint and main page load |

### Week 10 README
This week I set up the core data model for the Game Library MVC project and got everything working with Entity Framework Core. The main goal was to make sure each game could store multiple reviews, which meant building two entities that actually connect in the database. I added a Game class with basic fields like Title, Platform, Genre, ReleaseDate, and Price. Then I created a Review class that ties back to a specific game through a GameId foreign key which also includes a rating, a comment, and the reviewer’s name.

In the ApplicationDbContext, I defined DbSet<Game> and DbSet<Review> and set up the one-to-many relationship in OnModelCreating. I also added cascade delete so removing a game automatically removes its reviews. After wiring up the connection string in appsettings.json and registering the context in Program.cs, I ran Add-Migration AddReviews and Update-Database. Both the Games and Reviews tables now appear in SQL Server Object Explorer.

These additions officially gives the project a working relational structure for future CRUD pages and data features. It also serves as the foundation for expanding the app to include more advanced functionality later on, such as filtering reviews, displaying average ratings, and managing user feedback more efficiently.

### Week 11 README
This week I worked on Separation of Concerns by moving non-UI logic into a dedicated service and wiring it up through dependency injection. The goal was to keep controllers focused on routing and responses while pushing the review logic (queries, averages, and inserts) into a reusable class. I created an IReviewService interface and a ReviewService implementation that handles three main tasks: getting the average rating for a game, getting the list of reviews for a specific game, and adding a new review with a basic rating check to make sure values stay between 1 and 5. I registered the service with a Scoped lifetime so it matches the DbContext’s scope and avoids unnecessary re-instantiation.

In GamesController, I injected the service and kept the actions fairly thin as per the assignment requirements. The Details action retrieves the game record and delegates all the review work to the service, then passes the results to the view through ViewBag. The AddReview action just collects inputs and calls the service to save the new review, then redirects back to the same page.

Overall, this structure made the project feel cleaner and more organized. Having a dedicated service keeps the logic consistent and makes it easier to expand on later, especially if I decide to add new features like sorting, filtering, or more advanced validation for reviews.
