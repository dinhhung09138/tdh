using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDH.Common;
using TDH.DataAccess;
using TDH.Model.Personal;
using Utils;

namespace TDH.Services.Personal
{
    public class TimelineService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services/Personal/TimelineService.cs";

        #endregion

        /// <summary>
        /// Get all item
        /// </summary>
        /// <returns></returns>
        public List<TimelineModel> GetAll(Guid userID)
        {
            try
            {
                List<TimelineModel> _return = new List<TimelineModel>();
                using (var context = new TDHEntities())
                {
                    var _list = context.V_TIMELINE.Where(m => m.created_by == userID).OrderByDescending(m => m.date).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new TimelineModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            School = item.school,
                            Duration = item.duration,
                            Description = item.description,
                            Date = item.date,
                            DateString = item.date.DateToString(),
                            IsCancel = item.is_cancel,
                            IsFinish = item.is_finish,
                            IsPlan = item.is_plan,
                            MonthYear = int.Parse(item.date.DateToString("yyyyMM")),
                            TypeCode = item.type_code,
                            TypeName = item.type_name
                        });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetAll", userID, ex);
                throw new ApplicationException();
            }
        }

    }
}
