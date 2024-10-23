using CafeSanchez.POS.Models;
using RestSharp;

namespace CafeSanchez.POS.Services.OrderManagement
{
    public class OrderManagementService(IConfiguration configuration)
    {
        private readonly string _baseUrl = configuration.GetConnectionString("OrderApiUrl") ?? throw new Exception("Baseurl for API not provided"); // API connectionString;
        private readonly string _apiKey = configuration.GetValue<string>("ApiKeys:OrderApi") ?? throw new Exception("No api key provided");

        // Gets list of available products from api
        public IEnumerable<ProductModel> GetProductsList()
        {
            RestClientOptions options = new()
            {
                BaseUrl = new Uri(_baseUrl)
            };

            RestClient client = new(options);

            RestRequest request = new()
            {
                Method = Method.Get,
                Resource = "products"                
            };
            request.AddHeader("Client-Authorization-Key", _apiKey);

            var response = client.Get<ProductModel[]>(request);


            return [.. response];
        }

        // Gets list of active orders from api
        public IEnumerable<OrderModel> GetActiveOrdersList()
        {
            RestClientOptions options = new()
            {
                BaseUrl = new Uri(_baseUrl)
            };

            RestClient client = new(options);

            RestRequest request = new()
            {
                Method = Method.Get,
                Resource = "orders"
            };
            request.AddHeader("Client-Authorization-Key", _apiKey);

            var response = client.Get<OrderModel[]>(request);

            return [.. response];
        }

        public bool CreateOrder(NewOrderModel order)
        {
            throw new NotImplementedException();
        }

        public bool ChangeOrderStatus(Guid orderId, string newOrderStatus)
        {
            throw new NotImplementedException();
        }
    }
}
