namespace TopDriveSystem.ConfigApp.BsEthernetLogs
{
    internal interface IReadCycleModel
    {
        bool IsReadCycleEnabled { get; set; }
        event IcAnotherLogLineWasReadedOrNot AnotherLogLineWasReaded;

        /// <summary>
        ///     Если циклический опрос совсем больше не нужен
        /// </summary>
        void StopBackgroundThreadAndWaitForIt();
    }
}