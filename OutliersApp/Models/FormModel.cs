using OutliersLib;
using System.Collections.Generic;

namespace OutliersApp.Models
{
    public class FormModel
    {
        public List<ModuleFormModel> Algorithms { get; }
        public List<ModuleFormModel> Combinations { get; }
        public double[,] Values { get; set; }
        public int NotUsedColumns { get; set; }

        public bool ValuesValid
        {
            get
            {
                try
                {
                    Values = Converters.ParseInput(ValuesString, NotUsedColumns);
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

        public FormModel()
        {
            Algorithms = new List<ModuleFormModel>();
            Combinations = new List<ModuleFormModel>();
            Values = new double[0, 0];
            NotUsedColumns = 2;
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