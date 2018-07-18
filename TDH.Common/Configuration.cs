using System;
using System.Linq;
using TDH.DataAccess;

namespace TDH.Common
{
    public class Configuration
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private static readonly string FILE_NAME = "Common/Configuration.cs";

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
                using (var context = new TDHEntities())
                {
                    var _item = context.CONFIGURATIONs.FirstOrDefault(m => m.key == key);
                    if (_item != null)
                    {
                        return _item.value;
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "SettingValue", new Guid(), ex);
                throw new ApplicationException();
            }
        }

    }
}
