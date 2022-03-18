using System.Collections.Generic;

namespace NA_KD.Domain
{
	public class Customer
	{
		public List<Product> WishList = new();
		public readonly string name;
		public readonly string description;
		public readonly string id;

		public Customer(string name, string description, string id)
		{
			this.name        = name;
			this.description = description;
			this.id			  = id;
		}
	}
}
