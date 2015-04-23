using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ImmaChatService
{
    [DataContract]
    public class Users
    {
        [DataMember]
        public string username;
        [DataMember]
        public string password;
        [DataMember]
        public string first_name;
        [DataMember]
        public string last_name;
        [DataMember]
        public string gender;
        [DataMember]
        public string date_of_birth;
        public Users()
        {
        }
    }
}