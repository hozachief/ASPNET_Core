/** 
 * Controller actions are invoked in response to an incoming URL request. A controller
 * class is where the code is written that handles the incoming browser requests.
 * The controller retrieves data from a data source and decides what type of response
 * to send back to the browser. View templates can be used from a controller to
 * generate and format an HTML response to the browser.
 * 
 * Controllers are responsible for providing the data required in order for a view
 * template to render a response. A best practice: View templates should not perform
 * business logic or interact with a database directly. Rather, a view template should
 * work only with the data that's provided to it by the controller.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        /** 
         * Every public method in a controller is callable as an HTTP endpoint. Both
         * methods return a string.
         * An HTTP endpoint is a targetable URL in the web application, such as
         * https://localhost:5001/HelloWorld, and combines the protocol used: HTTPS,
         * the network location of the web server (including the TCP port): localhost:5001
         * and the target URI HelloWorld.
         * 
         * Calls the controller's View method. It uses a view template to generate
         * an HTML response. Controller methods (also known as action methods),
         * such as the Index method, generally return an IActionResult (or a class
         * derived from ActionResult), not a type like string.
        */
        // HTTP GET method that is invoked by appending /HelloWorld/ to the base URL.
        // GET: /HelloWorld/
        //public string Index()
        public IActionResult Index()
        {
            //return "This is my default action...";
            return View();
        }

        /** 
         * Modify the code to pass some parameter information from the URL to the controller.
         * For example, /HelloWorld/Welcome?name=Rick&numtimes=4
         * The URL segment (Parameters) isn't used, the name and numTimes parameters
         * are passed as query strings. The ? in the above URL is a separator, and
         * the query strings follow. The & character separates query strings.        
         * 
         * -Uses the C# optional-parameter feature to indicate that the numTimes
         * parameter defaults to 1 if no value is passed for that parameter.
         * -Uses HtmlEncoder.Default.Encode to protect that app from malicious input
         * (namely JavaScript).
         * -Uses Interpolated Strings
         * 
         * The MVC model binding system automatically maps the named parameters
         * from the query string in the address bar to parameters in your method.
         * 
         * Run the app and enter URL: https//localhost:xxx/HelloWorld/Welcome/3?name=Rick
         * This time the third URL segment matched the route parameter id. The Welcome
         * method contains a parameter id that matched the URL template in the MapRoute
         * method. The trailing ? (in id?) indicates the id parameter is optional.
         * 
         * Rather than have the controller render this response as a string, change
         * the controller to use a view template instead. The view template generates
         * a dynamic response, which means that appropriate bits of data must be
         * passed from the controller to the view in order to generate the response.
         * Do this by having the controller put the dynamic data (parameters) that
         * the view template needs in a ViewData dictionary that the view template
         * can then access.
         * ViewData dictionary is a dynamic object, meaning any type can be used;
         * the ViewData object has no defined properties until you put somthing inside it.
         * ViewData dictionary object contains data that will be passed to the view.        
        */
        // HTTP GET method that is invoked by appending /HelloWorld/Welcome/ to the URL.
        // GET: /HelloWorld/Welcome/
        //public string Welcome()
        //public string Welcome(string name, int numTimes = 1)
        //public string Welcome(string name, int ID = 1)
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            //return "This is the Welcome action method...";
            //return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
            //return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");

            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}
