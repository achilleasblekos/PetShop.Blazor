using Microsoft.AspNetCore.Components;
using PetShop.Blazor.Shared;
using PetShop.Model;
using System.Net.Http;
using System.Net.Http.Json;

namespace PetShop.Blazor.Client.Pages
{
    public partial class PetList 
    {
        List<PetListViewModel> petList = new();
        bool isLoading = true;



        protected override async Task OnInitializedAsync()
        {
            await LoadItemsFromServer();
            isLoading = false;

        }
        private async Task LoadItemsFromServer()
        {
            petList = await httpClient.GetFromJsonAsync<List<PetListViewModel>>("pet");
            
        }

        async Task AddNewPet()
        {
            navigationManager.NavigateTo("/petList/edit/");
        }
        async Task EditItem(PetListViewModel itemToEdit)
        {
            navigationManager.NavigateTo($"/petlist/edit/{itemToEdit.ID}");
        }
        async Task DeletePet(PetListViewModel itemToDelete)
        {
            var confirm = await jsRuntime.InvokeAsync<bool>("confirmDelete", null);
            if (confirm)
            {
                var response = await httpClient.DeleteAsync($"pet/{itemToDelete.ID}");
                response.EnsureSuccessStatusCode();
                await LoadItemsFromServer();
            }
        }
    }
}
