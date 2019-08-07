using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace enums
{
    public static class Extension
    {
        public static string Description(this CodeEnum myEnum)
        {
            Type type = typeof(CodeEnum);
            FieldInfo info = type.GetField(myEnum.ToString());
            DescriptionAttribute descriptionAttribute = info.GetCustomAttributes(typeof(DescriptionAttribute), true)[0] as DescriptionAttribute;
            if (descriptionAttribute != null)
            {
                return descriptionAttribute.Description;
            }
            else
            {
                return type.ToString();
            }
        }
    }
}
