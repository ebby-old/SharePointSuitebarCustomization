using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Microsoft.SharePoint;
using SharePointSuitebarCustomization.Common;
using SharePointSuitebarCustomization.Models;

namespace SharePointSuitebarCustomization.ControlTemplates.SharePointSuitebarCustomization
{
    public partial class SuiteBrandingControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void Render(HtmlTextWriter writer)
        {
            var rootWeb = SPContext.Current.Site.RootWeb;
            var cacheName = rootWeb.Title.ToString(CultureInfo.InvariantCulture) + Constants.SuiteBarLeftName;

            var dataItems = Utilities.ReadCacheData(Cache, cacheName) as List<SuiteLinkItem>;

            if (dataItems == null)
            {
                var dataList = rootWeb.Lists.TryGetList(Constants.SuiteLinksDataListName);
                if (dataList == null)
                    return;

                var query = new SPQuery
                {
                    Query = string.Format(Constants.ItemQuery, Constants.SuiteBarLeftName)
                };
                var listItems = dataList.GetItems(query);

                dataItems = (from SPListItem item
                    in listItems
                             select new SuiteLinkItem(item)).ToList();

                Utilities.WriteCacheData(Cache, cacheName, dataItems, true);
            }

            Utilities.RenderSuiteLinkList(writer, dataItems);
        }
    }
}