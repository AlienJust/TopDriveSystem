using System;
using System.Collections.Generic;
using DataAbstractionLevel.Low.PsnConfig.Contracts;

namespace TopDriveSystem.Model.Listening
{
    public interface IPsnParamsList
    {
        IReadOnlyDictionary<string,
                Tuple<IPsnProtocolCommandPartConfiguration, IPsnProtocolParameterConfigurationVariable>>
            PsnProtocolConfigurationParams { get; }
    }
}