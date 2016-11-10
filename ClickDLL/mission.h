#ifndef __MISSION_H__
#define __MISSION_H__

#include <pul/pul.h>

#define CS_MISSIONCLASS_ATT PU_TAG_USER + 0x10000
#define CS_MISSIONCLASS_MTD PU_TAG_USER + 0x11000

#define CSA_MISSION_TITLE           CS_MISSIONCLASS_ATT + 1
#define CSM_MISSION_PARSEMISSION    CS_MISSIONCLASS_MTD + 1

// As of version 15.3.3 according to Jayde @ aodb.info, MAX(LENGTH(name)) is 104, adding some padding here...
#define AODB_MAX_NAME_LEN 127

typedef struct
{
    PUU8    pName[ AODB_MAX_NAME_LEN + 1 ];
    PUU32   Value;
    PUU32   QL;
    PUU32   IconKey;
} MissionItem;

typedef struct
{
    pusObjectCollection*    pCol;
    pusObjectCollection*    pSingleCol;
    pusObjectCollection*    pTeamCol;
    PUU8                    CashStr[ 16 ];
    PUU8                    XPStr[ 16 ];
    PUU8                    TimeStr[ 16 ];
    PUU8                    TypeStr[ 16 ];
    MissionItem             Reward;
    PUU8*                   pImageData;
} MissionClassData;

typedef struct
{
    PUU32   Key1;
    PUU32   Key2;
    PUU32   QL;
    PUU32   Padding;
} Item;

PUU32 MissionClassHandler( PULID _Object, PULID _Class, void* _pData, PUU32 _MethodID, PUU32 _Param1, PUU32 _Param2 );

enum
{
    ROOTOBJ,
    LOCATION,
    CASH,
    MISHXP,
    FINDITEM,
    IMAGE,
    FOLD,
    MISHTYPE,
    TOTALVAL,
    ITEM1,
    ITEM2,
    ITEM3,
    ITEM4,
    ITEM5,
    ITEM6,
    ITEMVAL1,
    ITEMVAL2,
    ITEMVAL3,
    ITEMVAL4,
    ITEMVAL5,
    ITEMVAL6,
    ITEMROW1,
    ITEMROW2,
    ITEMROW3,
    ITEMROW4,
    ITEMROW5,
    ITEMROW6,
};

PULID RegisterMissionClass();

#endif
