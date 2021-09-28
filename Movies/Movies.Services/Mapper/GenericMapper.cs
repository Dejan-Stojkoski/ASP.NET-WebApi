using System;
using System.Linq;
using System.Reflection;

namespace Movies.Services.Mapper
{
    public static class GenericMapper
    {
        public static object MapObject(object source, object destination)
        {
            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            PropertyInfo[] sourceProperties = sourceType.GetProperties();
            PropertyInfo[] destinationProperties = destinationType.GetProperties();

            var commonProperties = from sp in sourceProperties
                                   join dp in destinationProperties on new { sp.Name, sp.PropertyType } equals new { dp.Name, dp.PropertyType }
                                   select new { sp, dp };

            foreach(var commonProperty in commonProperties)
            {
                var value = commonProperty.sp.GetValue(source);
                commonProperty.dp.SetValue(destination, value);
            }

            return destination;
        }
    }
}
