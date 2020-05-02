using System.Collections.Generic;
using OutliersLib;

namespace OutliersApp.Models
{
    public class Form
    {
        public List<ModuleFormModel> Algorithms { get; set; }
        public List<ModuleFormModel> Combinations { get; set; }
        public double[,] Values { get; set; }

        public bool ValuesValid
        {
            get
            {
                try
                {
                    Values = Utils.ParseInput(ValuesString);
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        public bool IsValid
        {
            get
            {
                foreach (var mod in Algorithms)
                {
                    if (!mod.IsValid) return false;
                }

                foreach (var mod in Combinations)
                {
                    if (!mod.IsValid) return false;
                }

                if (!ValuesValid)
                {
                    return false;
                }
                return true;
            }
        }

        public string ValuesString { get; set; }

        public Form()
        {
            Algorithms = new List<ModuleFormModel>();
            Combinations = new List<ModuleFormModel>();
            Values = new double[0,0];
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