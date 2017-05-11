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
            items.Add(new MenuItem() { IconFile = "\uE10F", Description = " Trang Chủ", Category = MenuItemCategory.Homepage });
            items.Add(new MenuItem() { IconFile = "\uE14C", Description = " Danh Sách", Category = MenuItemCategory.Lists });
            items.Add(new MenuItem() { IconFile = "\uE15E", Description = " Nghiệp Vụ", Category = MenuItemCategory.Businesses });
            items.Add(new MenuItem() { IconFile = "\uE82D", Description = " Báo Cáo", Category = MenuItemCategory.Reports });
            items.Add(new MenuItem() { IconFile = "\uE115", Description = " Cài Đặt", Category = MenuItemCategory.Settings });

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
