using System;
using System.Reflection;

namespace Supp.Core.Modifier
{
    public class DefaultModifier : IModelModifier
    {
        public string PropertyName => null;

        public Type ModelType => null;

        public Type PropertyType => null;

        public void SetValue(object model, PropertyInfo property, string propertyValue)
        {
            if (property.PropertyType == typeof(string))
            {
                property.SetValue(model, propertyValue);
            }
            else if (property.PropertyType.IsEnum)
            {
                property.SetValue(model, Enum.Parse(property.PropertyType, propertyValue));
            }
            else
                throw new NotImplementedException("Not known property type");
        }
    }
}
