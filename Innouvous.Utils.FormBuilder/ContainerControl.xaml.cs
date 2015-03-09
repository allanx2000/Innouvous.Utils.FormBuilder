using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Innouvous.Utils.FormBuilder
{
    /// <summary>
    /// A pre-implemented UserControl that utilizes FormBuilder 
    /// </summary>
    public partial class ContainerControl : UserControl
    {
        private FormBuilder builder;
        public ContainerControl()
        {
            InitializeComponent();
            
            builder = new FormBuilder();
        }

        public FormBuilder GetBuilder()
        {
            return builder;
        }

    }
}
