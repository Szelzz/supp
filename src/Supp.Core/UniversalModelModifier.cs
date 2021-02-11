using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core
{
    public class UniversalModelModifier
    {
        public Result SetValue<TModel>(TModel model, string propertyName, string propertyValue)
        {
            var property = typeof(TModel).GetProperty(propertyName);
            if (property == null)
                return Result.Fail();

            if (property.PropertyType != typeof(string))
                return Result.Fail(); // TODO handle other types

            property.SetValue(model, propertyValue);
            return Result.Success();
        }
    }

    public interface IValueConverter<T>
    {
        T ConvertTo(string value);
    }

    //public class DefaultValueConverter
    //{
    //    public object ConvertTo(Type propertyType, string value)
    //    {
    //        if (propertyType == typeof(string))
    //            return value;

    //        Convert.ChangeType()
    //    }
    //}
}
