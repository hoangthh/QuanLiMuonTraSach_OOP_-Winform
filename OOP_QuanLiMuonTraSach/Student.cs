using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Person
{
    public class Student : IPerson
    {
        public MuonSachList muonSachList;
        public string ID;
        public Student()
        {
            muonSachList = new MuonSachList();
        }
    }
}
