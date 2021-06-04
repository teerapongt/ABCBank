using System.Threading.Tasks;
using API.Application.Common.Interfaces;
using Microsoft.Playwright;

namespace API.Infrastructure.Services
{
    public class IBANService : IIBAN
    {
        public Task<string> Generate => Random();

        private async Task<string> Random()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();
            await page.GotoAsync("http://randomiban.com/?country=Netherlands");
            return await page.TextContentAsync("p.ibandisplay");
        }
    }
}