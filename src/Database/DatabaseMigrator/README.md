You can manually build and run the database migrations console app if you want to test out migrations during development by running the exe with the dev command. 
It will start an interactive mode where you can perform migrations and test rollbacks.

``` .\DatabaseMigrator.exe dev --server-connection-string="Server=host:port;User Id=my_secure_user_id;Password=my_secure_password;" ```

## Commands

- **dev**

  Start an interactive session to manually control migrations and rollbacks for development of new migrations

- **migrate**

  Run all migrations

## Options

- **-s, --server-connection-string**

  Required. The connection to the database server.