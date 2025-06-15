using System;

namespace FTN.Common
{	
	public enum PhaseCode : short
	{
		A = 0x0,
		AB = 0x1,
		ABC = 0x2,
		ABCN = 0x3,
		ABN = 0x4,
		AC = 0x5,
		ACN = 0x6,
		AN = 0x7,
		B = 0x8,
		BC = 0x9,
		BCN = 0xA,
		BN = 0xB,
		C = 0xC,
		CN = 0xD,
		N = 0xE,
		s1 = 0xF,
		s12 = 0x10,
		s12N = 0x11,
		s1N = 0x12,
		s2 = 0x13,
		s2N = 0x14
    }
	
	public enum WindingConnection : short
	{
		A = 0,      
        D = 1,		
		I = 2,		
		Y = 3,		
		Yn = 4,		
		Z = 5,   
		Zn = 6,		
	}

	public enum RegulatingControlModeKind : short
	{
        activePower = 1,    //Active power is specified.Add commentMore actions
        admittance = 2,     //Admittance is specified.
        currentFlow = 3,    //Current flow is specified.
        @fixed = 4,         //The regulation mode is fixed, and thus not regulating.
        powerFactor = 5,    //Power factor is specified.
        reactivePower = 6,  //Reactive power is specified.
        temperature = 7,    //Control switches on/off based on the local temperature (i.e., a thermostat).
        timeScheduled = 8,  //Control switches on/off by time of day. The times may change on the weekend, or in different seasons.
        voltage = 9,        //Voltage is specified.
    }			
}
