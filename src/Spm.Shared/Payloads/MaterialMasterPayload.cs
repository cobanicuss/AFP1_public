using System;
using System.Collections.Generic;

namespace Spm.Shared.Payloads
{
    [Serializable]
    public class MaterialMasterPayload
    {
        public List<MaterialMasterPayloadItem> MaterialMasterPayloadItem { get; set; }
    }

    [Serializable]
    public class MaterialMasterPayloadItem
    {
        public string E1MaramBismt { get; set; }
        public string E1MarcmWerks { get; set; }
        public string E1MarcmMsgfn { get; set; }
        public string E1MaktmMaktx { get; set; }
        public string E1MaramMatkl { get; set; }
        public string E1MaramPrdha { get; set; }
        public string E1MaramGroes { get; set; }
        public string E1MarcmDzeit { get; set; }

        //E1Cucfg[E1Cuins[0]]\\
        public string E1CucfgPosex0 { get; set; }
        public string E1CucfgConfigId0 { get; set; }
        public string E1CucfgRootId0 { get; set; }
        public string E1CuinsInstId0 { get; set; }
        public string E1CuinsObjType0 { get; set; }
        public string E1CuinsClassType0 { get; set; }
        public string E1CuinsObjKey0 { get; set; }

        //E1Cucfg[E1Cuins[1]]\\
        public string E1CucfgPosex1 { get; set; }
        public string E1CucfgConfigId1 { get; set; }
        public string E1CucfgRootId1 { get; set; }
        public string E1CuinsInstId1 { get; set; }
        public string E1CuinsObjType1 { get; set; }
        public string E1CuinsClassType1 { get; set; }
        public string E1CuinsObjKey1 { get; set; }

        //E1Cuval[0]\\
        public string E1CuvalInstId0 { get; set; }
        public string E1CuvalCharc0 { get; set; }
        public string E1CuvalValue0 { get; set; }

        //E1Cuval[1]\\
        public string E1CuvalInstId1 { get; set; }
        public string E1CuvalCharc1 { get; set; }
        public string E1CuvalValue1 { get; set; }

        //E1Cuval[2]\\
        public string E1CuvalInstId2 { get; set; }
        public string E1CuvalCharc2 { get; set; }
        public string E1CuvalValue2 { get; set; }

        //E1Cuval[3]\\
        public string E1CuvalInstId3 { get; set; }
        public string E1CuvalCharc3 { get; set; }
        public string E1CuvalValue3 { get; set; }

        //E1Cuval[4]\\
        public string E1CuvalInstId4 { get; set; }
        public string E1CuvalCharc4 { get; set; }
        public string E1CuvalValue4 { get; set; }

        //E1Cuval[5]\\
        public string E1CuvalInstId5 { get; set; }
        public string E1CuvalCharc5 { get; set; }
        public string E1CuvalValue5 { get; set; }

        //E1Cuval[6]\\
        public string E1CuvalInstId6 { get; set; }
        public string E1CuvalCharc6 { get; set; }
        public string E1CuvalValue6 { get; set; }

        //E1Cuval[7]\\
        public string E1CuvalInstId7 { get; set; }
        public string E1CuvalCharc7 { get; set; }
        public string E1CuvalValue7 { get; set; }

        //E1Mvkem[0]\\
        public string E1MvkemMsgfn0 { get; set; }
        public string E1MvkemVkorg0 { get; set; }
        public string E1MvkemProdh0 { get; set; }
        public string E1MvkemPrat10 { get; set; }
        public string E1MvkemPrat40 { get; set; }

        //E1Mvkem[1]\\
        public string E1MvkemMsgfn1 { get; set; }
        public string E1MvkemVkorg1 { get; set; }
        public string E1MvkemProdh1 { get; set; }
        public string E1MvkemPrat11 { get; set; }
        public string E1MvkemPrat41 { get; set; }

        public string E1MarcmPrctr { get; set; }

        //E1Mtxhm[E1Mtxlm[0]]\\
        public string E1MtxhmMsgfn0 { get; set; }
        public string E1MtxhmTdobject0 { get; set; }
        public string E1MtxhmTdid0 { get; set; }
        public string E1MtxhmTdspras0 { get; set; }
        public string E1MtxhmTdtexttype0 { get; set; }
        public string E1MtxhmTdname0 { get; set; }
        public string E1MtxlmMsgfn0 { get; set; }
        public string E1MtxlmTdformat0 { get; set; }
        public string E1MtxlmTdline0 { get; set; }

