using System;
using System.Collections.Generic;
using System.Text;

namespace Lettuce.DependencyInjection
{
    public class LettuceContainerStore<TAbstracter>
    {
        private static Type classType;

        public static void InitClassType<TImplementer>()
        {
            classType = typeof(TImplementer);
        }

        public static Type GetClassType()
        {
            return classType;
        }
    }
}