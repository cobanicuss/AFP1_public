using System;
using System.Linq;
using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Mapper
{
    public interface IMaterialMasterMessageMap : IMarkAsMapper
    {
        ZMATMAS5 MakeSoapMessage(MaterialMasterSapCommand message);
    }

    public class MaterialMasterMessageMap : IMaterialMasterMessageMap
    {
        public ZMATMAS5 MakeSoapMessage(MaterialMasterSapCommand message)
        {
            if (!message.Payload.MaterialMasterPayloadItem.Any()) throw new ArgumentException("No Payload found to make Soap Message. Cannot proceed!");

            var returnVal = new ZMATMAS5
            {
                IDOC = new MATMASMATMAS05ZMATMAS5
                {
                    EDI_DC40 = new EDI_DC40MATMASMATMAS05ZMATMAS5
                    {
                        TABNAM = DefaultSapVariables.Tabnam,
                        MANDT = DefaultSapVariables.Mandt,
                        DIRECT = EDI_DC40MATMASMATMAS05ZMATMAS5DIRECT.Item1,
                        DOCNUM = message.SagaReferenceId,
                        IDOCTYP = string.Empty,//Must be here; even if not needed. Welcome to SAP
                        MESTYP = string.Empty,//Must be here; even if not needed. Welcome to SAP
                        SNDPOR = string.Empty,//Must be here; even if not needed. Welcome to SAP
                        SNDPRT = DefaultSapVariables.SndPrt,
                        SNDPRN = ProfileConfigVariables.SndPrn,
                        RCVPOR = DefaultSapVariables.RcvPor,
                        RCVPRT = DefaultSapVariables.RcvPrt,
                        RCVPRN = DefaultSapVariables.RcvPrn
                    },
                    E1MARAM = message.Payload.MaterialMasterPayloadItem.Select(x => new ZMATMAS5E1MARAM
                    {
                        BISMT = x.E1MaramBismt,
                        MATKL = x.E1MaramMatkl,
                        PRDHA = x.E1MaramPrdha,
                        GROES = x.E1MaramGroes,
                        
                        E1MARCM = new[] 
                        { 
                            new ZMATMAS5E1MARCM
                            {
                                WERKS = x.E1MarcmWerks,
                                MSGFN = x.E1MarcmMsgfn,
                                PRCTR = x.E1MarcmPrctr,
                                EKGRP = x.E1MarcmEkgrp,
                                MAABC = x.E1MarcmMaabc,
                                DISPR = x.E1MarcmDispr,
                                BSTRF = x.E1MarcmBstrf,
                                DZEIT = x.E1MarcmDzeit
                            } 
                        },
                        E1MAKTM = new[] 
                        { 
                            new ZMATMAS5E1MAKTM
                            {
                                MAKTX = x.E1MaktmMaktx
                            } 
                        },
                        E1CUCFG = new[] 
                        { 
                            new ZMATMAS5E1CUCFG
                            {
                                /*POSEX = x.E1CucfgPosex0, Leave comment in. Not used here, IS used below*/
                                CONFIG_ID = x.E1CucfgConfigId0,
                                ROOT_ID = x.E1CucfgRootId0,

                                E1CUINS = new[]
                                {
                                    new ZMATMAS5E1CUINS
                                    {
                                        INST_ID = x.E1CuinsInstId0,
                                        OBJ_TYPE = x.E1CuinsObjType0,
                                        CLASS_TYPE = x.E1CuinsClassType0,
                                        OBJ_KEY = x.E1CuinsObjKey0
                                    }
                                },
                                E1CUVAL = new[] 
                                {
                                    //E1Cuval[0]//
                                    new ZMATMAS5E1CUVAL
                                    {
                                        INST_ID = x.E1CuvalInstId0,
                                        CHARC = x.E1CuvalCharc0,
                                        VALUE = x.E1CuvalValue0
                                    },
                                    //E1Cuval[1]//
                                    new ZMATMAS5E1CUVAL
                                    {
                                        INST_ID = x.E1CuvalInstId1,
                                        CHARC = x.E1CuvalCharc1,
                                        VALUE = x.E1CuvalValue1
                                    },
                                    //E1Cuval[2]//
                                    new ZMATMAS5E1CUVAL
                                    {
                                        INST_ID = x.E1CuvalInstId2,
                                        CHARC = x.E1CuvalCharc2,
                                        VALUE = x.E1CuvalValue2
                                    },
                                    //E1Cuval[3]//
                                    new ZMATMAS5E1CUVAL
                                    {
                                        INST_ID = x.E1CuvalInstId3,
                                        CHARC = x.E1CuvalCharc3,
                                        VALUE = x.E1CuvalValue3
                                    }
                                }
                            },
                            new ZMATMAS5E1CUCFG
                            {
                                POSEX = x.E1CucfgPosex1, /*Not used above but IS used here*/
                                CONFIG_ID = x.E1CucfgConfigId1,
                                ROOT_ID = x.E1CucfgRootId1,
                                E1CUINS = new[]
                                {
                                    new ZMATMAS5E1CUINS
                                    {
                                        INST_ID = x.E1CuinsInstId1,
                                        OBJ_TYPE = x.E1CuinsObjType1,
                                        CLASS_TYPE = x.E1CuinsClassType1,
                                        OBJ_KEY = x.E1CuinsObjKey1
                                    }
                                },
                                E1CUVAL = new[] 
                                {
                                    //E1Cuval[4]//
                                    new ZMATMAS5E1CUVAL
                                    {
                                        INST_ID = x.E1CuvalInstId4,
                                        CHARC = x.E1CuvalCharc4,
                                        VALUE = x.E1CuvalValue4
                                    },
                                    //E1Cuval[5]//
                                    new ZMATMAS5E1CUVAL
                                    {
                                        INST_ID = x.E1CuvalInstId5,
                                        CHARC = x.E1CuvalCharc5,
                                        VALUE = x.E1CuvalValue5
                                    },
                                    //E1Cuval[6]//
                                    new ZMATMAS5E1CUVAL
                                    {
                                        INST_ID = x.E1CuvalInstId6,
                                        CHARC = x.E1CuvalCharc6,
                                        VALUE = x.E1CuvalValue6
                                    },
                                    //E1Cuval[7]//
                                    new ZMATMAS5E1CUVAL
                                    {
                                        INST_ID = x.E1CuvalInstId7,
                                        CHARC = x.E1CuvalCharc7,
                                        VALUE = x.E1CuvalValue7
                                    }
                                }
                            }
                        },

                        E1MVKEM = new[] 
                        { 
                            new ZMATMAS5E1MVKEM
                            {
                                MSGFN = x.E1MvkemMsgfn0,
                                VKORG = x.E1MvkemVkorg0,
                                PRODH = x.E1MvkemProdh0,
                                PRAT1 = x.E1MvkemPrat10,
                                PRAT4 = x.E1MvkemPrat40

                            },
                            new ZMATMAS5E1MVKEM
                            {
                                MSGFN = x.E1MvkemMsgfn1,
                                VKORG = x.E1MvkemVkorg1,
                                PRODH = x.E1MvkemProdh1,
                                PRAT1 = x.E1MvkemPrat11,
                                PRAT4 = x.E1MvkemPrat41
                            }
                        },
                        E1MTXHM = new[] 
                        { 
                            new ZMATMAS5E1MTXHM
                            {
                                MSGFN = x.E1MtxhmMsgfn0,
                                TDOBJECT = x.E1MtxhmTdobject0,
                                TDID = x.E1MtxhmTdid0,
                                TDSPRAS = x.E1MtxhmTdspras0,
                                TDTEXTTYPE = x.E1MtxhmTdtexttype0,
                                TDNAME = x.E1MtxhmTdname0,
                                E1MTXLM = new[]
                                {
                                    new ZMATMAS5E1MTXLM
                                    {
                                        MSGFN = x.E1MtxlmMsgfn0,
                                        TDFORMAT = x.E1MtxlmTdformat0,
                                        TDLINE = x.E1MtxlmTdline0
                                    }
                                }
                            },
                            new ZMATMAS5E1MTXHM
                            {
                                MSGFN = x.E1MtxhmMsgfn1,
                                TDOBJECT = x.E1MtxhmTdobject1,
                                TDID = x.E1MtxhmTdid1,
                                TDSPRAS = x.E1MtxhmTdspras1,
                                TDTEXTTYPE = x.E1MtxhmTdtexttype1,
                                /*TDNAME = x.E1MtxhmTdname1, not used but leave comment in*/
                                E1MTXLM = new[]
                                {
                                    new ZMATMAS5E1MTXLM
                                    {
                                        MSGFN = x.E1MtxlmMsgfn1,
                                        TDFORMAT = x.E1MtxlmTdformat1,
                                        TDLINE = x.E1MtxlmTdline1
                                    }
                                }
                            },
                            new ZMATMAS5E1MTXHM
                            {
                                MSGFN = x.E1MtxhmMsgfn2,
                                TDOBJECT = x.E1MtxhmTdobject2,
                                TDID = x.E1MtxhmTdid2,
                                TDSPRAS = x.E1MtxhmTdspras2,
                                TDTEXTTYPE = x.E1MtxhmTdtexttype2,
                                TDNAME = x.E1MtxhmTdname2,
                                E1MTXLM = new[]
                                {
                                    new ZMATMAS5E1MTXLM
                                    {
                                        MSGFN = x.E1MtxlmMsgfn2,
                                        TDFORMAT = x.E1MtxlmTdformat2,
                                        TDLINE = x.E1MtxlmTdline2
                                    }
                                }
                            }
                        },
                        E1MLGNM = new[] 
                        { 
                            new ZMATMAS5E1MLGNM
                            {
                                MSGFN = x.E1MlgnmMsgfn,
                                LGNUM = x.E1MlgnmLgnum,
                                LGBKZ = x.E1MlgnmLgbkz,
                                LHMG1 = x.E1MlgnmLhmg1,
                                E1MLGTM = new[] 
                                {
                                    new ZMATMAS5E1MLGTM
                                    {
                                        KOBER = x.E1MlgtmKober,
                                        NSMNG = x.E1MlgtmNsmng
                                    }
                                }
                            }
                            
                        },
                        E1MBEWM = new[] 
                        { 
                            new ZMATMAS5E1MBEWM
                            {
                                MSGFN = x.E1MbewmMsgfn,
                                BWKEY = x.E1MbewmBwkey,
                                VERPR = x.E1MbewmVerpr
                            } 
                        },
                        Z1MARAM = new[] 
                        { 
                            new ZMATMAS5Z1MARAM
                            {
                                ZZSAN =  x.Z1MaramZzsan,
                                ZZQULN = x.Z1MaramZzquln,
                                ZZPCSN = x.Z1MaramZzpcsn,
                                ZZDM1N = x.Z1MaramZzdm1N,
                                ZZDM2N = x.Z1MaramZzdm2N,
                                ZZDM5N = x.Z1MaramZzdm5N,
                                ZZCOL1 = x.Z1MaramZzcol1,
                                ZZPRFN = x.Z1MaramZzprfn,
                                ZZSTOL = x.Z1MaramZzstol,
                                ZZROLL = x.Z1MaramZzroll,
                                ZZMTWC = x.Z1MaramZzmtwc,
                                ZZPKDM = x.Z1MaramZzpkdm
                            } 
                        },
                        E1MARMM = new[] 
                        {
                            new ZMATMAS5E1MARMM
                            {
                                MSGFN = x.E1MarmmMsgfn0,
                                MEINH = x.E1MarmmMeinh0,
                                UMREZ = x.E1MarmmUmrez0,
                                UMREN = x.E1MarmmUmren0
                            },
                            new ZMATMAS5E1MARMM
                            {
                                MSGFN = x.E1MarmmMsgfn1,
                                MEINH = x.E1MarmmMeinh1,
                                UMREZ = x.E1MarmmUmrez1,
                                UMREN = x.E1MarmmUmren1
                            },
                            new ZMATMAS5E1MARMM
                            {
                                MSGFN = x.E1MarmmMsgfn2,
                                MEINH = x.E1MarmmMeinh2,
                                UMREZ = x.E1MarmmUmrez2,
                                UMREN = x.E1MarmmUmren2
                            },
                            new ZMATMAS5E1MARMM
                            {
                                MSGFN = x.E1MarmmMsgfn3,
                                MEINH = x.E1MarmmMeinh3,
                                UMREZ = x.E1MarmmUmrez3,
                                UMREN = x.E1MarmmUmren3
                            },
                            new ZMATMAS5E1MARMM
                            {
                                MSGFN = x.E1MarmmMsgfn4,
                                MEINH = x.E1MarmmMeinh4,
                                UMREZ = x.E1MarmmUmrez4,
                                UMREN = x.E1MarmmUmren4,
                                NUMTP = x.E1MarmmNumtp4, 
                                VOLUM = x.E1MarmmVolume4,
                                VOLEH = x.E1MarmmVoleh4, 
                                BRGEW = x.E1MarmmBrgew4, 
                                GEWEI = x.E1MarmmGewei 
                            }
                       }

                    }).ToArray()
                }
            };

            return returnVal;
        }
    }
}