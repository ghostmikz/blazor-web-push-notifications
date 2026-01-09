\# 🚀 Blazor SignalNotify

A lightweight .NET demo showing how to send SignalR Live Updates and Web Push Notifications from a background server.

## ⚡ Quick Start

1. Run the Server:
   cd Server && dotnet run

2. Run the Client:
   cd Client && dotnet run

3. Test Notifications:
   - Open the app and go to the server-counter page.
   - Click "Enable Notifications" and allow permissions.
   - SignalR updates the UI live. Every 10 counts, a desktop push notification is sent.

## ✨ Key Features
- SignalR: Real-time UI synchronization.
- Web Push: Desktop alerts (works even if the tab is closed).
- Background Worker: Continuous server-side counting logic.

---

