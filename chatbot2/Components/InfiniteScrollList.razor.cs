
using Chatbot.Application.Shared;
using Chatbot.Application.Shared.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace chatbot2.Components
{
    public partial class InfiniteScrollList<TValue>
    {
        [Inject]
        public HttpClient httpClient { get; set; } = null!;
        [Parameter]
        public string Url { get; set; } = string.Empty;
        private string PrevUrl { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment<TValue> ChildContent { get; set; }
        [Parameter]
        public RenderFragment LoadingTemplate { get; set; }
        [Parameter]
        public List<TValue> Items { get; set; } = new List<TValue>();
        [Parameter]
        public EventCallback OnNoMacthingItems { get; set; }
        [Parameter]
        public EventCallback OnFirstLoad { get; set; }

        public bool IsLoading { get; set; }
        public PaginationParams paginationParams { get; set; } = new PaginationParams();
        public MetaData? metaData { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            if (PrevUrl == Url) return;

            PrevUrl = Url;
            paginationParams.PageNumber = 1;
            Items.Clear();
            await GetItems();

            if (OnFirstLoad.HasDelegate)
                await OnFirstLoad.InvokeAsync();
        }

        public async Task GetNextSet()
        {
            if (metaData.HasNext)
            {
                paginationParams.PageNumber++;
                await GetItems();
            }

        }
        public async Task GetItems()
        {
            if (string.IsNullOrEmpty(Url)) return;
            IsLoading = true;
            var UpdatedUrl = Url + (Url.Contains("?") ? "&" : "?") + $"PageNumber={paginationParams.PageNumber}&PageSize={paginationParams.PageSize}";

            var response = await httpClient.GetAsync(UpdatedUrl);

            if (!response.IsSuccessStatusCode)
                return;

            var result = await response.Content.ReadFromJsonAsync<PagedList<TValue>>();

            Items.AddRange(result.List);

            metaData = result.MetaData;
            IsLoading = false;
        }
        public bool IsLastItem(Enumeration<TValue> item)
        {
            return item.index == (Items.Count() - 1);
        }
        public void AddItem(TValue item)
        {
            Items.Add(item);
            StateHasChanged();
        }

        public void RemoveItem(TValue item)
        {
            Items.Remove(item);
            StateHasChanged();
        }
    }
}
