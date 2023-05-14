using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace RipassoGeneerale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            GetDataFromaWEB();
            //InitializeComponent();

        }

        static readonly HttpClient client = new HttpClient();

        static async Task GetDataFromaWEB()
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync("https://swapi.dev/api/people");
                response.EnsureSuccessStatusCode();
                var contentStream = await response.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(contentStream);
                using var jsonReader = new JsonTextReader(streamReader);
                JsonSerializer serializer = new JsonSerializer();

                try
                {
                    Example AllPepole = serializer.Deserialize<Example>(jsonReader);

                    ConvertJson(AllPepole);

                    string JSONresult = JsonConvert.SerializeObject(AllPepole.results);

                    StarWarsDS starWarsDSDB = new StarWarsDS();

                    using (var tw = new StreamWriter(@"C:\Users\leoci\Desktop\Giochi\FileProva\Prova.json", true))
                    {
                        tw.WriteLine(JSONresult.ToString());
                        tw.Close();
                    }

                    XDocument s = new XDocument();
                    XmlDocument doc = JsonConvert.DeserializeXmlNode(JSONresult);

                }
                catch (JsonReaderException e)
                {
                    Console.WriteLine(e.Message);
                }


            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }


        async static void ConvertJson(Example Persone)
        {
          

            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration Declaration = xmlDocument.CreateXmlDeclaration("1.1", "utf-8", null);
            xmlDocument.AppendChild(Declaration);
            XmlElement xmlElements = xmlDocument.CreateElement(null, "Persone_Di_StarWars", null);
            xmlDocument.AppendChild(xmlElements);
            XmlElement xmlElement;
            XmlElement ListaCaratterisctichePG = xmlDocument.CreateElement(null, "Personaggio", null);
           
            XmlText xmlText;

            foreach (Result Persona in Persone.results)
            {


                xmlElement = xmlDocument.CreateElement(null, "hair_color", null);

                xmlText = xmlDocument.CreateTextNode(Persona.hair_color.Replace(" ", "_"));

                xmlElement.AppendChild(xmlText);

                ListaCaratterisctichePG.AppendChild(xmlElement);



                xmlElement = xmlDocument.CreateElement(null, "height", null);

                xmlText = xmlDocument.CreateTextNode(Persona.height.Replace(" ", "_"));


                xmlElement.AppendChild(xmlText);

                ListaCaratterisctichePG.AppendChild(xmlElement);




                xmlElement = xmlDocument.CreateElement(null, "mass", null);

                xmlText = xmlDocument.CreateTextNode(Persona.mass.Replace(" ", "_"));

                xmlElement.AppendChild(xmlText);

                ListaCaratterisctichePG.AppendChild(xmlElement);



                xmlElement = xmlDocument.CreateElement(null, "skin_color", null);

                xmlText = xmlDocument.CreateTextNode(Persona.skin_color.Replace(" ", "_"));

                xmlElement.AppendChild(xmlText);

                ListaCaratterisctichePG.AppendChild(xmlElement);





                xmlElement = xmlDocument.CreateElement(null, "name", null);

                xmlText = xmlDocument.CreateTextNode(Persona.name.Replace(" ","_"));

                xmlElement.AppendChild(xmlText);

                ListaCaratterisctichePG.AppendChild(xmlElement);


                xmlElement = xmlDocument.CreateElement(null, "gender", null);

                xmlText = xmlDocument.CreateTextNode(Persona.gender);

                xmlElement.AppendChild(xmlText);

                ListaCaratterisctichePG.AppendChild(xmlElement);


                ListaCaratterisctichePG = xmlDocument.CreateElement(null, "Personaggio", null);

                xmlElements.AppendChild(ListaCaratterisctichePG);
            }

            xmlDocument.Save(@"C:\Users\leoci\Desktop\Giochi\FileProva\Test.xml");
        }
    }

    public class Result
    {
        public string? name { get; set; } = string.Empty;
        public string? height { get; set; } = string.Empty;
        public string? mass { get; set; } = string.Empty;
        public string? hair_color { get; set; } = string.Empty;
        public string? skin_color { get; set; } = string.Empty;
        public string? eye_color { get; set; } = string.Empty;
        public string? birth_year { get; set; } = string.Empty;
        public string? gender { get; set; } = string.Empty;
        public string? homeworld { get; set; } = string.Empty;
        public IList<string>? films { get; set; }
        public IList<string>? species { get; set; }
        public IList<string>? vehicles { get; set; }
        public IList<string>? starships { get; set; }
        public DateTime? created { get; set; } = DateTime.MinValue;
        public DateTime? edited { get; set; } = DateTime.MinValue;
        public string? url { get; set; } = string.Empty;
    }

    public class Example
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public IList<Result> results { get; set; }
    }

}
