using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD

{
    interface IDataBaseEntity
    {
        int Id {get; set;}
        string Name { get; set; }

        string ToString();
    }
}
