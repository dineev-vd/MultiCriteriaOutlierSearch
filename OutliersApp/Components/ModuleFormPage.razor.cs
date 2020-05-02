using Microsoft.AspNetCore.Components;
using OutliersApp.Models;

namespace OutliersApp.Components
{
    public partial class ModuleFormPage
    {
        [Parameter] public bool HasWeight { get; set; }
        [Parameter] public EventCallback<ModuleFormModel> OnDelete { get; set; }
        [Parameter] public string Id { get; set; }

        [Parameter] public ModuleFormModel ModuleFormModel { get; set; }

        //TODO:
        //Сделать анимации появления на переключении внутренний/внешний

        public void EnableName(ChangeEventArgs args)
        {
            ModuleFormModel.IsInternal = true;
            //IJSRuntime.InvokeVoidAsync("myCollapse", $"choose_{Id}", "show");
        }

        public void DisableName(ChangeEventArgs args)
        {
            ModuleFormModel.IsInternal = false;
            //IJSRuntime.InvokeVoidAsync("myCollapse", $"choose_{Id}", "hide");
        }


    }
}