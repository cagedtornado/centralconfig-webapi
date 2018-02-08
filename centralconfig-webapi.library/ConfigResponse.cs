﻿using System.Net;
using System.Runtime.Serialization;

namespace centralconfig_webapi.library
{
    [DataContract]
    public class ConfigResponse<T>
    {
        /// <summary>
        /// The response HTTP code status
        /// </summary>
        [DataMember(Name = "status")]
        public HttpStatusCode Status
        { get; set; }

        /// <summary>
        /// The response status message
        /// </summary>
        [DataMember(Name = "message")]
        public string Message
        { get; set; }

        /// <summary>
        /// The response data (if any)
        /// </summary>
        [DataMember(Name = "data")]
        public T Data
        { get; set; }
    }
}
