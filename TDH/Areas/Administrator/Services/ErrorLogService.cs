using System;
using System.Collections.Generic;
using System.Linq;
using TDH.Models;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using TDH.Areas.Administrator.Common;
using Utils;

namespace TDH.Areas.Administrator.Services
{
    public class ErrorLogService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator/Services/ErrorLogService.cs";

        #endregion


        /// <summary>
        /// Get list data using jquery datatable
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userID">User id</param>
        /// <returns><string, object></returns>
        public Dictionary<string, object> List(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<ErrorLogModel> _itemResponse = new DataTableResponse<ErrorLogModel>();
                //List of data
                List<ErrorLogModel> _list = new List<ErrorLogModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _lData = context.ERROR_LOG.Select(m => new { m.id, m.file_name, m.method_name, m.message, m.create_date }).OrderByDescending(m => m.create_date).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.file_name.ToLower().Contains(searchValue) ||
                                         m.method_name.ToLower().Contains(searchValue) ||
                                         m.message.ToLower().Contains(searchValue)).ToList();
                    }
                    foreach (var item in _lData)
                    {
                        _list.Add(new ErrorLogModel()
                        {
                            ID = item.id,
                            FileName = item.file_name,
                            MethodName = item.method_name,
                            Message = item.message,
                            Date = item.create_date,
                            DateString = item.create_date.DateToString("dd/MM/yyyy HH:mm:ss")
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<ErrorLogModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "FileName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.FileName) : _sortList.Sort(col.Dir, m => m.FileName);
                                    break;
                                case "MethodName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.MethodName) : _sortList.Sort(col.Dir, m => m.MethodName);
                                    break;
                                case "Message":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Message) : _sortList.Sort(col.Dir, m => m.Message);
                                    break;
                                case "DateString":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Date) : _sortList.Sort(col.Dir, m => m.Date);
                                    break;
                            }
                        }
                        _itemResponse.data = _sortList.Skip(request.start).Take(request.length).ToList();
                    }
                    else
                    {
                        _itemResponse.data = _list.Skip(request.start).Take(request.length).ToList();
                    }
                    _return.Add(DatatableCommonSetting.Response.DATA, _itemResponse);
                }
                _return.Add(DatatableCommonSetting.Response.STATUS, ResponseStatusCodeHelper.OK);
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "List", userID, ex);
                throw new ApplicationException();
            }

            return _return;
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>RoleModel. Throw exception if not found or get some error</returns>
        public ErrorLogModel GetItemByID(ErrorLogModel model)
        {
            ErrorLogModel _return = new ErrorLogModel() { ID = Guid.NewGuid() };
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _item = context.ERROR_LOG.FirstOrDefault(m => m.id == model.ID);
                    if(_item != null)
                    {
                        _return = new ErrorLogModel()
                        {
                            ID = _item.id,
                            FileName = _item.file_name,
                            Message = _item.message,
                            MethodName = _item.method_name,
                            DateString = _item.create_date.DateToString("dd/MM/yyyy HH:mm:ss"),
                            InnerException = _item.inner_exception,
                            StackTrace = _item.stack_trace
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "GetItemByID", model.CreateBy, ex);
                throw new ApplicationException();
            }
            return _return;
        }

    }
}