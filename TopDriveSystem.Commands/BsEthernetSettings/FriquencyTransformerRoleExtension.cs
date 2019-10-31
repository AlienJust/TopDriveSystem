using System;

namespace TopDriveSystem.Commands.BsEthernetSettings
{
    public static class FriquencyTransformerRoleExtension
    {
        public static FriquencyTransformerRole FromByte(byte value)
        {
            return value switch
            {
                1 => FriquencyTransformerRole.Single,
                2 => FriquencyTransformerRole.Master,
                3 => FriquencyTransformerRole.Slave,
                _ => throw new Exception("Недопустимое значение байта: " + value),
            };
        }

        public static byte ToByte(this FriquencyTransformerRole value)
        {
            return value switch
            {
                FriquencyTransformerRole.Single => (byte)1,
                FriquencyTransformerRole.Master => (byte)2,
                FriquencyTransformerRole.Slave => (byte)3,
                _ => throw new Exception("Невозможно представить данную роль ПЧ как байт"),
            };
        }

        public static string ToText(this FriquencyTransformerRole value)
        {
            return value switch
            {
                FriquencyTransformerRole.Single => "Одиночный прибор",
                FriquencyTransformerRole.Master => "Ведущий прибор",
                FriquencyTransformerRole.Slave => "Ведомый прибор",
                _ => throw new Exception("Невозможно представить данную роль ПЧ в виде текста"),
            };
        }
    }
}