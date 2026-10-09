using Dessins.Events;
using Microsoft.AspNetCore.Mvc;

namespace Dessins.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DessinsController : ControllerBase
    {
        [HttpGet]
        // Rien à modifier ici, juste un exemple de dessin très simple
        public ActionResult GetDrawing1()
        {
            var drawSquare = new DrawSquare(2, 2);
            
            return Ok(drawSquare);
        }

        // TODO: Il faut ajouter une nouvelle action pour dessiner la séquence mentionnée dans l'énoncé
        
        [HttpGet]
        public ActionResult GetDrawing2()
        {
            var blue = new ChangeColor("blue");
            var drawCircle = new DrawCircle(1, 1);
            var wait1 = new Wait(3);
            var red = new ChangeColor("red");
            var drawSquare1 = new DrawSquare(0, 2);
            var drawSquare2 = new DrawSquare(2, 2);
            var wait2 = new Wait(1);
            var yellow = new ChangeColor("yellow");
            var drawStar = new DrawStar(1, 3, 20);

            var events = new List<DrawingEvent> {blue, drawCircle, wait1, red, drawSquare1, drawSquare2, wait2, yellow, drawStar };
            return Ok(events);
        }
    }
}
