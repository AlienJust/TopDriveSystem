using System;
using TopDriveSystem.Commands.EngineSettings;

namespace TopDriveSystem.ConfigApp.AppControl.EngineSettingsSpace
{
	/// <summary>
	/// Интерфейс чтения и настроек АИН
	/// </summary>
	internal interface IEngineSettingsReader
	{
		/// <summary>
		/// Асинхронное чтение настроек
		/// </summary>
		/// <param name="callback">Метод обратного вызова, в который передаётся внутреннее исключение (если возникло) и результирующие настройки</param>
		/// <param name="forceRead">Флаг, указывающий не использовать настройки считанные ранее</param>
		void ReadSettingsAsync(bool forceRead, Action<Exception, IEngineSettings> callback);
	}
}