using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace elle
{
    public class DynamicFieldGenerator
    {
        private readonly Panel _dynamicFieldsPanel;

        public DynamicFieldGenerator(Panel dynamicFieldsPanel)
        {
            _dynamicFieldsPanel = dynamicFieldsPanel;
        }

        public void CreateFields(Dictionary<string, FieldType> fields)
        {
            _dynamicFieldsPanel.Controls.Clear();
            int yOffset = 0;

            foreach (var field in fields)
            {
                var label = new Label
                {
                    Text = field.Key,
                    Location = new Point(0, yOffset),
                    AutoSize = true,
                };
                _dynamicFieldsPanel.Controls.Add(label);

                Control inputControl = CreateInputControl(field.Key, field.Value, yOffset);
                _dynamicFieldsPanel.Controls.Add(inputControl);
                yOffset += 50;
            }
        }

        private Control CreateInputControl(string fieldName, FieldType fieldType, int yOffset)
        {
            switch (fieldType)
            {
                case FieldType.DateTimePicker:
                    return new DateTimePicker
                    {
                        Name = fieldName + "DateTimePicker",
                        Location = new Point(0, yOffset + 20),
                        Width = 150,
                    };

                case FieldType.TextBox:
                default:
                    return new TextBox
                    {
                        Name = fieldName + "TextBox",
                        Location = new Point(0, yOffset + 20),
                        Width = 150,
                    };
            }
        }
    }
}
