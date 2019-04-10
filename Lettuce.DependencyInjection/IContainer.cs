using System;
using System.Collections.Generic;
using System.Text;

namespace Lettuce.DependencyInjection
{
    public interface ILettuceContainer
    {
        T Resolver<T>();

        void Register<TAbstracter, TImplementer>();
    }
}