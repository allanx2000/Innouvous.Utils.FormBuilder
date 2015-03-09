using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Innouvous.Utils.FormBuilder
{
    /// <summary>
    /// A data object holding the field definition
    /// </summary>
    public struct FormField
    {
        public string FieldID { get; private set; }
        public string FieldName { get; private set; }
        public Control Component { get; private set; }

        /// <summary>
        /// An input field for FormBuilder
        /// </summary>
        /// <param name="id">ID for the extracted value</param>
        /// <param name="name">Name of the field, shown in the UI</param>
        /// <param name="component">A control used to receive user input</param>
        public FormField(string id, string name, Control component): this()
        {
            this.FieldID = id;
            this.FieldName = name;
            this.Component = component;
        }
    }
}
