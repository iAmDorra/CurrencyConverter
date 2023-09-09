using CurrencyConverter.Domain;
using CurrencyConverter.Domain.TourAttributes;
using NFluent;
using System.Reflection;
using System.Text;

namespace CurrencyConverter.LivingDocumentation
{
    [TestClass]
    public class GuidedTourDocument
    {
        private const string CONTEXT_PREFIX = "CurrencyConverter";
        private const int DefaultClassLineNumber = 0;
        private const string REPO_LINKS_PREFIX = "https://github.com/iAmDorra/CurrencyConverter/";
        private const string DOMAIN_LINK = "blob/master/CurrencyConverter.Domain/";
        private const string SEP = "\r\n";
        private readonly Dictionary<string, Tour> tours = new Dictionary<string, Tour>();

        [TestMethod]
        [TestCategory("LivingDocumentation")]
        public void PrintGuidedTour()
        {
            var domainClasses = typeof(Amount).Assembly.GetTypes();
            foreach (var classe in domainClasses.Where(cl => cl.Namespace!.Contains(CONTEXT_PREFIX)))
            {
                Process(classe);
            }

            StringBuilder sb = new StringBuilder();
            foreach (string tourName in tours.Keys)
            {
                sb.Append(WriteSightSeeingTour(tourName));
            }

            string targetFileName = "Quick_Developer_Tour.html";
            TemplateFiller.CreateTargetFile(
                sb.ToString(),
                "Quick Developer Tour",
                "./Ressources/strapdown-template.html",
                targetFileName);

            Check.That(File.Exists(targetFileName)).IsTrue();
            Check.That(sb.ToString()).IsEqualTo(File.ReadAllText("./Ressources/content_approved.txt"));

        }
        private string WriteSightSeeingTour(string tourName)
        {

            StringBuilder sb = new StringBuilder();
            Tour tour = tours[tourName];
            int count = 1;
            foreach (string step in tour.Sites.Values)
            {
                sb.Append(SEP);
                sb.Append("## " + count + ". " + step);
                sb.Append(SEP);
                count++;
            }
            return sb.ToString();
        }

        private void Process(Type classe)
        {
            AddTourStep(
                GetQuickDevTourStep(classe),
                classe.Name,
                classe.Name,
                DefaultClassLineNumber
                );
            if (classe.IsEnum)
            {
            }
            else
            {
                foreach (var methodInfo in classe.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    string name = methodInfo.Name;
                    int lineNumber = 0; //m.GetLineNumber();
                    TourStep step = GetQuickDevTourStep(methodInfo);
                    AddTourStep(step, name, classe.Name, lineNumber);
                }
            }
        }

        private TourStep GetQuickDevTourStep(MethodInfo methodInfo)
        {
            foreach (var guidedTour in methodInfo.GetCustomAttributes<StepAttribute>())
            {
                return new TourStep(
                    guidedTour.Name.Replace("\"", ""),
                    guidedTour.Description.Replace("\"", ""),
                    guidedTour.Rank);
            }
            return null;
        }
       
        private void AddTourStep(TourStep step, string name, string? qName, int lineNumber)
        {
            if (step != null)
            {
                StringBuilder content = new StringBuilder();
                content.Append(LinkSrc(name, qName, lineNumber));
                content.Append(SEP);
                if (step.Description() != null)
                {
                    content.Append(SEP);
                    content.Append("*" + step.Description().Trim() + "*");
                }
                content.Append(SEP);

                GetTourNamed(step.Name()).Put(step.Step(), content.ToString());
            }
        }

        private Tour GetTourNamed(string name)
        {
            if (tours.ContainsKey(name))
            {
                return tours[name];
            }

            var tour = new Tour();
            tours.Add(name, tour);
            return tour;
        }

        private string LinkSrc(string name, string qName, int lineNumber)
        {
            return Link(name, REPO_LINKS_PREFIX + DOMAIN_LINK + qName + ".cs#L" + lineNumber);
        }

        private string Link(string name, string url)
        {
            return "[" + name + "](" + url + ")";
        }

        private TourStep GetQuickDevTourStep(Type classe)
        {
            foreach (var guidedTour in classe.GetCustomAttributes<StepAttribute>())
            {
                return new TourStep(
                        guidedTour.Name.Replace("\"", ""),
                        guidedTour.Description.Replace("\"", ""),
                        guidedTour.Rank);
            }
            return null;
        }

        private class Tour
        {
            public readonly SortedDictionary<int, string> Sites = new SortedDictionary<int, string>();

            public string Put(int step, string describtion)
            {
                Sites.Add(step, describtion);
                return describtion;
            }

            public override string ToString() => Sites.ToString();
        }

        private class TourStep
        {
            private string name;
            private string description;
            private int step;

            public string Name()
            {
                return name;
            }

            public string Description()
            {
                return description;
            }

            public int Step()
            {
                return step;
            }

            public TourStep(string name, string description, int step)
            {
                this.name = name;
                this.description = description;
                this.step = step;
            }

        }
    }
}
