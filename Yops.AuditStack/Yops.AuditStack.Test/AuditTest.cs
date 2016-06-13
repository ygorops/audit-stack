namespace Yops.AuditStack.Test
{
	using System.Linq;
	using NUnit.Framework;
	using System.Collections.Generic;
	using System;

	[TestFixture]
    public class AuditTest
    {
		private static List<AuditEvidenceCollection> evidencesTest = new List<AuditEvidenceCollection>();
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
			Assert.AreEqual(myDog, evidencesTest.First().First().Data);
		}


		internal class Dog : Audit
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
				this.AddAuditEvidence(GetEvidenceSave());
				this.SaveAudit();
			}

			private AuditEvidence GetEvidenceSave()
			{
				return new AuditEvidence(Guid.NewGuid().ToString(), "userTest", this, "save", DateTime.Now);
			}
		}

		internal class AuditPersistenceTest : IAuditPersistense
		{
			public void SaveAudit(AuditEvidenceCollection evidences)
			{
				evidencesTest.Add(evidences);
			}
		}
	}
}
