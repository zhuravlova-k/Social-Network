# SocialNetwork
Console social network simulator with graph-based user connections


## Project Overview
SocialNetwokr is an object-oriented application that models human social interactions using graph theory. It allows users to register, manage profiles, establish mutual friend connections, and join thematic groups. The project demonstrates core software engineering principles, including multi-layered architecture, data serialization, and secure password handling.

Built as a university course project, the codebase demonstrates layered OOP architecture, custom exception handling, and secure password storage.

## Architecture

```
IDisplayable (interface)
    └── BaseEntity (abstract class)
            ├── User
            │     └── Admin          ← inherits all User behaviour + elevated privileges
            └── FriendGroup

UserProfile                          ← aggregated by User (composition)
SocialNetwork                        ← orchestrates all business logic
SecurityHelper                       ← static utility, SHA-256 hashing
NetworkException                     ← custom exception type
```

| Class | Role |
|---|---|
| `IDisplayable` | Contract: every entity exposes `GetInfo()` |
| `BaseEntity` | Abstract base; holds `Guid Id` and implements `IDisplayable` |
| `User` | Core entity: login, hashed password, name, friends list, groups list |
| `Admin` | Extends `User`; unlocks force-delete and user listing |
| `UserProfile` | Composition object: bio, registration date |
| `FriendGroup` | Named community with a member list |
| `SocialNetwork` | Graph manager: registration, auth, friend/group operations, persistence |
| `SecurityHelper` | Stateless helper: `HashPassword(string) → string` (SHA-256) |
| `NetworkException` | Domain-level exception; caught and displayed in red by `Program.cs` |

---

## Tech Stack

| Layer | Technology |
|---|---|
| Language | C# 10 |
| Runtime | .NET 6.0 |
| Persistence | `System.Text.Json` with `ReferenceHandler.Preserve` (handles circular graph references) |
| Security | `System.Security.Cryptography.SHA256` |
| IDE | Visual Studio / Rider / VS Code |

---


## Features
* **User Authentication:** Secure registration and login with SHA-256 password hashing.
* **Profile Management:** Edit personal biography and display names.
* **Social Graph:** Add or remove friends, and view your connections sorted by popularity.
* **Smart Recommendations:** Discover new people through an algorithm that analyzes mutual connections.
* **Thematic Groups:** Create, join, leave, and search for communities based on member count.
* **Data Persistence:** Automatic saving and loading of the network state using JSON serialization.
* **Admin Panel:** Special privileges for managing the network and forcefully deleting accounts.
