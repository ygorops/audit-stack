# audit-stack

### Install
To install Yops.AuditStack, run the following command in the Package Manager Console

```
Install-Package Yops.AuditStack
```

For more information... <a href="https://www.nuget.org/packages/Yops.AuditStack/" taget="_blank">NuGet Galley</a>

### How to use
First, implement the storage using the IAuditStackPersistence inteface. Then, set this class in AuditCore.

```csharp
AuditCore.ConfigureAuditPersistence(new MyCustomPersistence());
```

Extend your class with Audit class. The default constructor in Audit class initialize an AuditVO object.
```csharp
public class Dog : Audit
{
}
```

The AuditVO contains a list of events called AuditEventVOCollection. You can add events before persist the audit.
```csharp
public async Task SaveAsync(CancellationToken cancellationToken)
{
    try
    {
      // Validate Owner
      ValidateOwner();
      this.AuditAddEvent("checkOwner", this.Owner, DateTime.Now); // Adding event
      ValidateDog();
      this.AuditAddEvent("validate dog", this.Owner, DateTime.Now); // Adding event
  
      // Save in database
      SaveSQL();
      this.AuditAddEvent("save in sql database", this, DateTime.Now); // Adding event
    }
    finally
    {
      // Save audit
      this.AuditSetId(Guid.NewGuid().ToString());
      this.AuditSetAuthor("author_xpto");
      this.AuditSetOperation("save a dog");
      this.AuditSetDate(DateTime.Now);
      await this.AuditSaveAsync(cancellationToken); // Saving the audit
    }
}
```




