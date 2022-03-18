using System;

namespace NA_KD.Domain
{
	public class Product
	{
		public readonly String name;
		public readonly String description;
		public readonly String id;

		public Product(String name, String description, String id)
		{
			this.name        = name;
			this.description = description;
			this.id          = id;
		}
	}
}