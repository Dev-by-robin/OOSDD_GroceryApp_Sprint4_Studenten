
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;
        public BoughtProductsService(IGroceryListItemsRepository groceryListItemsRepository, IGroceryListRepository groceryListRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _groceryListItemsRepository=groceryListItemsRepository;
            _groceryListRepository=groceryListRepository;
            _clientRepository=clientRepository;
            _productRepository=productRepository;
        }
        public List<BoughtProducts> Get(int? productId)
        {
            // Geen productId meegegeven
            if (!productId.HasValue)
            {
                return new List<BoughtProducts>();
            }


            // Alle grocery list items ophalen die overeenkomen met het gegeven productId
            var allGroceryListItems = _groceryListItemsRepository.GetAll()
                .Where(item => item.ProductId == productId.Value)
                .ToList();

            List<BoughtProducts> boughtProducts = new List<BoughtProducts>();

            foreach (GroceryListItem item in allGroceryListItems)
            {
                // Krijg de grocery list waar het item bij hoort
                var groceryList = _groceryListRepository.Get(item.GroceryListId);
                if (groceryList == null) continue;

                // Krijg de client die de grocery list heeft
                var client = _clientRepository.Get(groceryList.ClientId);
                if (client == null) continue;

                // Krijg de productdetails
                var product = _productRepository.Get(productId.Value);
                if (product == null) continue;

                // Maak en voeg een BoughtProducts item toe
                boughtProducts.Add(new BoughtProducts(client, groceryList, product));
            }

            return boughtProducts;
        }
    }
}
