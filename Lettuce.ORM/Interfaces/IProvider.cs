using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Lettuce.ORM.Interfaces
{
    public interface IProvider
    {
        IDbConnection GetConnection();
        IDbCommand CreateCommand();

    }
}
