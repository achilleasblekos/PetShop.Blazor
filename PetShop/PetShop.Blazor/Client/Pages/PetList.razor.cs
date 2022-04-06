using Microsoft.AspNetCore.Components;
using PetShop.Blazor.Shared;
using PetShop.Model;
using System.Net.Http;
using System.Net.Http.Json;

namespace PetShop.Blazor.Client.Pages
{
    public partial class PetList 
    {
        private string Breed { get; set; }
        private AnimalType animalType { set; get; }
        private PetStatus status { set; get; }
        private decimal Cost { get; set; }
        private decimal Price { get; set; }
        List<PetListViewModel> petList = new();
        


        protected override async Task OnInitializedAsync()
        {
            await LoadItemsFromServer();
            
        }
        private async Task LoadItemsFromServer()
        {
            petList = await httpClient.GetFromJsonAsync<List<PetListViewModel>>("pet");
            
        }

        async Task AddNewPet()
        {
            //if (string.IsNullOrWhiteSpace(NewPetText)) return;
            var newPet = new PetListViewModel
            {
                Breed = Breed,
                AnimalType = animalType,
                PetStatus= status,
                Cost=Cost,
                Price=Price,
            };
            

            await httpClient.PostAsJsonAsync("pet", newPet);
            await LoadItemsFromServer();
        }


        void DeletePet(PetListViewModel pet)
        {

        }

        //async Task PetStatusChanged(ChangeEventArgs e, PetListViewModel pet)
        //{
        //    pet.PetStatus = 
        //    var response = await httpClient.PutAsJsonAsync("pet", pet);
        //    response.EnsureSuccessStatusCode();
        //}
    }
}
