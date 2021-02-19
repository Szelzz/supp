using System;
using System.Reflection;

namespace Supp.Core.Modifier
{
    public class DefaultModifier : IModelModifier
    {
        public string PropertyName => null;

        public Type ModelType => null;

        public Type PropertyType => typeof(string);

        public void SetValue(object model, PropertyInfo property, string propertyValue)
        {
            property.SetValue(model, propertyValue);
        }
    }
}
