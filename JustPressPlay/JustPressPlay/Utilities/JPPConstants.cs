﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

using System.Web.Mvc;
using System.Reflection;

using JustPressPlay.Models;
using JustPressPlay.Models.Repositories;

namespace JustPressPlay.Utilities
{
	/// <summary>
	/// Contains site-wide constants
	/// </summary>
	public static class JPPConstants
	{
		#region Roles
		/// <summary>
		/// Contains all of the roles in Just Press Play.
		/// Roles in this class will be automatically added to the database
		/// during Application_Start() in Global.asax
		/// </summary>
		public static class Roles
		{
			public const String FullAdmin = "FullAdmin";
			public const String AssignIndividualAchievements = "AssignIndividualAchievements";
			public const String AssignGlobalAchievements = "AssignGlobalAchievements";
			public const String HandleUserSubmittedContent = "HandleUserSubmittedContent";
			public const String ModerateAchievementsAndStories = "ModerateAchievementsAndStories";
			public const String CreateEditNews = "CreateEditNews";
			public const String SendAnnouncements = "SendAnnouncements";
			public const String HandleHighlightedAchievements = "HandleHighlightedAchievements";
			public const String DistributeCards = "DistributeCards";
			public const String ViewReports = "ViewReports";
			public const String CreateUsers = "CreateUsers";
			public const String EditUsers = "EditUsers";
			public const String CreateAchievements = "CreateAchievements";
			public const String EditAchievements = "EditAchievements";
			public const String CreateQuests = "CreateQuests";
			public const String EditQuests = "EditQuests";
			public const String ApproveUserSubmittedQuests = "ApproveUserSubmittedQuests";
			public const String ManageSiteSettings = "ManageSiteSettings";

			/// <summary>
			/// Updates a user to ONLY be in the roles specified
			/// </summary>
			/// <param name="username">The username of the user</param>
			/// <param name="roles">The array of roles</param>
			public static void UpdateUserRoles(String username, String[] roles)
			{
				String[] currentRoles = System.Web.Security.Roles.GetRolesForUser(username);

				String[] rolesToRemove = currentRoles.Except(roles).ToArray();
				String[] rolesToAdd = roles.Except(currentRoles).ToArray();

				if( rolesToRemove.Length > 0 )
					System.Web.Security.Roles.RemoveUserFromRoles(username, rolesToRemove);

				if (rolesToAdd.Length > 0)
					System.Web.Security.Roles.AddUserToRoles(username, rolesToAdd);
			}
		}
		#endregion

		#region Site Settings
		/// <summary>
		/// Contains constants and methods for getting and setting
		/// various site settings
		/// </summary>
		public static class SiteSettings
		{
			public const String ColorNavBar = "ColorNavBar";
			public const String ColorCreate = "ColorCreate";
			public const String ColorExplore = "ColorExplore";
			public const String ColorLearn = "ColorLearn";
			public const String ColorSocialize = "ColorSocialize";
			public const String ColorQuest = "ColorQuest";
			public const String SchoolName = "SchoolName";
			public const String SchoolLogo = "SchoolLogo";
			public const String CardDistributionEnabled = "CardDistributionEnabled";
			public const String MaxPointsPerAchievement = "MaxPointsPerAchievement";
			public const String SelfRegistrationEnabled = "SelfRegistrationEnabled";
			public const String UserGeneratedQuestsEnabled = "UserGeneratedQuestsEnabled";
			public const String CommentsEnabled = "CommentsEnabled";
			public const String FacebookIntegrationEnabled = "FacebookIntegrationEnabled";

            private static Dictionary<String, String> DefaultValues = new Dictionary<string, string>()
            {
                { ColorNavBar, "FFFFFF" },
                { ColorCreate, "FFFFFF" },
                { ColorExplore, "FFFFFF" },
                { ColorLearn, "FFFFFF" },
                { ColorSocialize, "FFFFFF" },
                { ColorQuest, "FFFFFF" },
                { SchoolName, "School Name" },
                { SchoolLogo, "School Logo" },
                { CardDistributionEnabled, true.ToString() },
                { MaxPointsPerAchievement, "16" },
                { SelfRegistrationEnabled, true.ToString() },
                { UserGeneratedQuestsEnabled, true.ToString() },
                { CommentsEnabled, true.ToString() },
                { FacebookIntegrationEnabled, true.ToString() },
            };

			/// <summary>
			/// Gets a setting's value from the database.  All values are string. 
			/// Returns null if the setting doesn't exist in the database.
			/// </summary>
			/// <param name="setting">The setting to get. Use pre-defined constants in SiteSettings class.</param>
			/// <returns>The value of the setting, or null</returns>
			public static String GetValue(String setting)
			{
				UnitOfWork work = new UnitOfWork();

				system_setting ss = (from s in work.EntityContext.system_setting
									 where s.key == setting
									 select s).FirstOrDefault();

                if (ss == null)
                {
                    // Return default
                    // TODO: Log default return?
                    return DefaultValues[setting];
                }

				// Return the actual stored value
				return ss.value;
			}

