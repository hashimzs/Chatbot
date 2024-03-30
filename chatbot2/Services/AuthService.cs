﻿using Blazored.LocalStorage;
using Chatbot.Application.Shared.ReqDtos;
using System.Net.Http.Json;

namespace chatbot2.Services
{
    public class AuthService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        public AuthService(HttpClient httpClient,ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }

        public async Task Register(RegisterReqDto registerReqDto)
        {
            var response = await this.httpClient.PostAsJsonAsync<RegisterReqDto>("api/auth/register", registerReqDto);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }
        }

        public async Task Login(LoginReqDto loginReqDto)
        {
            var response = await this.httpClient.PostAsJsonAsync<LoginReqDto>("api/auth/login", loginReqDto);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }

            var token = response.Headers.GetValues("token").First();

            await this.localStorageService.SetItemAsync<string>("token",token);

            this.httpClient.DefaultRequestHeaders.Add("token", token);
        }


        public async Task<bool> IsLoggedIn()
        {
            var token = await this.localStorageService.GetItemAsync<string>("token");

            if(string.IsNullOrEmpty(token))
                return false;
            return true;
        }

        public async Task AddDefaultReqHeaders()
        {
            var token = await this.localStorageService.GetItemAsync<string>("token");

            this.httpClient.DefaultRequestHeaders.Add("token", token);
        }
    }
}
