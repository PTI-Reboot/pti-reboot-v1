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
    
    public partial class achievement_user_content
    {
        public achievement_user_content()
        {
            this.achievement_instance = new HashSet<achievement_instance>();
        }
    
        public int id { get; set; }
        public int content_type { get; set; }
        public System.DateTime submitted_date { get; set; }
        public System.DateTime approved_date { get; set; }
        public int approved_by_id { get; set; }
        public string image { get; set; }
        public string text { get; set; }
        public string url { get; set; }
    
        public virtual user approved_by { get; set; }
        public virtual ICollection<achievement_instance> achievement_instance { get; set; }
    }
}