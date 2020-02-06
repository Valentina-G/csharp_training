using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    class FeelGroupForm
    {
        private int text;
        public FeelGroupForm(int text)
        {
            this.text = text;
        }
        public int Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }
    }
}
