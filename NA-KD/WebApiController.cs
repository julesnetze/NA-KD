using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NA_KD.Domain;
using NA_KD.Services;
using Newtonsoft.Json;

namespace NA_KD
{
	public class WebApiController
	{
		public HttpClient httpClient;
		private readonly User user;
		public ICustomerService customerService;
		public IProductService productService;
		public string baseUrl = "https://nakdbaseserviceapi20211025120549.azurewebsites.net/api/customer/v1/customers";
		public WebApiController(HttpClient httpClient, User user, ICustomerService customerService, IProductService productService)
		{
			this.httpClient      = httpClient;
			this.user            = user;
			this.customerService = customerService;
			this.productService  = productService;
		}

		public async Task CreateCustomer(string tenantId, string customerName, string customerDescription)
		{
			CustomerPayLoad payload = CreateCustomerPayload(tenantId, customerName, customerDescription);
			StringContent content = CreateContent(payload);

			var response = await httpClient.PostAsync($"{baseUrl}", content);

			if(response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				var responseBodyObject = JsonConvert.DeserializeObject<IdObject>(responseBody);

				response.Dispose();
				content.Dispose();

				var customer = customerService.CreateCustomer(customerName, customerDescription, responseBodyObject.id);
				user.CustomerList.Add(customer);
			} 
			else
			{
				throw new Exception();
			}
		}

		public async Task AddProductToWishList(string customerName, string productName, string productDescription)
		{
			var payload = CreateProductPayload(productName, productDescription);
			StringContent content = CreateContent(payload);

			var customer = user.CustomerList.Find(customer => customer.name == customerName);
			var customerId = customer?.id;
			var response = await httpClient.PostAsync($"{baseUrl}/{customerId}/wishListProducts", content);

			if (response.IsSuccessStatusCode)
			{
				response.Dispose();
				content.Dispose();

				productService.AddProductToWishList(customer, customerId, productName, productDescription, payload.id);
			}
			else
			{
				throw new Exception();
			}
		}

		public async Task RemoveProductFromWishList(string customerName, string productName)
		{
			var customer = user.CustomerList.Find(customer => customer.name == customerName);
			var customerId = customer?.id;
			var productId = customer?.WishList.Find(product => product.name == productName)?.id; 

			var response = await httpClient.DeleteAsync($"{baseUrl}/{customerId}/wishListProducts/{productId}");

			if (response.IsSuccessStatusCode)
			{
				response.Dispose();

				productService.RemoveProductFromWishList(customer, productId);
			}
			else
			{
				throw new Exception();
			}
		}
		
		private StringContent CreateContent(Payload payload)
		{
			var requestBody = JsonConvert.SerializeObject(payload);
			var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
			return content;
		}

		private CustomerPayLoad CreateCustomerPayload(string tenantId, string customerName, string customerDescription)
		{
			return new CustomerPayLoad()
			{
				tenantId    = tenantId,
				name        = customerName,
				description = customerDescription
			};
		}

		private ProductPayload CreateProductPayload(string productName, string productDescription)
		{
			return new ProductPayload()
			{
				id          = Guid.NewGuid().ToString(),
				name        = productName,
				description = productDescription
			};
		}
	}
}
