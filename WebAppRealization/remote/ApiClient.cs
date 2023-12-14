using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAppIImpl.remote;
using WebAppIImpl.remote.models;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient()
    {
        _httpClient = new HttpClient();
        
        _httpClient.BaseAddress = new Uri("https://localhost:7276/api/");
    }

    public async Task<string?>? LoginUserAsync(string username, string password)
    {
       
        var authData = new { Username = username, Password = password };

        var json = JsonConvert.SerializeObject(authData);
        
        var response = await _httpClient.PostAsync("authentication/login", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<TokenModel>(jsonResult);
            return authResult?.Token;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<string?> RegistrationUserAsync(RegistrationModel registrationModel)
    {
        var json = JsonConvert.SerializeObject(registrationModel);
        
        var response = await _httpClient.PostAsync("authentication", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<TokenModel>(jsonResult);
            return authResult?.Token;
        }
        else
        {
            return null;
        }
    }

    public async Task<ObservableCollection<DriverModel>?> GetDriversAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        HttpResponseMessage response = await _httpClient.GetAsync("drivers");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ObservableCollection<DriverModel>>(json);
            return items;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<ObservableCollection<CompanyModel>?> GetCompaniesAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        HttpResponseMessage response = await _httpClient.GetAsync("companies");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ObservableCollection<CompanyModel>>(json);
            return items;
        }
        else
        {
            return null;
        }
    }

    public async Task<ObservableCollection<CarModel>?> GetCarsByDriverAsync(Guid id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        HttpResponseMessage response = await _httpClient.GetAsync($"drivers/{id}/cars");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ObservableCollection<CarModel>>(json);
            return items;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<ObservableCollection<EmployeeModel>?> GetEmployeesByCompanyAsync(Guid id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        HttpResponseMessage response = await _httpClient.GetAsync($"companies/{id}/employees");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ObservableCollection<EmployeeModel>>(json);
            return items;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<DriverModel?> PostCreateDriverAsync(DriverCreationModel driverCreationModel)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        var json = JsonConvert.SerializeObject(driverCreationModel);
        
        HttpResponseMessage response = await _httpClient.PostAsync($"drivers", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<DriverModel>(jsonResult);
            return authResult;
        }

        return null;
    }

    public async Task<CarModel?> PostCarForDriverAsync(CarCreationModel carCreationModel, Guid driverId)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);

        var json = JsonConvert.SerializeObject(carCreationModel);

        HttpResponseMessage response = await _httpClient.PostAsync($"drivers/{driverId}/cars", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<CarModel>(jsonResult);
            return authResult;
        }

        return null;
    }

    public async Task DeleteDriverdAsync(Guid id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);

        await _httpClient.DeleteAsync($"drivers/{id}");
    }
    
    public async Task<DriverModel?> PutUpdateDriverdAsync(DriverCreationModel carBrandCreationModel, Guid id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        var json = JsonConvert.SerializeObject(carBrandCreationModel);
        
        HttpResponseMessage response = await _httpClient.PutAsync($"drivers/{id}", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<DriverModel>(jsonResult);
            return authResult;
        }

        return null;
    }
    
    public async Task DeleteCarAsync(Guid id, Guid driverId)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);

        await _httpClient.DeleteAsync($"drivers/{driverId}/cars/{id}");
    }
}