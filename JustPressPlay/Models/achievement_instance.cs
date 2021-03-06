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
    
    public partial class achievement_instance
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int achievement_id { get; set; }
        public System.DateTime achieved_date { get; set; }
        public bool has_user_content { get; set; }
        public Nullable<int> user_content_id { get; set; }
        public bool has_user_story { get; set; }
        public Nullable<int> user_story_id { get; set; }
        public bool card_given { get; set; }
        public Nullable<System.DateTime> card_given_date { get; set; }
        public int assigned_by_id { get; set; }
        public int points_create { get; set; }
        public int points_explore { get; set; }
        public int points_learn { get; set; }
        public int points_socialize { get; set; }
        public bool comments_disabled { get; set; }
        public bool globally_assigned { get; set; }
    
        public virtual user user { get; set; }
        public virtual achievement_template achievement_template { get; set; }
        public virtual achievement_user_content user_content { get; set; }
        public virtual achievement_user_story user_story { get; set; }
        public virtual user assigned_by { get; set; }
    }
}
