# Fitness-tracker

Fitness-tracker is a web application that allows users to look at recipes and create their own ones. They can also add their body measurements in order to create a diet!

## Table of Contents
1. Introduction
2. Getting Started
- Installation
- Configuration
3. Usage
4. Project Structure
5. Contributing


  
## Introduction
Fitness-tracker is a web application built on the MVC (Model-View-Controller) architecture.
The platform allows users to look at recipes and create their own ones. They can also add their body measurements in order to creeate a diet!


## Getting Started

## Installation

## Prerequisites
Make sure you have the following software installed on your machine:

- **Visual Studio:** I recommend using Visual Studio 2022 Preview.
- **.NET SDK:** Ensure you have the .NET SDK installed, targeting the `net7.0` framework.

1. Clone the repository

2. Open the Project:

- Open the Fitness-tracker.sln solution file in Visual Studio.

3. Configure the Database:

- Open appsettings.json and update the connection string in the DefaultConnection section to point to your desired database.

  "ConnectionStrings": {
    "DefaultConnection": "Server=ACERLAP;Database=FitnessTracker;Trusted_Connection=True;TrustServerCertificate=True"
  }
 }

4. Run Migrations:

Open the Package Manager Console in Visual Studio and run the following command:
Update-Database
- This will apply the database migrations and create the necessary tables.

5. Run the Application
6. Explore the Application

## Usage
Fitness-tracker is designed to make dieting easy and efficient. The application is divided into five endpoints:

## Home
- You can buy a diet plan
- You can also navigate to instagram or facebook page
  
## Recipes
- The user can filter recipes.
- The user can look at different recipes and add them to his daily macros, but he needs to create a body first!

## Create Recipe: 
- The user can create his own recipe and then by clicking the create button he adds it to Recipes.
  
## Create body: 
- The user can add his body measurements to the website.

## Macros Info:
- The user can monitor his macros.


## Project Structure:
1. Fitness-Tracker (It contains everything needed for using the website)


## Contributing
We welcome contributions to enhance Fitness-tracker. Follow these steps to contribute:

1. Fork the repository
2. Create a new branch
3. Make your changes
4. Open a pull request

## Current Database Diagram
![db fitness tracker](https://github.com/IskrenVanev/Fitness-tracker/assets/75131691/3f181102-ed71-4ad5-9971-0eba2e82a565)

