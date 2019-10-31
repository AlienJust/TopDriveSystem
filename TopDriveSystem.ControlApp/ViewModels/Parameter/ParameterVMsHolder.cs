﻿using AlienJust.Support.Concurrent.Contracts;
using TopDriveSystem.ControlApp.ViewModels.ParameterPresentation;
using System.Collections.Generic;
using TopDriveSystem.CommandSenders.Contracts;
using TopDriveSystem.ConfigApp.AppControl.ParamLogger;
using TopDriveSystem.Model.Listening;

namespace TopDriveSystem.ControlApp.ViewModels.Parameter
{
    internal sealed class ParameterVMsHolder : IParameterVMsHolder
    {
        public IReadOnlyDictionary<string, IParameterViewModel> Parameters { get; }

        public ParameterVMsHolder(IParametersPresenterXmlBuilder ppBuilder, IPsnParamsList psnParamsList, IParamListener paramListener, IThreadNotifier uiNotifier, IParameterLogger parameterLogger, IParameterSetter parameterSetter)
        {
            var paramsPresenter = ppBuilder.BuildParametersPresentationFromXml();
            var parameters = new Dictionary<string, IParameterViewModel>();
            foreach (var paramDescriptionAndKey in paramsPresenter.Parameters)
            {
                var key = paramDescriptionAndKey.Key;
                var description = paramDescriptionAndKey.Value;
                var configuration = psnParamsList.PsnProtocolConfigurationParams[description.Identifier];

                parameters.Add(
                    key, new ParameterViewModelSimple(description.CustomName, configuration.Item2.Name,
                    new ParameterGetterViewModelSimple(
                        description.Identifier, paramListener, uiNotifier, description.View,
                        parameterLogger, configuration.Item2.IsBitSignal,
                        configuration.Item1.PartName + ": " + configuration.Item2.Name),
                    description.Injection == null ? null : new ParameterSetterViewModelSimple(parameterSetter, uiNotifier, description.Injection)));
            }
            Parameters = parameters;
        }
    }
}