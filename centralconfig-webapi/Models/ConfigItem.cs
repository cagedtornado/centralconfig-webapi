using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace centralconfig_webapi.Models
{
    /// <summary>
    /// Represents a single configuration item
    /// </summary>
    [DataContract]
    public class ConfigItem
    {
        /// <summary>
        /// The unique id for this config item
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// The application name that the config item is associated with
        /// </summary>
        [DataMember(Name = "application")]
        public string Application { get; set; }

        /// <summary>
        /// The optional machine name this config item is associated with
        /// </summary>
        [DataMember(Name = "machine")]
        public string Machine { get; set; }

        /// <summary>
        /// The config item name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The config item value
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }

        /// <summary>
        /// The last time this config item was updated
        /// </summary>
        [DataMember(Name = "updated")]
        public DateTime Updated { get; set; }
    }
}