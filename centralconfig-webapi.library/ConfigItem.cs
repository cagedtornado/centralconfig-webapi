using System;
using System.Runtime.Serialization;

namespace centralconfig_webapi.library
{
    /// <summary>
    /// Represents a single configuration item
    /// </summary>
    [DataContract]
    public class ConfigItem
    {
        public ConfigItem()
        {
            Application = "";
            Machine = "";
            Name = "";
            Value = "";
        }

        /// <summary>
        /// The unique id for this config item
        /// </summary>
        [DataMember(Name = "id")]
        public long Id { get; set; }

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
