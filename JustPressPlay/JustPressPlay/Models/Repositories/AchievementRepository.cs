﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JustPressPlay.Models;
using JustPressPlay.Models.Repositories;

using System.Data.Entity;

namespace JustPressPlay.Models.Repositories
{
    public class AchievementRepository : Repository
    {
        //------------------------------------------------------------------------------------//
        //-------------------------------------Enums------------------------------------------//
        //------------------------------------------------------------------------------------//
        #region Enums
        #endregion
        //------------------------------------------------------------------------------------//
        //------------------------------------Variables---------------------------------------//
        //------------------------------------------------------------------------------------//
        #region Variables
        private JustPressPlayDBEntities entities;
        private UserRepository ur;
        private SystemRepository sr;
        #endregion
        //------------------------------------------------------------------------------------//
        //------------------------------------Properties--------------------------------------//
        //------------------------------------------------------------------------------------//
        #region Properties
        #endregion
        //------------------------------------------------------------------------------------//
        //-----------------------------------Constructors-------------------------------------//
        //------------------------------------------------------------------------------------//
        #region Constructors
        /// <summary>
        /// Creates a new achievement repository
        /// </summary>
        /// <param name="dbContext">The context for DB communications</param>
        public AchievementRepository(JustPressPlayDBEntities dbContext)
            : base(dbContext)
        {
            entities = dbContext;
        }
        #endregion
        //------------------------------------------------------------------------------------//
        //---------------------------------Populate ViewModels--------------------------------//
        //------------------------------------------------------------------------------------//
        #region Populate ViewModels
        #endregion
        //------------------------------------------------------------------------------------//
        //------------------------------------Query Methods-----------------------------------//
        //------------------------------------------------------------------------------------//
        #region Query Methods
        #endregion
        //------------------------------------------------------------------------------------//
        //------------------------------------Insert/Delete-----------------------------------//
        //------------------------------------------------------------------------------------//
        #region Insert/Delete

        //-----Admin Insert/Delete-----//
        #region Admin Insert/Delete

        private void AdminCreateAchievement()//CreateAchievementViewModel createAchievementModel)
        {
        }


        #endregion
        //-----User Insert/Delete------//
        #region User Insert/Delete
        #endregion

        #endregion
        //------------------------------------------------------------------------------------//
        //-------------------------------------Persistence------------------------------------//
        //------------------------------------------------------------------------------------//
        #region Persistence
        public void Save()
        {
            entities.SaveChanges();
        }
        #endregion
        //------------------------------------------------------------------------------------//
        //-----------------------------------Helper Methods-----------------------------------//
        //------------------------------------------------------------------------------------//
        #region Helper Methods
        #endregion
        //------------------------------------------------------------------------------------//
        //------------------------------------JSON Methods------------------------------------//
        //------------------------------------------------------------------------------------//
        #region JSON Methods
        #endregion
        //------------------------------------------------------------------------------------//
        //---------------------------------System Achievements--------------------------------//
        //------------------------------------------------------------------------------------//
        #region System Achievements
        #endregion

    }

}