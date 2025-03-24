
# Accounts Balance Viewer

This app will give brief overview about account balances of the company

## Table of Contents
- [Technologies](#technologies)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Default Users](#default-users)
- [Published URL](#published-url)

## Technologies

This project is built using the following technologies:
- **Node.js**: v20.x.x
- **Angular**: v17.x.x
- **.NET**: 8.0

## Prerequisites

Before you begin, ensure you have the following installed:
- [Node.js](https://nodejs.org/en/) (v20.x.x) - Includes npm
- [Angular CLI](https://angular.io/cli) (v17.x.x) - Install globally with `npm install -g @angular/cli@17`
- [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (8.0)
- A code editor like [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio 2022](https://visualstudio.microsoft.com/)

## Installation

1. **Clone the Repository**
   ```bash
   git clone https://github.com/dhamyth/AccountsApp.git
   cd AccountsApp

2. **Backend (.NET) Setup**
- Navigate to the backend folder (e.g., ./API):
   ```bash
   cd API

- Restore .NET dependencies:
   ```bash
   dotnet restore

- Build the project:
   ```bash
   dotnet build

3. **Frontend (Angular) Setup**
- Navigate to the frontend folder (e.g., ./frontend):
   ```bash
   cd client

- Install Node.js dependencies
   ```bash
   npm install

## Running the Application

1. **Start the Backend (.NET)**
- From the backend folder:
   ```bash
   cd API

- The .NET API will typically run on https://localhost:5001 (or as configured in appsettings.json).

2. **Start the Frontend (Angular)**
- From the frontend folder:
   ```bash
   cd client
   ng serve

- The Angular app will run on http://localhost:4200 by default.

3. **Access the Application**
- Open your browser and navigate to http://localhost:4200 to view the Angular frontend.
- The frontend will communicate with the .NET backend API.

## Default Users
The application comes with two pre-configured users for testing purposes:

| Role       | Username       | Password       | Description                     |
|------------|----------------|----------------|---------------------------------|
| Normal     | `lisa`         | `Pa$$w0rd`     | A standard user with basic access. |
| Admin      | `admin`        | `Pa$$w0rd`    | An admin user with full access.    |


## Published URL

The application is deployed and accessible at the following URL:
 https://accounting-app-gbffgvaqhkdaf0d3.southindia-01.azurewebsites.net/