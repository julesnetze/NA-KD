using NA_KD;
using NA_KD.Domain;
using NA_KD.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TestNA_KD
{
	public class TestWebApiController
	{
		readonly string customerName        = "test";
		readonly string customerDescription = "a test customer";
		readonly string productName         = "test product";
		readonly string productDescription  = "a test product";
		readonly string inexistentUser      = "not existing";
		readonly string inexistentCustomer  = "not existing";
		readonly string inexistentProduct   = "not existing";

		readonly ProductService productService = new();
		readonly CustomerService customerService = new();
		private User user;

		public TestWebApiController()
		{
			user = new("4ea5c5c2-b71b-4b0f-8229-2d3a5b8856ef");
		}

		[Fact]
		public async Task ShouldThrowExceptionGivenInexistentUser()
		{
			HttpClient client = new();
			var controller = new WebApiController(client, user, customerService, productService);

			await Assert.ThrowsAsync<Exception>(async () => await controller.CreateCustomer(inexistentUser, customerName, customerDescription));
			client.Dispose();
		}

		[Fact]
		public async Task ShouldThrowExceptionGivenInexistentCustomer()
		{
			HttpClient client = new();
			var controller = new WebApiController(client, user, customerService, productService);

			await Assert.ThrowsAsync<Exception>(async () => await controller.AddProductToWishList(inexistentCustomer, productName, productDescription));
			client.Dispose();
		}

		[Fact]
		public async Task ShouldThrowExceptionGivenInexistentCustomerAndInexistentProduct()
		{
			HttpClient client = new();
			var controller = new WebApiController(client, user, customerService, productService);

			await Assert.ThrowsAsync<Exception>(async () => await controller.RemoveProductFromWishList(inexistentCustomer, inexistentProduct));
			client.Dispose();
		}
	}
}
