using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class GroceryListItemsService : IGroceryListItemsService
    {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly IProductRepository _productRepository;

        public GroceryListItemsService(IGroceryListItemsRepository groceriesRepository, IProductRepository productRepository)
        {
            _groceriesRepository = groceriesRepository;
            _productRepository = productRepository;
        }

        public List<GroceryListItem> GetAll()
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId)
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll().Where(g => g.GroceryListId == groceryListId).ToList();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public GroceryListItem Add(GroceryListItem item)
        {
            return _groceriesRepository.Add(item);
        }

        public GroceryListItem? Delete(GroceryListItem item)
        {
            throw new NotImplementedException();
        }

        public GroceryListItem? Get(int id)
        {
            return _groceriesRepository.Get(id);
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            return _groceriesRepository.Update(item);
        }

        public List<BestSellingProducts> GetBestSellingProducts(int topX = 5)
        {
            //throw new NotImplementedException();
            var bestSellers = _groceriesRepository.GetAll()
                .GroupBy(g => g.ProductId) // groeperen op productId
                .Select(group => new BestSellingProducts(
                    productId: group.Key, // id van een product
                    name: _productRepository.Get(group.Key)?.Name ?? "Onbekend", // naam van een product
                    stock: _productRepository.Get(group.Key)?.Stock ?? 0, // voorraad van een product
                    nrOfSells: group.Sum(g => g.Amount), // totaal aantal verkopen van een product
                    ranking: 0 // rank van een product
                ))
                .OrderByDescending(b => b.NrOfSells) // sorteren op aantal verkopen
                .Take(topX) // top 5 maken
                .ToList(); // lijst maken
            for (int i = 0; i < bestSellers.Count; i++)
                {
                bestSellers[i].Ranking = i + 1;
            }
            return bestSellers;

        }

        private void FillService(List<GroceryListItem> groceryListItems)
        {
            foreach (GroceryListItem g in groceryListItems)
            {
                g.Product = _productRepository.Get(g.ProductId) ?? new(0, "", 0);
            }
        }
    }
}
