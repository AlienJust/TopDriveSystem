namespace TopDriveSystem.Commands.AinTelemetry
{
    public enum FaultState
    {
        NoError = 0, // 0 Нет ошибок.
        RuleImcwConflict, // 1 Конфликт ролей каналов (по IMCW).
        RuleAinConflict, // 2 Конфликт ролей АИН при работе двух ПЧ на одну лебедку.
        NoAinLink, // 3 Нет связи с АИН.
        NotMagnetized, // 4 Двигатель не намагнитился за 5 сек.
        SpeedLimit, // 5 Превышение максимальной скорости длительное время.
        StatusError, // 6 Появление ошибок STATUS АИН.
        UdcLow, // 7
        AinLinkError, //8 Потеря связи с АИН.
        EthernetLinkError, //9 Потеря связи с Ethernet.
        CanLinkError, //10 Потеря связи по линии CAN.
        ChangedAinMode, //11 Изменился режим работы (Одиночный/ведущий/ведомый).
        SlaveNotReady, //12 В режиме Ведущий  не готов Ведомый.
        RelayBlocking, //13
        RelayAlarmMo, //14
        OverheatProtection, //15 Сработала тепловая защита
        SystemStart, // 16 Старт системы. Неопределенное состояние АИН (после включения/рестарта)
        ChangedControlSource // 17 Изменился источник управления во время движения
    }
}