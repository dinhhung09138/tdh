using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDH.Models;

namespace TDH.Areas.Administrator.Common
{
    public static class Configuration
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private static readonly string FILE_NAME = "Administrator/Common/Configuration.cs";
        
        #endregion

        /// <summary>
        /// Get config setting value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string SettingValue(string key)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _item = context.CONFIGURATIONs.FirstOrDefault(m => m.key == key);
                    if(_item != null)
                    {
                        return _item.value;
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "SettingValue", new Guid(), ex);
                throw new ApplicationException();
            }
        }

    }
}