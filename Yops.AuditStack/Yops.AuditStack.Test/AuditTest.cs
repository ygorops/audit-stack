namespace Yops.AuditStack.Test
{
	using System.Linq;
	using NUnit.Framework;
	using System.Collections.Generic;
	using System;

	[TestFixture]
    public class AuditTest
    {
		private static List<Dog> dogsSaved = new List<Dog>();
		private static AuditPersistenceTest auditPersistence = new AuditPersistenceTest();

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
			Assert.AreEqual(myDog, dogsSaved.First());
		}


		internal class Dog : Audit<Dog>
		{
			public string Name { get; set; }
			public string Owner { get; set; }

			public Dog()
				: base(auditPersistence)
			{ }

			public void Save()
			{
				// Save in database


				// Audit
				this.SaveAudit(this);
			}
		}

		internal class AuditPersistenceTest : IAuditPersistense<Dog>
		{
			public void SaveAudit(Dog data)
			{
				dogsSaved.Add(data);
			}
		}
	}
}
