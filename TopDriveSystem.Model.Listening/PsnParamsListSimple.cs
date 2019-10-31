using System;
using System.Collections.Generic;
using DataAbstractionLevel.Low.PsnConfig.Contracts;

namespace TopDriveSystem.Model.Listening
{
    public class PsnParamsListSimple : IPsnParamsList
    {
        public PsnParamsListSimple(IPsnProtocolConfiguration protocolConfig)
        {
            var allPsnParams =
                new Dictionary<string, Tuple<IPsnProtocolCommandPartConfiguration,
                    IPsnProtocolParameterConfigurationVariable>>();
            foreach (var psnCommandPart in protocolConfig.CommandParts)
            foreach (var param in psnCommandPart.VarParams)
                allPsnParams.Add(param.Id.IdentyString,
                    new Tuple<IPsnProtocolCommandPartConfiguration, IPsnProtocolParameterConfigurationVariable>(
                        psnCommandPart, param));

            PsnProtocolConfigurationParams = allPsnParams;
        }

        public IReadOnlyDictionary<string,
                Tuple<IPsnProtocolCommandPartConfiguration, IPsnProtocolParameterConfigurationVariable>>
            PsnProtocolConfigurationParams { get; }
    }
}