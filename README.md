How to Use Frontend with Backend
Ensure that the backend server is running (instructions provided in the backend README). The frontend will communicate with the backend server for all API requests.




### Backend README


# MySocialProject - Backend

## Description
MySocialProject is a social media platform where users can interact, share posts, and connect with friends. This is the backend part of the project, built using ASP.NET Core.

## Features
- User Authentication: Secure login and registration system to protect user data.
- Post Management: Users can create, delete, and view posts.
- Comments: Engage with posts by adding comments.
- Likes: Show appreciation for posts by liking them.
- Relationships: Manage following and followers.
- Stories: Share short-lived content that disappears after 24 hours.

## Getting Started

### Prerequisites
- .NET SDK (v6.0 or higher)
- MySQL Database

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/GregoryKrichman/socialMediaVS.git
   
Update the appsettings.json file with your MySQL database connection string:


{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=social;User=root;Password=29071991;"
  },
  "Jwt": {
    "Key": "94df5f05e33949299d7a9ba6cc6bf554",
    "Issuer": "SocialMediaApp"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

Run the migrations to set up the database:


dotnet ef database update
Running the Application
Start the server:

bash
Copy code
dotnet run
The backend server will start at https://localhost:5001.

How to Use Backend with Frontend
Ensure that the frontend server is running (instructions provided in the frontend README). The frontend will communicate with the backend server for all API requests.

Admin Login
Use the following credentials to log in as an admin user:

Username: admin
Password: AdminPassword123!
Usage
Authentication: Secure login and registration for users.
Posts: Create, view, update, and delete posts.
Comments: Add and manage comments on posts.
Likes: Like or unlike posts.
Relationships: Follow or unfollow other users.
Stories: Share and view stories.
Project Structure
Controllers: Handles HTTP requests and responses.
Models: Contains the data models.
Repositories: Provides data access methods.
Services: Contains business logic and services.
