using centralconfig_webapi.library.Data;
using System;
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
        /// Retrieves a single configuration item. 
        /// If it doesn't exist for the given application, it attemps to get it for the default application (*).
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ConfigItem Get(ConfigItem request)
        {
            //  Our return result
            ConfigItem retval = new ConfigItem();

            //  ATTEMPT ONE:
            //	Get the application/name/machine combo
            var query = from item in _context.configitems
                        where 
                        item.application == request.Application 
                        && item.name == request.Name
                        && item.machine == request.Machine
                        select item;
            
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
                query = from item in _context.configitems
                            where
                            item.application == request.Application
                            && item.name == request.Name
                            && item.machine.Trim() == ""
                            select item;

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
                query = from item in _context.configitems
                        where
                        item.application == "*"
                        && item.name == request.Name
                        && item.machine.Trim() == ""
                        select item;

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
            ConfigItem retval = configItem;

            //  If we have a brand new item, add it:
            if (configItem.Id == 0)
            {
                var newItem = new configitem
                {
                    application = configItem.Application,
                    machine = configItem.Machine,
                    name = configItem.Name,
                    value = configItem.Value,
                    updated = DateTime.Now
                };

                _context.configitems.Add(newItem);
                _context.SaveChanges();

                retval.Id = newItem.id;
            }
            else
            {
                //  Otherwise, find the existing item and update it:
                var query = from item in _context.configitems
                            where item.id == configItem.Id
                            select item;

                if (query.Any())
                {
                    var item = query.FirstOrDefault();

                    item.application = configItem.Application;
                    item.name = configItem.Name;
                    item.value = configItem.Value;
                    item.machine = configItem.Machine;
                    item.updated = DateTime.Now;

                    _context.SaveChanges();
                }
            }            

            return retval;
        }

        /// <summary>
        /// Removes a single configuration item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public void Remove(ConfigItem request)
        {
            var query = from item in _context.configitems
                        where item.application.Trim() == request.Application.Trim()
                        && item.name.Trim() == request.Name.Trim()
                        && item.machine.Trim() == request.Machine.Trim()
                        select item;

            //  If we have a match, remove the first match:
            if (query.Any())
            {
                _context.configitems.Remove(query.FirstOrDefault());
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all configurations items
        /// </summary>
        /// <returns></returns>
        public List<ConfigItem> GetAll()
        {
            List<ConfigItem> retval = new List<ConfigItem>();

            var query = from item in _context.configitems
                        orderby item.application, item.name
                        select new ConfigItem()
                        {
                            Id = item.id,
                            Application = item.application,
                            Name = item.name,
                            Value = item.value,
                            Machine = item.machine,
                            Updated = item.updated
                        };

            //  If we have items, return them
            if (query.Any())
            {
                retval = query.ToList();
            }

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

            var query = from item in _context.configitems
                        where item.application == configItem.Application
                        orderby item.name
                        select new ConfigItem()
                        {
                            Id = item.id,
                            Application = item.application,
                            Name = item.name,
                            Value = item.value,
                            Machine = item.machine,
                            Updated = item.updated
                        };

            //  If we have items, return them
            if (query.Any())
            {
                retval = query.ToList();
            }

            return retval;
        }

        /// <summary>
        /// Retrieves all applications
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllApplications()
        {
            List<string> retval = new List<string>();

            var query = from item in _context.configitems
                        orderby item.application
                        select item.application;

            //  If we have items, return them
            if (query.Any())
            {
                retval = query.Distinct().ToList();
            }

            return retval;
        }
    }
}
