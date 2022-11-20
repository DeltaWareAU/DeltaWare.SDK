# SQL Stored Procedures
This middle-ware is intended to provide a simple and straightforward way of executing stored procedures. The current implementation handles creation of the SQL Command, opening and closing a connection, disposing of instances and mapping the return type.

**Supported Return Type**
*	Scalar
```ExecuteScalarAsync<int>();```
*	Types
```ExecuteAsync<MyType>();```
*	Lists
```ExecuteAsync<List<MyType>>();```


### Executing an SQL Stored Procedure.
**NOTE:** ```SQL Stored Procedures can only be executed when using UseSqlServer, otherwise a MethodAccessException will be thrown.```

There are presently three ways of assigned parameters to an SQL stored procedure.
1.	Anonymous type mapping
2.	Direct type mapping
	**NOTE:**	```Direct type mapping supports property selection.```
4.	SQL Parameters Building

## Anonymous Code Mapping
```csharp
public Task<ReturnType> RunSomethingAsnc(string myParameterA, int myParamaterB, DateTime myParamaterB)
	return _myDbContext
		.RunSqlStoredProcedure("{MyStoredProcedure}")
		.WithParameters(new
		{
		    myParameterA,
		    MYPARAMETERB = myParameterB,
		    MyPARAMETERc = myParameterC
		})
		.ExecuteAsync<ReturnType>();
	}
```
In the above example the following code will map to.
```sql
EXEC [dbo].[MyStoredProcedure]
		@myParameterA = 'Some text',
		@MYPARAMETERB = '42',
		@MyPARAMETERc = '01-01-2012'
```
## Direct Type Mapping
```csharp
public class MyType
{
	public string Title { get; set; }
	public DateTime endDate { get; set;}
}
```
```csharp
public Task<List<ReturnType>> DoSomethingElseAsync(MyType parameterA)
{
	return _myDbContext
		.RunSqlStoredProcedure("{MyOtherStoredProcedure}")
		.WithParameters(parameterA)
		.ExecuteAsync<List<ReturnType>>();
}
```
In the above example the following code will map to.
```sql
EXEC [dbo].[MyOtherStoredProcedure]
		@Title = 'Dr',
		@endDate = '01-01-2012'
```
### Using Property Selection
```csharp
public Task<List<ReturnType>> DoSomethingElseAsync(MyType parameterA)
{
	return _myDbContext
		.RunSqlStoredProcedure("{MyOtherStoredProcedure}")
		.WithParameters(parameterA, p => new { p.Title })
		.ExecuteAsync<List<ReturnType>>();
}
```
In the above example the following code will map to.
```sql
EXEC [dbo].[MyOtherStoredProcedure]
		@Title = 'Dr'
```
## SQL Parameter Builder
```csharp
public Task<ReturnType> RunAnotherThingAsync(string myParameterA, int myParamaterB, DateTime myParamaterB)
	return _myDbContext
		.RunSqlStoredProcedure("{MyStoredProcedure}")
		.WithParameters(b => 
		{
			b.AddParameter("@myParameterA", SqlDbType.VarChar, myParameterA);
			b.AddParameter("@MYPARAMETERB", SqlDbType.Int, myParameterB);
			b.AddParameter("@MyPARAMETERc", SqlDbType.Date, myParameterC);			
		})
		.ExecuteAsync<ReturnType>();
	}
```
In the above example the following code will map to.
```sql
EXEC [dbo].[MyStoredProcedure]
		@myParameterA = 'Some text',
		@MYPARAMETERB = '42',
		@MyPARAMETERc = '01-01-2012'
```
## Stored Procedure Options

### Set Command Timeout
```csharp
.RunSqlStoredProcedure("{MyStoredProcedure}", o => 
{
	o.Timeout = 1472;
})
```