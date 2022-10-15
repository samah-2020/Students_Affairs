using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.ViewModels
{

    public class DataTablesParam<T>
    {
        public DataTablesParam()
        {
            data = new List<T>();
        }


        public int start { get; set; }
        public int length { get; set; }
        public int draw { get; set; }
        public List<columns> columns { get; set; }
        public List<order> order { get; set; }
        public search search { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T> data { get; set; }

        // public int iTotalDisplayRecords { get; set; }
    }

    public class order
    {
        public string column { get; set; }
        public string dir { get; set; }
    }

    public class columns
    {
        public string data { get; set; }
        public string name { get; set; }
        public string orderable { get; set; }
        public string searchable { get; set; }
        public search search { get; set; }
    }

    public class search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }
}
