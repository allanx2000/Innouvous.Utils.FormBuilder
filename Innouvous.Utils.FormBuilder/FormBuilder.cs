using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Innouvous.Utils.FormBuilder
{
    /// <summary>
    /// Creates a user input form programmatically by generating a Grid with the fields, and provides methods for extracting the values.
    /// </summary>
    public class FormBuilder
    {

        private Func<string, Label> labelBuilder = null;

        //By default use BasicExtractor which recognizes common WPF components
        private ValueExtractor valueExtractor = new BasicExtractor();
        
        private List<FormField> fields = new List<FormField>();
        private List<string> fieldIds = new List<string>();

        private const string ContainerTag = "FormContainer";

        /// <summary>
        /// Initializes an instance of FormBuilder
        /// </summary>
        /// <param name="labelBuilder">A function for creating custom field labels</param>
        /// <param name="customValueExtractor">A custom implementation of ValueExtractor</param>
        public FormBuilder(Func<string, Label> labelBuilder = null, ValueExtractor customValueExtractor = null)
        {
            if (customValueExtractor != null)
                valueExtractor = customValueExtractor;

            this.labelBuilder = labelBuilder;
        }

        /// <summary>
        /// Adds a new field to the control
        /// </summary>
        /// <param name="field"></param>
        public void AddField(FormField field)
        {
            if (fieldIds.Contains(field.FieldID))
                throw new Exception(field.FieldID + " already added.");

            fields.Add(field);
            fieldIds.Add(field.FieldID);
        }

        /// <summary>
        /// Removes the given FieldId from the control
        /// </summary>
        /// <param name="id"></param>
        public void RemoveField(string id)
        {
            if (!fieldIds.Contains(id))
                throw new Exception("ID not found: " + id);
            else
            {
                //Remove at index, as order should be the same
                int idx = fieldIds.IndexOf(id);

                fieldIds.RemoveAt(idx);
                fields.RemoveAt(idx);
            }
        }

        /// <summary>
        /// Removes the given FieldId from the control
        /// </summary>
        /// <param name="field">Notw: it will use the FieldID to find it, not the field itself</param>
        public void RemoveField(FormField field)
        {
            RemoveField(field.FieldID);
        }

        /// <summary>
        /// Sets up the container with the defined fields
        /// </summary>
        /// <param name="container">A Grid control to hold the components in; the control has to be empty</param>
        public void SetupContainer(Grid container)
        {
            if (container.Children.Count > 0 || container.ColumnDefinitions.Count > 0 || container.RowDefinitions.Count > 0)
            {
                throw new Exception("Container is not empty");
            }
            else if (container.Tag != null)
            {
                throw new Exception("Tag is not empty: " + container.Tag.ToString());
            }

            //Modify the tag, as a check
            container.Tag = ContainerTag;
            
            container.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) });
            container.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            
            for (int i = 0; i<fields.Count; i++)
            {
                var rowDef = fields[i];

                //Label
                Label label;
                
                if (labelBuilder != null)
                    label = labelBuilder.Invoke(rowDef.FieldName);
                else
                    label = BuildDefaultLabel(rowDef.FieldName);
                
                label.SetValue(Grid.RowProperty, i);
                label.SetValue(Grid.ColumnProperty, 0);

                //Component
                Control component = rowDef.Component;
                component.SetValue(Grid.RowProperty, i);
                component.SetValue(Grid.ColumnProperty, 1);

                //Will OVERWRITE any custom Tags
                component.Tag = rowDef.FieldID;

                //Add the elements to the grid
                container.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
                container.Children.Add(label);
                container.Children.Add(component);
            }
        }

        public Dictionary<string, object> GetValues(Grid container)
        {
            CheckGridIsValid(container);
        
            Dictionary<string, object> values = new Dictionary<string, object>();
                        
            foreach (Control control in container.Children)
            {
                /*Label label = control as Label;

                if (label != null && (int)label.GetValue(Grid.ColumnProperty) == 0)
                {

                }*/

                if (!(control is Label)) //Skip labels
                {
                    var fieldId = control.Tag.ToString();
                    values[fieldId] = valueExtractor.Extract(control);      
                }
            }

            return values;
        }

        /// <summary>
        /// Logic for checking the container is "valid"
        /// </summary>
        /// <param name="container"></param>
        private void CheckGridIsValid(Grid container)
        {
            if (container.Tag != ContainerTag)
                throw new Exception("The Grid was not created by FormBuilder");
        }

        /// <summary>
        /// Creates a simple label for the field name
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private Label BuildDefaultLabel(string text)
        {
            return new Label() { Content = text };
        }

    }
}
