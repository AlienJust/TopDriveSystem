using TopDriveSystem.Commands.BsEthernetNominals;

namespace TopDriveSystem.ConfigApp.BsEthernetNominals
{
    public class BsEthernetNominalsSimple : IBsEthernetNominals
    {
        public BsEthernetNominalsSimple(short ratedRotationFriquencyCalculated, short ratedPwmModulationCoefficient,
            short ratedMomentumCurrentSetting, short ratedRadiatorTemperature, short ratedDcBusVoltage,
            short ratedAllPhasesCurrentAmplitudeEnvelopeCurve, short ratedRegulatorCurrentDoutput,
            short ratedRegulatorCurrentQoutput, short ratedFriquencyIntensitySetpointOutput, short ratedFlowSetting,
            short ratedMeasuredMoment, short ratedSpeedRegulatorOutputOrMomentSetting, short ratedMeasuredFlow,
            short ratedSettingExcitationCurrent)
        {
            RatedRotationFriquencyCalculated = ratedRotationFriquencyCalculated;
            RatedPwmModulationCoefficient = ratedPwmModulationCoefficient;
            RatedMomentumCurrentSetting = ratedMomentumCurrentSetting;
            RatedRadiatorTemperature = ratedRadiatorTemperature;
            RatedDcBusVoltage = ratedDcBusVoltage;
            RatedAllPhasesCurrentAmplitudeEnvelopeCurve = ratedAllPhasesCurrentAmplitudeEnvelopeCurve;
            RatedRegulatorCurrentDoutput = ratedRegulatorCurrentDoutput;
            RatedRegulatorCurrentQoutput = ratedRegulatorCurrentQoutput;
            RatedFriquencyIntensitySetpointOutput = ratedFriquencyIntensitySetpointOutput;
            RatedFlowSetting = ratedFlowSetting;
            RatedMeasuredMoment = ratedMeasuredMoment;
            RatedSpeedRegulatorOutputOrMomentSetting = ratedSpeedRegulatorOutputOrMomentSetting;
            RatedMeasuredFlow = ratedMeasuredFlow;
            RatedSettingExcitationCurrent = ratedSettingExcitationCurrent;
        }

        public short RatedRotationFriquencyCalculated { get; }

        public short RatedPwmModulationCoefficient { get; }

        public short RatedMomentumCurrentSetting { get; }

        public short RatedRadiatorTemperature { get; }

        public short RatedDcBusVoltage { get; }

        public short RatedAllPhasesCurrentAmplitudeEnvelopeCurve { get; }

        public short RatedRegulatorCurrentDoutput { get; }

        public short RatedRegulatorCurrentQoutput { get; }

        public short RatedFriquencyIntensitySetpointOutput { get; }

        public short RatedFlowSetting { get; }

        public short RatedMeasuredMoment { get; }

        public short RatedSpeedRegulatorOutputOrMomentSetting { get; }

        public short RatedMeasuredFlow { get; }

        public short RatedSettingExcitationCurrent { get; }
    }
}