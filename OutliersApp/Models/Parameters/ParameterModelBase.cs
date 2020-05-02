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

                throw new NotImplementedException($"This type of parameter ({this.GetType().Name}) is not supported");
            }
        }

        public bool IsValid { get; set; }
        public bool IsCustom { get; set; }
        public string Name { get; set; }

        public ParameterModelBase(string name)
        {
            Name = name;
            IsValid = true;
        }

        public object Clone()
        {
            if (this is DoubleParameterModel)
            {
                var current = this as DoubleParameterModel;
                return new DoubleParameterModel(current.Name, current.Min, current.Max, current.Default);
            }

            if (this is SelectParameterModel)
            {
                var current = this as SelectParameterModel;
                return new SelectParameterModel(current.Name, current.Options, current.Default);
            }

            if (this is BoolParameterModel)
            {
                var current = this as BoolParameterModel;
                return new BoolParameterModel(current.Name, current.Default);
            }
            
            throw new NotImplementedException($"Type {this.GetType()} cloning is not supported");
        }

        public int CompareTo(ParameterModelBase other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}