using Innouvous.Utils.FormBuilder;
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

namespace TestFormBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FormBuilder builder;
        public MainWindow()
        {
            InitializeComponent();

            builder =  new FormBuilder(customValueExtractor: new BasicExtractor());

            
            TextBox tb = new TextBox();
            FormField field = new FormField("TestInput", "Test Input:", tb);

            builder.AddField(field);

            builder.SetupContainer(TestContainer);
        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            var data = builder.GetValues(TestContainer);
        }
    }
}
