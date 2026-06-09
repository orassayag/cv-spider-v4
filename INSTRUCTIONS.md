# Setup and Usage Instructions

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Initial Setup](#initial-setup)
3. [Database Configuration](#database-configuration)
4. [Available Commands](#available-commands)
5. [Running the Application](#running-the-application)
6. [Configuration](#configuration)
7. [How It Works](#how-it-works)
8. [Extending the Application](#extending-the-application)
9. [Troubleshooting](#troubleshooting)
10. [Development](#development)
11. [Best Practices](#best-practices)
12. [Documentation](#documentation)
13. [External Resources](#external-resources)

## Setup Instructions

### Prerequisites

#### System Requirements

- **.NET Framework**: Version 4.5.2 or higher
- **IDE**: Visual Studio 2013 or higher
- **Database**: SQL Server (Express, Standard, or Enterprise)
- **Web Server**: IIS Express (for development) or IIS (for production)
- **Operating System**: Windows 7 or higher / Windows Server 2012 or higher

1. **Visual Studio** (2013 or higher recommended)
2. **.NET Framework** 4.5.2 or higher
3. **SQL Server** (any edition)
4. **IIS Express** (included with Visual Studio) or IIS

## Initial Setup

### 1. Install Dependencies

The project uses NuGet for package management. Dependencies are defined in `packages.config`.

**Using Visual Studio:**

- Right-click the Solution and select "Restore NuGet Packages".
- Visual Studio will automatically download and install the required packages.

**Using NuGet CLI:**

```bash
nuget restore CVSpider.csproj
```

### 2. Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/orassayag/cv-spider-v4.git
   cd cv-spider-v4
   ```

2. Open the solution in Visual Studio:
   - Open `CVSpider.csproj` in Visual Studio
   - Restore NuGet packages (Visual Studio will prompt you)

3. Configure the database:
   - Create a new database in SQL Server
   - Update the connection string in `Web.config`
   - Run the database setup scripts (create stored procedures)

## Database Configuration

### Connection String

Update the connection string in `Web.config`:

```xml
<connectionStrings>
  <add name="MainDB"
       connectionString="Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### Required Stored Procedures

Create the following stored procedures in your database:

#### 1. CreateEmail

```sql
CREATE PROCEDURE dbo.CreateEmail
    @Email VARCHAR(255)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Emails WHERE Email = @Email)
    BEGIN
        INSERT INTO Emails (Email, CreatedDate)
        VALUES (@Email, GETDATE())
    END
END
```

#### 2. GetEmail

```sql
CREATE PROCEDURE dbo.GetEmail
    @Email VARCHAR(255)
AS
BEGIN
    SELECT Email, CreatedDate
    FROM Emails
    WHERE Email = @Email
END
```

#### 3. Create Emails Table

```sql
CREATE TABLE Emails (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email VARCHAR(255) NOT NULL UNIQUE,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
)
```

## Available Commands

### Development Commands

**Building the Solution:**

```bash
# Build in Debug mode
msbuild CVSpider.csproj /p:Configuration=Debug

# Build in Release mode
msbuild CVSpider.csproj /p:Configuration=Release
```

**Restoring Packages:**

```bash
# Restore NuGet dependencies
nuget restore CVSpider.csproj
```

### Running Scripts

The application uses the `Spider.ashx` handler as its primary execution script.

**Triggering the Spider:**

- Access `http://localhost:PORT/Spider.ashx` in your browser.
- Or use a tool like `curl` or `Postman` to send a GET request to the handler.

## Running the Application

### Using Visual Studio

1. Set the startup project to `CVSpider`
2. Press F5 or click "Start Debugging"
3. The application will launch in your default browser
4. Navigate to `/Spider.ashx` to trigger the spider

### Using IIS

1. Build the solution in Release mode
2. Publish to a folder or directly to IIS
3. Configure the application pool to use .NET Framework 4.5.2
4. Set appropriate permissions for the application folder
5. Access the handler at `http://your-domain/Spider.ashx`

## Configuration

### Search Parameters

The spider searches for job postings with the following parameters (configurable in `Spider.ashx.cs`):

```csharp
// Action type: "search" or "print"
string actionType = "search";

// Date range for filtering results
int printFromYear = 2015;
int printFromMonth = 9;
int printFromDay = 28;
int printFromHour = 22;
int printFromMinute = 0;
int printFromSeconds = 0;

// Log file path
string mainPath = @"C:\Or\Web\CVSpider\CVSpider\CVSpider\CVSpider\Logs\";
```

## Extending the Application

### Customizing Search

To customize the search:

1. **Cities**: Edit `Code/Cities.cs` to add/remove cities
2. **Professions**: Edit `Code/Professions.cs` to add/remove professions
3. **Mail Types**: Edit `Code/MailTypes.cs` to add/remove mail type patterns
4. **Email Validation**: Edit `Code/TextUtils.cs` to modify validation rules

## How It Works

### Workflow

```
1. Select random city, profession, and mail type
2. Build search query for Walla search
3. Fetch search results (10 pages)
4. Extract URLs from search results
5. For each URL:
   - Fetch page source
   - Extract emails using regex
   - Validate email format
   - Clean email address
   - Check if email exists in database
   - Insert new emails with retry logic
```

### Main Components

1. **Spider.ashx.cs**: Main HTTP handler that orchestrates the workflow
2. **BLL.cs**: Business Logic Layer for email operations
3. **DAL.cs**: Data Access Layer for database operations
4. **TextUtils.cs**: Utility methods for web scraping and email validation
5. **Cities.cs**: List of Israeli cities to search
6. **Professions.cs**: List of job positions to search
7. **MailTypes.cs**: Email type patterns
8. **DbUtilsDal.cs**: Database connection utilities

### Email Processing

1. **Extraction**: Uses regex pattern to find email addresses in HTML
2. **Validation**: Checks format, domain, and common issues
3. **Cleaning**: Fixes common typos and formatting issues
4. **Storage**: Saves unique emails to database with retry logic

## Troubleshooting

### Common Issues

1. **Database connection fails**
   - Verify connection string in Web.config
   - Check SQL Server is running
   - Ensure user has appropriate permissions

2. **Web scraping returns no results**
   - Verify internet connection
   - Check if Walla search is accessible
   - Review search query format

3. **Email validation fails**
   - Check regex pattern in TextUtils.cs
   - Verify email cleaning logic
   - Test with sample email addresses

4. **Duplicate email errors**
   - Check if stored procedure prevents duplicates
   - Verify retry logic in CreateEmail method
   - Review database constraints

### Logs

Configure logging in `Web.config`:

```xml
<system.diagnostics>
  <trace enabled="true" />
</system.diagnostics>
```

Log files are stored in the configured `mainPath` directory.

## Development

### Building

```bash
# Restore NuGet packages
nuget restore CVSpider.csproj

# Build in Debug mode
msbuild CVSpider.csproj /p:Configuration=Debug

# Build in Release mode
msbuild CVSpider.csproj /p:Configuration=Release
```

### Testing

1. Test email validation with edge cases
2. Test database operations with sample data
3. Test web scraping with various URLs
4. Verify retry logic handles failures gracefully

## Notes

- The application requires an internet connection to fetch search results
- Respect robots.txt and rate limits when scraping
- Email addresses are stored without duplicates
- The retry logic attempts up to 10 times for database operations
- Hebrew text (cities, professions) is properly encoded in UTF-8

## Best Practices

- **Security**: Never expose your database connection string in public repositories. Use `Web.config` transformations or environment variables for production.
- **Performance**: Monitor the execution time of `Spider.ashx`. If it becomes too slow, consider moving the scraping logic to a background worker or Windows Service.
- **Data Privacy**: Ensure that the collected emails are handled in accordance with local data protection regulations.
- **Rate Limiting**: Use the randomized delays already implemented in the code to minimize the risk of being IP-blocked by search engines.

## Documentation

- **README.md**: High-level project overview, features, and architecture.
- **INSTRUCTIONS.md**: Detailed setup, usage, and development guidelines.
- **Code Comments**: Inline documentation within `.cs` files explaining complex logic and regex patterns.

## External Resources

- [ASP.NET Web Forms Documentation](https://docs.microsoft.com/en-us/aspnet/web-forms/)
- [.NET Framework 4.5.2 Lifecycle](https://docs.microsoft.com/en-us/lifecycle/products/microsoft-net-framework-452)
- [SQL Server Stored Procedures](https://docs.microsoft.com/en-us/sql/relational-databases/stored-procedures/stored-procedures-database-engine)
- [NuGet Package Manager Documentation](https://docs.microsoft.com/en-us/nuget/)

## Version

**Current Version**: 4.0.0

## Last Updated

**Date**: 2026-06-09

## Author

- **Or Assayag** - _Initial work_ - [orassayag](https://github.com/orassayag)
- Or Assayag <orassayag@gmail.com>
- GitHub: https://github.com/orassayag
- StackOverflow: https://stackoverflow.com/users/4442606/or-assayag?tab=profile
- LinkedIn: https://linkedin.com/in/orassayag
