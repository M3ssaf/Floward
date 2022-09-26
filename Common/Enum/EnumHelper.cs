using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public static class EnumHelper
    {
        public static T GetAttribute<T>(this System.Enum value)
        {
            var enumType = value.GetType();
            var name = System.Enum.GetName(enumType, value);
            return enumType.GetField(name)
                .GetCustomAttributes(false)
                .OfType<T>()
                .SingleOrDefault();
        }
    }
}
