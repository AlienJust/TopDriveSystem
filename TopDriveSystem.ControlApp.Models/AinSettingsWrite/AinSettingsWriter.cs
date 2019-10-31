using System;
using System.Threading;
using TopDriveSystem.Commands.AinSettings;
using TopDriveSystem.ControlApp.Models.AinsCounter;
using TopDriveSystem.ControlApp.Models.AinSettingsRead;
using TopDriveSystem.ControlApp.Models.CommandSenderHost;
using TopDriveSystem.ControlApp.Models.TargetAddressHost;

namespace TopDriveSystem.ControlApp.Models.AinSettingsWrite
{
    internal class AinSettingsWriter : IAinSettingsWriter
    {
        private readonly IAinsCounter _ainsCounter;

        private readonly object _ainsCountSyncObject;
        private readonly IAinSettingsReader _ainSettingsReader;
        private readonly ICommandSenderHost _commandSenderHost;
        private readonly ITargetAddressHost _targerAddressHost;
        private readonly TimeSpan _writeSettingsTimeout;

        public AinSettingsWriter(ICommandSenderHost commandSenderHost, ITargetAddressHost targerAddressHost,
            IAinsCounter ainsCounter, IAinSettingsReader ainSettingsReader)
        {
            _commandSenderHost = commandSenderHost;
            _targerAddressHost = targerAddressHost;
            _ainsCounter = ainsCounter;
            _ainSettingsReader = ainSettingsReader;

            _ainsCountSyncObject = new object();

            _writeSettingsTimeout = TimeSpan.FromMilliseconds(300.0);
        }

        private int AinsCountThreadSafe
        {
            get
            {
                lock (_ainsCountSyncObject)
                {
                    return _ainsCounter.SelectedAinsCount;
                }
            }
        }

