using System;
using Microsoft.AspNetCore.Mvc;
using PartyRSVP.Models;

namespace PartyRSVP.Conrollers {
    public class HomeController : Controller {
        public ViewResult Index () {
            return View ("Rsvp");
        }

        [HttpPost]
        public ViewResult RsvpForm (GuestResponse guestResponse) {
            // Todo: Store guest response, covered in later lessons
            return View ("Thanks", guestResponse);
        }
    }
}