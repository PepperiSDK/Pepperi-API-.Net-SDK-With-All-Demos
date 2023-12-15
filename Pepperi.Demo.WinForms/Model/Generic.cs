using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormApiDemo.Model
{
    public class DropdownItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public DropdownItem() { }
        public DropdownItem(string value, string text)
        {
            this.Value = value;
            this.Text = text;
        }
    }
}
