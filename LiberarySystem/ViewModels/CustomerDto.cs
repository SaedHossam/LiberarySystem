using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiberarySystem.ViewModels
{
    public class CustomerListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class CustomerCreateDto
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class CustomerEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}