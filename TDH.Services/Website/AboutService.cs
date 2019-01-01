using System;
using System.Data.Entity;
using System.Linq;
using TDH.Model.Website;
using TDH.DataAccess;
using TDH.Common;
using Utils;
using TDH.Common.UserException;

namespace TDH.Services.Website
{
    /// <summary>
    /// About service
    /// </summary>
    public class AboutService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Website/AboutService.cs";

        #endregion

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">About model</param>
        /// <returns>AboutModel</returns>
        public AboutModel GetItemByID(AboutModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_ABOUT _md = _context.WEB_ABOUT.FirstOrDefault(m => !m.deleted);
                    if (_md == null)
                    {
                        _md = new WEB_ABOUT()
                        {
                            id = Guid.NewGuid(),
                            content = "",
                            link = "",
                            image = "",
                            meta_title = "",
                            meta_description = "",
                            meta_keywords = "",
                            meta_article_name = "",
                            meta_article_publish = DateTime.Now,
                            meta_article_section = "",
                            meta_article_tag = "",
                            meta_next = "",
                            meta_og_image = "",
                            meta_og_site_name = "",
                            meta_twitter_image = "",
                            create_by = model.CreateBy,
                            create_date = DateTime.Now,
                            deleted = false,
                        };
                        _context.WEB_ABOUT.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                        _context.SaveChanges();
                    }
                    return new AboutModel()
                    {
                        ID = _md.id,
                        Content = _md.content,
                        Link = _md.link,
                        Image = _md.image,
                        MetaTitle = _md.meta_title,
                        MetaDescription = _md.meta_description,
                        MetaKeywords = _md.meta_keywords,
                        MetaNext = _md.meta_next,
                        MetaOgSiteName = _md.meta_og_site_name,
                        MetaOgImage = _md.meta_og_image,
                        MetaTwitterImage = _md.meta_twitter_image,
                        MetaArticleName = _md.meta_article_name,
                        MetaArticleTag = _md.meta_article_tag,
                        MetaArticleSection = _md.meta_article_section,
                        MetaArticlePublish = _md.meta_article_publish,
                        Insert = false
                    };
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "GetItemByID", model.CreateBy, ex);
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">About model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(AboutModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    WEB_ABOUT _md = new WEB_ABOUT();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = _context.WEB_ABOUT.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, "Save", model.CreateBy);
                        }
                    }
                    _md.content = model.Content;
                    _md.link = model.Link;
                    _md.image = model.Image;
                    _md.meta_title = model.MetaTitle;
                    _md.meta_description = model.MetaDescription;
                    _md.meta_keywords = model.MetaKeywords;
                    _md.meta_next = model.MetaNext;
                    _md.meta_og_site_name = model.MetaOgSiteName;
                    _md.meta_og_image = model.MetaOgImage;
                    _md.meta_twitter_image = model.MetaTwitterImage;
                    _md.meta_article_name = model.MetaArticleName;
                    _md.meta_article_tag = model.MetaArticleTag;
                    _md.meta_article_section = model.MetaArticleSection;
                    if (model.Insert)
                    {
                        _md.create_by = model.CreateBy;
                        _md.create_date = DateTime.Now;
                        _context.WEB_ABOUT.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.update_by = model.UpdateBy;
                        _md.update_date = DateTime.Now;
                        _context.WEB_ABOUT.Attach(_md);
                        _context.Entry(_md).State = EntityState.Modified;
                    }
                    _context.SaveChanges();
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "Save", model.CreateBy, ex);
            }
            if (model.Insert)
            {
                Notifier.Notification(model.CreateBy, Message.InsertSuccess, Notifier.TYPE.Success);
            }
            else
            {
                Notifier.Notification(model.CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            }
            return ResponseStatusCodeHelper.Success;
        }

    }
}
