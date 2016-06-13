namespace Yops.AuditStack.Test
{
	using System.Linq;
	using NUnit.Framework;
	using System.Collections.Generic;
	using System;
	using VO;

	[TestFixture]
	public class AuditTest
	{
		private static AuditVO _auditVO = null;

		[OneTimeSetUp]
		public void SetUp()
		{
			// Set up IAuditPersistence
			AuditCore.ConfigureAuditPersistence(new AuditPersistenceTest());
		}

		[Test]
		public void SaveAudit()
		{
			// Arrange
			Dog myDog = new Dog()
			{
				Name = "Snoop",
				Owner = "Charlie Brown"
			};

			// Act
			myDog.Save();

			// Assert
			Assert.AreEqual(myDog, _auditVO.Events[1].Data);
		}


		public class Dog : Audit
		{
			public string Name { get; set; }
			public string Owner { get; set; }

			public void Save()
			{
				// Validate Owner
				ValidateOwner();
				this.AuditAddEvent("checkOwner", this.Owner, DateTime.Now);

				// Save in database
				SaveSQL();
				this.AuditAddEvent("save", this, DateTime.Now);

				// Audit	
				SaveAudit("saveOperation");
			}

			private void SaveAudit(string operation)
			{
				this.AuditSetId(Guid.NewGuid().ToString());
				this.AuditSetAuthor("customAuthor");
				this.AuditSetOperation(operation);
				this.AuditSetDate(DateTime.Now);

				this.AuditSave();
			}

			private void SaveSQL() { }
			private void ValidateOwner() { }
		}

		internal class AuditPersistenceTest : IAuditPersistense
		{
			public void SaveAudit(AuditVO auditVO)
			{
				_auditVO = auditVO;
			}
		}
	}
}
