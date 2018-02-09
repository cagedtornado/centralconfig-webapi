using centralconfig_webapi.library;
using System.Collections.Generic;
using System.Web.Http;
using centralconfig_webapi.library.Data;

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
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("get")]
        [HttpPost]
        public ConfigResponse<ConfigItem> Get(ConfigItem request)
        {
            ConfigResponse<ConfigItem> retval = new ConfigResponse<ConfigItem>();

            using (var db = new CentralConfigDb())
            {
                ConfigDataManager manager = new ConfigDataManager(db);
                retval.Data = manager.Get(request);
                retval.Status = System.Net.HttpStatusCode.OK;
                retval.Message = "Config item found";
            }

            return retval;
        }

        /// <summary>
        /// Sets the value of a single configuration item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("set")]
        [HttpPost]
        public ConfigResponse<ConfigItem> Set(ConfigItem request)
        {
            ConfigResponse<ConfigItem> retval = new ConfigResponse<ConfigItem>();

            using (var db = new CentralConfigDb())
            {
                ConfigDataManager manager = new ConfigDataManager(db);
                retval.Data = manager.Set(request);
                retval.Status = System.Net.HttpStatusCode.OK;
                retval.Message = "Config item updated";
            }

            return retval;
        }

        /// <summary>
        /// Removes a single configuration item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("remove")]
        [HttpPost]
        public ConfigResponse<ConfigItem> Remove(ConfigItem request)
        {
            ConfigResponse<ConfigItem> retval = new ConfigResponse<ConfigItem>();

            using (var db = new CentralConfigDb())
            {
                ConfigDataManager manager = new ConfigDataManager(db);
                manager.Remove(request);

                retval.Data = request;
                retval.Status = System.Net.HttpStatusCode.OK;
                retval.Message = "Config item removed";
            }

            return retval;
        }

        /// <summary>
        /// Retrieves all configurations items
        /// </summary>
        /// <returns></returns>
        [Route("getall")]
        [HttpGet]
        public ConfigResponse<List<ConfigItem>> GetAll()
        {
            ConfigResponse<List<ConfigItem>> retval = new ConfigResponse<List<ConfigItem>>();

            using (var db = new CentralConfigDb())
            {
                ConfigDataManager manager = new ConfigDataManager(db);
                retval.Data = manager.GetAll();
                retval.Status = System.Net.HttpStatusCode.OK;
                retval.Message = "Config items found";
            }

            return retval;
        }

        /// <summary>
        /// Retrieves all configurations items for a given application
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("getallforapp")]
        [HttpPost]
        public ConfigResponse<List<ConfigItem>> GetAllForApp(ConfigItem request)
        {
            ConfigResponse<List<ConfigItem>> retval = new ConfigResponse<List<ConfigItem>>();

            using (var db = new CentralConfigDb())
            {
                ConfigDataManager manager = new ConfigDataManager(db);
                retval.Data = manager.GetAllForApp(request);
                retval.Status = System.Net.HttpStatusCode.OK;
                retval.Message = "Config items found";
            }

            return retval;
        }

        /// <summary>
        /// Retrieves all applications
        /// </summary>
        /// <returns></returns>
        [Route("~/applications/getall")]
        [HttpGet]
        public ConfigResponse<List<string>> GetAllApplications()
        {
            ConfigResponse<List<string>> retval = new ConfigResponse<List<string>>();

            using (var db = new CentralConfigDb())
            {
                ConfigDataManager manager = new ConfigDataManager(db);
                retval.Data = manager.GetAllApplications();
                retval.Status = System.Net.HttpStatusCode.OK;
                retval.Message = "Applications found";
            }

            return retval;
        }
    }
}
