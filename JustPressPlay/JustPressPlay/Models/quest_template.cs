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
    
    public partial class quest_template
    {
        public quest_template()
        {
            this.quest_keyword = new HashSet<quest_keyword>();
            this.quest_tracking = new HashSet<quest_tracking>();
            this.quest_achievement_step = new HashSet<quest_achievement_step>();
            this.quest_instance = new HashSet<quest_instance>();
        }
    
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public bool featured { get; set; }
        public int state { get; set; }
        public int creator_id { get; set; }
        public System.DateTime created_date { get; set; }
        public Nullable<System.DateTime> posted_date { get; set; }
        public Nullable<System.DateTime> retire_date { get; set; }
        public Nullable<int> last_modified_by_id { get; set; }
        public Nullable<System.DateTime> last_modified_date { get; set; }
        public Nullable<int> threshold { get; set; }
        public bool user_generated { get; set; }
    
        public virtual ICollection<quest_keyword> quest_keyword { get; set; }
        public virtual ICollection<quest_tracking> quest_tracking { get; set; }
        public virtual ICollection<quest_achievement_step> quest_achievement_step { get; set; }
        public virtual ICollection<quest_instance> quest_instance { get; set; }
        public virtual user creator { get; set; }
        public virtual user last_modified_by { get; set; }
    }
}
