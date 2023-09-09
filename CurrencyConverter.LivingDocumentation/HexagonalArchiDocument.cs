using CurrencyConverter.Domain;
using CurrencyConverter.Infrastructure;
using DotNetGraph;
using DotNetGraph.Attributes;
using DotNetGraph.Edge;
using DotNetGraph.Node;
using DotNetGraph.SubGraph;
using NFluent;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace CurrencyConverter.LivingDocumentation
{
    [TestClass]
    public class HexagonalArchiDocument
    {
        const string prefix = "CurrencyConverter";
        const string domainPrefix = ".Domain";

        [TestMethod]
        [TestCategory("LivingDocumentation")]
        public void GenerateHexagonalArchitectureDiagram()
        {
            var graph = CreateGraph();
            string content = CreateContent(graph);

            var title = "Living Diagram";
            string templatePath = "./Ressources/viz-template.html";
            const string targetFileName = "HexagonalArchitecture.html";

            TemplateFiller.CreateTargetFile(content, title, templatePath, targetFileName);

            Check.That(File.Exists(targetFileName)).IsTrue();
        }

        private DotGraph CreateGraph()
        {
            Assembly domainAssembly = typeof(Amount).Assembly;
            var infraAssembly = typeof(Rates).Assembly;
                        
            IEnumerable<Type> allClasses = domainAssembly.GetTypes().Union(infraAssembly.GetTypes());
            var topLevelClasses = allClasses.Where(classe => classe.Namespace!=null && classe.Namespace.Contains(prefix));

            Dictionary<Type, int> elements = new Dictionary<Type, int>();
            var domainClasses = topLevelClasses.Where(classe => classe.Namespace.Contains(prefix + domainPrefix));
            DotSubGraph domainGraph = CreateSubGraph(domainClasses, "Core Domain", elements);
            DotGraph graph = new DotGraph("Hexagonal Architecture", true);
            graph.Elements.Add(domainGraph);

            var infraClasses = topLevelClasses.Where(classe => classe.Namespace.Contains(prefix) && !classe.Namespace.Contains(domainPrefix));
            var infraSubGraph = CreateSubGraph(infraClasses, "Infrastructure", elements);
            graph.Elements.Add(infraSubGraph);

            foreach (var classe in topLevelClasses)
            {
                var fields = classe.GetRuntimeFields();
                foreach (var field in fields)
                {
                    if (topLevelClasses.Any(cl => field.FieldType == cl))
                    {
                        var edge = new DotEdge(elements[classe].ToString(), elements[field.FieldType].ToString());
                        edge.ArrowHead = new DotEdgeArrowHeadAttribute(DotEdgeArrowType.Open);
                        graph.Elements.Add(edge);
                    }
                }

                var interfaces = classe.GetInterfaces();
                foreach (var classInterface in interfaces)
                {
                    if (topLevelClasses.Any(cl => classInterface == cl))
                    {
                        var edge = new DotEdge(elements[classe].ToString(), elements[classInterface].ToString());
                        edge.ArrowHead = new DotEdgeArrowHeadAttribute(DotEdgeArrowType.Normal);
                        edge.Style = new DotEdgeStyleAttribute(DotEdgeStyle.Dashed);
                        graph.Elements.Add(edge);
                    }
                }
            }

            return graph;
        }

        private string CreateContent(DotGraph graph)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"# Class diagram {graph.Identifier}");
            stringBuilder.AppendLine("digraph G {");
            stringBuilder.AppendLine($"graph[labelloc = top, label =\"{graph.Identifier}\", fontname = \"Verdana\", fontsize = 12, rankdir = LR];");
            stringBuilder.AppendLine("node [fontname=\"Verdana\",fontsize=9,shape=record];");
            foreach (var element in graph.Elements)
            {
                var subgraph = element as DotSubGraph;
                if (subgraph != null)
                {
                    stringBuilder.Append($"subgraph cluster_c{subgraph.Identifier} ");
                    stringBuilder.AppendLine("{");

                    stringBuilder.AppendLine($"label = \"{subgraph.Label.Text}\";");
                    foreach (var item in subgraph.Elements)
                    {
                        var node = item as DotNode;
                        if (node != null)
                        {
                            stringBuilder.AppendLine($"{node.Identifier} [label = \"{node.Label.Text}\"]");
                        }
                    }

                    stringBuilder.AppendLine("}");
                }
                var edge = element as DotEdge;
                if (edge != null)
                {
                    stringBuilder.Append($"{(edge.Left as DotString).Value} -> {(edge.Right as DotString).Value} [");
                    if (edge.ArrowHead != null)
                    {
                        string arrow = edge.ArrowHead.ArrowType == DotEdgeArrowType.Normal
                            ? "onormal"
                            : edge.ArrowHead.ArrowType.ToString().ToLower();
                        stringBuilder.Append($"arrowhead={arrow},");
                    }
                    if (edge.ArrowTail != null)
                    {
                        stringBuilder.Append($"arrowtail={edge.ArrowTail.ArrowType.ToString().ToLower()},");
                    }
                    if (edge.Style != null)
                    {
                        stringBuilder.Append($"style={edge.Style.Style.ToString().ToLower()},");
                    }

                    stringBuilder.AppendLine("];");
                }
            }
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        int idElement = 0;
        private DotSubGraph CreateSubGraph(IEnumerable<Type> classes, string subGraphIdentifier, Dictionary<Type, int> elements)
        {
            DotSubGraph subGraph = new DotSubGraph(idElement.ToString());
            idElement++;
            foreach (var topLevelClass in classes)
            {
                var typeElement = new DotNode(idElement.ToString());
                typeElement.Label = topLevelClass.Name;
                subGraph.Elements.Add(typeElement);
                elements.Add(topLevelClass, idElement);
                idElement++;
            }

            subGraph.Label = subGraphIdentifier;
            subGraph.Style = DotSubGraphStyle.Dashed;
            subGraph.Color = new DotColorAttribute(Color.Red);

            return subGraph;
        }

    }
}