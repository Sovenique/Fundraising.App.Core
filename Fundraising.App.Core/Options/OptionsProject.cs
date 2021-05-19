﻿using Fundraising.App.Core.Entities;
using System;
using System.Collections.Generic;


namespace Fundraising.App.Core.Options
{
    public class OptionsProject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public Member Creator { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal AmountGathered { get; set; }
        public decimal TargetAmount { get; set; }
        public List<Reward> Rewards { get; set; }

        public OptionsProject() { }
        public OptionsProject(Project project)
        {
            if (project != null)
            {
                Title = project.Title;
                Description = project.Description;
                Category = project.Category;
                ProjectStatus = project.ProjectStatus;
                Creator = project.Creator;
                TargetAmount = project.TargetAmount;
                Rewards = project.Rewards;
            }

        }


    }

}

