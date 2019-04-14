using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineSample.Lib
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static readonly Lazy<T> LazySingleton = new Lazy<T>(() => new T());

        public static T Instance { get { return LazySingleton.Value; } }

        protected Singleton() { }
    }
}