        public void WriteSettingsAsync(IAinSettingsPart settingsPart, Action<Exception> callback)
        {
            var sender = _commandSenderHost.Sender;
            if (sender == null) throw new NullReferenceException("Порт передачи данных не открыт");

            var ainsCountToWriteSettings = AinsCountThreadSafe;

            // Читаем настройки перед записью (из хранилища, или нет - неважно)
            _ainSettingsReader.ReadSettingsAsync(0, false, (readSettingsException, readedAin1Settings) =>
            {
                if (readSettingsException != null)
                {
                    callback(new Exception(
                        "Не удалось записать настройки, возникла ошибка при предварительном их чтении из блока АИН1 - нет ответа от BsEthernet",
                        readSettingsException));
                    return;
                }

                var settingsForAin1 = new AinSettingsWritable(readedAin1Settings);
                settingsForAin1.ModifyFromPart(settingsPart);
                settingsForAin1.Imcw = (ushort) (settingsForAin1.Imcw & 0xFCFF); // биты 8 и 9 занулены, ведущий
                settingsForAin1.ModifyFromPart(new AinSettingsPartWritable
                {
                    Ia0 = readedAin1Settings.Ia0, Ib0 = readedAin1Settings.Ib0, Ic0 = readedAin1Settings.Ic0,
                    Udc0 = readedAin1Settings.Udc0
                }); // Эти параметры всегда должны оставаться неизменными


                if (ainsCountToWriteSettings == 1)
                {
                    // Когда в системе один блок АИН
                    // TODO: проверить наличие связи с АИН1

                    settingsForAin1.Imcw =
                        (ushort) (settingsForAin1.Imcw & 0xF0FF); // биты 8,9,10 и 11 занулены, одиночая работа
                    var writeAin1SettingsCmd = new WriteAinSettingsCommand(0, settingsForAin1);
                    sender.SendCommandAsync(
                        _targerAddressHost.TargetAddress,
                        writeAin1SettingsCmd,
                        _writeSettingsTimeout, 2,
                        (sendException, replyBytes) =>
                        {
                            if (sendException != null)
                            {
                                callback(new Exception(
                                    "Ошибка отправки команды записи настроек АИН1 - нет ответа от BsEthernet",
                                    sendException));
                                return;
                            }

                            // Пауза _writeSettingsTimeout мс для того, чтобы АИН успел записать новые данные в EEPROM,
                            // а затем БС-Ethernet успел их вычитать из АИН.
                            Thread.Sleep(_writeSettingsTimeout);

                            // Проверка записи настроек АИН1 путем их повторного чтения
                            _ainSettingsReader.ReadSettingsAsync(0, true, (exceptionReRead1, settings1ReReaded) =>
                            {
                                if (exceptionReRead1 != null)
                                {
                                    callback(new Exception(
                                        "Не удалось проконтролировать корректность записи настроек АИН1 путём их повтороного вычитывания - нет ответа от BsEthernet"));
                                    return;
                                }

                                try
                                {
                                    settingsForAin1.CompareSettingsAfterReReading(settings1ReReaded, 0);
                                }
                                catch (Exception compareEx1)
                                {
                                    callback(new Exception(
                                        "Ошибка при повторном чтении настроек АИН1: " + compareEx1.Message,
                                        compareEx1));
                                    return;
                                }

                                callback(null);
                            });
                        });
                }


                else if (ainsCountToWriteSettings == 2)
                {
                    // Когда в системе два блока АИН
                    // TODO: проверить наличие связи с АИНами

                    settingsForAin1.Imcw =
                        (ushort) (settingsForAin1.Imcw & 0xF0FF); // биты 8,9,11 занулены, два АИНа в системе
                    settingsForAin1.Imcw =
                        (ushort) (settingsForAin1.Imcw | 0x0400); // бит 10 взведен, два АИНа в системе

                    var writeAin1SettingsCmd = new WriteAinSettingsCommand(0, settingsForAin1);
                    sender.SendCommandAsync(
                        _targerAddressHost.TargetAddress,
                        writeAin1SettingsCmd,
                        _writeSettingsTimeout, 2,
                        (sendException, replyBytes) =>
                        {
                            if (sendException != null)
                            {
                                callback(new Exception(
                                    "Ошибка отправки команды записи настроек АИН1 - нет ответа от BsEthernet",
                                    sendException));
                                return;
                            }

                            // Пауза _writeSettingsTimeout мс для того, чтобы АИН успел записать новые данные в EEPROM,
                            // а затем БС-Ethernet успел их вычитать из АИН.
                            Thread.Sleep(_writeSettingsTimeout);

                            // Проверка записи настроек АИН1 путем их повторного чтения
                            _ainSettingsReader.ReadSettingsAsync(0, true, (exceptionReRead1, settings1ReReaded) =>
                            {
                                if (exceptionReRead1 != null)
                                {
                                    callback(new Exception(
                                        "Не удалось проконтролировать корректность записи настроек АИН1 путём их повтороного вычитывания - нет ответа от BsEthernet"));
                                    return;
                                }

                                try
                                {
                                    settingsForAin1.CompareSettingsAfterReReading(settings1ReReaded, 0);
                                }
                                catch (Exception compareEx1)
                                {
                                    callback(new Exception(
                                        "Ошибка при повторном чтении настроек АИН1: " + compareEx1.Message,
                                        compareEx1));
                                    return;
                                }

                                Thread.Sleep(_writeSettingsTimeout);

                                // Читаем настройки АИН №2 перед записью (из хранилища, или нет - неважно)
                                _ainSettingsReader.ReadSettingsAsync(1, false,
                                    (readSettings2Exception, readedAin2Settings) =>
                                    {
                                        if (readSettings2Exception != null)
                                        {
                                            callback(new Exception(
                                                "Не удалось записать настройки, возникла ошибка при предварительном их чтении из блока АИН1 - нет ответа от BsEthernet",
                                                readSettings2Exception));
                                            return;
                                        }

                                        var settingsForAin2 = new AinSettingsWritable(readedAin1Settings);
                                        settingsForAin2
                                            .ModifyFromPart(
                                                settingsPart); // Модификация настроек значениями, введёнными пользователем
                                        settingsForAin2.ModifyFromPart(new AinSettingsPartWritable
                                        {
                                            Ia0 = readedAin2Settings.Ia0, Ib0 = readedAin2Settings.Ib0,
                                            Ic0 = readedAin2Settings.Ic0, Udc0 = readedAin2Settings.Udc0
                                        }); // Эти параметры всегда должны оставаться неизменными

                                        settingsForAin2.Imcw =
                                            (ushort) (settingsForAin2.Imcw & 0xF0FF); // биты 9,11 занулены, ведомый 1
                                        settingsForAin2.Imcw =
                                            (ushort) (settingsForAin2.Imcw | 0x0500); // биты 8,10 взведены,, ведомый 1

                                        var writeAin2SettingsCmd = new WriteAinSettingsCommand(1, settingsForAin2);
                                        sender.SendCommandAsync(
                                            _targerAddressHost.TargetAddress,
                                            writeAin2SettingsCmd,
                                            _writeSettingsTimeout, 2,
                                            (sendException2, replyBytes2) =>
                                            {
                                                if (sendException2 != null)
                                                {
                                                    callback(new Exception(
                                                        "Ошибка отправки команды записи настроек АИН2",
                                                        sendException2));
                                                    return;
                                                }

                                                // Пауза _writeSettingsTimeout мс для того, чтобы АИН успел записать новые данные в EEPROM,
                                                // а затем БС-Ethernet успел их вычитать из АИН.
                                                Thread.Sleep(_writeSettingsTimeout);

                                                // Проверка записи настроек АИН2 путем их повторного чтения
                                                _ainSettingsReader.ReadSettingsAsync(1, true,
                                                    (exceptionReRead2, settings2ReReaded) =>
                                                    {
                                                        if (exceptionReRead2 != null)
                                                        {
                                                            callback(new Exception(
                                                                "Не удалось проконтролировать корректность записи настроек АИН2 путём их повтороного вычитывания - нет ответа от BsEthernet"));
                                                            return;
                                                        }

                                                        try
                                                        {
                                                            settingsForAin2.CompareSettingsAfterReReading(
                                                                settings2ReReaded, 1);
                                                        }
                                                        catch (Exception compareEx2)
                                                        {
                                                            callback(new Exception(
                                                                "Ошибка при повторном чтении настроек АИН2: " +
                                                                compareEx2.Message, compareEx2));
                                                            return;
                                                        }

                                                        callback(null);
                                                    });
                                            });
                                    });
                            });
                        });
                }


                else if (ainsCountToWriteSettings == 3)
                {
                    // Когда в системе три блока АИН
                    // TODO: проверить наличие связи с АИНами

                    settingsForAin1.Imcw =
                        (ushort) (settingsForAin1.Imcw & 0xF0FF); // биты 8.9.10 занулены, три АИНа в системе
                    settingsForAin1.Imcw =
                        (ushort) (settingsForAin1.Imcw | 0x0800); // бит 11 взведен, три АИНа в системе

                    var writeAin1SettingsCmd = new WriteAinSettingsCommand(0, settingsForAin1);
                    sender.SendCommandAsync(
                        _targerAddressHost.TargetAddress,
                        writeAin1SettingsCmd,
                        _writeSettingsTimeout, 2,
                        (sendException, replyBytes) =>
                        {
                            if (sendException != null)
                            {
                                callback(new Exception("Ошибка отправки команды записи настроек АИН1", sendException));
                                return;
                            }

                            // Пауза _writeSettingsTimeout мс для того, чтобы АИН успел записать новые данные в EEPROM,
                            // а затем БС-Ethernet успел их вычитать из АИН.
                            Thread.Sleep(_writeSettingsTimeout);

                            // Проверка записи настроек АИН1 путем их повторного чтения
                            _ainSettingsReader.ReadSettingsAsync(0, true, (exceptionReRead1, settings1ReReaded) =>
                            {
                                if (exceptionReRead1 != null)
                                {
                                    callback(new Exception(
                                        "Не удалось проконтролировать корректность записи настроек АИН1 путём их повтороного вычитывания - нет ответа от BsEthernet"));
                                    return;
                                }

                                try
                                {
                                    settingsForAin1.CompareSettingsAfterReReading(settings1ReReaded, 0);
                                }
                                catch (Exception compareEx1)
                                {
                                    callback(new Exception(
                                        "Ошибка при повторном чтении настроек АИН1: " + compareEx1.Message,
                                        compareEx1));
                                    return;
                                }

                                Thread.Sleep(_writeSettingsTimeout);

                                // читаем настройки АИН2 (если нет в хранилище)
                                _ainSettingsReader.ReadSettingsAsync(1, false,
                                    (readSettings2Exception, readedAin2Settings) =>
                                    {
                                        if (readSettings2Exception != null)
                                        {
                                            callback(new Exception(
                                                "Не удалось записать настройки, возникла ошибка при предварительном их чтении из блока АИН1 - нет ответа от BsEthernet",
                                                readSettings2Exception));
                                            return;
                                        }

                                        var settingsForAin2 = new AinSettingsWritable(readedAin1Settings);
                                        settingsForAin2
                                            .ModifyFromPart(
                                                settingsPart); // Модификация настроек значениями, введёнными пользователем
                                        settingsForAin2.ModifyFromPart(new AinSettingsPartWritable
                                        {
                                            Ia0 = readedAin2Settings.Ia0, Ib0 = readedAin2Settings.Ib0,
                                            Ic0 = readedAin2Settings.Ic0, Udc0 = readedAin2Settings.Udc0
                                        }); // Эти параметры всегда должны оставаться неизменными
                                        settingsForAin2.Imcw =
                                            (ushort) (settingsForAin2.Imcw & 0xF0FF); // биты 9,10 занулены, ведомый 1
                                        settingsForAin2.Imcw =
                                            (ushort) (settingsForAin2.Imcw | 0x0900); // биты 8,11 взведены, ведомый 1

                                        var writeAin2SettingsCmd = new WriteAinSettingsCommand(1, settingsForAin2);
                                        sender.SendCommandAsync(
                                            _targerAddressHost.TargetAddress,
                                            writeAin2SettingsCmd,
                                            _writeSettingsTimeout, 2,
                                            (sendException2, replyBytes2) =>
                                            {
                                                if (sendException2 != null)
                                                {
                                                    callback(new Exception(
                                                        "Ошибка отправки команды записи настроек АИН2",
                                                        sendException2));
                                                    return;
                                                }

                                                // Пауза _writeSettingsTimeout мс для того, чтобы АИН успел записать новые данные в EEPROM,
                                                // а затем БС-Ethernet успел их вычитать из АИН.
                                                Thread.Sleep(_writeSettingsTimeout);

                                                // Проверка записи настроек АИН2 путем их повторного чтения:
                                                _ainSettingsReader.ReadSettingsAsync(1, true,
                                                    (exceptionReRead2, settings2ReReaded) =>
                                                    {
                                                        if (exceptionReRead2 != null)
                                                        {
                                                            callback(new Exception(
                                                                "Не удалось проконтролировать корректность записи настроек АИН2 путём их повтороного вычитывания - нет ответа от BsEthernet"));
                                                            return;
                                                        }

                                                        try
                                                        {
                                                            settingsForAin2.CompareSettingsAfterReReading(
                                                                settings2ReReaded, 1);
                                                        }
                                                        catch (Exception compareEx2)
                                                        {
                                                            callback(new Exception(
                                                                "Ошибка при повторном чтении настроек АИН2: " +
                                                                compareEx2.Message, compareEx2));
                                                            return;
                                                        }

                                                        Thread.Sleep(_writeSettingsTimeout);

                                                        // читаем настройки АИН3 (если нет в хранилище):
                                                        _ainSettingsReader.ReadSettingsAsync(2, false,
                                                            (readSettings3Exception, readedAin3Settings) =>
                                                            {
                                                                if (readSettings3Exception != null)
                                                                {
                                                                    callback(new Exception(
                                                                        "Не удалось записать настройки, возникла ошибка при предварительном их чтении из блока АИН1 - нет ответа от BsEthernet",
                                                                        readSettings3Exception));
                                                                    return;
                                                                }

                                                                var settingsForAin3 =
                                                                    new AinSettingsWritable(readedAin1Settings);
                                                                settingsForAin3
                                                                    .ModifyFromPart(
                                                                        settingsPart); // Модификация настроек значениями, введёнными пользователем
                                                                settingsForAin3.ModifyFromPart(
                                                                    new AinSettingsPartWritable
                                                                    {
                                                                        Ia0 = readedAin3Settings.Ia0,
                                                                        Ib0 = readedAin3Settings.Ib0,
                                                                        Ic0 = readedAin3Settings.Ic0,
                                                                        Udc0 = readedAin3Settings.Udc0
                                                                    }); // Эти параметры всегда должны оставаться неизменными
                                                                settingsForAin3.Imcw =
                                                                    (ushort) (settingsForAin3.Imcw &
                                                                              0xF0FF); // биты 8,10 занулены, ведомый 2
                                                                settingsForAin3.Imcw =
                                                                    (ushort) (settingsForAin3.Imcw |
                                                                              0x0A00); // биты 9,11 взведены, ведомый 2

                                                                var writeAin3SettingsCmd =
                                                                    new WriteAinSettingsCommand(2, settingsForAin3);
                                                                sender.SendCommandAsync(
                                                                    _targerAddressHost.TargetAddress,
                                                                    writeAin3SettingsCmd,
                                                                    _writeSettingsTimeout, 2,
                                                                    (sendException3, replyBytes3) =>
                                                                    {
                                                                        // Пауза _writeSettingsTimeout мс для того, чтобы АИН успел записать новые данные в EEPROM,
                                                                        // а затем БС-Ethernet успел их вычитать из АИН.
                                                                        Thread.Sleep(_writeSettingsTimeout);

                                                                        // Проверка записи настроек АИН3 путем их повторного чтения:
                                                                        _ainSettingsReader.ReadSettingsAsync(2, true,
                                                                            (exceptionReRead3, settings3ReReaded) =>
                                                                            {
                                                                                if (exceptionReRead3 != null)
                                                                                {
                                                                                    callback(new Exception(
                                                                                        "Не удалось проконтролировать корректность записи настроек АИН3 путём их повтороного вычитывания - нет ответа от BsEthernet"));
                                                                                    return;
                                                                                }

                                                                                try
                                                                                {
                                                                                    settingsForAin3
                                                                                        .CompareSettingsAfterReReading(
                                                                                            settings3ReReaded, 2);
                                                                                }
                                                                                catch (Exception compareEx3)
                                                                                {
                                                                                    callback(new Exception(
                                                                                        "Ошибка при повторном чтении настроек АИН3: " +
                                                                                        compareEx3.Message,
                                                                                        compareEx3));
                                                                                    return;
                                                                                }

                                                                                callback(null);
                                                                            });
                                                                    });
                                                            });
                                                    });
                                            });
                                    });
                            });
                        });
                }
                else
                {
                    callback.Invoke(new Exception("Неподдердживаемое число блоков АИН: " + ainsCountToWriteSettings +
                                                  ", поддерживается 1, 2 и 3 блока АИН"));
                }
            });
        }
    }
}