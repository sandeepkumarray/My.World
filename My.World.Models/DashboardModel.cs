using System;
using System.Collections.Generic;
using System.Text;

namespace My.World.Api.Models
{
    public class DashboardModel
    {
        public Int64 User_Id { get; set; }
        public Int32 buildings_total { get; set; }
        public Int32 characters_total { get; set; }
        public Int32 conditions_total { get; set; }
        public Int32 continents_total { get; set; }
        public Int32 countries_total { get; set; }
        public Int32 creatures_total { get; set; }
        public Int32 deities_total { get; set; }
        public Int32 floras_total { get; set; }
        public Int32 foods_total { get; set; }
        public Int32 governments_total { get; set; }
        public Int32 groups_total { get; set; }
        public Int32 items_total { get; set; }
        public Int32 jobs_total { get; set; }
        public Int32 landmarks_total { get; set; }
        public Int32 languages_total { get; set; }
        public Int32 locations_total { get; set; }
        public Int32 lores_total { get; set; }
        public Int32 magics_total { get; set; }
        public Int32 planets_total { get; set; }
        public Int32 races_total { get; set; }
        public Int32 religions_total { get; set; }
        public Int32 scenes_total { get; set; }
        public Int32 sports_total { get; set; }
        public Int32 technologies_total { get; set; }
        public Int32 towns_total { get; set; }
        public Int32 traditions_total { get; set; }
        public Int32 universes_total { get; set; }
        public Int32 vehicles_total { get; set; }
        public object this[string property]
        {
            get
            {
                return this.GetType().GetProperty(property).GetValue(this, null);
            }
            set
            {
                this.GetType().GetProperty(property).SetValue(this, value, null);
            }
        }

    }
}
