using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pepperi.Test.MVC.Controllers
{
    public class DemoWebhookController : Controller
    {
        //
        // GET: /DemoWebhook/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SalesOrder(PepperiWebhookRequest webhook, bool check_signature = false, bool async = false, bool success = true, bool error = false, bool image = false)
        {
            try
            {

                if (check_signature == true)
                {
                    if (Request.Headers == null)
                    {
                        Response.StatusCode = 400;
                        var msg = new System.Net.Http.HttpResponseMessage();
                        msg.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        msg.ReasonPhrase = "X-Pepperi-Signature does not exist";
                        return Json(msg);
                    }

                    if (Request.Headers["X-Pepperi-Signature"] == null)
                    {
                        Response.StatusCode = 400;
                        var msg = new System.Net.Http.HttpResponseMessage();
                        msg.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        msg.ReasonPhrase = "X-Pepperi-Signature does not exist";
                        return Json(msg);
                    }

                    if (Request.Headers["X-Pepperi-Signature"].ToString() == "")
                    {
                        Response.StatusCode = 400;
                        var msg = new System.Net.Http.HttpResponseMessage();
                        msg.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        msg.ReasonPhrase = "X-Pepperi-Signature does not exist";
                        return Json(msg);
                    }

                    string signature = Request.Headers["X-Pepperi-Signature"];

                    if (signature != null && signature.Trim().IndexOf("sha256=") == 0)
                    {
                        signature = signature.Substring(7, signature.Length - 7);

                        System.IO.Stream req = Request.InputStream;
                        req.Seek(0, System.IO.SeekOrigin.Begin);
                        string body = new System.IO.StreamReader(req).ReadToEnd();
                        string expectedSignature = GetSignature(body, "my_private_key");

                        if (expectedSignature != signature)
                        {
                            Response.StatusCode = 400;
                            var msg = new System.Net.Http.HttpResponseMessage();
                            msg.StatusCode = System.Net.HttpStatusCode.BadRequest;
                            msg.ReasonPhrase = "signature for body should be: " + expectedSignature + "  instead its: " + signature;
                            return Json(msg);
                        }
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        var msg = new System.Net.Http.HttpResponseMessage();
                        msg.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        msg.ReasonPhrase = "signature should start with sha256=";
                        return Json(msg);
                    }
                }

                if (webhook.Data == null)
                {
                    Response.StatusCode = 400;
                    var msg = new System.Net.Http.HttpResponseMessage();
                    msg.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    msg.ReasonPhrase = "data cannot be null";
                    return Json(msg);
                }

                if (webhook.CompnayID == null)
                {
                    Response.StatusCode = 400;
                    var msg = new System.Net.Http.HttpResponseMessage();
                    msg.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    msg.ReasonPhrase = "company_id cannot be null";
                    return Json(msg);
                }

                if (webhook.Data.ID == null)
                {
                    Response.StatusCode = 400;
                    var msg = new System.Net.Http.HttpResponseMessage();
                    msg.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    msg.ReasonPhrase = "data id cannot be null";
                    return Json(msg);
                }

                if (webhook.Delivery.ID == null)
                {
                    Response.StatusCode = 400;
                    var msg = new System.Net.Http.HttpResponseMessage();
                    msg.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    msg.ReasonPhrase = "delivery id cannot be null";
                    return Json(msg);
                }

                if (webhook.Data.URI == null)
                {
                    Response.StatusCode = 400;
                    var msg = new System.Net.Http.HttpResponseMessage();
                    msg.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    msg.ReasonPhrase = "data uri cannot be null";
                    return Json(msg);
                }

                PepperiWebhookResponse pepRes = new PepperiWebhookResponse();

                if (error == true)
                {
                    Response.StatusCode = 400;
                    var msg = new System.Net.Http.HttpResponseMessage();
                    msg.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    msg.ReasonPhrase = "this is an error response sample";
                    return Json(msg);
                }

                pepRes.Success = true;

                if (success == false)
                {
                    pepRes.Success = false;

                    if (image == false)
                        pepRes.HTMLResponse = "<b>failed! too bad!</b>";
                    else
                        pepRes.HTMLResponse = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'> <html xmlns='http://www.w3.org/1999/xhtml' > <head> <meta charset='utf-8' /> <title>massages dialog</title> <style type='text/css'> </style> </head> <body> <div style='position:absolute; top:20%'> <div style='width:96%; margin:0 auto; background:#ffffff; -moz-border-radius: 10px; -webkit-border-radius: 10px; -khtml-border-radius: 10px; border-radius: 10px; box-shadow: 1px 1px 10px #000000;'> <div id='Div2' style='width:100%; padding-bottom:2%;'> <div style='width:100%; padding-top:1%; padding-bottom:1%; text-align:center; font-size:72px;'>Transaction Declined</div> <div style='width:100%; max-height:100px; text-align:center;'> <img style='max-height:100px;' src='data:image/png;base64,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'/> </div> <div style='width:90%; max-height:100px; padding-top:1%; padding-left:5%; padding-right:5%; text-align:center; font-size:48px; display: table; clear:both; '> <p><b>Reason:</b></p> <p>placeholder_reason</p> </div> </div> </div> </div> </body> </html>";
                }

                else if (async == true && success == true)
                {
                    pepRes.AsyncCallback = new AsyncCallback();
                    pepRes.AsyncCallback.URI = @"https://<your domain>/demowebhook/salesorder?success=true";
                    pepRes.AsyncCallback.IntervalSeconds = 30;
                    pepRes.AsyncCallback.StartAfterSeconds = 60;
                }
                else if (async == false && success == true)
                {
                    pepRes.Success = true;
                    if (image == false)
                        pepRes.HTMLResponse = "<b>great!</b>";
                    else
                        pepRes.HTMLResponse = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'> <html xmlns='http://www.w3.org/1999/xhtml' > <head> <meta charset='utf-8' /> <title>massages dialog</title> <style type='text/css'> </style> </head> <body> <div style='position:absolute; top:20%; left:20%'> <div style='width:96%; margin:0 auto; background:#ffffff; -moz-border-radius: 10px; -webkit-border-radius: 10px; -khtml-border-radius: 10px; border-radius: 10px; box-shadow: 1px 1px 10px #000000;'> <div id='content' style='width:100%; padding-bottom:2%;'> <div style='width:100%; padding-top:1%; padding-bottom:1%; text-align:center; font-size:72px;'>Transaction Approved</div> <div style='width:100%; max-height:100px; text-align:center;'> <img style='max-height:100px;' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAQgAAADICAMAAAAjt4/NAAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAADAFBMVEUAAACTyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ6TyQ4AAADrIDwGAAAA/nRSTlMABDFgi63L4/b88OjZu52AVyEV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'/> </div> <div style='width:90%; max-height:100px; padding-top:1%; padding-left:5%; padding-right:5%; text-align:center; font-size:48px; display: table; clear:both; '> <p><b>Type:</b> placeholder_type</p> <p><b>Number:</b> placeholder_number</p> </div> <div style='width:100%; max-height:100px; text-align:center; font-size:48px; padding-top:1%;'> <a href='placeholder_external_link' style='color:#fe5000; border:0; background:none; text-decoration:none; cursor:pointer; font-size:36px;'>View/Edit in browser</a> </div> </div> </div> <div style='height:100px;'></div> </div> </body> </html>";
                }

                pepRes.Timestamp = DateTime.UtcNow.ToString("u");

                return Json(pepRes);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, ex.ToString());
            }
        }

        private static string GetSignature(string args, string privatekey)
        {
            var encoding = new System.Text.UTF8Encoding();
            byte[] key = encoding.GetBytes(privatekey);
            var myhmacsha256 = new System.Security.Cryptography.HMACSHA256(key);
            byte[] hashValue = myhmacsha256.ComputeHash(encoding.GetBytes(args));
            string hmac64 = Convert.ToBase64String(hashValue);
            myhmacsha256.Clear();
            return hmac64;
        }
    }

    public class Delivery
    {
        public int ID { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }

    public class TriggeredBy
    {
        public int ID { get; set; }
        public string URI { get; set; }
    }

    public class Data
    {
        public int ID { get; set; }
        public string ExternalID { get; set; }
        public string URI { get; set; }
    }

    public class PepperiWebhookRequest
    {
        public string ID { get; set; }
        public Delivery Delivery { get; set; }
        public int CompnayID { get; set; }
        public TriggeredBy TriggeredBy { get; set; }
        public Data Data { get; set; }
    }


    public class AsyncCallback
    {
        public string URI { get; set; }
        public int StartAfterSeconds { get; set; }
        public int IntervalSeconds { get; set; }
    }

    public class PepperiWebhookResponse
    {
        public bool Success { get; set; }
        public string HTMLResponse { get; set; }
        public AsyncCallback AsyncCallback { get; set; }
        public string Timestamp { get; set; }
    }
}
