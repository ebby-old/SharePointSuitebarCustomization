namespace SharePointSuitebarCustomization.Common
{
    public static class Constants
    {
        public const string SuiteLinksDataListName = "Suite Links";
        public const string SuiteBarLeftName = "SuiteBarLeft";
        public const string SuiteBarLinksBoxName = "SuiteLinksBox";

        public const string IndexFieldName = "Index";
        public const string LinkFieldName = "Link";
        public const string SuiteBarPositionName = "Suite";
        public const string ImageFieldName = "Image";

        public const string ItemQuery = "<Where>" +
                                        "   <Eq><FieldRef Name='Suite' /><Value Type='Choice'>{0}</Value></Eq>" +
                                        "</Where>" +
                                        "<OrderBy><FieldRef Name='Index' /></OrderBy>";
    }
}