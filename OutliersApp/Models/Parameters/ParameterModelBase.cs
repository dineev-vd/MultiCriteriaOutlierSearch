using System;
using OutliersApp.Components.Parameters;

namespace OutliersApp.Models.Parameters
{
    public abstract class ParameterModelBase : ICloneable, IComparable<ParameterModelBase>
    {
        public abstract int Id { get; set; }
        public object Value
        {
            get
            {
                if (this is DoubleParameterModel)
                {
                    return ((DoubleParameterModel) this).Value;
                }
                
                if (this is SelectParameterModel)
                {
                    return ((SelectParameterModel) this).Value;
                }
                
                if (this is BoolParameterModel)
                {
                    return ((BoolParameterModel) this).Value;
                }
                
                if (this is StringParameterModel)
                {
                    return ((StringParameterModel) this).Value;
                }
                
                if (this is IntParameterModel)
                {
                    return ((IntParameterModel) this).Value;
                }

                throw new NotImplementedException($"This type of parameter ({this.GetType().Name}) is not supported");
            }
        }

        public bool IsValid { get; set; }
        public bool IsCustom { get; set; }
        public string Name { get; set; }
        public string CoolName { get; set; }

        public ParameterModelBase(string name, string coolName)
        {
            Name = name;
            CoolName = coolName;
            IsValid = true;
        }

        public object Clone()
        {
            if (this is DoubleParameterModel)
            {
                var current = this as DoubleParameterModel;
                return new DoubleParameterModel(current.Name,current.CoolName, current.Min, current.Max, current.Default);
            }

            if (this is SelectParameterModel)
            {
                var current = this as SelectParameterModel;
                return new SelectParameterModel(current.Name, current.CoolName,current.Options, current.Default);
            }

            if (this is BoolParameterModel)
            {
                var current = this as BoolParameterModel;
                return new BoolParameterModel(current.Name, current.CoolName,current.Default);
            }

            if (this is StringParameterModel)
            {
                var current = this as StringParameterModel;
                return new StringParameterModel(current.Name, current.CoolName, current.Default);
            }

            if (this is IntParameterModel)
            {
                var current = this as IntParameterModel;
                return new IntParameterModel(current.Name, current.CoolName, current.Min, current.Max, current.Default);
            }
            
            throw new NotImplementedException($"Type {this.GetType()} cloning is not supported");
        }

        public int CompareTo(ParameterModelBase other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}