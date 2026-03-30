# Contributing

Contributions to this project are [released](https://help.github.com/articles/github-terms-of-service/#6-contributions-under-repository-license) to the public under the [project's open source license](LICENSE).

Everyone is welcome to contribute to this project. Contributing doesn't just mean submitting pull requests—there are many different ways for you to get involved, including answering questions, reporting issues, improving documentation, or suggesting new features.

## How to Contribute

### Reporting Issues

If you find a bug or have a feature request:
1. Check if the issue already exists in the [GitHub Issues](https://github.com/orassayag/cv-spider-v4/issues)
2. If not, create a new issue with:
   - Clear title and description
   - Steps to reproduce (for bugs)
   - Expected vs actual behavior
   - Error messages or stack traces (if applicable)
   - Your environment details (OS, .NET version, SQL Server version)

### Submitting Pull Requests

1. Fork the repository
2. Create a new branch for your feature/fix:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Make your changes following the code style guidelines below
4. Test your changes thoroughly
5. Commit with clear, descriptive messages
6. Push to your fork and submit a pull request

### Code Style Guidelines

This project uses:
- **C#** with .NET Framework 4.5.2
- **ASP.NET Web Forms** for the web framework
- **SQL Server** with Stored Procedures for data access

Before submitting:
1. Build the solution in Visual Studio to ensure no compilation errors
2. Test the web scraping functionality with sample data
3. Verify database stored procedures work correctly
4. Check that email validation logic handles edge cases

### Coding Standards

1. **Naming conventions**: Follow C# naming conventions (PascalCase for classes and methods, camelCase for local variables)
2. **Error handling**: Use try-catch blocks appropriately and handle exceptions gracefully
3. **Database access**: Always use parameterized queries to prevent SQL injection
4. **Code organization**: Keep related functionality in separate classes (BLL, DAL, Utils)
5. **Comments**: Add XML documentation comments for public methods
6. **Resource disposal**: Always dispose of resources (SqlConnection, WebClient) using `using` statements

### Adding New Features

When adding new features:
1. Update the appropriate layer (DAL for database, BLL for business logic, Utils for utilities)
2. Add corresponding stored procedures in SQL Server if needed
3. Update the `Spider.ashx.cs` handler if the feature affects the main workflow
4. Test thoroughly with real-world scenarios
5. Document any new configuration required in `Web.config`

### Database Changes

When modifying database logic:
1. Create or update stored procedures in SQL Server
2. Update the DAL layer to call the new/modified stored procedures
3. Ensure proper parameter types and validation
4. Test with various input scenarios
5. Document the changes in pull request description

## Security Considerations

This project handles email addresses and performs web scraping. Please ensure:
1. Email addresses are properly validated and sanitized
2. SQL injection is prevented through parameterized queries
3. Web scraping respects robots.txt and rate limits
4. Sensitive data is not logged or exposed
5. Connection strings are properly secured in Web.config

## Questions or Need Help?

Please feel free to contact me with any question, comment, pull-request, issue, or any other thing you have in mind.

* Or Assayag <orassayag@gmail.com>
* GitHub: https://github.com/orassayag
* StackOverflow: https://stackoverflow.com/users/4442606/or-assayag?tab=profile
* LinkedIn: https://linkedin.com/in/orassayag

Thank you for contributing! 🙏
