using System.Collections.Generic;

namespace TopDriveSystem.Model.Listening
{
    public delegate void DataReceivedDelegate<in T>(IList<byte> bytes, T data);
}