        //E1Mtxhm[E1Mtxlm[1]]\\
        public string E1MtxhmMsgfn1 { get; set; }
        public string E1MtxhmTdobject1 { get; set; }
        public string E1MtxhmTdid1 { get; set; }
        public string E1MtxhmTdspras1 { get; set; }
        public string E1MtxhmTdtexttype1 { get; set; }
        public string E1MtxhmTdname1 { get; set; }
        public string E1MtxlmMsgfn1 { get; set; }
        public string E1MtxlmTdformat1 { get; set; }
        public string E1MtxlmTdline1 { get; set; }

        //E1Mtxhm[E1Mtxlm[2]]\\
        public string E1MtxhmMsgfn2 { get; set; }
        public string E1MtxhmTdobject2 { get; set; }
        public string E1MtxhmTdid2 { get; set; }
        public string E1MtxhmTdspras2 { get; set; }
        public string E1MtxhmTdtexttype2 { get; set; }
        public string E1MtxhmTdname2 { get; set; }
        public string E1MtxlmMsgfn2 { get; set; }
        public string E1MtxlmTdformat2 { get; set; }
        public string E1MtxlmTdline2 { get; set; }

        public string E1MarcmEkgrp { get; set; }
        public string E1MarcmMaabc { get; set; }
        public string E1MarcmDispr { get; set; }
        public string E1MarcmBstrf { get; set; }
        public string E1MlgnmMsgfn { get; set; }
        public string E1MlgnmLgnum { get; set; }
        public string E1MlgnmLgbkz { get; set; }
        public string E1MlgnmLhmg1 { get; set; }
        public string E1MlgtmKober { get; set; }
        public string E1MlgtmNsmng { get; set; }
        public string E1MbewmMsgfn { get; set; }
        public string E1MbewmBwkey { get; set; }
        public string E1MbewmVerpr { get; set; }
        public string Z1MaramZzsan { get; set; }
        public string Z1MaramZzquln { get; set; }
        public string Z1MaramZzpcsn { get; set; }
        public string Z1MaramZzdm1N { get; set; }
        public string Z1MaramZzdm2N { get; set; }
        public string Z1MaramZzdm5N { get; set; }
        public string Z1MaramZzcol1 { get; set; }
        public string Z1MaramZzprfn { get; set; }
        public string Z1MaramZzstol { get; set; }
        public string Z1MaramZzroll { get; set; }
        public string Z1MaramZzmtwc { get; set; }
        public string Z1MaramZzpkdm { get; set; }

        //E1Marmm[0]\\
        public string E1MarmmMsgfn0 { get; set; }
        public string E1MarmmMeinh0 { get; set; }
        public string E1MarmmUmrez0 { get; set; }
        public string E1MarmmUmren0 { get; set; }

        //E1Marmm[1]\\
        public string E1MarmmMsgfn1 { get; set; }
        public string E1MarmmMeinh1 { get; set; }
        public string E1MarmmUmrez1 { get; set; }
        public string E1MarmmUmren1 { get; set; }

        //E1Marmm[2]\\
        public string E1MarmmMsgfn2 { get; set; }
        public string E1MarmmMeinh2 { get; set; }
        public string E1MarmmUmrez2 { get; set; }
        public string E1MarmmUmren2 { get; set; }

        //E1Marmm[3]\\
        public string E1MarmmMsgfn3 { get; set; }
        public string E1MarmmMeinh3 { get; set; }
        public string E1MarmmUmrez3 { get; set; }
        public string E1MarmmUmren3 { get; set; }

        //E1Marmm4]\\
        public string E1MarmmMsgfn4 { get; set; }
        public string E1MarmmMeinh4 { get; set; }
        public string E1MarmmUmren4 { get; set; }
        public string E1MarmmUmrez4 { get; set; }
        public string E1MarmmNumtp4 { get; set; }
        public string E1MarmmVolume4 { get; set; }
        public string E1MarmmVoleh4 { get; set; }
        public string E1MarmmBrgew4 { get; set; }
        public string E1MarmmGewei { get; set; }
    }

