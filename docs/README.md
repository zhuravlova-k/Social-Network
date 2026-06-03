# SocialNetwork
Console social network simulator with graph-based user connections

![Language](https://img.shields.io/badge/language-C%23-239120?logo=csharp&logoColor=white)
![Framework](https://img.shields.io/badge/.NET-6.0-512BD4?logo=dotnet&logoColor=white)
![Storage](https://img.shields.io/badge/storage-JSON-F7DF1E?logo=json&logoColor=black)
![Security](https://img.shields.io/badge/security-SHA--256-red)

## Project Overview
SocialNetwokr is an object-oriented application that models human social interactions using graph theory. It allows users to register, manage profiles, establish mutual friend connections, and join thematic groups. The project demonstrates core software engineering principles, including multi-layered architecture, data serialization, and secure password handling.

Built as a university course project, the codebase demonstrates layered OOP architecture, custom exception handling, and secure password storage.

## Architecture

IDisplayable (interface)
    └── BaseEntity (abstract class)
            ├── User
            │     └── Admin          ← inherits all User behaviour + elevated privileges
            └── FriendGroup

UserProfile                          ← aggregated by User (composition)
SocialNetwork                        ← orchestrates all business logic
SecurityHelper                       ← static utility, SHA-256 hashing
NetworkException                     ← custom exception type

## Features
* **User Authentication:** Secure registration and login with SHA-256 password hashing.
* **Profile Management:** Edit personal biography and display names.
* **Social Graph:** Add or remove friends, and view your connections sorted by popularity.
* **Smart Recommendations:** Discover new people through an algorithm that analyzes mutual connections.
* **Thematic Groups:** Create, join, leave, and search for communities based on member count.
* **Data Persistence:** Automatic saving and loading of the network state using JSON serialization.
* **Admin Panel:** Special privileges for managing the network and forcefully deleting accounts.
