using System.Collections.Generic;

namespace TopDriveSystem.ConfigApp {
	internal interface IDebugInformationShower {
		void ShowBytes(IList<byte> bytes);
	}
}