    public class MaterialMasterPayloadAsString
    {
        public static string ToString(MaterialMasterPayloadItem item)
        {
           
            var str = $@"E1MaramBismt = {item.E1MaramBismt},
            E1MarcmWerks = {item.E1MarcmWerks},
            E1MarcmMsgfn = {item.E1MarcmMsgfn},
            E1MaktmMaktx = {item.E1MaktmMaktx},
            E1MaramMatkl = {item.E1MaramMatkl},
            E1MaramPrdha = {item.E1MaramPrdha},
            E1MaramGroes = {item.E1MaramGroes},
            E1CucfgPosex0 = {item.E1CucfgPosex0},
            E1CucfgConfigId0 = {item.E1CucfgConfigId0},
            E1CucfgRootId0 = {item.E1CucfgRootId0},
            E1CuinsInstId0 = {item.E1CuinsInstId0},
            E1CuinsObjType0 = {item.E1CuinsObjType0},
            E1CuinsClassType0 = {item.E1CuinsClassType0},
            E1CuinsObjKey0 = {item.E1CuinsObjKey0},
            E1CucfgPosex1 = {item.E1CucfgPosex1},
            E1CucfgConfigId1 = {item.E1CucfgConfigId1},
            E1CucfgRootId1 = {item.E1CucfgRootId1},
            E1CuinsInstId1 = {item.E1CuinsInstId1},
            E1CuinsObjType1 = {item.E1CuinsObjType1},
            E1CuinsClassType1 = {item.E1CuinsClassType1},
            E1CuinsObjKey1 = {item.E1CuinsObjKey1},
            E1CuvalInstId0 = {item.E1CuvalInstId0},
            E1CuvalCharc0 = {item.E1CuvalCharc0},
            E1CuvalValue0 = {item.E1CuvalValue0},
            E1CuvalInstId1 = {item.E1CuvalInstId1},
            E1CuvalCharc1 = {item.E1CuvalCharc1},
            E1CuvalValue1 = {item.E1CuvalValue1},
            E1CuvalInstId2 = {item.E1CuvalInstId2},
            E1CuvalCharc2 = {item.E1CuvalCharc2},
            E1CuvalValue2 = {item.E1CuvalValue2},
            E1CuvalInstId3 = {item.E1CuvalInstId3},
            E1CuvalCharc3 = {item.E1CuvalCharc3},
            E1CuvalValue3 = {item.E1CuvalValue3},
            E1CuvalInstId4 = {item.E1CuvalInstId4},
            E1CuvalCharc4 = {item.E1CuvalCharc4},
            E1CuvalValue4 = {item.E1CuvalValue4},
            E1CuvalInstId5 = {item.E1CuvalInstId5},
            E1CuvalCharc5 = {item.E1CuvalCharc5},
            E1CuvalValue5 = {item.E1CuvalValue5},
            E1CuvalInstId6 = {item.E1CuvalInstId6},
            E1CuvalCharc6 = {item.E1CuvalCharc6},
            E1CuvalValue6 = {item.E1CuvalValue6},
            E1CuvalInstId7 = {item.E1CuvalInstId7},
            E1CuvalCharc7 = {item.E1CuvalCharc7},
            E1CuvalValue7 = {item.E1CuvalValue7},
            E1MvkemMsgfn0 = {item.E1MvkemMsgfn0},
            E1MvkemVkorg0 = {item.E1MvkemVkorg0},
            E1MvkemProdh0 = {item.E1MvkemProdh0},
            E1MvkemMsgfn1 = {item.E1MvkemMsgfn1},
            E1MvkemVkorg1 = {item.E1MvkemVkorg1},
            E1MvkemProdh1 = {item.E1MvkemProdh1},
            E1MarcmPrctr = {item.E1MarcmPrctr},
            E1MtxhmMsgfn0 = {item.E1MtxhmMsgfn0},
            E1MtxhmTdobject0 = {item.E1MtxhmTdobject0},
            E1MtxhmTdid0 = {item.E1MtxhmTdid0},
            E1MtxhmTdspras0 = {item.E1MtxhmTdspras0},
            E1MtxhmTdtexttype0 = {item.E1MtxhmTdtexttype0},
            E1MtxhmTdname0 = {item.E1MtxhmTdname0},
            E1MtxlmMsgfn0 = {item.E1MtxlmMsgfn0},
            E1MtxlmTdformat0 = {item.E1MtxlmTdformat0},
            E1MtxlmTdline0 = {item.E1MtxlmTdline0},
            E1MtxhmMsgfn1 = {item.E1MtxhmMsgfn1},
            E1MtxhmTdobject1 = {item.E1MtxhmTdobject1},
            E1MtxhmTdid1 = {item.E1MtxhmTdid1},
            E1MtxhmTdspras1 = {item.E1MtxhmTdspras1},
            E1MtxhmTdtexttype1 = {item.E1MtxhmTdtexttype1},
            E1MtxhmTdname1 = {item.E1MtxhmTdname1},
            E1MtxlmMsgfn1 = {item.E1MtxlmMsgfn1},
            E1MtxlmTdformat1 = {item.E1MtxlmTdformat1},
            E1MtxlmTdline1 = {item.E1MtxlmTdline1},
            E1MtxhmMsgfn2 = {item.E1MtxhmMsgfn2},
            E1MtxhmTdobject2 = {item.E1MtxhmTdobject2},
            E1MtxhmTdid2 = {item.E1MtxhmTdid2},
            E1MtxhmTdspras2 = {item.E1MtxhmTdspras2},
            E1MtxhmTdtexttype2 = {item.E1MtxhmTdtexttype2},
            E1MtxhmTdname2 = {item.E1MtxhmTdname2},
            E1MtxlmMsgfn2 = {item.E1MtxlmMsgfn2},
            E1MtxlmTdformat2 = {item.E1MtxlmTdformat2},
            E1MtxlmTdline2 = {item.E1MtxlmTdline2},
            E1MarcmEkgrp = {item.E1MarcmEkgrp},
            E1MarcmMaabc = {item.E1MarcmMaabc},
            E1MarcmDispr = {item.E1MarcmDispr},
            E1MarcmBstrf = {item.E1MarcmBstrf},
            E1MlgnmMsgfn = {item.E1MlgnmMsgfn},
            E1MlgnmLgnum = {item.E1MlgnmLgnum},
            E1MlgnmLgbkz = {item.E1MlgnmLgbkz},
            E1MlgnmLhmg1 = {item.E1MlgnmLhmg1},
            E1MlgtmKober = {item.E1MlgtmKober},
            E1MlgtmNsmng = {item.E1MlgtmNsmng},
            E1MbewmMsgfn = {item.E1MbewmMsgfn},
            E1MbewmBwkey = {item.E1MbewmBwkey},
            Z1MaramZzsan = {item.Z1MaramZzsan},
            Z1MaramZzquln = {item.Z1MaramZzquln},
            Z1MaramZzpcsn = {item.Z1MaramZzpcsn},
            Z1MaramZzdm1N = {item.Z1MaramZzdm1N},
            Z1MaramZzdm2N = {item.Z1MaramZzdm2N},
            Z1MaramZzdm5N = {item.Z1MaramZzdm5N},
            Z1MaramZzcol1 = {item.Z1MaramZzcol1},
            Z1MaramZzprfn = {item.Z1MaramZzprfn},
            Z1MaramZzstol = {item.Z1MaramZzstol},
            Z1MaramZzroll = {item.Z1MaramZzroll},
            Z1MaramZzmtwc = {item.Z1MaramZzmtwc},
            Z1MaramZzpkdm = {item.Z1MaramZzpkdm},
            E1MarmmMsgfn0 = {item.E1MarmmMsgfn0},
            E1MarmmMeinh0 = {item.E1MarmmMeinh0},
            E1MarmmUmrez0 = {item.E1MarmmUmrez0},
            E1MarmmUmren0 = {item.E1MarmmUmren0},
            E1MarmmMsgfn1 = {item.E1MarmmMsgfn1},
            E1MarmmMeinh1 = {item.E1MarmmMeinh1},
            E1MarmmUmrez1 = {item.E1MarmmUmrez1},
            E1MarmmUmren1 = {item.E1MarmmUmren1},
            E1MarmmMsgfn2 = {item.E1MarmmMsgfn2},
            E1MarmmMeinh2 = {item.E1MarmmMeinh2},
            E1MarmmUmrez2 = {item.E1MarmmUmrez2},
            E1MarmmUmren2 = {item.E1MarmmUmren2},";

            return str;
        }
    }
}