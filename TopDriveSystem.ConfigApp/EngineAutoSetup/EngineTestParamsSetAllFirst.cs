using TopDriveSystem.Commands.EngineTests;

namespace TopDriveSystem.ConfigApp.EngineAutoSetup
{
	/// <summary>
	///     Выдаст исключение при доступе к свойству, если оно не было сперва инициализировано
	/// </summary>
	internal class EngineTestParamsSetAllFirst : IEngineTestParams
    {
        private int? _acc8;
        private int? _dir10;
        private float? _f0;
        private float? _f1;
        private float? _f2;
        private int? _k21;
        private float? _ki1;
        private float? _ki6;
        private float? _kp1;
        private float? _kp6;
        private float? _t1C;
        private float? _t2C;
        private float? _tauFi;
        private float? _tauI;
        private float? _tauSpd;
        private int? _te1;
        private int? _te6;
        private int? _tj1;
        private int? _tj2;
        private int? _tj3;
        private int? _tj4;

        public float F1
        {
            get => _f1.Value;
            set => _f1 = value;
        }

        public float F2
        {
            get => _f2.Value;
            set => _f2 = value;
        }

        public int Te1
        {
            get => _te1.Value;
            set => _te1 = value;
        }

        public int Te6
        {
            get => _te6.Value;
            set => _te6 = value;
        }

        public int K21
        {
            get => _k21.Value;
            set => _k21 = value;
        }

        public int Tj1
        {
            get => _tj1.Value;
            set => _tj1 = value;
        }

        public int Tj2
        {
            get => _tj2.Value;
            set => _tj2 = value;
        }

        public int Tj3
        {
            get => _tj3.Value;
            set => _tj3 = value;
        }

        public int Tj4
        {
            get => _tj4.Value;
            set => _tj4 = value;
        }

        public int Acc8
        {
            get => _acc8.Value;
            set => _acc8 = value;
        }

        public int Dir10
        {
            get => _dir10.Value;
            set => _dir10 = value;
        }

        public float T1C
        {
            get => _t1C.Value;
            set => _t1C = value;
        }

        public float T2C
        {
            get => _t2C.Value;
            set => _t2C = value;
        }

        public float Kp1
        {
            get => _kp1.Value;
            set => _kp1 = value;
        }

        public float Ki1
        {
            get => _ki1.Value;
            set => _ki1 = value;
        }

        public float Kp6
        {
            get => _kp6.Value;
            set => _kp6 = value;
        }

        public float Ki6
        {
            get => _ki6.Value;
            set => _ki6 = value;
        }

        public float TauI
        {
            get => _tauI.Value;
            set => _tauI = value;
        }

        public float TauSpd
        {
            get => _tauSpd.Value;
            set => _tauSpd = value;
        }

        public float TauFi
        {
            get => _tauFi.Value;
            set => _tauFi = value;
        }

        public float F0
        {
            get => _f0.Value;
            set => _f0 = value;
        }
    }
}