using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),

		TAPCHANGER							= 0x0001,
		TERMINAL							= 0x0002,
		POWERTR								= 0x0003,
		POWERTREND							= 0x0004,
        TAPCHANGCONTROL                     = 0x0005,
	}

    [Flags]
	public enum ModelCode : long
	{
		//CORE::
		//IdentifidObject
		IDOBJ								= 0x1000000000000000,//IdentifiedObject
        IDOBJ_GID							= 0x1000000000000104,//globalId
        IDOBJ_ALIASNAME						= 0x1000000000000207,//aliasName
        IDOBJ_MRID							= 0x1000000000000307,//mrId
        IDOBJ_NAME							= 0x1000000000000407,//name	
		//PowerSystemResource
        PSR									= 0x1100000000000000,//PowerSystemResource
        //Terminal
        TERMINAL							= 0x1200000000020000,//Terminal
        TERMINAL_CONNECTED					= 0x1200000000020101,//connected
        TERMINAL_PHASES						= 0x120000000002020a,//phases
        TERMINAL_SEQNUM						= 0x1200000000020304,//sequenceNumber
        TERMINAL_CONDEQUIPMENT				= 0x1200000000020409,//ConductingEquipment
        TERMINAL_REGCONTROLS				= 0x1200000000020519,//RegulatingControls
        TERMINAL_TRANSENDS					= 0x1200000000020619,//TransformerEnds
        //Equipment
        EQUIPMENT							= 0x1130000000000000,//Equipment
        EQUIPMENT_AGGREGATE					= 0x1130000000000101,//aggregate
        EQUIPMENT_NORMALSERVICE				= 0x1130000000000201,//normallylnService
        //ConductingEquipment
        CONDEQUIPMENT						= 0x1131000000000000,//ConductingEquipment
        CONDEQUIPMENT_TERMINALS				= 0x1131000000000119,//Terminals

        //WIRES::
        //TransformerEnd
        TRANSEND							= 0x1300000000000000,//TransformerEnd
        TRANSEND_TERMINAL					= 0x1300000000000109,//Terminal		
        //TapChanger
        TAPCHANGER							= 0x1110000000010000,//TapChanger
        TAPCHANGER_HIGHSTEP					= 0x1110000000010104,//highStep
        TAPCHANGER_INITDELAY				= 0x1110000000010205,//initialDelay
        TAPCHANGER_LOWSTEP					= 0x1110000000010304,//lowStep
        TAPCHANGER_LTCFLAG					= 0x1110000000010401,//ltcFlag
        TAPCHANGER_NTRSTEP					= 0x1110000000010504,//neutralStep
        TAPCHANGER_NTRU						= 0x1110000000010605,//neutralU
        TAPCHANGER_NORSTEP					= 0x1110000000010704,//normalStep
        TAPCHANGER_REGSTATUS                = 0x1110000000010801,//regulationStatus
        TAPCHANGER_SUBSDELAY                = 0x1110000000010905,//subsequentDelay
        TAPCHANGER_TCC                      = 0x1110000000010a09,//TapChangerControl
        //RegulatingControl
        REGCONTROL                          = 0x1120000000000000,//RegulatingControl
        REGCONTROL_DIS                      = 0x1120000000000101,//discrete
        REGCONTROL_MODE                     = 0x112000000000020a,//mode
        REGCONTROL_MONIPHASE                = 0x112000000000030a,//monitoredPhase
        REGCONTROL_TARGRANGE                = 0x1120000000000405,//targetRange
        REGCONTROL_TARGVALUE                = 0x1120000000000505,//targetValue
        //TapChangerControl
        TAPCHANGCONTROL                     = 0x1121000000050000,//TapChangerControl
        TAPCHANGCONTROL_LIMVOLT             = 0x1121000000050105,//limVoltage
        TAPCHANGCONTROL_LDC                 = 0x1121000000050201,//lineDropCompensation
        TAPCHANGCONTROL_LDR                 = 0x1121000000050305,//lineDropR
        TAPCHANGCONTROL_LDX                 = 0x1121000000050405,//lineDropX
        TAPCHANGCONTROL_RLDR                = 0x1121000000050505,//reverseLineDropR
        TAPCHANGCONTROL_RLDX                = 0x1121000000050605,//reverseLineDropX
        TAPCHANGCONTROL_TAPCHANGS           = 0x1121000000050719,//TapChangers
        //PowerTransformerEnd
        POWERTREND                          = 0x1310000000040000,//PowerTransformerEnd
        POWERTREND_B                        = 0x1310000000040105,//b
        POWERTREND_B0                       = 0x1310000000040205,//b0
        POWERTREND_CONNKIND                 = 0x131000000004030a,//connectionKind
        POWERTREND_G                        = 0x1310000000040405,//g
        POWERTREND_G0                       = 0x1310000000040505,//g0
        POWERTREND_PHANCLOCK                = 0x1310000000040604,//phaseAngleClock
        POWERTREND_R                        = 0x1310000000040705,//r
        POWERTREND_R0                       = 0x1310000000040805,//r0
        POWERTREND_RATEDS                   = 0x1310000000040905,//ratedS
        POWERTREND_RATEDU                   = 0x1310000000040a05,//ratedU
        POWERTREND_X                        = 0x1310000000040b05,//x
        POWERTREND_X0                       = 0x1310000000040c05,//x0
        POWERTREND_POWTRANSFORMER           = 0x1310000000040d09,//PowerTransformer
        //PowerTransformer
        POWERTR                             = 0x1131000000030000,//PowerTransformer
        POWERTR_VECGROUP                    = 0x1131000000030107,//vectorGroup
        POWERTR_POWERTRENDS                 = 0x1131000000030219,//PowerTransformerEnds
    }

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}


