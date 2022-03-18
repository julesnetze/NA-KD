using NA_KD.Domain;

namespace NA_KD.Services
{
	public class ProductService : IProductService
	{
		public void AddProductToWishList(Customer customer, string customerId, string productName, string productDescription, string productId)
		{
			var newProduct = new Product(productName, productDescription, productId);

			customer.WishList.Add(newProduct);
		}

		public void RemoveProductFromWishList(Customer customer, string productId)
		{
			var productToRemove = customer.WishList.Find(product => product.id == productId);
			
			customer.WishList.Remove(productToRemove);
		}
	}
}
