# Migration
##
dotnet ef migrations add

##
Add-Migration AddedPosts



## Added the nuget packages:
Cosmonaut
Cosmonaut.Extensions.Microsoft.DependencyInjection

##
dotnet ef migrations add "Added_UserId_InPosts"


# Login info
* email: nickfury@shield.com
* password: Foobar!0


# Docker
from the solution folder:

docker-compose build
docker-compose up

# Add-Migration not recognized
Add-Migration AddedTags
Add-Migration : The term 'Add-Migration' is not recognized as the name of a cmdlet, function, script file, or operable program. Check the spelling of the name, or if a path was included, verify that the path is correct and try again.

install-package Microsoft.EntityFrameworkCore.Tools

Add-Migration AddedTags
Build started...
Build succeeded.
Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 3.1.9 initialized 'DataContext' using provider 'Microsoft.EntityFrameworkCore.SqlServer' with options: None
To undo this action, use Remove-Migration.


# Integrating with Redis

## Launch Redis
docker run -p 6379:6379 redis

## Consult Redis
* Launch "Another Redis Desktop Manager"
