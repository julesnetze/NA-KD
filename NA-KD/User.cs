using NA_KD.Domain;
using System.Collections.Generic;

namespace NA_KD
{
	public class User
	{
		public List<Customer> CustomerList = new();
		public readonly string tenantId;

		public User(string tenantId)
		{
			this.tenantId = tenantId;
		}
	}
}