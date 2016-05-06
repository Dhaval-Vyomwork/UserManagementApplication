using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserCrudApp.Data;
using log4net;

namespace UserCrudApp.Controllers
{
    public class UserDetailsController : Controller
    {
        #region Declaration

        private UserDBEntities db = new UserDBEntities();
        public ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion

        #region Display Users List
        /// <summary>
        /// This Will Display Details of Users List.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            logger.Info("Getting Users List at " + DateTime.Now);
            return View(db.UserDetails.ToList());            
        }
        
        #endregion

        #region Create New User
        /// <summary>
        /// This will open create user page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            logger.Info("In Create Method at " + DateTime.Now);
            return View();
        }

        /// <summary>
        /// This will create a new user in userdetails table.
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,EmailId,Password,MobileNo")] UserDetail userDetail)
        {
            try
            {
                logger.Info("Create Method starts at " + DateTime.Now);
                if (ModelState.IsValid)
                {
                    db.UserDetails.Add(userDetail);
                    logger.Info("Create Method Insert Data On Tables " + userDetail.UserName + " at : " + DateTime.Now);
                    db.SaveChanges();
                    logger.Info("Create Method Data Save & End In Tables " + DateTime.Now);
                    return RedirectToAction("Index");
                }
                logger.Info("Create Method Model Is Invalid at " + DateTime.Now);
                return View(userDetail);
            }
            catch(Exception ex)
            {
                logger.Error("Create New User Error : " + ex + " at " + DateTime.Now);
                throw ex;
            }            
        }
        
        #endregion

        #region Edit and Update Details of Selected User
        /// <summary>
        /// This will edit the details of selected user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            try
            {
                logger.Info("Edit Method starts " + id + "at " + DateTime.Now);
                if (id == null)
                {
                    logger.Info("Edit Method Id Found Null at " + DateTime.Now);
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserDetail userDetail = db.UserDetails.Find(id);
                logger.Info("Edit Method Get Details of " + userDetail.UserName + " at: " + DateTime.Now);
                if (userDetail == null)
                {
                    logger.Info("Edit Method UserDetails Is Null  at " + DateTime.Now);
                    return HttpNotFound();
                }
                logger.Info("Edit Method End at " + DateTime.Now);
                return View(userDetail);
            }
            catch(Exception ex)
            {
                logger.Error("Edit Error : " + ex + " at " + DateTime.Now);
                throw ex;
            }
        }

        /// <summary>
        /// This will update the details of selected user.
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,EmailId,Password,MobileNo")] UserDetail userDetail)
        {
            try
            {
                logger.Info("Edit Method starts " + userDetail.UserName + "at " + DateTime.Now);
                if (ModelState.IsValid)
                {
                    db.Entry(userDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    logger.Info("Edit Method Details Updated Successfull & Method End " + userDetail.UserName + " at " + DateTime.Now);
                    return RedirectToAction("Index");
                }
                logger.Info("Edit Method Model Is InValid at " + DateTime.Now);
                return View(userDetail);
            }
            catch (Exception ex)
            {
                logger.Error("Edit Error : " + ex + " at " + DateTime.Now);
                throw ex;
            }
        }

        #endregion

        #region Delete Selected User Details
        /// <summary>
        /// This will delete the selected user from userdetails table.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            try
            {
                logger.Info("Delete Method Start at " + DateTime.Now);
                if (id == null)
                {
                    logger.Info("Delete Method Id Found Null at " + DateTime.Now);
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserDetail userDetail = db.UserDetails.Find(id);
                logger.Info("Delete Method Get User Details" + userDetail.UserName + " at " + DateTime.Now);
                db.UserDetails.Remove(userDetail);
                db.SaveChanges();
                logger.Info("Delete Method End at " + DateTime.Now);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error("Delete Confirmed Error : " + ex + " at " + DateTime.Now);
                throw ex;
            }
        }

        #endregion

        #region Dispose
        /// <summary>
        /// This will dispose the current running process.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            logger.Info("Dispose Start at " + DateTime.Now);
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
