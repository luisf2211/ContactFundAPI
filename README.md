# Contact Fund API

## Project Overview
This API manages relationships between Contacts and Funds, implementing a many-to-many relationship with business rules validation. Built with .NET 8, it follows Clean Architecture and Domain-Driven Design principles.

Key Features:
- CRUD operations for Contacts
- Assign/Unassign Contacts to Funds
- JWT Authentication
- Business Rules Validation
- Swagger Documentation

## Architecture and Design

### Clean Architecture Layers
1. **Domain Layer**
   - Core business entities (Contact, Fund, ContactFund)
   - Repository interfaces
   - Business rules validation

2. **Application Layer**
   - Use cases implementation using CQRS with MediatR
   - DTOs and mapping
   - Business logic orchestration

3. **Infrastructure Layer**
   - Entity Framework Core implementation
   - Repository implementations
   - JWT Token service
   - Database context

4. **API Layer**
   - Minimal API controllers
   - Authentication
   - Global error handling
   - Swagger configuration

### Design Patterns Used
- Repository Pattern
- CQRS (using MediatR)
- Dependency Injection
- Unit of Work
- Factory Pattern (for JWT tokens)

## Code Walkthrough

### Key Implementation Details

1. **Business Rules Validation**
   - Contact name is mandatory
   - Prevents duplicate Contact-Fund assignments
   - Prevents deletion of Contacts assigned to Funds

2. **Error Handling**
   - Global middleware for consistent error responses
   - Custom domain exceptions
   - Standardized JSON response format

3. **Authentication**
   - JWT-based authentication
   - Token generation and validation
   - Protected endpoints

## Testing Strategy

### Unit Tests
- Domain entity validation tests
- Business rules validation tests
- Use case handler tests

### Test Coverage
- Core business rules
- Edge cases for Contact-Fund assignments
- Authentication scenarios

### Test Examples
```csharp
[Fact]
public void Contact_WithoutName_ThrowsException()
[Fact]
public void Contact_DuplicateFundAssignment_ThrowsException()
[Fact]
public void Contact_DeleteWithAssignedFunds_NotAllowed()
```

## Challenges and Solutions

1. **Many-to-Many Relationship Management**
   - Challenge: Maintaining data consistency
   - Solution: Implemented validation rules at domain level

2. **Clean Architecture Implementation**
   - Challenge: Proper separation of concerns
   - Solution: Clear boundaries between layers, dependency flow inward

3. **Error Handling**
   - Challenge: Consistent error responses
   - Solution: Global middleware with standardized format

## Future Improvements

1. **Technical Enhancements**
   - Implement caching layer
   - Add pagination for large datasets
   - Implement refresh tokens
   - Add rate limiting

2. **Feature Additions**
   - Bulk operations for Contact-Fund assignments
   - Audit logging
   - Role-based authorization
   - Contact history tracking

3. **Infrastructure**
   - Health checks implementation
   - Monitoring and logging
   - CI/CD pipeline
   - Container orchestration

## Getting Started

1. **Prerequisites**
   - .NET 8 SDK
   - Docker (optional)
   - SQL Server instance

2. **Configuration**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your-server;Database=ContactFundDb;..."
     },
     "JwtSettings": {
       "SecretKey": "your-secret-key",
       "ExpiryMinutes": 60
     }
   }
   ```

3. **Running the Application**
   ```bash
   # Using dotnet CLI
   dotnet run --project src/ContactFundApi.Api

   # Using Docker
   docker-compose up
   ```

4. **API Documentation**
   - Swagger UI: http://localhost:5000/swagger
   - Authentication: Use /api/auth/login with test credentials
