# OAuthServer project Changelog

## Table of Contents

- [Unreleased](#unreleased)
- [Released](#released)

## Unreleased  

* Project Setup with Clean Architecture
  * Created a New Solution with Separate Layers:
     * **Domain Layer** (OauthServer.Domain): 
       * Contains the ApplicationUser entity and IUserRepository interface.
     * **Application Layer** (OauthServer.Application): 
       * (Reserved for business logic and services; to be expanded).
     * **Infrastructure Layer** (OauthServer.Infrastructure): 
       * Implements the IUserRepository using MongoDB.
     * **Presentation Layer** (OauthServer.Presentation): 
       * Hosts the ASP.NET Core Web API and configures IdentityServer.
* Implemented the Domain Layer
   * Defined the ApplicationUser Entity:
   * Represents the user data model.
   * Included properties for Id, UserName, Password, and Email.
   * Used MongoDB BSON attributes to map properties to MongoDB fields and handle ObjectId conversions.
   * Defined the IUserRepository Interface:
     * Specifies methods for user data access, such as GetUserByIdAsync, GetUserByUserNameAsync, and CreateUserAsync.
* Set Up the Infrastructure Layer with MongoDB
  * Installed MongoDB Driver:
    * Added MongoDB.Driver NuGet package to the Infrastructure project.
    * Created MongoDbContext:
      * Manages the MongoDB connection and provides access to the Users collection.
    * Implemented UserRepository:
      * Implements IUserRepository using MongoDB operations. 
      * Handles user data retrieval and storage.
* Configured the Presentation Layer
  * Installed Required Packages:
    * Added Duende.IdentityServer and other necessary packages. 
    * Configured IdentityServer:
      * Set up IdentityServer with in-memory clients, scopes, and resources. 
      * Used AddDeveloperSigningCredential for development purposes. 
    * Created Config.cs:
      * Defined IdentityServer resources, scopes, and clients. 
      * Configured a client (client1) to use the Resource Owner Password grant type. 
    * Implemented Custom Services:
      * CustomResourceOwnerPasswordValidator:
        * Validates user credentials using IUserRepository.
        * Implements password verification with BCrypt hashing.
        * CustomProfileService:
          * Provides user profile data to IdentityServer.
          * Ensures the user is active before issuing tokens.
  * Secured Password Handling 
    * Implemented Password Hashing:
      * Used BCrypt.Net-Next to hash passwords before storing them in the database. 
      * Updated password validation to verify hashed passwords. 
  * Created User Registration Endpoint:
    * AccountController:
      * Provides an API endpoint to register new users.
      * Hashes the user's password during registration.
      
## Released  

