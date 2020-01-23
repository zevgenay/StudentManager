using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManager.Web.Models
{
    public class StudentForm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Enum Gender { get; set; }
        public string NickName { get; set; }
    }
}
