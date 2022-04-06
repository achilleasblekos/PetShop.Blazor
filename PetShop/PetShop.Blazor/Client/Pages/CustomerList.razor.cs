using PetShop.Blazor.Shared;
using System.Net.Http.Json;


namespace PetShop.Blazor.Client.Pages
{
    public partial class CustomerList
    {
        List<CustomerListViewModel> customerList = new();
        bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadItemsFromServer();
            isLoading = false;
        }

        private async Task LoadItemsFromServer()
        {
            customerList = await httpClient.GetFromJsonAsync<List<CustomerListViewModel>>("customer");
        }

        async Task AddItem()
        {
            navigationManager.NavigateTo("/customerlist/edit");
        }

        async Task EditItem(CustomerListViewModel itemToEdit)
        {
            navigationManager.NavigateTo($"/customerlist/edit/{itemToEdit.ID}");
        }

        async Task DeleteItem(CustomerListViewModel itemToDelete) //TODO: douleuei me thn 2i fora
        {
            var confirm = await jsRuntime.InvokeAsync<bool>("confirmDelete", null);
            if (confirm)
            {
                var response = await httpClient.DeleteAsync($"customer/{itemToDelete.ID}");
                response.EnsureSuccessStatusCode();
                await LoadItemsFromServer();
            }
        }
    }
}
