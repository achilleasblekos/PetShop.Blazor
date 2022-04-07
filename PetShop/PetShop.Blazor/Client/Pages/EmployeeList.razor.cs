using Microsoft.AspNetCore.Components;
using PetShop.Blazor.Shared;
using PetShop.Model;
using System.Net.Http.Json;

namespace PetShop.Blazor.Client.Pages {
    public partial class EmployeeList {
        private string NewItemText { get; set; }
        List<EmployeeListViewModel> employeeList = new();
        bool isLoading = true;
       

        protected override async Task OnInitializedAsync() { // is called when the component is rendered
            await LoadItemsFromServer();
            isLoading = false;
        }

        private async Task LoadItemsFromServer() {
            employeeList = await httpClient.GetFromJsonAsync<List<EmployeeListViewModel>>("employee"); //get the list to route "employees"
        }

        async Task AddItem() {
            
            navigationManager.NavigateTo("employeeList/edit");
        }
        async Task EditItem(EmployeeListViewModel itemToEdit) {
            navigationManager.NavigateTo($"/employeeList/edit/{itemToEdit.ID}");
        }

        async Task DeleteItem(EmployeeListViewModel itemToDelete)
        {
            var confirm = await jsRuntime.InvokeAsync<bool>("confirmDelete", null);
            if (confirm)
            {
                var response = await httpClient.DeleteAsync($"employee/{itemToDelete.ID}");
                response.EnsureSuccessStatusCode();
                await LoadItemsFromServer(); //works with refresh
            }
        }
    }
}

