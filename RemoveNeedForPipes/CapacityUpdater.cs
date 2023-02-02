using ColossalFramework;
using ICities;
using System;

namespace RemoveNeedForPipes
{
    public class CapacityUpdater : IThreadingExtension
    {
        public void OnAfterSimulationFrame()
        {
            
        }

        public void OnAfterSimulationTick()
        {
            
        }

        public void OnBeforeSimulationFrame()
        {
            
        }

        public void OnBeforeSimulationTick()
        {
            int CurrentDailyWaterConsumption = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetWaterConsumption() / 7;

            WaterManagerMod.WaterCapacity = Math.Max(CurrentDailyWaterConsumption, 1000);

            int CurrentDailyHeatingConsumption = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetHeatingConsumption() / 7;

            WaterManagerMod.HeatingCapacity = Math.Max(CurrentDailyHeatingConsumption, 1000);
        }

        public void OnCreated(IThreading threading)
        {
            
        }

        public void OnReleased()
        {
            
        }

        public void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            
        }
    }
}