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
    
    public partial class achievement_user_content_pending
    {
        public int id { get; set; }
        public int achievement_id { get; set; }
        public int content_type { get; set; }
        public int submitted_by_id { get; set; }
        public System.DateTime submitted_date { get; set; }
        public string image { get; set; }
        public string text { get; set; }
        public string url { get; set; }
    
        public virtual achievement_template achievement_template { get; set; }
        public virtual user submitted_by { get; set; }
    }
}
