using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Security.Cryptography;
using Blazored.LocalStorage;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;

    public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
    {
        _localStorage = localStorage;
        _http = http;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {

		//string token = await _localStorage.GetItemAsStringAsync("token");

		//string token = await _localStorage.GetItemAsStringAsync("token");
		string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVG9ueSBTdGFyayIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Iklyb24gTWFuIiwiZXhwIjozMTY4NTQwMDAwfQ.IbVQa1lNYYOzwso69xYfsMOHnQfO3VLvVqV2SOXS7sTtyyZ8DEf5jmmwz2FGLJJvZnQKZuieHnmHkg7CGkDbvA";
		//string token = tokenTest;//"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJleHAiOjE2ODM0NTYyNTksImNsYWltMSI6MCwiY2xhaW0yIjoiY2xhaW0yLXZhbHVlIn0.cIb2BsRyULGB06pN1rPbr20kHeKwU-nCumiqHtSwgMiSrr187SgXgAwFojNry6Hv0JuwSdbWXUtMCxKl8yC5005NNknqluEzF3a3ntHrTAkK4d_XS2YitKM6KV3OEMeXrw_C7Y3t4PfXSFllJsGVOpBX6Dizmaw5eE9eacGkaabY3nRRFdiPIcGXmTprqgQoq3bxZwFQEg-e9siHpdkq_wEkhs0NQ7WUfBaTeh6id-TjPgCuQtUpYcMvyb7r1CXaJgNjUR-HUn5zwGmkvxN3DbIk3v3R7Y2pjmAEa1ubKhOhWniGG_ICiHcEBabgG0MsBR3NC7FRnYtFE17Ly3Wnbw";

		var identity = new ClaimsIdentity();
        _http.DefaultRequestHeaders.Authorization = null;//todo Http验证

        if (!string.IsNullOrEmpty(token))
        {
            var data = ParseClaimsFromJwt(token);
			identity = new ClaimsIdentity(data, "jwt");
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
	}


	public static IEnumerable<Claim> ParseClaimsFromJwt2(string jwt,RSA certificate,RSA pri)
	{
		var payload = jwt.Split('.')[1];
		var jsonBytes = ParseBase64WithoutPadding(payload);
		var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
		var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
		return claims;
	}

	public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        var claims= keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        return claims;
	}

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}

