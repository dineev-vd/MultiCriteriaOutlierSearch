using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using OutliersLib;
using Module = OutliersLib.Module;

namespace OutliersApp.Data
{
    public class Form
    {
        public List<ModuleForm> Algorithms { get; set; }
        public List<ModuleForm> Combinations { get; set; }
        public List<double> Values { get; set; }

        public Form()
        {
            Algorithms = new List<ModuleForm>();
            Combinations = new List<ModuleForm>();
            Values = new List<double>();
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