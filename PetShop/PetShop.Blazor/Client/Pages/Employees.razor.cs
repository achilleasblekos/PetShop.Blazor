using Microsoft.AspNetCore.Components;
using PetShop.Blazor.Shared;
using PetShop.Model;
using System.Net.Http.Json;

namespace PetShop.Blazor.Client.Pages {
	public partial class Employees {
		private string NewItemText { get; set; }
		List<EmployeesViewModel> employees = new(); 
			//{
			//new EmployeesViewModel() { Name ="Takis",Surname="Manageridis",EmployeeType=EmployeeType.Manager, SallaryPerMonth=2000m},
			//new EmployeesViewModel() { Name = "Akis", Surname = "Staffikis", EmployeeType = EmployeeType.Staff, SallaryPerMonth = 450.60m },

		protected override async Task OnInitializedAsync() { // is called when the component is rendered
			await LoadItemsFromServer();
		}

		private async Task LoadItemsFromServer() {
			employees = await httpClient.GetFromJsonAsync<List<EmployeesViewModel>>("employees"); //get the list to route "employees"
		}

		async Task AddItem() {
			if (string.IsNullOrWhiteSpace(NewItemText)) return;

			var newEmployee = new EmployeesViewModel { //create new employee on server
				Name = NewItemText
			};
			//NewItemText = string.Empty;
			NewItemText = null;

			await httpClient.PostAsJsonAsync("employees", newEmployee); // send the new item to "employee route" //id=0?
			await LoadItemsFromServer(); // bring back the list (also gets the newly  created id)
		}

		async Task DeleteItem(EmployeesViewModel itemtoDelete) {
			var response = await httpClient.DeleteAsync($"employees/{itemtoDelete}");
			response.EnsureSuccessStatusCode();
			await LoadItemsFromServer();
		}

		async Task NameChanged(ChangeEventArgs e, EmployeesViewModel itemtoUpdate) {
			itemtoUpdate.Name = e.Value?.ToString();
			UpdateLogic(e, itemtoUpdate);
		}

		async Task SurnameChanged(ChangeEventArgs e, EmployeesViewModel itemtoUpdate) {
			itemtoUpdate.Surname = e.Value?.ToString();
			UpdateLogic(e, itemtoUpdate);
		}

		//async Task TypeChanged(ChangeEventArgs e, EmployeesViewModel itemtoUpdate) { // TODO: update employeeType
		//	itemtoUpdate.EmployeeType = 
		//	UpdateLogic(e, itemtoUpdate);
		//}
		async Task SalaryChanged(ChangeEventArgs e, EmployeesViewModel itemtoUpdate) { // TODO: parse salary
			//itemtoUpdate.SallaryPerMonth = e.Value.;
			UpdateLogic(e, itemtoUpdate);
		}
		async void UpdateLogic(ChangeEventArgs e, EmployeesViewModel itemtoUpdate) {
			var response = await httpClient.PutAsJsonAsync("employees", itemtoUpdate);
			response.EnsureSuccessStatusCode();
			await LoadItemsFromServer();
		}
	}
}
