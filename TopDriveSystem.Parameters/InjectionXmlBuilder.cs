using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace TopDriveSystem.ControlApp.ViewModels.ParameterPresentation
{
    internal sealed class InjectionXmlBuilder
    {
        public static IParameterInjectionConfiguration GetInjectionConfiguration(XElement parameterElement)
        {
            return parameterElement.Elements("Injection").Select(xmlTag =>
            {
                var predefinedValuesTag = xmlTag.Elements("InjectTextValue");
                IList<ParameterPreselectedValue> predefinedValues;
                try
                {
                    predefinedValues = predefinedValuesTag.Select(xmlTagTv =>
                    new ParameterPreselectedValue(
                            xmlTagTv.Attribute("Text").Value,
                            double.Parse(xmlTagTv.Attribute("Value").Value, CultureInfo.InvariantCulture))).ToList();
                }
                catch
                {
                    predefinedValues = new List<ParameterPreselectedValue>();
                }

                return new ParameterInjectionConfigNcalc(
                    int.Parse(xmlTag.Attribute("ParamNumberInPackage").Value),
                    xmlTag.Attribute("ConvertExpression").Value,
                    bool.Parse(xmlTag.Attribute("InvertBytesOrder").Value),
                    predefinedValues);
            }).FirstOrDefault();

        }
    }
}