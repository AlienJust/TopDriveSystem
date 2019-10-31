using System.Linq;
using System.Xml.Linq;
using TopDriveSystem.Parameters.Views;

namespace TopDriveSystem.Parameters
{
    public sealed class ParametersPresenterXmlBuilder : IParametersPresenterXmlBuilder
    {
        private readonly string _filename;

        public ParametersPresenterXmlBuilder(string filename)
        {
            _filename = filename;
        }

        public IParametersPresenter BuildParametersPresentationFromXml()
        {
            var xdoc = XDocument.Load(_filename);
            return new ParametersPresenterSimple
            {
                Parameters =
                    xdoc.Element("Parameters").Elements("Parameter").Select(xmlTagParameter =>
                    {
                        var customNameXmlAttribute = xmlTagParameter.Attribute("CustomName");

                        return (IParameterDescription) new ParameterDescriptionSimple
                        {
                            Key = xmlTagParameter.Attribute("Key").Value,
                            Identifier = xmlTagParameter.Attribute("Identifier").Value,
                            CustomName = customNameXmlAttribute?.Value,
                            View = ViewXmlBuilder.GetViews(xmlTagParameter).FirstOrDefault() ??
                                   new DefaultView(), // TODO: move away default view, use null here or NOT?
                            Injection = InjectionXmlBuilder.GetInjectionConfiguration(xmlTagParameter)
                        };
                    }).ToDictionary(p => p.Key, p => p)
            };
        }
    }

    public interface IParametersPresenterXmlBuilder
    {
        IParametersPresenter BuildParametersPresentationFromXml();
    }
}