using NA_KD.Domain;

namespace NA_KD.Services
{
	public interface IProductService
	{
		public void AddProductToWishList(Customer customer, string customerName, string productName, string productDescription, string productId);
		public void RemoveProductFromWishList(Customer customer, string productId);
	}
}
