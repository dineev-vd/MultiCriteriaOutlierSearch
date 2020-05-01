using System.Collections.Generic;
using OutliersLib;

namespace OutliersApp.Models
{
    public class Form
    {
        public List<ModuleFormModel> Algorithms { get; set; }
        public List<ModuleFormModel> Combinations { get; set; }
        public double[,] Values { get; set; }
        
        public string ValuesString { get; set; }

        public Form()
        {
            Algorithms = new List<ModuleFormModel>();
            Combinations = new List<ModuleFormModel>();
            Values = new double[0,0];
        }

        public bool Check()
        {
            foreach (var item in Algorithms)
            {
                if (item.IsInternal)
                {
                    foreach (var setting in item.Internal.Selected.Settings)
                    {
                        if (!setting.IsValid) return false;
                    }
                }
                else
                {
                    foreach (var setting in item.External.Settings)
                    {
                        if (!setting.IsValid) return false;
                    }
                }
            }
            
            foreach (var item in Combinations)
            {
                if (item.IsInternal)
                {
                    foreach (var setting in item.Internal.Selected.Settings)
                    {
                        if (!setting.IsValid) return false;
                    }
                }
                else
                {
                    foreach (var setting in item.External.Settings)
                    {
                        if (!setting.IsValid) return false;
                    }
                }
            }

            return true;
        }
        
        public OutlierRequestData ToRequestData()
        {
            var requestData = new OutlierRequestData();
            foreach (var item in Algorithms)
            {
                requestData.Algorithms.Add(item.GetCurrent());
            }
            
            foreach (var item in Combinations)
            {
                requestData.Combinations.Add(item.GetCurrent());
            }
            
            requestData.Values = Values;
            return requestData;
        }
    }
}