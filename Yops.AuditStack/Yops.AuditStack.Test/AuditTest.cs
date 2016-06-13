namespace Yops.AuditStack.Test
{
	using NUnit.Framework;
	using System.Collections.Generic;
	using System;
	using System.Linq;
	using VO;
	using System.Threading;
	using System.Threading.Tasks;

	[TestFixture]
	public class AuditTest
	{
		private static List<AuditVO> _audits = new List<AuditVO>();
		private const string AUTHOR_VALUE = "testAuthor";

		[OneTimeSetUp]
		public void SetUp()
		{
			// Set up IAuditPersistence
			AuditCore.ConfigureAuditPersistence(new AuditPersistenceTest());
		}

		[Test]
		[Order(0)]
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
			Assert.AreEqual(myDog, _audits.First().Events[1].Data);
		}

		[Test]
		[Order(1)]
		public async Task SaveAuditAsync()
		{
			// Arrange
			Dog myDog = new Dog()
			{
				Name = "Snoop",
				Owner = "Charlie Brown"
			};
			CancellationTokenSource tokenSource = new CancellationTokenSource(100);

			// Act
			await myDog.SaveAsync(tokenSource.Token);

			// Assert
			Assert.AreEqual(myDog, _audits[1].Events[1].Data);
		}

		[Test]
		[Order(2)]
		public void GetAuditById()
		{
			// Arrange
			string id = _audits[0].Id;

			// Act
			AuditVO vo = Dog.AuditGet(id);

			// Assert
			Assert.AreEqual(vo, _audits[0]);
		}

		[Test]
		[Order(3)]
		public async Task GetAuditByIdAsync()
		{
			// Arrange
			string id = _audits[0].Id;
			CancellationTokenSource token = new CancellationTokenSource(100);

			// Act
			AuditVO vo = await Dog.AuditGetAsync(id, token.Token);

			// Assert
			Assert.AreEqual(vo, _audits[0]);
		}

		[Test]
		[Order(4)]
		public void GetAuditByAuthor()
		{
			// Arrange
			string author = "testAuthor";
			int page = 1;
			int size = 2;

			// Act
			List<AuditVO> auditList = Dog.AuditGetByAuthor(author, page, size);

			// Assert
			Assert.IsNotNull(auditList);
			Assert.AreEqual(auditList[0].Author, author);
			Assert.AreEqual(auditList[1].Author, author);
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
				this.AuditSetAuthor(AUTHOR_VALUE);
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
				return _audits.Find(a => a.Id.Equals(id));
			}

			public Task<AuditVO> GetAsync(string id, CancellationToken cancellationToken)
			{
				return Task.Factory.StartNew<AuditVO>(() =>
				{
					return _audits.Find(a => a.Id.Equals(id));
				});
			}

			public List<AuditVO> GetByAuthor(string author, int page, int size)
			{
				if (page < 1)
					throw new ArgumentOutOfRangeException("page", page, "Value cannot be less than one.");
				
				return new List<AuditVO>(_audits.Where(a => a.Author.Equals(author)).Skip((page - 1) * size).Take(size));
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
				_audits.Add(auditVO);
			}

			public async Task SaveAuditAsync(AuditVO auditVO, CancellationToken cancellationToken)
			{
				await Task.Factory.StartNew(() => { _audits.Add(auditVO); });
			}
		}
	}
}
