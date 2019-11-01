using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheWall.Models;

namespace TheWall.Controllers
{
    public class WallController: Controller
    {
        public int? SessionId
        {
            get {return HttpContext.Session.GetInt32("UserID");}
            set {HttpContext.Session.SetInt32("UserID", (int)value);}
        }
        
        public string SessionName
        {
            get {return HttpContext.Session.GetString("UserName");}
            set {HttpContext.Session.SetString("UserName", value);}
        }
        private WallContext database;
        // CONSTRUCTOR
        public WallController(WallContext context)
        {
            database = context;
            Console.WriteLine("###### CONTEXT ACQUIRED");
        }
        // -------------------- READs --------------------
        [HttpGet("wall")]
        public IActionResult MessageWall()
        {
            if (SessionId == null){
                return RedirectToAction("UserIndex");
            }
            List<Message> allMessages = database.Messages
                .Include(m => m.Messenger)
                .Include(m => m.Comments)
                .ThenInclude(c => c.User)
                .OrderByDescending(m => m.CreateAt).ToList();
            ViewBag.UserName = SessionName;
            ViewBag.UserId = SessionId;
            return View(allMessages);
        }

        // -------------------- CREATEs --------------------
        [HttpPost("wall/maker")]
        public IActionResult MessageMaker(string contents)
        {
            if (contents != null)
            {
                Message message = new Message();
                message.Content = contents;
                message.UserId = (int)SessionId;
                database.Add(message);
                database.SaveChanges();
                return RedirectToAction("MessageWall");
            }
            return RedirectToAction("MessageWall");
        }

        [HttpPost("wall/creator")]
        public IActionResult CommentCreator(string contents, int messageId)
        {
            if (contents != null)
            {
                Comment comment = new Comment();
                comment.Content = contents;
                comment.MessageId = messageId;
                comment.UserId = (int)SessionId;
                database.Add(comment);
                database.SaveChanges();
                return RedirectToAction("MessageWall");
            }
            return RedirectToAction("MessageWall");
        }

        [HttpGet("delete/{id:int}")]
        public IActionResult Destroy(int id)
        {
            Message target = database.Messages
            .FirstOrDefault(m => m.MessageId == id);
            database.Remove(target);
            database.SaveChanges();
            return RedirectToAction("MessageWall");
        }
    }
}