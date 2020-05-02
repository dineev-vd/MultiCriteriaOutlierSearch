﻿using System.Collections.Generic;

namespace OutliersApp.Models.Parameters
{
    public class SelectParameterModel : ParameterModelBase
    {
        public override int Id { get; set; } = 2;
        public string Value { get; set; }
        public string Default { get; set; }
        public List<string> Options { get; set; }

        public SelectParameterModel(string name,string coolName, List<string> options, string def) : base(name, coolName)
        {
            Options = options;
            Default = def;
            Value = Default;
            IsCustom = false;
        }
    }
}