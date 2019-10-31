using DataAbstractionLevel.Low.PsnConfig.Contracts;
using System;
using System.Collections.Generic;

namespace TopDriveSystem.Model.Listening
{
    public interface IPsnParamsList
    {
        IReadOnlyDictionary<string, Tuple<IPsnProtocolCommandPartConfiguration, IPsnProtocolParameterConfigurationVariable>> PsnProtocolConfigurationParams { get; }
    }

}