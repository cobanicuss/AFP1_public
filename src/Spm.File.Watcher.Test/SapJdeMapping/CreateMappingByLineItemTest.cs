using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Spm.File.Watcher.Service;
using Spm.File.Watcher.Service.Dto;
using Spm.File.Watcher.Service.JdeToSapMapping;
using Spm.File.Watcher.Service.Validation;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class CreateMappingByLineItemTest
    {
        private ICreateMappingByLineItem _classUnderTest;
        private Mock<IImplementMapping> _validateMapping;
        private Mock<ICastDto> _castDto;

        private GeneralLedgerSapDto _generalLedgerSapDto;
        private GeneralLedgerItemDto _generalLedgerItemDto;

        private MappingResultGoodsDto _mappingResultGoodsDto;
        private GoodsDto _goodsDto;

        private MappingResultMaterialMasterDto _mappingResultMaterialMasterDto;
        private MaterialMasterDto _materialMasterDto;

        private PurchaseOrderSapDto _purchaseOrderSapDto;
        private PurchaseOrderDto _purchaseOrderDto;

        private readonly ICollection<ProblemDto> _mappingProblemList = new List<ProblemDto>();

        private const string DocDate = "a";
        private const string PstingDate = "b";
        private const string DocType = "c";
        private const string CostCentre = "d";
        private const string GlAccount = "e";
        private const string GlProfitCenter = "f";
        private const string HeaderTxt = "jde1";
        private const string RefDocNo = "jde2";
        private const int ItemNoAcc = 1;
        private const string AllocNmbr = "jde3";
        private const string AmtDoccur = "jde4";
        private const string SagaRefId = "AE61EF8B-EA7B-408C-9224-2C21513DCE73";
        private const string PstngDate = "a";
        private const string GmCode = "d";
        private const string Plant = "e";
        private const string StgeLoc = "f";
        private const string EntryUom = "g";
        private const string PoNumber = "h";
        private const string PoItem = "i";
        private const string EntryQnt = "jde2";
        private const string MvtInd = "jde3";
        private const string MoveType = "101";
        private const string PackWeight = "a";
        private const string KgDivMt = "b";
        private const string ProfitCenre = "d";
        private const string StorageType = "e";
        private const string StorageSection = "f";
        private const string MaterialGroup = "g";
        private const string ProductHierachy = "h";
        private const string ActualHeigh = "i";
        private const string ActualWidth = "j";
        private const string Length3Deci = "k";
        private const string RedBlueBlack = "l";
        private const string PhysicalPackSize = "m";
        private const string Prpm = "n";
        private const string ProductAttribute = "o";
        private const string SizeOne = "p";
        private const string Denominator0 = "q";
        private const string Numerator0 = "r";
        private const string TdLine = "s";
        private const string Maktx = "t";
        private const string Dzeit = "u";
        private const string Zzdm2N = "v";
        private const string UnitCost = "w";
        private const string Aitm = "jde1";
        private const string Kgs = "jde2";
        private const string Srp1 = "jde3";
        private const string Dsc1 = "jde4";
        private const string Dsc2 = "jde5";
        private const string Prp2Desc2 = "jde6";
        private const string Srp6Desc = "jde7";
        private const string Bq = "jde8";
        private const string Prp1Desc2 = "jde9";
        private const string Sec2 = "jde10";
        private const string Sec1 = "jde11";
        private const string Srp7Desc = "jde12";
        private const string Draw = "jde13";
        private const string Prp0 = "jde14";
        private const string Prp4 = "jde15";
        private const string Dsc12 = "jde16";
        private const string Prp5Desc = "jde17";
        private const string CompCode = "b";
        private const string CreateDate = "d";
        private const string Vendor = "e";
        private const string PurchOrg = "f";
        private const string PurGroup = "g";
        private const string MatlGroup = "j";
        private const string PoUnit = "k";
        private const string OrderPrUn = "l";
        private const string NetPrice = "m";
        private const string CostCenter = "o";
        private const string DeliveryDate = "p";
        private const string Currency = "jde1";
        private const string DeleteInd = "jde2";
        private const string ShortText = "jde3";
        private const string VendMat = "jde4";
        private const string Quantity = "jde5";
        private const string PriceUnit = "jde6";
        private const string OverDlvTol = "jde7";
        private const string Acctasscat = "jde8";
        private const string PreqName = "jde9";
        private const string SerialNo = "jde10";

        private const int GuidStringLengthIs36 = 36;

        private static readonly string NoMoreGr = string.Empty;
        private static readonly string ItemText = string.Empty;

        [Test]
        public void JdeToSapMappingByLineItemGeneralLedger()
        {
            _generalLedgerItemDto = SetGeneralLedgerItemDto();

            this.Given(_ => MappingFromJdeToSapForGeneralLedger())
            .When(_ => MappingByLineItemDetailsForGeneralLedge())
            .Then(_ => ReturnedMappingShouldBeCorrectForGeneralLedge())

            .BDDfy();
        }

        [Test]
        public void JdeToSapMappingByLineItemForGoods()
        {
            _goodsDto = SetGoodsDto();

            this.Given(_ => MappingFromJdeToSapForGoods())
            .When(_ => MappingByLineItemDetailsForGoods())
            .Then(_ => ReturnedMappingShouldBeCorrectForGoods())

            .BDDfy();
        }

        [Test]
        public void JdeToSapMappingByLineItemShouldBeDoneCorrectForMaterialMaster()
        {
            _materialMasterDto = SetMaterialMasterDto();

            this.Given(_ => MappingFromJdeToSapForMaterialMaster())
            .When(_ => MappingIsDoneMaterialMaster())
            .Then(_ => ReturnedMappingShouldBeCorrectMaterialMaster())

            .BDDfy();
        }

        [Test]
        public void JdeToSapMappingByLineItemForPurchaseOrderChange()
        {
            this.Given(_ => MappingFromJdeToSapForPurchaseOrderChange())
            .When(_ => MappingIsDoneForPurchaseOrderChange())
            .Then(_ => ReturnedMappingShouldBeCorrectForPurchaseOrderChange())

            .BDDfy();
        }

        [Test]
        public void JdeToSapMappingByLineItemForPurchaseOrderCreate()
        {
            this.Given(_ => MappingFromJdeToSapForPurchaseOrderCreate())
            .When(_ => MappingIsDoneForPurchaseOrderCreate())
            .Then(_ => ReturnedMappingShouldBeCorrectForPurchaseOrderCreate())

            .BDDfy();
        }

        private void MappingFromJdeToSapForGeneralLedger()
        {
            _castDto = new Mock<ICastDto>();

            _validateMapping = new Mock<IImplementMapping>();

            _validateMapping.Setup(x => x.ForGlDocDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(DocDate);
            _validateMapping.Setup(x => x.ForPostingDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PstingDate);
            _validateMapping.Setup(x => x.ForDocType(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(DocType);
            _validateMapping.Setup(x => x.ForGlCostCenter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(CostCentre);
            _validateMapping.Setup(x => x.ForGlAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(GlAccount);
            _validateMapping.Setup(x => x.ForGlProfitCentre(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(GlProfitCenter);

            _classUnderTest = new CreateMappingByLineItem(_validateMapping.Object, _castDto.Object);
        }

        private void MappingByLineItemDetailsForGeneralLedge()
        {
            _generalLedgerSapDto = _classUnderTest.ForGeneralLedger(_generalLedgerItemDto, _mappingProblemList, 1);
        }

        private void ReturnedMappingShouldBeCorrectForGeneralLedge()
        {
            _validateMapping.Verify(x => x.ForGlDocDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPostingDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForDocType(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForGlCostCenter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForGlAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForGlProfitCentre(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));

            Assert.AreEqual(_generalLedgerSapDto.UserName, Constants.DefaultUserName);
            Assert.AreEqual(_generalLedgerSapDto.HeaderTxt, _generalLedgerItemDto.HeaderTxt);
            Assert.AreEqual(_generalLedgerSapDto.CompCode, Constants.Default2Zeros70);
            Assert.AreEqual(_generalLedgerSapDto.DocDate, DocDate);
            Assert.AreEqual(_generalLedgerSapDto.PstingDate, PstingDate);
            Assert.AreEqual(_generalLedgerSapDto.DocType, DocType);
            Assert.AreEqual(_generalLedgerSapDto.RefDocNo, _generalLedgerItemDto.RefDocNo);
            Assert.AreEqual(_generalLedgerSapDto.ItemNoAcc, _generalLedgerItemDto.ItemNoAcc);
            Assert.AreEqual(_generalLedgerSapDto.AllocNmbr, _generalLedgerItemDto.AllocNmbr);
            Assert.AreEqual(_generalLedgerSapDto.ItemText, ItemText);
            Assert.AreEqual(_generalLedgerSapDto.CostCentre, CostCentre);
            Assert.AreEqual(_generalLedgerSapDto.GlAccount, GlAccount);
            Assert.AreEqual(_generalLedgerSapDto.GlProfitCenter, GlProfitCenter);
            Assert.AreEqual(_generalLedgerSapDto.AmtDoccur, _generalLedgerItemDto.AmtDoccur);
            Assert.AreEqual(_generalLedgerSapDto.Currency, Constants.DefaultCurrency);
        }

        private static GeneralLedgerItemDto SetGeneralLedgerItemDto()
        {
            return new GeneralLedgerSapDto
            {
                HeaderTxt = HeaderTxt,
                RefDocNo = RefDocNo,
                ItemNoAcc = ItemNoAcc,
                AllocNmbr = AllocNmbr,
                AmtDoccur = AmtDoccur
            };
        }

        private void MappingFromJdeToSapForGoods()
        {
            _castDto = new Mock<ICastDto>();

            _validateMapping = new Mock<IImplementMapping>();

            _validateMapping.Setup(x => x.ForPostingDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PstngDate);
            _validateMapping.Setup(x => x.ForDocDate(It.IsAny<string>())).Returns(DocDate);
            _validateMapping.Setup(x => x.ForHeaderText(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(HeaderTxt);
            _validateMapping.Setup(x => x.ForGmCode(It.IsAny<string>())).Returns(GmCode);
            _validateMapping.Setup(x => x.ForPlant(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(Plant);
            _validateMapping.Setup(x => x.ForLocation(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(StgeLoc);
            _validateMapping.Setup(x => x.ForPoUnit(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(EntryUom);
            _validateMapping.Setup(x => x.ForPoNumber(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PoNumber);
            _validateMapping.Setup(x => x.ForPoItem(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PoItem);

            _classUnderTest = new CreateMappingByLineItem(_validateMapping.Object, _castDto.Object);
        }

        private void MappingByLineItemDetailsForGoods()
        {
            _mappingResultGoodsDto = _classUnderTest.ForGoods(_goodsDto, 1, Constants.GoodsReceiptType, SagaRefId);
        }

        private void ReturnedMappingShouldBeCorrectForGoods()
        {
            _validateMapping.Verify(x => x.ForPostingDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForDocDate(It.IsAny<string>()));
            _validateMapping.Verify(x => x.ForHeaderText(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForGmCode(It.IsAny<string>()));
            _validateMapping.Verify(x => x.ForPlant(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForLocation(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPoUnit(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPoNumber(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPoItem(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));

            Assert.IsFalse(_mappingResultGoodsDto.MappingProblemList.Any());

            Assert.AreEqual(_mappingResultGoodsDto.Mapped.SagaReferenceId.Length, GuidStringLengthIs36);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.PstngDate, PstngDate);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.DocDate, DocDate);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.RefDocNo, RefDocNo);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.HeaderTxt, HeaderTxt);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.GmCode, GmCode);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.Plant, Plant);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.StgeLoc, StgeLoc);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.MoveType, MoveType);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.EntryQnt, EntryQnt);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.EntryUom, EntryUom);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.PoNumber, PoNumber);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.PoItem, PoItem);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.MvtInd, MvtInd);
            Assert.AreEqual(_mappingResultGoodsDto.Mapped.NoMoreGr, NoMoreGr);
        }

        private static GoodsDto SetGoodsDto()
        {
            return new GoodsDto
                {
                    PstngDate = PstngDate,
                    DocDate = DocDate,
                    RefDocNo = RefDocNo,
                    HeaderTxt = HeaderTxt,
                    GmCode = GmCode,
                    Plant = Plant,
                    StgeLoc = StgeLoc,
                    MoveType = MoveType,
                    EntryQnt = EntryQnt,
                    EntryUom = EntryUom,
                    PoNumber = PoNumber,
                    PoItem = PoItem,
                    MvtInd = MvtInd,
                    NoMoreGr = NoMoreGr
            };
        }

        private void MappingFromJdeToSapForMaterialMaster()
        {
            _castDto = new Mock<ICastDto>();

            _castDto.Setup(x => x.AsMaterialMasterSagaDto(It.IsAny<MaterialMasterDto>())).Returns(SetMaterialMasterSagaDto());

            _validateMapping = new Mock<IImplementMapping>();

            _validateMapping.Setup(x => x.ForPackWeight(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PackWeight);
            _validateMapping.Setup(x => x.ForKgDivMt(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(KgDivMt);
            _validateMapping.Setup(x => x.ForPlantBranch(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(Plant);
            _validateMapping.Setup(x => x.ForProfitCenre(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(ProfitCenre);
            _validateMapping.Setup(x => x.ForStorageType(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(StorageType);
            _validateMapping.Setup(x => x.ForStorageSection(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(StorageSection);
            _validateMapping.Setup(x => x.ForMaterialGroupByPlant(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(MaterialGroup);
            _validateMapping.Setup(x => x.ForProductHierachy(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(ProductHierachy);
            _validateMapping.Setup(x => x.ForActualHeight(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(ActualHeigh);
            _validateMapping.Setup(x => x.ForActualWidth(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(ActualWidth);
            _validateMapping.Setup(x => x.ForThreeDecimalPlacesOnly(It.IsAny<string>())).Returns(Length3Deci);
            _validateMapping.Setup(x => x.ForRedBlueBlack(It.IsAny<string>())).Returns(RedBlueBlack);
            _validateMapping.Setup(x => x.ForPhysicalPackSize(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PhysicalPackSize);
            _validateMapping.Setup(x => x.ForPrp(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(Prpm);
            _validateMapping.Setup(x => x.ForProductAttribute(It.IsAny<string>())).Returns(ProductAttribute);
            _validateMapping.Setup(x => x.ForSizeOne(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(SizeOne);
            _validateMapping.Setup(x => x.ForDenominator(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(Denominator0);
            _validateMapping.Setup(x => x.ForNumerator(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(Numerator0);
            _validateMapping.Setup(x => x.ForTdLine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TdLine);
            _validateMapping.Setup(x => x.ForMaktx(It.IsAny<string>())).Returns(Maktx);
            _validateMapping.Setup(x => x.ForDzeit(It.IsAny<string>())).Returns(Dzeit);
            _validateMapping.Setup(x => x.ForZzdm2N(It.IsAny<string>(), It.IsAny<string>())).Returns(Zzdm2N);
            _validateMapping.Setup(x => x.ForUnitCost(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(UnitCost);

            _classUnderTest = new CreateMappingByLineItem(_validateMapping.Object, _castDto.Object);
        }

        private void MappingIsDoneMaterialMaster()
        {
            _mappingResultMaterialMasterDto = _classUnderTest.ForMaterialMaster(_materialMasterDto, 1, SagaRefId);
        }

        private void ReturnedMappingShouldBeCorrectMaterialMaster()
        {
            _validateMapping.Verify(x => x.ForPackWeight(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForKgDivMt(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPlantBranch(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForProfitCenre(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForStorageType(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForStorageSection(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForMaterialGroupByPlant(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForProductHierachy(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForActualHeight(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForActualWidth(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForThreeDecimalPlacesOnly(It.IsAny<string>()));
            _validateMapping.Verify(x => x.ForRedBlueBlack(It.IsAny<string>()));
            _validateMapping.Verify(x => x.ForPhysicalPackSize(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPrp(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForProductAttribute(It.IsAny<string>()));
            _validateMapping.Verify(x => x.ForSizeOne(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForDenominator(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForNumerator(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForTdLine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _validateMapping.Verify(x => x.ForMaktx(It.IsAny<string>()));
            _validateMapping.Verify(x => x.ForDzeit(It.IsAny<string>()));
            _validateMapping.Verify(x => x.ForZzdm2N(It.IsAny<string>(), It.IsAny<string>()));
            _validateMapping.Verify(x => x.ForUnitCost(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));

            Assert.IsFalse(_mappingResultMaterialMasterDto.MappingProblemList.Any());

            var dictionaryValue = _mappingResultMaterialMasterDto.Mapped.Value;

            Assert.AreEqual(dictionaryValue.E1MaramBismt, Aitm);
            Assert.AreEqual(dictionaryValue.E1MarcmWerks, Plant);
            Assert.AreEqual(dictionaryValue.E1MarcmMsgfn, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MaktmMaktx, Maktx);
            Assert.AreEqual(dictionaryValue.E1MaramMatkl, MaterialGroup);
            Assert.AreEqual(dictionaryValue.E1MaramPrdha, ProductHierachy);
            Assert.AreEqual(dictionaryValue.E1MaramGroes, Kgs);
            Assert.AreEqual(dictionaryValue.E1MarcmDzeit, Dzeit);
            //Assert.AreEqual(dictionaryValue.E1CucfgPosex0, /*Not used: Leave comment in*/);
            Assert.AreEqual(dictionaryValue.E1CucfgConfigId0, Constants.Default5Zeros1);
            Assert.AreEqual(dictionaryValue.E1CucfgRootId0, Constants.Default7Zeros1);
            Assert.AreEqual(dictionaryValue.E1CuinsInstId0, Constants.Default7Zeros1);
            Assert.AreEqual(dictionaryValue.E1CuinsObjType0, Constants.DefaultMara);
            Assert.AreEqual(dictionaryValue.E1CuinsClassType0, Constants.DefaultThreeHundred);
            Assert.AreEqual(dictionaryValue.E1CuinsObjKey0, Constants.DefaultLength);
            Assert.AreEqual(dictionaryValue.E1CucfgPosex1, Plant);
            Assert.AreEqual(dictionaryValue.E1CucfgConfigId1, Constants.Default5Zeros2);
            Assert.AreEqual(dictionaryValue.E1CucfgRootId1, Constants.Default7Zeros1);
            Assert.AreEqual(dictionaryValue.E1CuinsInstId1, Constants.Default7Zeros1);
            Assert.AreEqual(dictionaryValue.E1CuinsObjType1, Constants.DefaultMara);
            Assert.AreEqual(dictionaryValue.E1CuinsClassType1, Constants.DefaultThreeHundred);
            Assert.AreEqual(dictionaryValue.E1CuinsObjKey1, Constants.DefaultLength);
            Assert.AreEqual(dictionaryValue.E1CuvalInstId0, Constants.Default7Zeros1);
            Assert.AreEqual(dictionaryValue.E1CuvalCharc0, Constants.DefaultLengthPerKgM);
            Assert.AreEqual(dictionaryValue.E1CuvalValue0, KgDivMt);
            Assert.AreEqual(dictionaryValue.E1CuvalInstId1, Constants.Default7Zeros1);
            Assert.AreEqual(dictionaryValue.E1CuvalCharc1, Constants.DefaultLengthActualHeight);
            Assert.AreEqual(dictionaryValue.E1CuvalValue1, ActualHeigh);
            Assert.AreEqual(dictionaryValue.E1CuvalInstId2, Constants.Default7Zeros1);
            Assert.AreEqual(dictionaryValue.E1CuvalCharc2, Constants.DefaultLengthActualWidth);
            Assert.AreEqual(dictionaryValue.E1CuvalValue2, ActualWidth);
            Assert.AreEqual(dictionaryValue.E1CuvalInstId3, Constants.Default7Zeros1);
            Assert.AreEqual(dictionaryValue.E1CuvalCharc3, Constants.DefaultLengthNrOfProcesses);
            Assert.AreEqual(dictionaryValue.E1CuvalValue3, Constants.Default1);
            Assert.AreEqual(dictionaryValue.E1CuvalInstId4, Constants.Default7Zeros1);
            Assert.AreEqual(dictionaryValue.E1CuvalCharc4, Constants.DefaultLengthPerKgM);
            Assert.AreEqual(dictionaryValue.E1CuvalValue4, KgDivMt);
            Assert.AreEqual(dictionaryValue.E1CuvalInstId5, Constants.Default7Zeros1);
            Assert.AreEqual(dictionaryValue.E1CuvalCharc5, Constants.DefaultLengthActualHeight);
            Assert.AreEqual(dictionaryValue.E1CuvalValue5, ActualHeigh);
            Assert.AreEqual(dictionaryValue.E1CuvalInstId6, Constants.Default7Zeros1);
            Assert.AreEqual(dictionaryValue.E1CuvalCharc6, Constants.DefaultLengthActualWidth);
            Assert.AreEqual(dictionaryValue.E1CuvalValue6, ActualWidth);
            Assert.AreEqual(dictionaryValue.E1CuvalInstId7, Constants.Default7Zeros1);
            Assert.AreEqual(dictionaryValue.E1CuvalCharc7, Constants.DefaultLengthNrOfProcesses);
            Assert.AreEqual(dictionaryValue.E1CuvalValue7, Constants.Default1);
            Assert.AreEqual(dictionaryValue.E1MvkemMsgfn0, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MvkemVkorg0, Constants.Default2Zeros20);
            Assert.AreEqual(dictionaryValue.E1MvkemProdh0, ProductHierachy);
            Assert.AreEqual(dictionaryValue.E1MvkemPrat10, ProductAttribute);
            Assert.AreEqual(dictionaryValue.E1MvkemPrat40, ProductAttribute);
            Assert.AreEqual(dictionaryValue.E1MvkemMsgfn1, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MvkemVkorg1, Constants.Default2Zeros70);
            Assert.AreEqual(dictionaryValue.E1MvkemProdh1, ProductHierachy);
            Assert.AreEqual(dictionaryValue.E1MvkemPrat11, ProductAttribute);
            Assert.AreEqual(dictionaryValue.E1MvkemPrat41, ProductAttribute);
            Assert.AreEqual(dictionaryValue.E1MarcmPrctr, ProfitCenre);
            Assert.AreEqual(dictionaryValue.E1MtxhmMsgfn0, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdobject0, Constants.DefaultMvke);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdid0, Constants.Default3Zeros1);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdspras0, Constants.DefaultEn);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdtexttype0, Constants.DefaultTx);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdname0, Constants.SalesTxtName1);
            Assert.AreEqual(dictionaryValue.E1MtxlmMsgfn0, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MtxlmTdformat0, Constants.DefaultStar);
            Assert.AreEqual(dictionaryValue.E1MtxlmTdline0, TdLine);
            Assert.AreEqual(dictionaryValue.E1MtxhmMsgfn1, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdobject1, Constants.DefaultMaterial);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdid1, Constants.DefaultBest);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdspras1, Constants.DefaultEn);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdtexttype1, Constants.DefaultTx);
            //Assert.AreEqual(dictionaryValue.//E1MtxhmTdname1, /*Not used: Leave comment in */);
            Assert.AreEqual(dictionaryValue.E1MtxlmMsgfn1, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MtxlmTdformat1, Constants.DefaultStar);
            Assert.AreEqual(dictionaryValue.E1MtxlmTdline1, TdLine);
            Assert.AreEqual(dictionaryValue.E1MtxhmMsgfn2, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdobject2, Constants.DefaultMvke);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdid2, Constants.Default3Zeros1);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdspras2, Constants.DefaultEn);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdtexttype2, Constants.DefaultTx);
            Assert.AreEqual(dictionaryValue.E1MtxhmTdname2, Constants.SalesTxtName2);
            Assert.AreEqual(dictionaryValue.E1MtxlmMsgfn2, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MtxlmTdformat2, Constants.DefaultStar);
            Assert.AreEqual(dictionaryValue.E1MtxlmTdline2, TdLine);
            Assert.AreEqual(dictionaryValue.E1MarcmEkgrp, Prpm);
            Assert.AreEqual(dictionaryValue.E1MarcmMaabc, Constants.DefaultA);
            Assert.AreEqual(dictionaryValue.E1MarcmDispr, Srp6Desc);
            Assert.AreEqual(dictionaryValue.E1MarcmBstrf, PackWeight);
            Assert.AreEqual(dictionaryValue.E1MlgnmMsgfn, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MlgnmLgnum, StorageType);
            Assert.AreEqual(dictionaryValue.E1MlgnmLgbkz, StorageSection);
            Assert.AreEqual(dictionaryValue.E1MlgnmLhmg1, Bq);
            Assert.AreEqual(dictionaryValue.E1MlgtmKober, StorageSection);
            Assert.AreEqual(dictionaryValue.E1MlgtmNsmng, PackWeight);
            Assert.AreEqual(dictionaryValue.E1MbewmMsgfn, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MbewmBwkey, Plant);
            Assert.AreEqual(dictionaryValue.E1MbewmVerpr, UnitCost);
            Assert.AreEqual(dictionaryValue.Z1MaramZzsan, Prp5Desc);
            Assert.AreEqual(dictionaryValue.Z1MaramZzquln, Prp2Desc2);
            Assert.AreEqual(dictionaryValue.Z1MaramZzpcsn, Prp1Desc2);
            Assert.AreEqual(dictionaryValue.Z1MaramZzdm1N, SizeOne);
            Assert.AreEqual(dictionaryValue.Z1MaramZzdm2N, Zzdm2N);
            Assert.AreEqual(dictionaryValue.Z1MaramZzdm5N, Length3Deci);
            Assert.AreEqual(dictionaryValue.Z1MaramZzcol1, RedBlueBlack);
            Assert.AreEqual(dictionaryValue.Z1MaramZzprfn, Srp7Desc);
            Assert.AreEqual(dictionaryValue.Z1MaramZzstol, Draw);
            Assert.AreEqual(dictionaryValue.Z1MaramZzroll, Prp0);
            Assert.AreEqual(dictionaryValue.Z1MaramZzmtwc, Prp4);
            Assert.AreEqual(dictionaryValue.Z1MaramZzpkdm, PhysicalPackSize);
            Assert.AreEqual(dictionaryValue.E1MarmmMsgfn0, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MarmmMeinh0, Constants.DefaultPk);
            Assert.AreEqual(dictionaryValue.E1MarmmUmrez0, Numerator0);
            Assert.AreEqual(dictionaryValue.E1MarmmUmren0, Denominator0);
            Assert.AreEqual(dictionaryValue.E1MarmmMsgfn1, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MarmmMeinh1, Constants.DefaultM);
            Assert.AreEqual(dictionaryValue.E1MarmmUmrez1, Numerator0);
            Assert.AreEqual(dictionaryValue.E1MarmmUmren1, Denominator0);
            Assert.AreEqual(dictionaryValue.E1MarmmMsgfn2, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MarmmMeinh2, Constants.DefaultEa);
            Assert.AreEqual(dictionaryValue.E1MarmmUmrez2, Numerator0);
            Assert.AreEqual(dictionaryValue.E1MarmmUmren2, Denominator0);
            Assert.AreEqual(dictionaryValue.E1MarmmMsgfn3, Constants.Default2Zeros9);
            Assert.AreEqual(dictionaryValue.E1MarmmMeinh3, Constants.DefaultTn);
            Assert.AreEqual(dictionaryValue.E1MarmmUmrez3, Constants.DefaultOneThousand);
            Assert.AreEqual(dictionaryValue.E1MarmmUmren3, Constants.Default1);
            Assert.AreEqual(dictionaryValue.E1MarmmMsgfn4, Constants.Default2Zeros5);
            Assert.AreEqual(dictionaryValue.E1MarmmMeinh4, Constants.DefaultKilograms);
            Assert.AreEqual(dictionaryValue.E1MarmmUmren4, Constants.Default1);
            Assert.AreEqual(dictionaryValue.E1MarmmUmrez4, Constants.Default1);
            Assert.AreEqual(dictionaryValue.E1MarmmNumtp4, Constants.DefaultZg);
            Assert.AreEqual(dictionaryValue.E1MarmmVolume4, Constants.Default1);
            Assert.AreEqual(dictionaryValue.E1MarmmVoleh4, Constants.DefaultKgp);
            Assert.AreEqual(dictionaryValue.E1MarmmBrgew4, Constants.Default1);
            Assert.AreEqual(dictionaryValue.E1MarmmGewei, Constants.DefaultKilograms);
        }

        private static MaterialMasterDto SetMaterialMasterDto()
        {
            return new MaterialMasterDto
            {
                Aitm = Aitm,
                Kgs = Kgs,
                Srp1 = Srp1,
                Dsc1 = Dsc1,
                Dsc2 = Dsc2,
                Prp2Desc2 = Prp2Desc2,
                Srp6Desc = Srp6Desc,
                Bq = Bq,
                Prp1Desc2 = Prp1Desc2,
                Sec2 = Sec2,
                Sec1 = Sec1,
                Srp7Desc = Srp7Desc,
                Draw = Draw,
                Prp0 = Prp0,
                Prp4 = Prp4,
                Dsc12 = Dsc12,
                Prp5Desc = Prp5Desc
            };
        }

        public static MaterialMasterSagaDto SetMaterialMasterSagaDto()
        {
            return new MaterialMasterSagaDto
            {
                SagaReferenceId = SagaRefId,
                Aitm = Aitm,
                Kgs = Kgs,
                Srp1 = Srp1,
                Dsc1 = Dsc1,
                Dsc2 = Dsc2,
                Prp2Desc2 = Prp2Desc2,
                Srp6Desc = Srp6Desc,
                Bq = Bq,
                Prp1Desc2 = Prp1Desc2,
                Sec2 = Sec2,
                Sec1 = Sec1,
                Srp7Desc = Srp7Desc,
                Draw = Draw,
                Prp0 = Prp0,
                Prp4 = Prp4,
                Dsc12 = Dsc12,
                Prp5Desc = Prp5Desc
            };
        }

        private void MappingFromJdeToSapForPurchaseOrderChange()
        {
            _castDto = new Mock<ICastDto>();

            _validateMapping = new Mock<IImplementMapping>();

            _validateMapping.Setup(x => x.ForPoNumber(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PoNumber);
            _validateMapping.Setup(x => x.ForCompCode(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(CompCode);
            _validateMapping.Setup(x => x.ForDocType(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(DocType);
            _validateMapping.Setup(x => x.ForCreateDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(CreateDate);
            _validateMapping.Setup(x => x.ForVendor(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(Vendor);
            _validateMapping.Setup(x => x.ForPurchaseOrg(It.IsAny<string>())).Returns(PurchOrg);
            _validateMapping.Setup(x => x.ForPurGroup(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PurGroup);
            _validateMapping.Setup(x => x.ForPoItem(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PoItem);
            _validateMapping.Setup(x => x.ForPlant(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(Plant);
            _validateMapping.Setup(x => x.ForMaterialGroup(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(MatlGroup);
            _validateMapping.Setup(x => x.ForPoUnit(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PoUnit);
            _validateMapping.Setup(x => x.ForOrderPrUn(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(OrderPrUn);
            _validateMapping.Setup(x => x.ForNetPrice(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(NetPrice);
            _validateMapping.Setup(x => x.ForGlAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(GlAccount);
            _validateMapping.Setup(x => x.ForCostCenter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(CostCenter);
            _validateMapping.Setup(x => x.ForDeliveryDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(DeliveryDate);

            _classUnderTest = new CreateMappingByLineItem(_validateMapping.Object, _castDto.Object);
        }

        private void MappingIsDoneForPurchaseOrderChange()
        {
            _purchaseOrderDto = SetPurchaseOrderDto();
            _purchaseOrderSapDto = _classUnderTest.ForPurchaseOrderChange(_purchaseOrderDto, _mappingProblemList, 1);
        }

        private void ReturnedMappingShouldBeCorrectForPurchaseOrderChange()
        {
            _validateMapping.Verify(x => x.ForCompCode(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForDocType(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForCreateDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForVendor(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPurchaseOrg(It.IsAny<string>()));
            _validateMapping.Verify(x => x.ForPurGroup(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPoItem(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPlant(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForMaterialGroup(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPoUnit(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForOrderPrUn(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForNetPrice(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForGlAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForCostCenter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForDeliveryDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));

            Assert.AreEqual(_purchaseOrderSapDto.PoNumber, PoNumber);
            Assert.AreEqual(_purchaseOrderSapDto.CompCode, CompCode);
            Assert.AreEqual(_purchaseOrderSapDto.DocType, DocType);
            Assert.AreEqual(_purchaseOrderSapDto.CreateDate, CreateDate);
            Assert.AreEqual(_purchaseOrderSapDto.CreatedBy, Constants.DefaultUserName);
            Assert.AreEqual(_purchaseOrderSapDto.ItemIntvl, Constants.DefaultItemIntvlChange);
            Assert.AreEqual(_purchaseOrderSapDto.Vendor, Vendor);
            Assert.AreEqual(_purchaseOrderSapDto.PurchOrg, PurchOrg);
            Assert.AreEqual(_purchaseOrderSapDto.PurGroup, PurGroup);
            Assert.AreEqual(_purchaseOrderSapDto.Currency, Currency);
            Assert.AreEqual(_purchaseOrderSapDto.PoItem, PoItem);
            Assert.AreEqual(_purchaseOrderSapDto.DeleteInd, DeleteInd);
            Assert.AreEqual(_purchaseOrderSapDto.ShortText, ShortText);
            Assert.AreEqual(_purchaseOrderSapDto.Plant, Plant);
            Assert.AreEqual(_purchaseOrderSapDto.StgeLoc, Constants.Default3Zeros1);
            Assert.AreEqual(_purchaseOrderSapDto.MatlGroup, MatlGroup);
            Assert.AreEqual(_purchaseOrderSapDto.VendMat, VendMat);
            Assert.AreEqual(_purchaseOrderSapDto.Quantity, Quantity);
            Assert.AreEqual(_purchaseOrderSapDto.PoUnit, PoUnit);
            Assert.AreEqual(_purchaseOrderSapDto.OrderPrUn, OrderPrUn);
            Assert.AreEqual(_purchaseOrderSapDto.NetPrice, NetPrice);
            Assert.AreEqual(_purchaseOrderSapDto.PriceUnit, PriceUnit);
            Assert.AreEqual(_purchaseOrderSapDto.OverDlvTol, OverDlvTol);
            Assert.AreEqual(_purchaseOrderSapDto.Acctasscat, Acctasscat);
            Assert.AreEqual(_purchaseOrderSapDto.PreqName, PreqName);
            Assert.AreEqual(_purchaseOrderSapDto.SchedLine, Constants.Default3Zeros1);
            Assert.AreEqual(_purchaseOrderSapDto.SerialNo, Constants.PurchaseOrderChangeSerialNo);
            Assert.AreEqual(_purchaseOrderSapDto.GlAccount, GlAccount);
            Assert.AreEqual(_purchaseOrderSapDto.CostCenter, CostCenter);
            Assert.AreEqual(_purchaseOrderSapDto.DeliveryDate, DeliveryDate);
        }

        private static PurchaseOrderDto SetPurchaseOrderDto()
        {
            return new PurchaseOrderDto
            {
                
                Currency = Currency,
                DeleteInd = DeleteInd,
                ShortText = ShortText,
                VendMat = VendMat,
                Quantity = Quantity,
                PriceUnit = PriceUnit,
                OverDlvTol = OverDlvTol,
                Acctasscat = Acctasscat,
                PreqName = PreqName
            };
        }

        private static PurchaseOrderDto SetPurchaseOrderCreateDto()
        {
            return new PurchaseOrderDto
            {
                SerialNo = SerialNo,
                PoNumber = PoNumber,
                Currency = Currency,
                DeleteInd = DeleteInd,
                ShortText = ShortText,
                VendMat = VendMat,
                Quantity = Quantity,
                PriceUnit = PriceUnit,
                OverDlvTol = OverDlvTol,
                Acctasscat = Acctasscat,
                PreqName = PreqName
            };
        }

        private void MappingFromJdeToSapForPurchaseOrderCreate()
        {
            _castDto = new Mock<ICastDto>();

            _validateMapping = new Mock<IImplementMapping>();

            _validateMapping.Setup(x => x.ForCompCode(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(CompCode);
            _validateMapping.Setup(x => x.ForDocType(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(DocType);
            _validateMapping.Setup(x => x.ForCreateDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(CreateDate);
            _validateMapping.Setup(x => x.ForVendor(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(Vendor);
            _validateMapping.Setup(x => x.ForPurchaseOrg(It.IsAny<string>())).Returns(PurchOrg);
            _validateMapping.Setup(x => x.ForPurGroup(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PurGroup);
            _validateMapping.Setup(x => x.ForPoItem(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PoItem);
            _validateMapping.Setup(x => x.ForPlant(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(Plant);
            _validateMapping.Setup(x => x.ForMaterialGroup(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(MatlGroup);
            _validateMapping.Setup(x => x.ForPoUnit(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(PoUnit);
            _validateMapping.Setup(x => x.ForOrderPrUn(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(OrderPrUn);
            _validateMapping.Setup(x => x.ForNetPrice(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(NetPrice);
            _validateMapping.Setup(x => x.ForGlAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(GlAccount);
            _validateMapping.Setup(x => x.ForCostCenter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(CostCenter);
            _validateMapping.Setup(x => x.ForDeliveryDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>())).Returns(DeliveryDate);

            _classUnderTest = new CreateMappingByLineItem(_validateMapping.Object, _castDto.Object);
        }

        private void MappingIsDoneForPurchaseOrderCreate()
        {
            _purchaseOrderDto = SetPurchaseOrderCreateDto();
            _purchaseOrderSapDto = _classUnderTest.ForPurchaseOrderCreate(_purchaseOrderDto, _mappingProblemList, 1);
        }

        private void ReturnedMappingShouldBeCorrectForPurchaseOrderCreate()
        {
            _validateMapping.Verify(x => x.ForCompCode(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForDocType(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForCreateDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForVendor(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPurchaseOrg(It.IsAny<string>()));
            _validateMapping.Verify(x => x.ForPurGroup(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPoItem(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPlant(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForMaterialGroup(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForPoUnit(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForOrderPrUn(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForNetPrice(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForGlAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForCostCenter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
            _validateMapping.Verify(x => x.ForDeliveryDate(It.IsAny<string>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));

            Assert.AreEqual(_purchaseOrderSapDto.PoNumber, PoNumber);
            Assert.AreEqual(_purchaseOrderSapDto.CompCode, CompCode);
            Assert.AreEqual(_purchaseOrderSapDto.DocType, DocType);
            Assert.AreEqual(_purchaseOrderSapDto.CreateDate, CreateDate);
            Assert.AreEqual(_purchaseOrderSapDto.CreatedBy, Constants.DefaultUserName);
            Assert.AreEqual(_purchaseOrderSapDto.ItemIntvl, Constants.DefaultPentaZero);
            Assert.AreEqual(_purchaseOrderSapDto.Vendor, Vendor);
            Assert.AreEqual(_purchaseOrderSapDto.PurchOrg, PurchOrg);
            Assert.AreEqual(_purchaseOrderSapDto.PurGroup, PurGroup);
            Assert.AreEqual(_purchaseOrderSapDto.Currency, Currency);
            Assert.AreEqual(_purchaseOrderSapDto.PoItem, PoItem);
            Assert.AreEqual(_purchaseOrderSapDto.DeleteInd, DeleteInd);
            Assert.AreEqual(_purchaseOrderSapDto.ShortText, ShortText);
            Assert.AreEqual(_purchaseOrderSapDto.Plant, Plant);
            Assert.AreEqual(_purchaseOrderSapDto.StgeLoc, Constants.Default3Zeros1);
            Assert.AreEqual(_purchaseOrderSapDto.MatlGroup, MatlGroup);
            Assert.AreEqual(_purchaseOrderSapDto.VendMat, VendMat);
            Assert.AreEqual(_purchaseOrderSapDto.Quantity, Quantity);
            Assert.AreEqual(_purchaseOrderSapDto.PoUnit, PoUnit);
            Assert.AreEqual(_purchaseOrderSapDto.OrderPrUn, OrderPrUn);
            Assert.AreEqual(_purchaseOrderSapDto.NetPrice, NetPrice);
            Assert.AreEqual(_purchaseOrderSapDto.PriceUnit, PriceUnit);
            Assert.AreEqual(_purchaseOrderSapDto.OverDlvTol, OverDlvTol);
            Assert.AreEqual(_purchaseOrderSapDto.Acctasscat, Acctasscat);
            Assert.AreEqual(_purchaseOrderSapDto.PreqName, PreqName);
            Assert.AreEqual(_purchaseOrderSapDto.SchedLine, Constants.Default3Zeros1);
            Assert.AreEqual(_purchaseOrderSapDto.SerialNo, SerialNo);
            Assert.AreEqual(_purchaseOrderSapDto.GlAccount, GlAccount);
            Assert.AreEqual(_purchaseOrderSapDto.CostCenter, CostCenter);
            Assert.AreEqual(_purchaseOrderSapDto.DeliveryDate, DeliveryDate);
        }
    }
}