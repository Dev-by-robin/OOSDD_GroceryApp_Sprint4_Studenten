using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        public Client Client { get; set; }


        public ObservableCollection<GroceryList> GroceryLists { get; set; }
        private readonly IGroceryListService _groceryListService;
        private readonly GlobalViewModel _global;


        public GroceryListViewModel(IGroceryListService groceryListService, GlobalViewModel global) 
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            _global = global;
            GroceryLists = new(_groceryListService.GetAll());
        }

        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, paramater);
        }
        [RelayCommand]
        public async Task ShowBoughtProducts()
        {
            //await Toast.Make($"Client id: {_global.Client.Id}").Show();
            if (_global.Client.UserRole == Client.Role.Admin)
            {
                await Shell.Current.GoToAsync(nameof(Views.BoughtProductsView));
            }
            else
            {
                await Toast.Make("Geen Admin").Show();
            }
        }
        public override void OnAppearing()
        {
            base.OnAppearing();
            GroceryLists = new(_groceryListService.GetAll());
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }
    }
}
