using htmlSerializerMiri;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
Console.OutputEncoding = System.Text.Encoding.UTF8;
var html = await Load("https://www.w3schools.com/html/");
var cleanHtml = new Regex("\\s+").Replace(html, " ");
var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => !string.IsNullOrWhiteSpace(s));
var htmlElement = "<div id=\"my-id\" class=\"my-class-1 my-class-2\" width=\"100%\">text</div>";

var root = BuildTree(htmlLines);

var selector = Selector.parse("div p");
var found = root.FindElements(selector);
Console.WriteLine("נמצאו: " + found.Count);
async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

static HtmlElement BuildTree(IEnumerable<string> htmlLines)
{
    var root = new HtmlElement();
    var current = root;

    foreach (var line in htmlLines)
    {
        var trimmed = line.Trim();
        var firstWord = trimmed.Split(' ')[0];

        if (firstWord == "/html")
            break;

        if (firstWord.StartsWith("/"))
        {
            if (HtmlHelper.Instance.HtmlTags.Contains(firstWord.Substring(1)) && current.Parent != null)
                current = current.Parent;
        }
        else if (HtmlHelper.Instance.HtmlTags.Contains(firstWord))
        {
            var newElement = new HtmlElement();
            var rest = trimmed.Substring(firstWord.Length);
            var atributes = new Regex("([^\\s].*?)=\"(.*?)\"").Matches(rest);

            foreach (Match atribute in atributes)
            {
                newElement.Attributes[atribute.Groups[1].Value] = atribute.Groups[2].Value;
            }

            if (newElement.Attributes.TryGetValue("id", out var id))
                newElement.Id = id;

            if (newElement.Attributes.TryGetValue("class", out var cls))
                newElement.Classes = cls.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            newElement.Name = firstWord;
            newElement.Parent = current;
            current.Children.Add(newElement);
            bool isSelf= trimmed.EndsWith("/")||  HtmlHelper.Instance.HtmlVoidTags.Contains(firstWord);
            if (!isSelf)
                current = newElement;
        }
        else
            current.InnerHtml= trimmed;

    }

    return root;
}

