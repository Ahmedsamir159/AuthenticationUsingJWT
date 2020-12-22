#to run DB Migration run this command in folder Web.Api.Infrastructure
- <code>Web.Api.Infrastructure>**dotnet ef database update --context AppDbContext**</code>
- <code>Web.Api.Infrastructure>**dotnet ef database update --context AppIdentityDbContext**</code>

The available APIs include:
- POST `/api/accounts` - Creates a new user.
- POST `/api/auth/login` - Authenticates a user.
- POST `/api/auth/refreshtoken` - Refreshes expired access tokens.
- GET `/api/protected` - Protected controller for testing role-based authorization.
# Product 
CRUD Operation on product 
# Contact
ahmedsamir4334@gmail.com
 
