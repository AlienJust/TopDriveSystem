using DataAbstractionLevel.Low.PsnConfig.Contracts;
using System;
using System.Collections.Generic;

namespace TopDriveSystem.Model.Listening
{
    public class PsnParamsListSimple : IPsnParamsList
    {
        public IReadOnlyDictionary<string, Tuple<IPsnProtocolCommandPartConfiguration, IPsnProtocolParameterConfigurationVariable>> PsnProtocolConfigurationParams { get; }
        public PsnParamsListSimple(IPsnProtocolConfiguration protocolConfig)
        {
            var allPsnParams = new Dictionary<string, Tuple<IPsnProtocolCommandPartConfiguration, IPsnProtocolParameterConfigurationVariable>>();
            foreach (var psnCommandPart in protocolConfig.CommandParts)
            {
                foreach (var param in psnCommandPart.VarParams)
                {
                    allPsnParams.Add(param.Id.IdentyString, new Tuple<IPsnProtocolCommandPartConfiguration, IPsnProtocolParameterConfigurationVariable>(psnCommandPart, param));
                }
            }

            PsnProtocolConfigurationParams = allPsnParams;
        }
    }

}