using NA_KD.Domain;

namespace NA_KD.Services
{
	public class CustomerService : ICustomerService
	{
		public Customer CreateCustomer(string name, string description, string id)
		{
			return new Customer(name, description, id);
		}
	}
}
