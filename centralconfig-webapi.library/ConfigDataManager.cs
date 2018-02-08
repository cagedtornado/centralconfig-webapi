﻿using centralconfig_webapi.library.Data;
using System.Collections.Generic;
using System.Linq;

namespace centralconfig_webapi.library
{
    public class ConfigDataManager
    {
        private CentralConfigDb _context;

        public ConfigDataManager(CentralConfigDb context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a single configuration item. If it doesn't exist for the given application, it attemps to get it for the default application (*).
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ConfigItem Get(ConfigItem request)
        {
            //  Our return result
            ConfigItem retval = new ConfigItem();

            //  ATTEMPT ONE:
            //	Get the application/name/machine combo
            var query = from items in _context.configitems
                        where 
                        items.application == request.Application 
                        && items.name == request.Name
                        && items.machine == request.Machine
                        select items;
            
            //  Execute the query and see the results:
            if (query.Any())
            {
                var item = query.FirstOrDefault();

                retval = new ConfigItem()
                {
                    Id = item.id,
                    Application = item.application,
                    Name = item.name,
                    Value = item.value,
                    Machine = item.machine,
                    Updated = item.updated
                };
            }

            //  ATTEMPT TWO:
            //	If we haven't found it, get the application/name combo with a blank machine name
            if (retval.Id == 0)
            {
                query = from items in _context.configitems
                            where
                            items.application == request.Application
                            && items.name == request.Name
                            && items.machine.Trim() == ""
                            select items;

                //  Execute the query and see the results:
                if (query.Any())
                {
                    var item = query.FirstOrDefault();

                    retval = new ConfigItem()
                    {
                        Id = item.id,
                        Application = item.application,
                        Name = item.name,
                        Value = item.value,
                        Machine = item.machine,
                        Updated = item.updated
                    };
                }
            }

            //  ATTEMPT THREE:
            //	If we still haven't found it, get the default application/name and blank machine name
            if (retval.Id == 0)
            {
                query = from items in _context.configitems
                        where
                        items.application == "*"
                        && items.name == request.Name
                        && items.machine.Trim() == ""
                        select items;

                //  Execute the query and see the results:
                if (query.Any())
                {
                    var item = query.FirstOrDefault();

                    retval = new ConfigItem()
                    {
                        Id = item.id,
                        Application = item.application,
                        Name = item.name,
                        Value = item.value,
                        Machine = item.machine,
                        Updated = item.updated
                    };
                }
            }

            return retval;
        }

        /// <summary>
        /// Sets the value of a single configuration item
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
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
        public ConfigItem Remove(ConfigItem configItem)
        {
            ConfigItem retval = new ConfigItem();

            return retval;
        }

        /// <summary>
        /// Retrieves all configurations items
        /// </summary>
        /// <returns></returns>
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
        public List<ConfigItem> GetAllForApp(ConfigItem configItem)
        {
            List<ConfigItem> retval = new List<ConfigItem>();

            return retval;
        }

        /// <summary>
        /// Retrieves all applications
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllApplications()
        {
            List<string> retval = new List<string>();

            return retval;
        }
    }
}
