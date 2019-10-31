using System.Collections.Generic;

namespace TopDriveSystem.ConfigApp
{
    public interface IDebugInformationShower
    {
        void ShowBytes(IList<byte> bytes);
    }
}