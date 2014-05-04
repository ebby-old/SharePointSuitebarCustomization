using System;
using System.Collections.Generic;
using System.Web.Caching;
using System.Web.UI;
using Microsoft.SharePoint;
using SharePointSuitebarCustomization.Models;

namespace SharePointSuitebarCustomization.Common
{
    public static class Utilities
    {
        private static readonly object Lock = new object();
        public static void RenderSuiteLinkList(HtmlTextWriter writer, List<SuiteLinkItem> linkItems)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-core-suiteLinkList");
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);

            foreach (var suiteLinkItem in linkItems)
            {
                RenderSuiteLink(writer, suiteLinkItem.Link, suiteLinkItem.LinkText,
                    suiteLinkItem.Link.Replace(' ', '_'), suiteLinkItem.LinkImage);
            }

            writer.RenderEndTag();
        }

        public static void RenderSuiteLink(HtmlTextWriter writer, string url, string name, string linkId,
            string imageUrl)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-core-suiteLink");
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-core-suiteLink-a");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, url);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, linkId);
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-verticalAlignMiddle");

            if (imageUrl != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src, imageUrl);
                writer.AddStyleAttribute("height", "20px");
                writer.AddStyleAttribute("padding-right", "5px");
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(name);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        public static Object ReadCacheData(Cache cache, string cacheName)
        {
            if (cache == null)
            {
                throw new ArgumentOutOfRangeException("cache");
            }

            return cache[cacheName];
        }

        public static void WriteCacheData(Cache cache, string cacheName, Object dataToBeCached, bool forceWrite)
        {
            if (cache == null || string.IsNullOrEmpty(cacheName) || dataToBeCached == null) return;

            lock (Lock)
            {
                var cacheData = cache[cacheName];

                if (cacheData != null && forceWrite != true) return;

                var cacheTimeOut = 60.00D; //Default Cache Timeout value
                //TODO: Make the cacheTimeout value configurable.
                
                cache.Add(cacheName, dataToBeCached, null, DateTime.Now.AddMinutes(cacheTimeOut),
                          Cache.NoSlidingExpiration, CacheItemPriority.High,
                          delegate
                          { });
            }
        }
    }
}