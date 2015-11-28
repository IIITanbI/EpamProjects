using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4
{
    class A
    {
        public int abc;
    }

    class B
    {
        public A abc;

        public void test()
        {
            test1(ref abc);
            
        }

        public void test1(ref A a)
        {
            throw new ArgumentNullException(nameof(a));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            B b = new B();
            //b.test();
        }
    }
}
