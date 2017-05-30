using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy_Source.Models
{
    public class MenuItem
    {
        public string IconFile { get; set; }
        public string CommandLetter { get; set; }
        public string Description { get; set; }
        public MenuItemCategory Category { get; set; }
    }

    public enum MenuItemCategory
    {
        //MenuItems for main navigation menu
        Homepage,
        Lists,
        Businesses,
        Reports,
        Settings,
        About,

        //MenuItems for Menu "About"
        Help,
        Credits,

    }
    public class MenuItemManager
    {
        public static List<MenuItem> GetMenuItems()
        {
            var items = new List<MenuItem>();
            items.Add(new MenuItem() { IconFile = "\uE10F", CommandLetter = "T", Description = "rang Chủ", Category = MenuItemCategory.Homepage });
            items.Add(new MenuItem() { IconFile = "\uE14C", CommandLetter = "D", Description = "anh Sách", Category = MenuItemCategory.Lists });
            items.Add(new MenuItem() { IconFile = "\uE15E", CommandLetter = "N", Description = "ghiệp Vụ", Category = MenuItemCategory.Businesses });
            items.Add(new MenuItem() { IconFile = "\uE82D", CommandLetter = "B", Description = "áo Cáo", Category = MenuItemCategory.Reports });
            items.Add(new MenuItem() { IconFile = "\uE115", CommandLetter = "C", Description = "ài Đặt", Category = MenuItemCategory.Settings });

            return items;
        }
        public static List<MenuItem> GetAboutMenuItems()
        {
            var items = new List<MenuItem>();
            items.Add(new MenuItem { });
            return items;
        }
    }
}
