using System.Collections.Generic;
using OutliersApp.Data;
using OutliersLib;
using Microsoft.AspNetCore.Components;



namespace OutliersApp.Pages
{
    public partial class ModuleFormPage
    {
        [Parameter] public string Id { get; set; }

        [Parameter] public ModuleForm ModuleForm { get; set; }

        //TODO:
        //Сделать анимации появления на переключении внутренний/внешний
        
        public void EnableName(ChangeEventArgs args)
        {
            ModuleForm.IsInternal = true;
            //IJSRuntime.InvokeVoidAsync("myCollapse", $"choose_{Id}", "show");
        }

        public void DisableName(ChangeEventArgs args)
        {
            ModuleForm.IsInternal = false;
            //IJSRuntime.InvokeVoidAsync("myCollapse", $"choose_{Id}", "hide");
        }
    }
}