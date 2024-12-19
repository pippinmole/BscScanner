using System.Threading.Tasks;

namespace BscScanner.Tests;

public static class Helpers
{
    /// <summary>
    /// Because the free tier API key is limited to 5req/s, we have to delay each test to make sure we don't fail due to getting ratelimited
    /// </summary>
    public static async Task AvoidRateLimitAsync()
    {
        await Task.Delay(2000);
    }
}