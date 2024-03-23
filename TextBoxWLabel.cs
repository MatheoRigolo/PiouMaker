using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiouMaker
{
    internal class TextBoxWLabel : TextBox
    {
        private string label = "";

        public string Label { get => label; set => label = value; }
    }

    internal class ComboBoxWLabel : ComboBox
    {
        private string label = "";

        public string Label { get => label; set => label = value; }
    }
}
