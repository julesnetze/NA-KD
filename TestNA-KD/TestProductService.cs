using NA_KD.Domain;
using NA_KD.Services;
using Xunit;

namespace TestNA_KD
{
	public class TestProductService
	{
		readonly string customerName        = "test";
		readonly string customerDescription = "a test customer";
		readonly string customerId          = "a customerId";
		readonly string productName         = "testProduct";
		readonly string productDescription  = "a test product";
		readonly string productId           = "a productId";

		[Fact]
		public void ShouldBeAbleToAddProductToCustomerWishList()
		{
			var customer = new Customer(customerName, customerDescription, customerId);
			var productService = new ProductService();

			productService.AddProductToWishList(customer, customerId, productName, productDescription, productId);

			Assert.Single(customer.WishList);
		}

		[Fact]
		public void ShouldBeAbleToRemoveProductFromCustomerWishList()
		{
			var customer = new Customer(customerName, customerDescription, customerId);
			var productService = new ProductService();
			productService.AddProductToWishList(customer, customerId, productName, productDescription, productId);

			productService.RemoveProductFromWishList(customer, productId);

			Assert.Empty(customer.WishList);
		}
	}
}
