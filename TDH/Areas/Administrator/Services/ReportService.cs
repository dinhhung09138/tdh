using System;
using System.Collections.Generic;
using System.Linq;
using TDH.Models;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using Utils;

namespace TDH.Areas.Administrator.Services
{
    public class ReportService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator/Services/IdeaService.cs";

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
                DataTableResponse<ReportModel> _itemResponse = new DataTableResponse<ReportModel>();
                //List of data
                List<ReportModel> _list = new List<ReportModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _lData = context.REPORTs.Where(m => !m.deleted).OrderByDescending(m => m.create_date).Select(m => new
                    {
                        m.id,
                        m.title,
                        full_name = context.USERs.FirstOrDefault(u => u.id == m.create_by).full_name,
                        m.create_date
                    }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.title.ToLower().Contains(searchValue) || m.full_name.ToLower().Contains(searchValue) || m.create_date.ToString().Contains(searchValue)).ToList();
                    }
                    int _count = 0;
                    foreach (var item in _lData)
                    {
                        _count = context.REPORT_COMMENT.Count(m => m.report_id == item.id && !m.deleted);
                        _list.Add(new ReportModel()
                        {
                            ID = item.id,
                            Title = item.title,
                            UserCreate = item.full_name,
                            CreateDate = item.create_date,
                            CreateDateString = item.create_date.DateToString(),
                            Count = _count.NumberToString()
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<ReportModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Title":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Title) : _sortList.Sort(col.Dir, m => m.Title);
                                    break;
                                case "UserCreate":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.UserCreate) : _sortList.Sort(col.Dir, m => m.UserCreate);
                                    break;
                                case "CreateDateString":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.CreateDate) : _sortList.Sort(col.Dir, m => m.CreateDate);
                                    break;
                                case "Count":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Count) : _sortList.Sort(col.Dir, m => m.Count);
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
                TDH.Services.Log.WriteLog(FILE_NAME, "List", userID, ex);
                throw new ApplicationException();
            }

            return _return;
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <returns></returns>
        public List<ReportModel> GetAll(Guid userID)
        {
            try
            {
                List<ReportModel> _return = new List<ReportModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = context.REPORTs.Where(m => !m.deleted).OrderByDescending(m => m.create_date).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new ReportModel() { ID = item.id, Title = item.title });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "GetAll", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ReportModel. Throw exception if not found or get some error</returns>
        public ReportModel GetItemByID(ReportModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    REPORT _md = context.REPORTs.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    return new ReportModel()
                    {
                        ID = _md.id,
                        Title = _md.title,
                        Content = _md.content,
                        CreateDate = _md.create_date,
                        CreateDateString = _md.create_date.DateToString(),
                        UserCreate = context.USERs.FirstOrDefault(u => u.id == _md.create_by).full_name
                    };
                }
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "GetItemByID", model.CreateBy, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(ReportModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            REPORT _md = new REPORT();
                            if (model.Insert)
                            {
                                _md.id = Guid.NewGuid();
                            }
                            else
                            {
                                _md = context.REPORTs.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                                if (_md == null)
                                {
                                    throw new FieldAccessException();
                                }
                            }
                            _md.title = model.Title;
                            _md.content = model.Content;
                            if (model.Insert)
                            {
                                _md.create_by = model.CreateBy;
                                _md.create_date = DateTime.Now;
                                context.REPORTs.Add(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _md.update_by = model.UpdateBy;
                                _md.update_date = DateTime.Now;
                                context.REPORTs.Attach(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Modified;
                            }
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                throw new ApplicationException();
            }
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(ReportModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            REPORT _md = context.REPORTs.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            _md.deleted = true;
                            _md.delete_by = model.DeleteBy;
                            _md.delete_date = DateTime.Now;
                            context.REPORTs.Attach(_md);
                            context.Entry(_md).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "Delete", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Delete", model.CreateBy, ex);
                throw new ApplicationException();
            }
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Check Delete item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(ReportModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    REPORT_COMMENT _md = context.REPORT_COMMENT.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        return ResponseStatusCodeHelper.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDelete", model.CreateBy, ex);
                throw new ApplicationException();
            }
            return ResponseStatusCodeHelper.NG;
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <returns></returns>
        public List<ReportCommentModel> GetAllComment(Guid reportID, Guid userID)
        {
            try
            {
                List<ReportCommentModel> _return = new List<ReportCommentModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = context.REPORT_COMMENT.Where(m => m.report_id == reportID && !m.deleted).OrderBy(m => m.create_date).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new ReportCommentModel()
                        {
                            ID = item.id,
                            Title = item.title,
                            Content = item.content,
                            UserCreate = context.USERs.FirstOrDefault(u => u.id == item.create_by).full_name,
                            CreateDate = item.create_date,
                            CreateDateString = item.create_date.DateToString()
                        });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "GetAllComment", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Save comment model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper SaveComment(ReportCommentModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var _rp = context.REPORTs.FirstOrDefault(m => m.id == model.ReportID && !m.deleted);
                            if (_rp == null)
                            {
                                throw new FieldAccessException();
                            }
                            REPORT_COMMENT _md = new REPORT_COMMENT();
                            _md.id = Guid.NewGuid();
                            _md.report_id = model.ReportID;
                            _md.title = model.Title;
                            _md.content = model.Content;
                            _md.create_by = model.CreateBy;
                            _md.create_date = DateTime.Now;
                            _md.create_by = model.CreateBy;
                            _md.create_date = DateTime.Now;
                            context.REPORT_COMMENT.Add(_md);
                            context.Entry(_md).State = System.Data.Entity.EntityState.Added;
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "SaveComment", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                throw new ApplicationException();
            }
            return ResponseStatusCodeHelper.Success;
        }

    }
}