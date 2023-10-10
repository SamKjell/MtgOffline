using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MtgOffline.EnderScript;

namespace MtgOffline
{
    public class WebUtils
    {
        public string link;
        bool offline = false;

        HtmlDocument document;
        public WebUtils(string websiteLink)
        {
            if (Program.windowInstance.offlineMode) { offline = true;  return; }
            link = websiteLink;
            try
            {
                WebClient wClient = new WebClient();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string html = wClient.DownloadString(link);
                WebBrowser web = new WebBrowser();
                web.ScriptErrorsSuppressed = true;
                web.DocumentText = html;
                web.Document.OpenNew(true);
                web.Document.Write(html);
                web.Refresh();
                document = web.Document;
            }
            catch (WebException e)
            {
                Program.windowInstance.offlineMode = true;
                offline = true;
            }
            catch (ArgumentException e) { offline = true; }
        }

        public string GetCardLink(string cardName,out string multiverseId)
        {
            multiverseId = null;
            if (offline) return null;
            HtmlElementCollection tables = document.GetElementsByTagName("table");
            if (tables.Count > 0)
            {
                HtmlElement ele = tables[0];
                HtmlElementCollection cardOptions = ele.GetElementsByTagName("a");
                if (cardOptions.Count > 0)
                {
                    HtmlElement card = cardOptions[0];
                    string innerHtml = card.InnerHtml;
                    if (innerHtml == "Click here to view ratings and comments. ")
                        innerHtml = ele.InnerHtml;
                    else
                    {
                        bool foundName = false;
                        for (int i = 1; i < tables.Count-1; i++)
                        {
                            HtmlElement c = tables[i].GetElementsByTagName("td")[1].GetElementsByTagName("a")[0];
                            string name = c.InnerText;
                            if (name == cardName)
                            {
                                innerHtml = c.OuterHtml;
                                foundName = true;
                                break;
                            }
                        }

                        if (!foundName)
                        {
                            HtmlElement c = tables[1].GetElementsByTagName("td")[1].GetElementsByTagName("a")[0];
                            innerHtml = c.OuterHtml;
                        }
                    }
                    Regex expression = new Regex(@"multiverseid=\d*");
                    Match match = expression.Match(innerHtml);
                    string data = match.ToString();
                    string id = card.Id;
                    if (data != "")
                        id = data.Split('=')[1];
                    multiverseId = id;
                    return "https://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=" + id + "&type=card";
                }

            }
            return null;
        }

        public string GetCardImageLink(string cardName)
        {
            return GetCardLink(cardName, out string id);
        }

        public string GetCardES()
        {
            if (offline) return null;
            //abilities, super - normal - subtypes, if(Creature): power and toughness

            ESBuffer buffer = new ESBuffer();

            HtmlElement infoBox = document.GetElementsByTagName("td")[2];
            HtmlElement generalInfo = infoBox.GetElementsByTagName("div")[2];
            string[] cardTypes = generalInfo.GetElementsByTagName("div")[11].InnerText.Split("â€”".ToCharArray());
            string cardType = cardTypes[0].Trim() + (cardTypes[cardTypes.Length-1]!=cardTypes[0]?" - " + cardTypes[cardTypes.Length - 1].Trim():"");

            if (cardType.Contains("Creature"))
            {
                int i = generalInfo.InnerText.IndexOf("P/T:");
                int k = 0;
                string temp = generalInfo.InnerText.Substring(i+6);
                foreach (char c in temp.ToCharArray())
                {
                    if (c == '\r')
                        break;
                    k++;
                }
                string pt = temp.Substring(0, k);
                buffer.Add("pt", pt);
            }
            else if (cardType.Contains("Enchantment"))
            {

            }
            else if (cardType.Contains("Planeswalker"))
            {

            }
            else if (cardType.Contains("Artifact"))
            {

            }
            else
            {
                cardTypes = generalInfo.GetElementsByTagName("div")[5].InnerText.Split("â€”".ToCharArray());
                cardType = cardTypes[0].Trim() + " - " + cardTypes[cardTypes.Length - 1].Trim();
            }

            
            buffer.Add("cardTypes", cardType);
            return buffer.GetES();
        }

    }
}
