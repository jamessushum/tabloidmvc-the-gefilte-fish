# Tabloid MVC
## Nashville Software School Cohort 41 Team: The Gefilte Fish

The Gefilte Fish are:
- James Su-Shum
- David Larsen
- John Hester
- Anthony Johnson

### Introduction
This project builds on a previous "proof-of-concept" built in a command line. Now, the team utilizes ASP.NET Core MVC to build a fully-functioning website. Within that website are users who can create content ("Posts") and share that content with other users. Admin users also have additional privileges, with the ability to create means of organizing those posts ("Categories" and "Tags").

### To begin:
1. Clone this repo
2. From the root directory, navigate to the SQL directory and run the two SQL scripts in the order indicated in the filenames
3. Run the program by either typing ```dotnet run``` in the console or by using your IDE's debugger
4. If it does not open automatically, navigate to ```https://localhost:5001```
5. You should see the login screen. Register a new user, or login using the sample admin, ```admin@example.com```
6. Browse the app and try out the different features. Don't worry: safeguards are in place to where you cannot do too much damage!

![Tabloid Screenshot](./Tabloid-Homepage.PNG)

# Original Project Description

Good news, everyone, our [Tabloid CLI Proof of Concept](https://github.com/nashville-software-school/TabloidCLI) did it's job! We were able to test our business idea after a minimal amount of development time. And we learned people don't want to keep a list of other people's blog content. What they really want is to make their own content.

So it's time to pivot. We're still going to focus on long-form writing, but not we'll let people write their own posts.

## Tabloid Prototype

We're ready to build a [working prototype](https://en.wikipedia.org/wiki/Prototype) of the Tabloid application. This prototype will help us better understand and refine our product. It won't be as feature-rich or as polished as our final product, but it should implement the core features and be as close to our _current vision_ of the product as possible. 

We know we want a multi-user web application with a rich user experience. For the final product, we we'd like to use react, but we'd like to develope the prototype as rapidly as possible. The architect has decided that ASP<span>.NET</span> Core MVC is the perfect balance of features and rapid development.

### Users

Tabloid MVC will have two types of users:

* **Authors** can create Posts, manage their own Posts, and read and comment on other authors' posts.

* **Admins** can do all the things authors can do, but are also in charge of managing all the data in the system.

### ERD

![Tabloid ERD](./Tabloid.png)
