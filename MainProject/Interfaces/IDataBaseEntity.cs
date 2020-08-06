using System;
using System.Collections.Generic;
using System.Text;

namespace MainProject
{
    interface IDataBaseEntity
    {
        int ID {get; set;}
        string Name { get; set; }

        string ToString();
    }
}
