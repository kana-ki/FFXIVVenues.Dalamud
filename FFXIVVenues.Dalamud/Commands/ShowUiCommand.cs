﻿using Dalamud.Game.Gui;
using Dalamud.Interface;
using FFXIVVenues.Dalamud.Commands.Brokerage;
using FFXIVVenues.Dalamud.UI;
using FFXIVVenues.VenueModels.V2022;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace FFXIVVenues.Dalamud.Commands
{

    [Command("/showui", "Test command while we design a UI")]
    internal class ShowUiCommand : ICommandHandler
    {
        private readonly UiBuilder _uiBuilder;
        private readonly HttpClient _httpClient = new HttpClient();
        Venue[] venues;

        public ShowUiCommand(UiBuilder uiBuilder)
        {
            this._uiBuilder = uiBuilder;
        }

        public async Task GetVenuesArray()
        {
            this.venues = await this._httpClient.GetFromJsonAsync<Venue[]>("https://api.ffxivvenues.com/venue?open=true");
        }

        public Task Handle(string args)
        {
            await GetVenuesArray();
            var newWindow = new VenueDirectoryWindow(this._uiBuilder, venues);
            newWindow.Show();
            
            return Task.CompletedTask;
        }
    }

}