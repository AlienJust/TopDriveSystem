namespace TopDriveSystem.Commands.AinTelemetry
{
    public enum EngineState
    {
        PowerOn, // 0 Неопределенное состояние АИН (после включения/рестарта)
        WaitStart, // 1 (3 сек) Завершение переходных процессов в АИН, Ожидание пульта.
        WaitLinkAin1, //2 Ожидание связи с АИН1 (с ведущим). 

        /// <summary>
        ///     3 Проверка ролей АИН.
        /// </summary>
        TestImcw,
        PreResetErrors, //4 Подготовка к Сбросу аварий АИН3,2,1.

        ResetErrors, // 5 Сброс аварий АИН2,3,1 (ведущий - последний!).
        NotReady, // 6 НЕ Готов к запуску двигателя.
        ReadyToSwitchOn, // 7 Готов к работе.
        ReadyRun, // 8 Готов к запуску двигателя.

        EnableOperation, // 9 Запущен ШИМ, RAMP_OUT_ZERO.
        EnableOutput, // 10 Запущен ШИМ, RAMP_HOLD
        AcceleratorEnable, // 11 Запущен ШИМ, RAMP_IN_ZERO
        OperatingState, // 12 Запущен двигатель с ненулевой скоростью

        ReadySlave, // 13 Готов к запуску двигателя с управлением от мастера по CAN
        DriveSlave, //14 Двигатель запущен с управлением от мастера по CAN.
        Off2, //15 Останов выбегом (высший приоритет).
        Off3, //16 Аварийный Останов линейным замедлением (средний приоритет).
        Off1, //17 Останов линейным замедлением (низший приоритет).

        Inching1, //18 Толчок1.
        Inching2, //19 Толчок2.
        PostInching, //20 После толчка поддержание ключей включенными

        FaultState, //21 Авария.
        FaultStateWaitReset, // 22 Авария ожидаем рестарт
        SwitchOnInhibit, // 23 Ожидание штатного отключения после аварийного
        InhibitOperationActive, // 24 Ожидание штатного отключения после пропадания сигнала RUN.
        ChopperNotReady, // 25 Чоппер не готов
        ChopperRun, //26 Чоппер запущен.
        ReadKIs, //27 Опрос КИ.
        ReadMOs, //28 Опрос МО.

        /// <summary>
        ///     29 Начало процедуры определения параметров двигателя
        /// </summary>
        AciIdentifyStart,

        /// <summary>
        ///     30 Проведение процедуры определения параметров двигателя
        /// </summary>
        AciIdentifyExecution,

        /// <summary>
        ///     31 РЕЗЕРВ Завершение процедуры определения параметров двигателя
        /// </summary>
        AciIdentifyEnd,

        /// <summary>
        ///     32 ТРАМВАЙ. Отключен. На тормозе.
        /// </summary>
        TramStop,

        /// <summary>
        ///     33 ТРАМВАЙ. Двигатель намагничивается. На тормозе.
        /// </summary>
        TramMagnetizing,

        /// <summary>
        ///     34 ТРАМВАЙ. Разгон. Движение ВПЕРЕД.
        /// </summary>
        TramAccelerationForward,

        /// <summary>
        ///     35 ТРАМВАЙ. Движение без тяги. Движение ВПЕРЕД.
        /// </summary>
        TramMovingForward,

        /// <summary>
        ///     36 ТРАМВАЙ. Торможение. Движение ВПЕРЕД.
        /// </summary>
        TramDecelerationForward,

        /// <summary>
        ///     37 ТРАМВАЙ. Разгон. Движение НАЗАД.
        /// </summary>
        TramAccelerationBackward,

        /// <summary>
        ///     38 ТРАМВАЙ. Движение без тяги. Движение НАЗАД.
        /// </summary>
        TramMovingBackward,

        /// <summary>
        ///     39 ТРАМВАЙ. Торможение. Движение НАЗАД.
        /// </summary>
        TramDecelerationBackward
    }
}