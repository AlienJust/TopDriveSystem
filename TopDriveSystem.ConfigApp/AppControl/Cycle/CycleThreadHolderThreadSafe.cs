using System;
using System.Collections.Generic;
using System.Threading;
using AlienJust.Support.Concurrent;

namespace TopDriveSystem.ConfigApp.AppControl.Cycle
{
    internal class CycleThreadHolderThreadSafe : ICycleThreadHolder
    {
        private readonly SingleThreadedRelayQueueWorkerProceedAllItemsBeforeStopNoLog<Action>
            _backWorker; // TODO: use it to stop worker on app close

        private readonly List<ICyclePart> _cycleParts;
        private readonly object _cyclePartsSync;

        public CycleThreadHolderThreadSafe()
        {
            // циклический опрос
            _cyclePartsSync = new object();
            _cycleParts = new List<ICyclePart>();
            _backWorker = new SingleThreadedRelayQueueWorkerProceedAllItemsBeforeStopNoLog<Action>("CycleBackWorker",
                a => a(), ThreadPriority.Lowest, true, null);
            _backWorker.AddWork(CycleWork);
        }

        public void RegisterAsCyclePart(ICyclePart part)
        {
            lock (_cyclePartsSync)
            {
                _cycleParts.Add(part);
            }
        }

        private void CycleWork()
        {
            while (true)
                lock (_cyclePartsSync)
                {
                    foreach (var cyclePart in _cycleParts)
                        if (!cyclePart.Cancel)
                            try
                            {
                                cyclePart.InCycleAction();
                                Thread.Sleep(20);
                            }
                            catch
                            {
                                Thread.Sleep(20);
                            }
                        else Thread.Sleep(10);
                }
        }
    }
}