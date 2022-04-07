using PetShop.Blazor.Shared;
using System.Net.Http.Json;

namespace PetShop.Blazor.Client.Pages
{
    public partial class PetFoodList
    {
        List<PetFoodListViewModel> petfoodList = new();
        bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadItemsFromServer();
            isLoading = false;
        }

        private async Task LoadItemsFromServer()
        {
            petfoodList = await httpClient.GetFromJsonAsync<List<PetFoodListViewModel>>("petfood");
        }

        async Task AddItem()
        {
            navigationManager.NavigateTo("/petfoodlist/edit");
        }

        async Task EditItem(PetFoodListViewModel itemToEdit)
        {
            navigationManager.NavigateTo($"/petfoodlist/edit/{itemToEdit.ID}");
        }

        async Task DeleteItem(PetFoodListViewModel itemToDelete) //TODO: douleuei me thn 2i fora
        {
            var confirm = await jsRuntime.InvokeAsync<bool>("confirmDelete", null);
            if (confirm)
            {
                var response = await httpClient.DeleteAsync($"petfood/{itemToDelete.ID}");
                response.EnsureSuccessStatusCode();
                await LoadItemsFromServer();
            }
        }
    }
}
