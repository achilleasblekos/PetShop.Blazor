using PetShop.Blazor.Shared;
using System.Net.Http.Json;

namespace PetShop.Blazor.Client.Pages
{
    public partial class TransactionList
    {
        List<TransactionListViewModel> transactionList = new();
        bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadItemsFromServer();
            isLoading = false;
        }

        private async Task LoadItemsFromServer()
        {
            transactionList = await httpClient.GetFromJsonAsync<List<TransactionListViewModel>>("transaction");
        }

        async Task AddItem()
        {
            navigationManager.NavigateTo("/transactionList/edit");
        }

        async Task EditItem(TransactionListViewModel itemToEdit)
        {
            navigationManager.NavigateTo($"/transactionList/edit/{itemToEdit.ID}");//prosthesa ID sto transactionListViewModel
        }

        async Task DeleteItem(TransactionListViewModel itemToDelete)
        {
            var confirm = await jsRuntime.InvokeAsync<bool>("confirmDelete", null);
            if (confirm)
            {
                var response = await httpClient.DeleteAsync($"transaction/{itemToDelete.ID}");
                response.EnsureSuccessStatusCode();
                await LoadItemsFromServer();
            }
        }
    }
}
