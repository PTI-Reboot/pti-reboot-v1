//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JustPressPlay.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class external_token
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string source { get; set; }
        public string token { get; set; }
        public System.DateTime created_date { get; set; }
        public System.DateTime expiration_date { get; set; }
    
        public virtual user user { get; set; }
    }
}
