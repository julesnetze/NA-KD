using NA_KD.Services;
using System;
using System.Net.Http;

namespace NA_KD
{
	class Program
	{
		static void Main(string[] args)
		{
			User user = new("d290f1ee-6c54-4b01-90e6-d701748f0851");
			HttpClient client = new();
			WebApiController controller = new(client, user, new CustomerService(), new ProductService());

			Console.WriteLine("Welcome!");
			int choice = 0;
			while (choice < 4)
			{
				Console.WriteLine("What would like you to do?");
				Console.WriteLine("1: Create customer");
				Console.WriteLine("2: Add product to wishlist");
				Console.WriteLine("3: remove product from wishlist");
				Console.WriteLine("4: exit");
				choice = Convert.ToInt32(Console.ReadLine());

				if (choice == 1)
				{
					ExecuteCreateCustomer(user, controller);
				}
				else if (choice == 2)
				{
					ExecuteAddProductToWishList(controller);
				}
				else if (choice == 3)
				{
					ExecuteRemoveProductFromWishList(controller);
				}
			}
			client.Dispose();
		}

		private static void ExecuteRemoveProductFromWishList(WebApiController controller)
		{
			string customerName;
			string productName;
			Console.WriteLine("Give an existing customer name:");
			customerName = Convert.ToString((Console.ReadLine()));
			Console.WriteLine("Give an existing product name:");
			productName = Convert.ToString((Console.ReadLine()));

			try
			{
				controller.RemoveProductFromWishList(customerName, productName).Wait();
				Console.WriteLine("Product succesfully removed from wishlist");
			}
			catch (Exception)
			{
				Console.WriteLine("Inexistent customer and/or inexistent product");
			}
		}

		private static void ExecuteAddProductToWishList(WebApiController controller)
		{
			string customerName;
			string productName;
			string productDescription;
			Console.WriteLine("Give an existing customer name:");
			customerName = Convert.ToString((Console.ReadLine()));
			Console.WriteLine("Give a product name:");
			productName = Convert.ToString((Console.ReadLine()));
			Console.WriteLine("Give a product description:");
			productDescription = Convert.ToString((Console.ReadLine()));

			try
			{
				controller.AddProductToWishList(customerName, productName, productDescription).Wait();
				Console.WriteLine("Product succesfully added to wishlist");
			}
			catch (Exception)
			{
				Console.WriteLine("Inexistent customer");
			}
		}

		private static void ExecuteCreateCustomer(User user, WebApiController controller)
		{
			string customerName;
			string customerDescription;
			Console.WriteLine("Give a customer name:");
			customerName = Convert.ToString((Console.ReadLine()));
			Console.WriteLine("Give a customer description:");
			customerDescription = Convert.ToString((Console.ReadLine()));

			try
			{
				controller.CreateCustomer(user.tenantId, customerName, customerDescription).Wait();
				Console.WriteLine("Customer succesfully created");
			}
			catch (Exception)
			{
				Console.WriteLine("Inexistent user");
			}
		}
	}
}
