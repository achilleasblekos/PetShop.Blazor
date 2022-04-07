using Microsoft.AspNetCore.Components;
using PetShop.Blazor.Shared;
using PetShop.Model;
using System.Net.Http.Json;

namespace PetShop.Blazor.Client.Pages {
    public partial class EmployeeList {
        private string NewItemText { get; set; }
        List<EmployeeListViewModel> employees = new();
        bool isLoading = true;
        //{
        //new EmployeesViewModel() { Name ="Takis",Surname="Manageridis",EmployeeType=EmployeeType.Manager, SallaryPerMonth=2000m},
        //new EmployeesViewModel() { Name = "Akis", Surname = "Staffikis", EmployeeType = EmployeeType.Staff, SallaryPerMonth = 450.60m },

        protected override async Task OnInitializedAsync() { // is called when the component is rendered
            await LoadItemsFromServer();
            isLoading = false;
        }

        private async Task LoadItemsFromServer() {
            employees = await httpClient.GetFromJsonAsync<List<EmployeeListViewModel>>("employee"); //get the list to route "employees"
        }

        async Task AddItem() {
            //if (string.IsNullOrWhiteSpace(NewItemText)) return;

            //var newEmployee = new EmployeeListViewModel { //create new employee on server
            //    Name = NewItemText
            //};
            ////NewItemText = string.Empty;
            //NewItemText = null;

            //await httpClient.PostAsJsonAsync("employees", newEmployee); // send the new item to "employee route" //id=0?
            //await LoadItemsFromServer(); // bring back the list (also gets the newly  created id)
            navigationManager.NavigateTo("employeelist/edit");
        }
        async Task EditItem(EmployeeListViewModel itemToEdit) {
            navigationManager.NavigateTo($"/employeelist/edit/{itemToEdit.ID}");
        }

        async Task DeleteItem(EmployeeListViewModel itemToDelete) {
            var confirm = await jsRuntime.InvokeAsync<bool>("confirmDelete", null);
            if (confirm) {
                var response = await httpClient.DeleteAsync($"employee/{itemToDelete.ID}");
                response.EnsureSuccessStatusCode();
                await LoadItemsFromServer(); //works with refresh
            }
        }

        //async Task NameChanged(ChangeEventArgs e, EmployeeListViewModel itemtoUpdate) {
        //    itemtoUpdate.Name = e.Value?.ToString();
        //    UpdateLogic(e, itemtoUpdate);
        //}

        //async Task SurnameChanged(ChangeEventArgs e, EmployeeListViewModel itemtoUpdate) {
        //    itemtoUpdate.Surname = e.Value?.ToString();
        //    UpdateLogic(e, itemtoUpdate);
        //}

        //async Task TypeChanged(ChangeEventArgs e, EmployeeListViewModel itemtoUpdate) { // TODO: update employeeType
        //	itemtoUpdate.EmployeeType = 
        //	//UpdateLogic(e, itemtoUpdate);
        //}
        //async Task SalaryChanged(ChangeEventArgs e, EmployeeListViewModel itemtoUpdate) { // TODO: parse salary
        //                                                                                  //itemtoUpdate.SallaryPerMonth = e.Value.;
        //    UpdateLogic(e, itemtoUpdate);
        //}
        //async void UpdateLogic(ChangeEventArgs e, EmployeeListViewModel itemtoUpdate) {
        //    var response = await httpClient.PutAsJsonAsync("employees", itemtoUpdate);
        //    response.EnsureSuccessStatusCode();
        //    await LoadItemsFromServer();
        //}
    }
}

