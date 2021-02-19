using System;
using System.Reflection;

namespace Supp.Core.Modifier
{
    public interface IModelModifier
    {
        string PropertyName { get; }
        Type ModelType { get; }
        Type PropertyType { get; }

        void SetValue(object model, PropertyInfo property, string propertyValue);
    }
}
