namespace ServerCounterApp.Client.Services; // <--- Check this line

using Microsoft.JSInterop;
using Microsoft.Extensions.Configuration;

public class PushNotificationService(IJSRuntime jsRuntime, IConfiguration config)
{
    public async Task<object> SubscribeUserAsync()
    {
        var publicKey = config["Vapid:PublicKey"];
        return await jsRuntime.InvokeAsync<object>(
            "blazorPushNotifications.requestSubscription", publicKey);
    }
}