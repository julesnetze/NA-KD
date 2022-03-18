using NA_KD.Domain;

namespace NA_KD.Services
{
	public interface ICustomerService
	{
		public Customer CreateCustomer(string name, string description, string id);
	}
}
