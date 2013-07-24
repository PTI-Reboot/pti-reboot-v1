﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using JustPressPlay.ViewModels;

namespace JustPressPlay.Models.Repositories
{
	public class QuestRepository : Repository
	{
		/// <summary>
		/// Creates a new user repository
		/// </summary>
		/// <param name="unitOfWork">The unit of work that created this repository</param>
		public QuestRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{

		}

        #region Insert/Delete

        private void AddQuestTemplateToDatabase(quest_template template)
        {
            _dbContext.quest_template.Add(template);
        }

        private void AddAchievementStepsToDatabase(IEnumerable<quest_achievement_step> steps)
        {
            foreach (quest_achievement_step step in steps)
            {
                _dbContext.quest_achievement_step.Add(step);
            }
        }

        public void AdminAddQuest(AddQuestViewModel model)
        {
            quest_template newQuest = new quest_template
            {
                created_date = DateTime.Now, 
                creator_id = model.CreatorID, 
                description = model.Description, 
                featured = false,
                icon = model.IconFilePath,
                last_modified_by_id = null,
                last_modified_date = null,
                posted_date = null, // TODO: Check if posted immediately?
                retire_date = null,
                state = 0, // TODO: Get state from enum once it's implemented
                threshold = model.Threshold,
                title = model.Title,
                user_generated = false,
            };

            List<quest_achievement_step> questAchievementSteps = new List<quest_achievement_step>();
            foreach (int i in model.SelectedAchievementsList)
            {
                quest_achievement_step q = new quest_achievement_step
                {
                    achievement_id = i,
                    quest_id = newQuest.id
                };
                questAchievementSteps.Add(q);
            }

            AddQuestTemplateToDatabase(newQuest);
            AddAchievementStepsToDatabase(questAchievementSteps);

            Save();
        }

        internal void AdminEditQuest(int id, EditQuestViewModel model)
        {
            quest_template currentQuest = _dbContext.quest_template.SingleOrDefault(q => q.id == id);

            // Replace quest data
            if (currentQuest.title != model.Title)
                currentQuest.title = model.Title;

            if (currentQuest.description != model.Description)
                currentQuest.description = model.Description;

            if (currentQuest.icon != model.IconFilePath)
                currentQuest.icon = model.IconFilePath;

            if (currentQuest.state != model.State)
                currentQuest.state = model.State;

            // TODO: posted date?

            currentQuest.last_modified_by_id = model.EditorID;
            currentQuest.last_modified_date = DateTime.Now;

            if (currentQuest.threshold != model.Threshold)
                currentQuest.threshold = model.Threshold;

            // Replace achievement steps
            IEnumerable<quest_achievement_step> oldQuestAchievementSteps = _dbContext.quest_achievement_step.Where(q => q.quest_id == id);
            foreach (quest_achievement_step step in oldQuestAchievementSteps)
                _dbContext.quest_achievement_step.Remove(step);

            List<quest_achievement_step> newQuestAchievementSteps = new List<quest_achievement_step>();
            foreach (int i in model.SelectedAchievementsList)
            {
                quest_achievement_step q = new quest_achievement_step
                {
                    achievement_id = i,
                    quest_id = id
                };
                newQuestAchievementSteps.Add(q);
            }

            AddAchievementStepsToDatabase(newQuestAchievementSteps);

            Save();
        }

        #endregion

        #region Query methods

        public IEnumerable<quest_tracking> GetTrackedQuestsForUser(int userID)
        {
            return _dbContext.quest_tracking.Where(q => q.user_id == userID);
        }

        #endregion

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        
    }
}