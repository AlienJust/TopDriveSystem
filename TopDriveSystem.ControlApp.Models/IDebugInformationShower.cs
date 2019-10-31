using System.Collections.Generic;

namespace TopDriveSystem.ControlApp.Models
{
    public interface IDebugInformationShower
    {
        void ShowBytes(IList<byte> bytes);
    }
}