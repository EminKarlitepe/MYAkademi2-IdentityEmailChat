using IdentityProject.Context;
using IdentityProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace IdentityProject.Controllers
{
    public class MessageController : Controller
    {
        private readonly EmailContext _emailContext;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(EmailContext emailContext, UserManager<AppUser> userManager)
        {
            _emailContext = emailContext;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Inbox(string search)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string email = user.Email;
            ViewBag.email = email;
            ViewBag.search = search;

            var messagesQuery = _emailContext.Messages
                .Where(x => x.ReceiverEmail == email && !x.IsInTrash);

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                messagesQuery = messagesQuery.Where(x => x.MessageDetail.ToLower().Contains(search));
            }

            var messages = messagesQuery.ToList();
            ViewBag.InboxCount = messages.Count;

            return View(messages);
        }

        [Authorize]
        public async Task<IActionResult> Sendbox(string search)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string email = user.Email;
            ViewBag.email = email;
            ViewBag.search = search;

            var messagesQuery = _emailContext.Messages
                .Where(x => x.SenderEmail == email && !x.IsInTrash);

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                messagesQuery = messagesQuery
                    .Where(x => x.Subject.ToLower().Contains(search));
            }

            var messages = messagesQuery.ToList();
            ViewBag.SendboxCount = messages.Count;

            return View(messages);
        }


        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            message.SenderEmail = user.Email;
            message.SenderName = user.Name + " " + user.Surname;
            message.IsRead = false;
            message.SendDate = DateTime.Now;
            message.IsInTrash = false;

            var receiver = await _userManager.FindByEmailAsync(message.ReceiverEmail);
            message.ReceiverName = receiver != null ? receiver.Name + " " + receiver.Surname : "Bilinmeyen Alıcı";

            _emailContext.Messages.Add(message);
            await _emailContext.SaveChangesAsync();

            TempData["Success"] = "Mesajınız başarıyla gönderildi!";
            return RedirectToAction("Inbox");
        }

        public async Task<IActionResult> MessageDetail(int id)
        {
            var value = await _emailContext.Messages.FindAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeMessageStatus(List<int> messageID)
        {
            foreach (var id in messageID)
            {
                var value = await _emailContext.Messages.FindAsync(id);
                if (value != null)
                {
                    value.IsRead = true;
                }
            }
            await _emailContext.SaveChangesAsync();
            return RedirectToAction("Inbox");
        }

        [HttpPost]
        public async Task<IActionResult> MoveToTrash(List<int> messageID)
        {
            foreach (var id in messageID)
            {
                var message = await _emailContext.Messages.FindAsync(id);
                if (message != null)
                {
                    message.IsInTrash = true;
                }
            }
            await _emailContext.SaveChangesAsync();
            return RedirectToAction("Inbox");
        }

        [HttpPost]
        public async Task<IActionResult> RestoreMessage(List<int> messageID)
        {
            foreach (var id in messageID)
            {
                var message = await _emailContext.Messages.FindAsync(id);
                if (message != null)
                {
                    message.IsInTrash = false;
                }
            }
            await _emailContext.SaveChangesAsync();
            return RedirectToAction("TrashBox");
        }

        public async Task<IActionResult> TrashBox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string email = user.Email;
            var deletedValues = _emailContext.Messages
                .Where(x => x.ReceiverEmail == email && x.IsInTrash)
                .ToList();
            ViewBag.TrashCount = deletedValues.Count;

            return View(deletedValues);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteMessagePermanent(List<int> messageID)
        {
            foreach (var id in messageID)
            {
                var message = await _emailContext.Messages.FindAsync(id);
                if (message != null)
                {
                    _emailContext.Messages.Remove(message);
                }
            }
            await _emailContext.SaveChangesAsync();
            return RedirectToAction("TrashBox");
        }

    }
}