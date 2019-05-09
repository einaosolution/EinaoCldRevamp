using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPORevamp.N.Models
{
    public class FirebaseModels
    {
        

        
    }

    public class Message
    {
        public string[] registration_ids { get; set; }
        public Notification notification { get; set; }
        public object data { get; set; }
    }

    public class Notification
    {
        public string title { get; set; }
        public string text { get; set; }
    }
}
