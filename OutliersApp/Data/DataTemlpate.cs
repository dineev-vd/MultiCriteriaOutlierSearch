using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using OutliersLib;

namespace OutliersApp.Data
{
    public class NameModule : Module
    {
        public string Name { get; set; }

        public NameModule() : base()
        {
            Name = String.Empty;
        }
    }

    public class InternalStruct
    {
        public NameModule Internal { get; set; }
        public KeyValuePair<string, ModuleSettings> Selected { get; set; }

        public InternalStruct()
        {
            Internal = new NameModule();
            Selected = new KeyValuePair<string, ModuleSettings>(String.Empty, new ModuleSettings());
        }
    }

    public class PowerCouple
    {
        public bool IsInternal { get; set; }
        public InternalStruct Internal { get; set; }
        public NameModule External { get; set; }

        public PowerCouple()
        {
            IsInternal = true;
            Internal = new InternalStruct();
            External = new NameModule();
        }
    }

    public class DataTemlpate
    {
        public List<PowerCouple> Algorithms { get; set; }
        public List<PowerCouple> Combinations { get; set; }
        public List<double> Values { get; set; }

        public DataTemlpate()
        {
            Algorithms = new List<PowerCouple>();
            Combinations = new List<PowerCouple>();
            Values = new List<double>();
        }

        public OutlierRequestData ToRequestData()
        {
            var requestData = new OutlierRequestData();
            foreach (var item in Algorithms)
            {
                if (item.IsInternal)
                {
                    requestData.Algorithms.Add(item.Internal.Internal.Name, item.Internal.Internal);
                }
                else
                {
                    requestData.Algorithms.Add(item.External.Name, item.External);
                }
            }

            foreach (var item in Combinations)
            {
                if (item.IsInternal)
                {
                    requestData.Combinations.Add(item.Internal.Internal.Name, item.Internal.Internal);
                }
                else
                {
                    requestData.Combinations.Add(item.External.Name, item.External);
                }
            }

            requestData.Values = Values;
            return requestData;
        }
    }
}