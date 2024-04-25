using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MdToSpeOutput.Models;
using Markdig;

namespace MdToSpeOutput.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Upload(IFormFile markdownFile)
    {
        if(markdownFile == null || markdownFile.Length == 0)
        {
            ViewBag.Error = "Please Select the correct file to upload";
            return View();
        }

        using(var reader = new StreamReader(markdownFile.OpenReadStream()))
        {
            string markdownContent = reader.ReadToEnd();
            string htmlContent = TransformMarkdownToHtml(markdownContent);
            System.Console.WriteLine(htmlContent);
            ViewBag.htmlContent = htmlContent;
        }
        return View("TransformContent");
    }

    public string TransformMarkdownToHtml(string markdownContent)
    {
         // Configure the Markdown pipeline
        var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Use<GitHubUserProfileExtension>().Build();

        // string html = Markdig.Markdown.ToHtml(markdownText, pipeline,MarkdownConfig.Pipeline);
        var mdToHtml = Markdig.Markdown.ToHtml(markdownContent,pipeline);
        return mdToHtml;
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}
