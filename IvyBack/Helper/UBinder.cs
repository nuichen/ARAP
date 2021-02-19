using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;

namespace IvyBack.Helper
{
    public class UBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {

            try
            {
                Assembly ass = Assembly.GetExecutingAssembly();
                Type t = ass.GetType(typeName);
                return t;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
