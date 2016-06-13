namespace Yops.AuditStack.Test
{
	using System.Linq;
	using NUnit.Framework;
	using System.Collections.Generic;
	using System;
	using VO;
	using System.Threading;
	using System.Threading.Tasks;

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

		[Test]
		public async Task SaveAuditAsync()
		{
			// Arrange
			Dog myDog = new Dog()
			{
				Name = "Snoop",
				Owner = "Charlie Brown"
			};
			CancellationTokenSource tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(1));

			// Act
			await myDog.SaveAsync(tokenSource.Token);

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

			public async Task SaveAsync(CancellationToken cancellationToken)
			{
				// Validate Owner
				ValidateOwner();
				this.AuditAddEvent("checkOwner", this.Owner, DateTime.Now);

				// Save in database
				SaveSQL();
				this.AuditAddEvent("save", this, DateTime.Now);

				// Audit	
				await SaveAuditAsync("saveOperation", CancellationToken.None);
			}
			
			#region SaveAudit
			private void SaveAudit(string operation)
			{
				SetAuditParameters(operation);
				this.AuditSave();
			}

			private async Task SaveAuditAsync(string operation, CancellationToken cancellationToken)
			{
				SetAuditParameters(operation);
				await this.AuditSaveAsync(cancellationToken);
			}

			private void SetAuditParameters(string operation)
			{
				this.AuditSetId(Guid.NewGuid().ToString());
				this.AuditSetAuthor("customAuthor");
				this.AuditSetOperation(operation);
				this.AuditSetDate(DateTime.Now);
			}
			#endregion

			private void SaveSQL() { }
			private void ValidateOwner() { }
		}

		internal class AuditPersistenceTest : IAuditPersistense
		{
			public AuditVO Get(string id)
			{
				throw new NotImplementedException();
			}

			public Task<AuditVO> GetAsync(string id, CancellationToken cancellationToken)
			{
				throw new NotImplementedException();
			}

			public List<AuditVO> GetByAuthor(string author, int page, int size)
			{
				throw new NotImplementedException();
			}

			public Task<List<AuditVO>> GetByAuthorAsync(string author, int page, int size, CancellationToken cancellationToken)
			{
				throw new NotImplementedException();
			}

			public List<AuditVO> GetByOperation(string operation, int page, int size)
			{
				throw new NotImplementedException();
			}

			public Task<List<AuditVO>> GetByOperationAsync(string operation, int page, int size, CancellationToken cancellationToken)
			{
				throw new NotImplementedException();
			}

			public void SaveAudit(AuditVO auditVO)
			{
				_auditVO = auditVO;
			}

			public async Task SaveAuditAsync(AuditVO auditVO, CancellationToken cancellationToken)
			{
				await Task.Factory.StartNew(() => { _auditVO = auditVO; });
			}
		}
	}
}
