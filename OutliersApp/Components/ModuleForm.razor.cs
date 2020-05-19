using Microsoft.AspNetCore.Components;
using OutliersApp.Models;

namespace OutliersApp.Components
{
    public partial class ModuleForm
    {
        [Parameter] public bool HasWeight { get; set; }
        [Parameter] public EventCallback<ModuleFormModel> OnDelete { get; set; }
        [Parameter] public string Id { get; set; }

        [Parameter] public ModuleFormModel Model { get; set; }

        //TODO:
        //Сделать анимации появления на переключении внутренний/внешний

        public void EnableName(ChangeEventArgs args)
        {
            Model.IsInternal = true;
            //IJSRuntime.InvokeVoidAsync("myCollapse", $"choose_{Id}", "show");
        }

        public void DisableName(ChangeEventArgs args)
        {
            Model.IsInternal = false;
            //IJSRuntime.InvokeVoidAsync("myCollapse", $"choose_{Id}", "hide");
        }


    }
}