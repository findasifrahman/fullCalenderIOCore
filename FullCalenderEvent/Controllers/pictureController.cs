using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FullCalenderEvent.Controllers
{
    public class pictureController : Controller
    {
        public string serverpath = "/Uploads/";
        string servermappath = "~/Uploads";
        // GET: picture
        [NonAction]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //do not validate request token (XSRF)
        //[AdminAntiForgery(true)]
        public ActionResult Upload(string picid)
        {

            string path = Server.MapPath(servermappath);
            var fileExists = Request.Files.Count > 0;
            if (fileExists)
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                try
                {
                    HttpPostedFileBase fileBase = Request.Files[0];
                    if (fileBase != null)
                    {
                        string fileName = Path.GetFileName(fileBase.FileName);
                        string ext = Path.GetExtension(fileName);
                        //if(model == null)

                        fileBase.SaveAs(Path.Combine(path, picid + ext));
                        return Json(new { success = true, fileid = picid + ext, full = serverpath + picid + ext, fullpdf = "/Content/images/pdficon.png", error = "" });


                        //fileBase.SaveAs(path + "fileit" + id + ext);


                    }


                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });

                }
            }
            return Json(new { success = false, fileid = "failed", error = "something went wrong" });
            //return Json(new { success = "false" });
        }

        [HttpPost]

        //do not validate request token (XSRF)
        //[AdminAntiForgery(true)]
        public JsonResult Delete(string picid)
        {
            string path = Server.MapPath(servermappath);


            if (System.IO.File.Exists(Path.Combine(path, picid)))
            {
                System.IO.File.Delete(Path.Combine(path, picid));
            }

            return Json(new { success = true, fileid = path + picid, name = picid, error = "" });


        }
        public static string GetPictureUrl(string picid)
        {
            return "/Uploads/" + picid;
            //for pdf
            //return "/Content/images/pdficon.png";
        }
    }
}