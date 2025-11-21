using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_Update
{
    class ChildTable
    {
        public string TableName { get; set; }
        public List<FormColumn> FormColumn { get; set; }
    }

    class FormColumn
    {
        public string FormColumnAlias { get; set; }
        public string FormColumnDescription { get; set; }
        public SAPbobsCOM.BoYesNoEnum Editable { get; set; }
    }
}
