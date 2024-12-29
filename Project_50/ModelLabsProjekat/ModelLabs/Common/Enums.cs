using System;

namespace FTN.Common
{	
	public enum PhaseCode : short
	{
		Unknown = 0x0,
		N = 0x1,
		C = 0x2,
		CN = 0x3,
		B = 0x4,
		BN = 0x5,
		BC = 0x6,
		BCN = 0x7,
		A = 0x8,
		AN = 0x9,
		AC = 0xA,
		ACN = 0xB,
		AB = 0xC,
		ABN = 0xD,
		ABC = 0xE,
		ABCN = 0xF
	}
	
	public enum WindingConnection : short
	{
		Y = 1,		// Wye
		D = 2,		// Delta
		Z = 3,		// ZigZag
		I = 4,		// Single-phase connection. Phase-to-phase or phase-to-ground is determined by elements' phase attribute.
		Scott = 5,   // Scott T-connection. The primary winding is 2-phase, split in 8.66:1 ratio
		OY = 6,		// 2-phase open wye. Not used in Network Model, only as result of Topology Analysis.
		OD = 7		// 2-phase open delta. Not used in Network Model, only as result of Topology Analysis.
	}

    public enum RegulatingControlModeKind : short
	{
        activePower = 1,    //Active power is specified.
        admittance = 2,     //Admittance is specified.
        currentFlow = 3,	//Current flow is specified.
		@fixed = 4,         //The regulation mode is fixed, and thus not regulating.
        powerFactor = 5,    //Power factor is specified.
        reactivePower = 6,  //Reactive power is specified.
        temperature = 7,    //Control switches on/off based on the local temperature (i.e., a thermostat).
        timeScheduled = 8,  //Control switches on/off by time of day. The times may change on the weekend, or in different seasons.
        voltage = 9,		//Voltage is specified.
    }
}
