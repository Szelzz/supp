using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Modifier
{
    public class UniversalModelModifier
    {
        private readonly IEnumerable<IModelModifier> modifiers;

        public UniversalModelModifier(IEnumerable<IModelModifier> modifiers)
        {
            this.modifiers = modifiers;
        }

        public Result SetValue<TModel>(TModel model, string propertyName, string propertyValue)
        {
            var property = typeof(TModel).GetProperty(propertyName);
            if (property == null)
                return Result.Fail();

            var modifier = FindBestModifier(typeof(TModel), property.PropertyType, propertyName);
            modifier.SetValue(model, property, propertyValue);

            return Result.Success();
        }

        private IModelModifier FindBestModifier(Type modelType, Type propertyType, string propertyName)
        {
            IModelModifier bestModifier = null;
            var bestModifierRank = -1;

            foreach (var modifier in modifiers)
            {
                var rank = 0;
                if (modifier.ModelType == modelType)
                    rank += 100;
                else if (modifier.ModelType != null)
                    continue;

                if (modifier.PropertyName == propertyName)
                    rank += 10;
                else if (modifier.PropertyName != null)
                    continue;

                if (modifier.PropertyType == propertyType)
                    rank += 1;
                else if (modifier.PropertyType != null)
                    continue;

                if (bestModifierRank < rank)
                {
                    bestModifier = modifier;
                    bestModifierRank = rank;
                }
            }
            if (bestModifier == null)
                throw new NotImplementedException("Missing matching modifier");

            return bestModifier;
        }
    }
}
