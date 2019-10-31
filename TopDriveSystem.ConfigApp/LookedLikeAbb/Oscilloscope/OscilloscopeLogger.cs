using System;
using AlienJust.Support.Mvvm;
using TopDriveSystem.ConfigApp.AppControl.ParamLogger;

namespace TopDriveSystem.ConfigApp.LookedLikeAbb.Oscilloscope {
	class OscilloscopeLogger : ViewModelBase, IParameterLogger {
		public void LogAnalogueParameter(string parameterName, double? value) {
			throw new NotImplementedException();
		}

		public void LogDiscreteParameter(string parameterName, bool? value) {
			throw new NotImplementedException(); // cannot register, or can?
		}

		public void RemoveSeries(string parameterName) {
			throw new NotImplementedException();
		}
	}
}
