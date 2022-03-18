using NA_KD.Domain;
using NA_KD.Services;
using Xunit;

namespace TestNA_KD
{
	public class TestCustomerService
	{
		readonly string customerName        = "test";
		readonly string customerDescription = "a test customer";
		readonly string customerId          = "a customerId";

		[Fact]
		public void ShouldBeAbleToCreateCustomer()
		{
			var service = new CustomerService();

			var customer = service.CreateCustomer(customerName, customerDescription, customerId);

			Assert.IsType<Customer>(customer);
		}

		[Fact]
		public void ShouldBeAbleToCreateCustomerWithEmptyWishList()
		{
			var service = new CustomerService();

			var customer = service.CreateCustomer(customerName, customerDescription, customerId);

			Assert.Empty(customer.WishList);
		}
	}
}
