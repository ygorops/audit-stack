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

### License

The MIT License (MIT)
Copyright (c) 2016

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


