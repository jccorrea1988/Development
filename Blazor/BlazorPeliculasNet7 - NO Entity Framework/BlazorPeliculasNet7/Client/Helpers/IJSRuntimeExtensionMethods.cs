using BlazorPeliculasNet7.Shared.Entidades;
using Microsoft.JSInterop;

namespace BlazorPeliculasNet7.Client.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask<bool> Confirm(this IJSRuntime js, string mensaje) 
        {
            await js.InvokeVoidAsync("console.log", "Prueba de console log");
            return await js.InvokeAsync<bool>("confirm", mensaje);
        }
    }
}
