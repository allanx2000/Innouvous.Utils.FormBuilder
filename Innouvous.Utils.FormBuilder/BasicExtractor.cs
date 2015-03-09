using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Innouvous.Utils.FormBuilder
{
    /// <summary>
    /// An extension of ValueExtractor that has some common WPF components pre-registered
    /// </summary>
    public class BasicExtractor : ValueExtractor
    {
        public BasicExtractor() : base()
        {
            RegisterBasicExtractors();
        }

        private void RegisterBasicExtractors()
        {
            RegisterExtractor(typeof(TextBox), (ctrl) => ((TextBox) ctrl).Text);
        }
    }
}
