# SocialNetwork
Console social network simulator with graph-based user connections

A console-based social network simulation built with C#.

## Project Overview
SocialNetwokr is an object-oriented application that models human social interactions using graph theory. It allows users to register, manage profiles, establish mutual friend connections, and join thematic groups. The project demonstrates core software engineering principles, including multi-layered architecture, data serialization, and secure password handling.

## Tech Stack
* **Language:** C#
* **Framework:** .NET 6.0
* **Data Storage:** JSON (with recursive graph reference handling)
* **Security:** SHA-256 Password Hashing

## Features
* **User Authentication:** Secure registration and login with SHA-256 password hashing.
* **Profile Management:** Edit personal biography and display names.
* **Social Graph:** Add or remove friends, and view your connections sorted by popularity.
* **Smart Recommendations:** Discover new people through an algorithm that analyzes mutual connections.
* **Thematic Groups:** Create, join, leave, and search for communities based on member count.
* **Data Persistence:** Automatic saving and loading of the network state using JSON serialization.
* **Admin Panel:** Special privileges for managing the network and forcefully deleting accounts.
