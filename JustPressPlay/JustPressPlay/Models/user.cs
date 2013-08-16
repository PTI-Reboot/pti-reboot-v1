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
    
    public partial class user
    {
        public user()
        {
            this.friend_source = new HashSet<friend>();
            this.friend_destination = new HashSet<friend>();
            this.friend_pending_source = new HashSet<friend_pending>();
            this.friend_pending_destination = new HashSet<friend_pending>();
            this.logs = new HashSet<log>();
            this.notification_sources = new HashSet<notification>();
            this.notification_destinations = new HashSet<notification>();
            this.achievement_template_creator = new HashSet<achievement_template>();
            this.achievement_template_last_modified_by = new HashSet<achievement_template>();
            this.achievement_caretaker = new HashSet<achievement_caretaker>();
            this.achievement_user_content_approved_by = new HashSet<achievement_user_content>();
            this.achievement_user_content_pending = new HashSet<achievement_user_content_pending>();
            this.achievement_instance = new HashSet<achievement_instance>();
            this.achievement_instance_assigned_by = new HashSet<achievement_instance>();
            this.quest_tracking = new HashSet<quest_tracking>();
            this.quest_instance = new HashSet<quest_instance>();
            this.quest_template_creator = new HashSet<quest_template>();
            this.quest_template = new HashSet<quest_template>();
            this.external_token = new HashSet<external_token>();
            this.comments = new HashSet<comment>();
            this.comment_last_modified_by = new HashSet<comment>();
            this.news = new HashSet<news>();
        }
    
        public int id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public bool is_player { get; set; }
        public System.DateTime created_date { get; set; }
        public int status { get; set; }
        public bool first_login { get; set; }
        public string email { get; set; }
        public System.DateTime last_login_date { get; set; }
        public string organization_id { get; set; }
        public string organization_program_code { get; set; }
        public string organization_year_level { get; set; }
        public string organization_user_type { get; set; }
        public string display_name { get; set; }
        public string six_word_bio { get; set; }
        public string full_bio { get; set; }
        public string image { get; set; }
        public string personal_url { get; set; }
        public int privacy_settings { get; set; }
        public bool has_agreed_to_tos { get; set; }
        public Nullable<int> creator_id { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public string custom_1 { get; set; }
        public string custom_2 { get; set; }
        public string custom_3 { get; set; }
        public int communication_settings { get; set; }
        public int notification_settings { get; set; }
    
        public virtual ICollection<friend> friend_source { get; set; }
        public virtual ICollection<friend> friend_destination { get; set; }
        public virtual ICollection<friend_pending> friend_pending_source { get; set; }
        public virtual ICollection<friend_pending> friend_pending_destination { get; set; }
        public virtual ICollection<log> logs { get; set; }
        public virtual ICollection<notification> notification_sources { get; set; }
        public virtual ICollection<notification> notification_destinations { get; set; }
        public virtual ICollection<achievement_template> achievement_template_creator { get; set; }
        public virtual ICollection<achievement_template> achievement_template_last_modified_by { get; set; }
        public virtual ICollection<achievement_caretaker> achievement_caretaker { get; set; }
        public virtual ICollection<achievement_user_content> achievement_user_content_approved_by { get; set; }
        public virtual ICollection<achievement_user_content_pending> achievement_user_content_pending { get; set; }
        public virtual ICollection<achievement_instance> achievement_instance { get; set; }
        public virtual ICollection<achievement_instance> achievement_instance_assigned_by { get; set; }
        public virtual ICollection<quest_tracking> quest_tracking { get; set; }
        public virtual ICollection<quest_instance> quest_instance { get; set; }
        public virtual ICollection<quest_template> quest_template_creator { get; set; }
        public virtual ICollection<quest_template> quest_template { get; set; }
        public virtual ICollection<external_token> external_token { get; set; }
        public virtual ICollection<comment> comments { get; set; }
        public virtual ICollection<comment> comment_last_modified_by { get; set; }
        public virtual ICollection<news> news { get; set; }
        public virtual facebook_connection facebook_connection { get; set; }
    }
}
