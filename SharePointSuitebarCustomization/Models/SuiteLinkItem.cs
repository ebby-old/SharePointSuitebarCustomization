using System;
using Microsoft.SharePoint;
using SharePointSuitebarCustomization.Common;

namespace SharePointSuitebarCustomization.Models
{
    public class SuiteLinkItem
    {
        public SuiteLinkItem()
        {
        }

        public SuiteLinkItem(SPListItem item)
        {
            if (item == null)
                return;

            Index = Int32.Parse(item[Constants.IndexFieldName].ToString());

            var linkValue = new SPFieldUrlValue(item[Constants.LinkFieldName].ToString());
            LinkText = linkValue.Description;
            Link = linkValue.Url;

            var imageValue = new SPFieldUrlValue(item[Constants.ImageFieldName].ToString());
            LinkImage = imageValue.Url;

            SuitebarPosition = item[Constants.SuiteBarPositionName].ToString();
        }

        public int Index { get; set; }
        public string Link { get; set; }
        public string LinkText { get; set; }
        public string SuitebarPosition { get; set; }
        public string LinkImage { get; set; }
    }
}