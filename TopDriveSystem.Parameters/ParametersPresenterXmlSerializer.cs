using DataAbstractionLevel.Low.PsnConfig.Contracts;
using System.Xml.Linq;

namespace TopDriveSystem.ControlApp.ViewModels.ParameterPresentation
{
    public sealed class ParametersPresenterXmlSerializer
    {
        private static void AddChildXmlNodesWithParameters(XElement parametersRootElement, IPsnProtocolConfiguration config, bool includeCustomName)
        {
            foreach (var commandPart in config.CommandParts)
            {
                int address = (int)commandPart.Address.DefinedValue;
                int command = (int)commandPart.CommandCode.DefinedValue;
                int signalNumber = 1;
                foreach (var varParam in commandPart.VarParams)
                {
                    if (varParam.Name.StartsWith("#")) continue;

                    var key = "param_" +
                        address.ToString("d3") + "_" +
                        command.ToString("d3") + "_" +
                        (commandPart.PartType == PsnProtocolCommandPartType.Request ? "request_" : "reply_") + 
                        signalNumber.ToString("d3");


                    var node = new XElement("Parameter", new XAttribute("Key", key), new XAttribute("Identifier", varParam.Id.IdentyString));
                    if (includeCustomName) node.Add(new XAttribute("CustomName", varParam.Name));

                    node.Add(new XAttribute("Comment", commandPart.PartName + " - " + varParam.Name));

                    parametersRootElement.Add(node);
                    signalNumber++;
                }
            }
        }

        public static void Serialize(string filename, IPsnProtocolConfiguration config, bool includeCustomName)
        {
            var xDoc = new XDocument();
            var rootElement = new XElement("Parameters");


            AddChildXmlNodesWithParameters(rootElement, config, includeCustomName);

            xDoc.Add(rootElement);
            xDoc.Save(filename);
        }
    }
}