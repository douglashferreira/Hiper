using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiper.Domain.Utils
{
    public static class Functions
    {
        public static void CopyPropertyValues(object source, object destination)
        {
            if (source is null)
                return;

            if (destination is null)
                return;

            var destProperties = destination.GetType().GetProperties();

            Parallel.ForEach(source.GetType().GetProperties(), (sourceProperty) =>
            {
                Parallel.ForEach(destProperties, (destProperty, state) =>
                {
                    if (destProperty != null && destProperty.Name == sourceProperty.Name &&
                        destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    {
                        destProperty.SetValue(destination, sourceProperty.GetValue(source, new object[] { }),
                            new object[] { });
                        state.Break();
                    }
                });
            });
        }
    }
}
