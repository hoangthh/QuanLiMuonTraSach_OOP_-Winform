using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_QuanLiMuonTraSach
{
    internal interface IList<T>
    {
        void Add(T item);

        T Find(int id);

        void Remove(T item);
    }
}
