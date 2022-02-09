using My.World.Api.Models;
using My.World.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.World.Web.ViewModel
{
    public class DashboardViewModel
    {
        private SiteTemplateModel siteTemplateModel;

        public List<DashboardItem> DashboardItemList { get; set; }
        public List<ContentPlansModel> ContentPlans { get; set; }
        public ContentPlansModel UserPlan { get; set; }
        public List<DashboardItem> DashboardCreateItemList { get; set; }
        public List<DashboardRecentModel> DashboardRecentList { get; set; }

        public List<ContentTypesModel> ContentTypesList { get; set; }
        public DashboardViewModel()
        {
            DashboardItemList = new List<DashboardItem>();
            ContentPlans = new List<ContentPlansModel>();
            DashboardCreateItemList = new List<DashboardItem>();
            DashboardRecentList = new List<DashboardRecentModel>();
            ContentTypesList = new List<ContentTypesModel>();
        }

        public DashboardViewModel(DashboardModel dashboardModel, SiteTemplateModel siteTemplateModel) : this()
        {
            this.siteTemplateModel = siteTemplateModel;
        }

        public void GetDashboardCreateList(DashboardModel dashboardModel)
        {
            var content_list = ContentTypesList;

            var random = new Random();

            int indexOne = random.Next(1, content_list.Count());
            int indexTwo = random.Next(1, content_list.Count());
            int indexThree = random.Next(1, content_list.Count());

            do
            {
                indexTwo = random.Next(1, content_list.Count());
                indexThree = random.Next(1, content_list.Count());
            } while (indexOne == indexTwo || indexOne == indexThree || indexTwo == indexThree);

            var itemOne = content_list[indexOne];
            var itemTwo = content_list[indexTwo];
            var itemThree = content_list[indexThree];

            DashboardCreateItemList.Add(new DashboardItem()
            {
                Header = itemOne.name,
                icon = itemOne.icon,
                primary_color = itemOne.primary_color,
                CountString = "You've created " + Convert.ToString(dashboardModel[itemOne.name.ToLower() + "_total"]) + " " + itemOne.name,
                ItemKey = itemOne.name,
                Controller = itemOne.name,
                Action = "View" + itemOne.name
            });

            DashboardCreateItemList.Add(new DashboardItem()
            {
                Header = itemTwo.name,
                icon = itemTwo.icon,
                primary_color = itemTwo.primary_color,
                CountString = "You've created " + Convert.ToString(dashboardModel[itemTwo.name.ToLower() + "_total"]) + " " + itemTwo.name,
                ItemKey = itemTwo.name,
                Controller = itemTwo.name,
                Action = "View" + itemTwo.name
            });

            DashboardCreateItemList.Add(new DashboardItem()
            {
                Header = itemThree.name,
                icon = itemThree.icon,
                primary_color = itemThree.primary_color,
                CountString = "You've created " + Convert.ToString(dashboardModel[itemThree.name.ToLower() + "_total"]) + " " + itemThree.name,
                ItemKey = itemThree.name,
                Controller = itemThree.name,
                Action = "View" + itemThree.name
            });
        }

        public void GetDashboardFromPlans(DashboardModel dashboardModel)
        {
            var content_list = siteTemplateModel.PlanContentList.OrderBy(i => i.OrderId);
            var content_type_list = ContentTypesList;

            foreach (var item in content_list)
            {
                DashboardItemList.Add(new DashboardItem()
                {
                    Header = item.name,
                    CountString = Convert.ToString(dashboardModel[item.name.ToLower() + "_total"]),
                    Color = content_type_list.Find(c=>c.name.ToLower() == item.name.ToLower()).primary_color,
                    icon = content_type_list.Find(c => c.name.ToLower() == item.name.ToLower()).icon,
                    ItemKey = item.name,
                    Controller = item.name,
                    Action = "View" + item.name
                });
            }
        }

        private void CaptureItems(DashboardModel dashboardModel)
        {
            DashboardItemList.Add(new DashboardItem() { Header = "Buildings", CountString = Convert.ToString(dashboardModel.buildings_total), ItemKey = "Buildings", Controller = "Building", Action = "ViewBuilding" });
            DashboardItemList.Add(new DashboardItem() { Header = "Characters", CountString = Convert.ToString(dashboardModel.characters_total), ItemKey = "Characters", Controller = "Character", Action = "ViewCharacter" });
            DashboardItemList.Add(new DashboardItem() { Header = "Conditions", CountString = Convert.ToString(dashboardModel.conditions_total), ItemKey = "Conditions", Controller = "Condition", Action = "ViewCondition" });
            DashboardItemList.Add(new DashboardItem() { Header = "Continents", CountString = Convert.ToString(dashboardModel.continents_total), ItemKey = "Continents", Controller = "Continent", Action = "ViewContinent" });
            DashboardItemList.Add(new DashboardItem() { Header = "Countries", CountString = Convert.ToString(dashboardModel.countries_total), ItemKey = "Countries", Controller = "Countrie", Action = "ViewCountrie" });
            DashboardItemList.Add(new DashboardItem() { Header = "Creatures", CountString = Convert.ToString(dashboardModel.creatures_total), ItemKey = "Creatures", Controller = "Creature", Action = "ViewCreature" });
            DashboardItemList.Add(new DashboardItem() { Header = "Deities", CountString = Convert.ToString(dashboardModel.deities_total), ItemKey = "Deities", Controller = "Deitie", Action = "ViewDeitie" });
            DashboardItemList.Add(new DashboardItem() { Header = "Floras", CountString = Convert.ToString(dashboardModel.floras_total), ItemKey = "Floras", Controller = "Flora", Action = "ViewFlora" });
            DashboardItemList.Add(new DashboardItem() { Header = "Foods", CountString = Convert.ToString(dashboardModel.foods_total), ItemKey = "Foods", Controller = "Food", Action = "ViewFood" });
            DashboardItemList.Add(new DashboardItem() { Header = "Governments", CountString = Convert.ToString(dashboardModel.governments_total), ItemKey = "Governments", Controller = "Government", Action = "ViewGovernment" });
            DashboardItemList.Add(new DashboardItem() { Header = "Groups", CountString = Convert.ToString(dashboardModel.groups_total), ItemKey = "Groups", Controller = "Group", Action = "ViewGroup" });
            DashboardItemList.Add(new DashboardItem() { Header = "Items", CountString = Convert.ToString(dashboardModel.items_total), ItemKey = "Items", Controller = "Item", Action = "ViewItem" });
            DashboardItemList.Add(new DashboardItem() { Header = "Jobs", CountString = Convert.ToString(dashboardModel.jobs_total), ItemKey = "Jobs", Controller = "Job", Action = "ViewJob" });
            DashboardItemList.Add(new DashboardItem() { Header = "Landmarks", CountString = Convert.ToString(dashboardModel.landmarks_total), ItemKey = "Landmarks", Controller = "Landmark", Action = "ViewLandmark" });
            DashboardItemList.Add(new DashboardItem() { Header = "Languages", CountString = Convert.ToString(dashboardModel.languages_total), ItemKey = "Languages", Controller = "Language", Action = "ViewLanguage" });
            DashboardItemList.Add(new DashboardItem() { Header = "Locations", CountString = Convert.ToString(dashboardModel.locations_total), ItemKey = "Locations", Controller = "Location", Action = "ViewLocation" });
            DashboardItemList.Add(new DashboardItem() { Header = "Lores", CountString = Convert.ToString(dashboardModel.lores_total), ItemKey = "Lores", Controller = "Lore", Action = "ViewLore" });
            DashboardItemList.Add(new DashboardItem() { Header = "Magics", CountString = Convert.ToString(dashboardModel.magics_total), ItemKey = "Magics", Controller = "Magic", Action = "ViewMagic" });
            DashboardItemList.Add(new DashboardItem() { Header = "Planets", CountString = Convert.ToString(dashboardModel.planets_total), ItemKey = "Planets", Controller = "Planet", Action = "ViewPlanet" });
            DashboardItemList.Add(new DashboardItem() { Header = "Races", CountString = Convert.ToString(dashboardModel.races_total), ItemKey = "Races", Controller = "Race", Action = "ViewRace" });
            DashboardItemList.Add(new DashboardItem() { Header = "Religions", CountString = Convert.ToString(dashboardModel.religions_total), ItemKey = "Religions", Controller = "Religion", Action = "ViewReligion" });
            DashboardItemList.Add(new DashboardItem() { Header = "Scenes", CountString = Convert.ToString(dashboardModel.scenes_total), ItemKey = "Scenes", Controller = "Scene", Action = "ViewScene" });
            DashboardItemList.Add(new DashboardItem() { Header = "Sports", CountString = Convert.ToString(dashboardModel.sports_total), ItemKey = "Sports", Controller = "Sport", Action = "ViewSport" });
            DashboardItemList.Add(new DashboardItem() { Header = "Technologies", CountString = Convert.ToString(dashboardModel.technologies_total), ItemKey = "Technologies", Controller = "Technologie", Action = "ViewTechnologie" });
            DashboardItemList.Add(new DashboardItem() { Header = "Towns", CountString = Convert.ToString(dashboardModel.towns_total), ItemKey = "Towns", Controller = "Town", Action = "ViewTown" });
            DashboardItemList.Add(new DashboardItem() { Header = "Traditions", CountString = Convert.ToString(dashboardModel.traditions_total), ItemKey = "Traditions", Controller = "Tradition", Action = "ViewTradition" });
            DashboardItemList.Add(new DashboardItem() { Header = "Universes", CountString = Convert.ToString(dashboardModel.universes_total), ItemKey = "Universes", Controller = "Universe", Action = "ViewUniverse" });
            DashboardItemList.Add(new DashboardItem() { Header = "Vehicles", CountString = Convert.ToString(dashboardModel.vehicles_total), ItemKey = "Vehicles", Controller = "Vehicle", Action = "ViewVehicle" });

        }
    }

    public class DashboardItem : DashboardRecentModel
    {
        public string Header;
        public string CountString;
        public string ItemKey;
        public string Controller;
        public string Action;
        public string Color;
    }
}

