﻿using Newtonsoft.Json;
using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace U13SK.ContentNodeIcons.Database
{
    [TableName("U13SK_ContentNodeIcons")]
    [PrimaryKey("ContentId", AutoIncrement = false)]
    [ExplicitColumns]
    public class Schema
    {
        [Column("ContentId")]
        [PrimaryKeyColumn(AutoIncrement = false)]
        [JsonProperty("contentId")]
        public int ContentId { get; set; }

        [Column("Icon")]
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [Column("IconColor")]
        [JsonProperty("iconColor")]
        public string IconColor { get; set; }
    }
}
