using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Models
{
    //This was created as a replacement for ContactModel since there were problems deserializing
    //the object
    public class Model
    {
        public class CreatedAt
        {
            public int Seconds { get; set; }
            public int Nanoseconds { get; set; }
        }

        public string id { get; set; }
        public string[] contactID { get; set; }
        public string[] contactName { get; set; }
        public string[] contactEmail { get; set; }
        public CreatedAt created_at { get; set; }
    }
}
