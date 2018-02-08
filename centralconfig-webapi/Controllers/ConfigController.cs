using centralconfig_webapi.library;
using System.Collections.Generic;
using System.Web.Http;

namespace centralconfig_webapi.Controllers
{
    /// <summary>
    /// Configuration operations
    /// </summary>
    [RoutePrefix("config")]
    public class ConfigController : ApiController
    {
        /// <summary>
        /// Retrieves a single configuration item. If it doesn't exist for the given application, it attemps to get it for the default application (*).
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        [Route("get")]
        [HttpPost]
        public ConfigItem Get(ConfigItem configItem)
        {
            ConfigItem retval = new ConfigItem();

            return retval;
        }

        /// <summary>
        /// Sets the value of a single configuration item
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        [Route("set")]
        [HttpPost]
        public ConfigItem Set(ConfigItem configItem)
        {
            ConfigItem retval = new ConfigItem();

            return retval;
        }

        /// <summary>
        /// Removes a single configuration item
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        [Route("remove")]
        [HttpPost]
        public ConfigItem Remove(ConfigItem configItem)
        {
            ConfigItem retval = new ConfigItem();

            return retval;
        }

        /// <summary>
        /// Retrieves all configurations items
        /// </summary>
        /// <returns></returns>
        [Route("getall")]
        [HttpGet]
        public List<ConfigItem> GetAll()
        {
            List<ConfigItem> retval = new List<ConfigItem>();

            return retval;
        }

        /// <summary>
        /// Retrieves all configurations items for a given application
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        [Route("getallforapp")]
        [HttpPost]
        public List<ConfigItem> GetAllForApp(ConfigItem configItem)
        {
            List<ConfigItem> retval = new List<ConfigItem>();

            return retval;
        }

        /// <summary>
        /// Retrieves all applications
        /// </summary>
        /// <returns></returns>
        [Route("~/applications/getall")]
        [HttpGet]
        public List<string> GetAllApplications()
        {
            List<string> retval = new List<string>();

            return retval;
        }
    }
}