			/// <summary>
			/// Sets a site setting in the database.  All values are strings.
			/// </summary>
			/// <param name="setting">The setting to set. Use pre-defined constants in SiteSettings class.</param>
			/// <param name="value">The value to store in the database.</param>
			/// <returns>The value of the setting, or null</returns>
			public static void SetValue(String setting, String value)
			{
				UnitOfWork work = new UnitOfWork();

				system_setting ss = (from s in work.EntityContext.system_setting
									 where s.key == setting
									 select s).FirstOrDefault();

				// Making a new one, or updating?
				if (ss == null)
				{
					// Make a new one and add it
					ss = new system_setting()
					{
						key = setting,
						key_hash = 0,	// Unused for now
						value = value
					};
					work.EntityContext.system_setting.Add(ss);
				}
				else
				{
					ss.value = value;
				}

				work.SaveChanges();
			}
		}

		#endregion

		#region User Status
		/// <summary>
		/// Contains possible user status options
		/// </summary>
		public enum UserStatus
		{
			/// <summary>
			/// Account is currently active
			/// </summary>
			Active = 1,

			/// <summary>
			/// Account is suspended
			/// </summary>
			Suspended = 2,

			/// <summary>
			/// Account has been deactivated, although the user remains
			/// in the system and can be reactivated
			/// </summary>
			Deactivated = 3,

			/// <summary>
			/// Account has been deleted and all user information
			/// has been purged
			/// </summary>
			Deleted = 4
		}
		#endregion

		#region Privacy Settings
		/// <summary>
		/// A user's current privacy setting
		/// </summary>
		public enum PrivacySettings
		{
			/// <summary>
			/// Only friends have access
			/// </summary>
			[Description("Friends Only")]
			FriendsOnly,

			/// <summary>
			/// Everyone with an account has access
			/// </summary>
			[Description("Just Press Play Only")]
			JustPressPlayOnly,

			/// <summary>
			/// Information, achievements, etc are public
			/// </summary>
			[Description("Public")]
			Public
		}
		#endregion

		#region Achievement and Quest States
		/// <summary>
		/// States for achievements and quests. "Hidden" is not a state,
		/// as it can be applied in addition to any state.
		/// </summary>
		public enum AchievementQuestStates
		{
			/// <summary>
			/// It exists but is not yet available to users
			/// </summary>
			Draft,

			/// <summary>
			/// The achievement/quest can be attained
			/// </summary>
			Active,

			/// <summary>
			/// The achievement/quest is visible but not attainable
			/// </summary>
			Inactive,

			/// <summary>
			/// The achievement/quest is visible but can never be attained again
			/// </summary>
			Retired
		}
		#endregion

		#region Achievement Types
		/// <summary>
		/// Types of achievements
		/// </summary>
		public enum AchievementTypes
        {
			/// <summary>
			/// Achievements with caretakers, which can be given via the admin section
			/// or scanned via the mobile app
			/// </summary>
            Scan,

			/// <summary>
			/// Automatic achievements granted by the system
			/// </summary>
            System,

			/// <summary>
			/// Achievements that require a specific number of repeats
			/// of another repeatable achievement
			/// </summary>
            Threshold,

			/// <summary>
			/// Achievements that require the user to submit type of content
			/// to the site
			/// </summary>
            UserSubmission,

			/// <summary>
			/// Can only be assigned by an admin
			/// </summary>
			AdminAssigned
        }

        #endregion

		#region Comment Locations
		/// <summary>
		/// Possible comment locations
		/// </summary>
		public enum CommentLocation
		{
			Achievement,
			Quest
		}
		#endregion

		#region User Submission Types
		/// <summary>
		/// Types of user submission content
		/// </summary>
        public enum UserSubmissionTypes
        {
            Image,
            Text,
            URL
        }

        #endregion

        #region System Achievement Types
        /// <summary>
        /// TODO: Finalize this list
        /// </summary>
        public enum SystemAchievementTypes
        {
            SixWordBio,
            ProfilePic,
            Friends_x1,
            Friends_x10,
            Friends_x25,
            OneK,
            TenK,
            Ring_x4,
            Ring_x25,
            Ring_x100,
            PublicProfile,
            FacebookLink
        }

        #endregion

        #region Constants Static Helper Methods
        /// <summary>
		/// Converts an enum to a List of SelectListItems, which will contain Description attributes
		/// if found, otherwise it will contain the names of the enum fields
		/// </summary>
		/// <typeparam name="T">The type to convert (must be an enum)</typeparam>
		/// <param name="enumObj">The enum object</param>
		/// <returns>A list of SelectListItems based on the enum</returns>
		public static List<SelectListItem> SelectListFromEnum<T>() where T : struct, IConvertible
		{
			// Make sure it's an enum
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException("T must be an enum");
			}

			// Get the field members
			Type type = typeof(T);
			FieldInfo[] infos = type.GetFields(BindingFlags.Public | BindingFlags.Static);

			// Loop and look for attributes
			List<SelectListItem> list = new List<SelectListItem>();
			for (int i = 0; i < infos.Length; i++)
			{
				DescriptionAttribute attr = infos[i].GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
				String desc = attr == null ? infos[i].Name : attr.Description;
				list.Add(new SelectListItem()
				{
					Text = desc,
					Value = infos[i].GetRawConstantValue().ToString()
				});
			}

			return list;
		}
		#endregion

        #region Image Size Restrictions
        public const int NewsItemImageMaxSideSize = 200; // TODO: get real value
        public static int SiteLogoMaxSideSize = 200; // TODO: get real value
        #endregion
    }
}