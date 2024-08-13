using Spm.Shared;

namespace Spm.File.Watcher.Service.Dto
{
    public class MaterialMasterSapDto : IMarkAsDto
    {
        public string SagaReferenceId { get; set; }

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
        public string E1MarmmUmren3 { get; set; }
        public string E1MarmmUmrez3 { get; set; }

        //E1Marmm[4]\\
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
}