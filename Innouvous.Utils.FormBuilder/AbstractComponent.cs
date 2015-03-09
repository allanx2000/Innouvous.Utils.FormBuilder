using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Innouvous.Utils.FormBuilder
{
    /// <summary>
    /// A custom user control that supports FormBuilder, without need to define an extractor
    /// </summary>
    public abstract class AbstractComponent : Control
    {   
        public abstract object GetData();
    }
}
