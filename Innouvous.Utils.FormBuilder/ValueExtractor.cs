using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Innouvous.Utils.FormBuilder
{
    public class ValueExtractor
    {
        private Dictionary<Type, Func<Control, object>> Extractors = new Dictionary<Type, Func<Control, object>>();

        public void RegisterExtractor(Type type, Func<Control, object> extractor)
        {
            if (Extractors.ContainsKey(type))
                throw new Exception("Type already registered");

            Extractors[type] = extractor;
        }

        public object Extract(Control control)
        {
            Type t = control.GetType();

            if (control is AbstractComponent)
                return ((AbstractComponent)control).GetData();
            else if (Extractors.ContainsKey(t))
            {
                return Extractors[t].Invoke(control);
            }
            else
                throw new Exception("No extractor registered for type: " + t.Name);
        }
    }
}
