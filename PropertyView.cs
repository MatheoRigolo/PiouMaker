using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiouMaker
{
    internal class PropertyView
    {
        private Label propertyLabel;
        private Control propertyControl;

        public PropertyView()
        {
            propertyLabel = new Label();
            propertyControl = new Control();
            propertyLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
        }

        public PropertyView(string propertyName) : this()
        {
            propertyLabel.Text = propertyName;
        }

        public PropertyView(Label panel, Control control) : this()
        {
            propertyLabel = panel;
            propertyControl = control;
        }

        public PropertyView(string panelText, Control control) : this(panelText)
        {
            propertyControl = control;
        }

        public Label getLabel()
        {
            return propertyLabel;
        }

        public Control getControl()
        {
            return propertyControl;
        }

        public void setPanelString(string panelText)
        {
            propertyLabel.Text = panelText;
        }

        public void setPos(Rectangle displayRectangle, int positionInList)
        {
            propertyLabel.SetBounds(displayRectangle.X, displayRectangle.Height / 20 + displayRectangle.Y, displayRectangle.Width, propertyLabel.Height);
            propertyControl.SetBounds(displayRectangle.X, propertyLabel.Bounds.Y + propertyLabel.Height, propertyLabel.Width, propertyLabel.Height);

            propertyLabel.Location = new Point(propertyLabel.Bounds.X, propertyLabel.Bounds.Y + (propertyLabel.Height + propertyControl.Height) * positionInList);
            propertyControl.Location = new Point(displayRectangle.X, propertyLabel.Location.Y + propertyLabel.Height);
        }

        public void setControl(Control control)
        {
            propertyControl = control;
        }


    }
}
