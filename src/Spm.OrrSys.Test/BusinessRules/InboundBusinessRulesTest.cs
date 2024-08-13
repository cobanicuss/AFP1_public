using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Spm.OrrSys.Service.Business;
using Spm.OrrSys.Service.Domain;
using Spm.OrrSys.Service.Dto;
using Spm.Shared;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.BusinessRules
{
    [TestFixture]
    public class InboundBusinessRulesTest
    {
        private IProvideSapInboundSpecificBusinessRules _classUnderTest;
        private Mock<IUniqueNumbers> _uniqueNumbers;

        private PlannedOrderDto _plannedOrderDto;
        private ProductionOrderDto _productionOrderDto;
        private List<OrrSysF3460Z1> _finalPlannedOrderList;
        private List<OrrSysF3460Z1> _openPlannedOrders;
        private List<DemandSapJde> _inboundPlannedOrderList;
        private List<JdeItemMasterDetailDto> _jdeItemMasterDetailList;
        private PlannedOrderDto _inputPlannedOrderDto;

        private readonly DateTime _cyclePlannedBefore = DateTime.Now;

        private const string Unique15BitString = "A";
        private const double Qyy = 1d;
        private const string Ftaitm = "c";
        private const string Ftlitm = "d";
        private const string Ftmcu = "a";
        private const int Ftitm = 2;
        private const string Ftedbt = "b";
        private const string Ftedtn = "e";
        private const decimal Fteddt = 3m;

        private const int Count = 4;
        private const int OnlyOnePlannedOrderAdded = 1;
        private const int OnlyOneProductionOrderAdded = 1;

        private const int Ibitm = 1;
        private const string Iblitm = "f";
        private const int Qty = 2;

        private const string BaseUom = "g";
        private const string Ibmcu = "h";
        private const string ProOrder = "i";

        private const int StartCount = 1000;
        private const int JdeMultiplier = 10000;

        private IList<DemandSapJdeWo> _inboundProductionOrderList;
        private List<OrrSysF554801Z> _finalProductionOrderList;

        [SetUp]
        public void Setup()
        {
            _uniqueNumbers = new Mock<IUniqueNumbers>();
            _uniqueNumbers.Setup(x => x.GetUniqueNDigitNumberAsString(It.IsAny<int>())).Returns(Unique15BitString);

            _classUnderTest = new SapInboundSpecificBusinessRules(_uniqueNumbers.Object);
        }

        [Test]
        public void PlannedOrderDtoMustContainSpicificValues()
        {
            this.Given("Planned-Order dto must contain calculateable values")
            .When(_ => GettingPlannedOrderInboundDto())
            .Then(_ => PlannedOrderDtoDataMustBeCorrect())

            .BDDfy();
        }

        [Test]
        public void ProductionOrderDtoMustContainSpicificValues()
        {
            this.Given("Production-Order dto must contain calculateable values")
            .When(_ => GettingProductionOrderInboundDto())
            .Then(_ => ProductionOrderDtoDataMustBeCorrect())

            .BDDfy();
        }

        [Test]
        public void WhenAddingInboundPlannedOrdersToOpenPlannedOrdersIfJdeShortItemNrCannotBeFoundThenThrowError()
        {
            this.Given("Adding inbound Planned-Ordrs to Open-Planned orders and Jde Short-Item-Nr cannot be found")
            .When(_ => JdeShortItemDetailsCannotBeFound())
            .Then(_ => ErrorConditionIsRaised())

            .BDDfy();
        }

        [Test]
        public void AddingInboundPlannedOrdersToOpenPlannedOrdersShouldHaveAmalgematedResults()
        {
            this.Given("Adding inbound Planned-Ordrs to Open-Planned orders with proper details")
            .When(_ => CorrectPlannedOrderDataHaveBeenPassedIn())
            .Then(_ => PlannedOrderMustHaveCorrectData())

            .BDDfy();
        }

        [Test]
        public void AddingProductionOrdersToInboundListShouldContainCorrectData()
        {
            this.Given(_ => InboundProductionOrders())
            .When(_ => AddProductionOrdersToJdeList())
            .Then(_ => ProductionOrderMustHaveCorrectData())

            .BDDfy();
        }

        #region GIVEN
        private void InboundProductionOrders()
        {
            _inboundProductionOrderList = new List<DemandSapJdeWo>
            {
                new DemandSapJdeWo
                {
                    Ibitm = Ibitm,
                    Iblitm = Iblitm,
                    CyclePlannedBefore = _cyclePlannedBefore,
                    Qty = Qty,
                    BaseUom = BaseUom,
                    Ibmcu = Ibmcu,
                    ProOrder = ProOrder
                }
            };
        }
        #endregion

        #region WHEN
        private void GettingPlannedOrderInboundDto()
        {
            _plannedOrderDto = _classUnderTest.CreatePlannedOrderDto();
        }

        private void GettingProductionOrderInboundDto()
        {
            _productionOrderDto = _classUnderTest.CreateProductionOrderDto();
        }

        private void JdeShortItemDetailsCannotBeFound()
        {
            _jdeItemMasterDetailList = new List<JdeItemMasterDetailDto>();

            _inputPlannedOrderDto = new PlannedOrderDto();

            _openPlannedOrders = new List<OrrSysF3460Z1>();

            _inboundPlannedOrderList = new List<DemandSapJde>
            {
                new DemandSapJde
                {
                    Ibmcu = Ftmcu
                }
            };
        }

        private void CorrectPlannedOrderDataHaveBeenPassedIn()
        {
            _inboundPlannedOrderList = new List<DemandSapJde>
            {
                new DemandSapJde
                {
                    Ibitm = Ftitm,
                    Ibmcu = Ftmcu,
                    CyclePlannedBefore = _cyclePlannedBefore,
                    Qyy = Qyy
                }
            };

            _jdeItemMasterDetailList = new List<JdeItemMasterDetailDto>
            {
                new JdeItemMasterDetailDto
                {
                    Imitm = Ftitm,
                    Imaitm = Ftaitm,
                    Imlitm = Ftlitm
                }
            };

            _inputPlannedOrderDto = new PlannedOrderDto
            {
                Ftedbt = Ftedbt,
                Ftedtn = Ftedtn,
                Fteddt = Fteddt
            };

            _openPlannedOrders = new List<OrrSysF3460Z1>();
        }

        private void AddProductionOrdersToJdeList()
        {
            _productionOrderDto = _classUnderTest.CreateProductionOrderDto(); //has been tested independantly//
            _finalProductionOrderList = _classUnderTest.CreateJdeProductionOrders(_inboundProductionOrderList,
                _productionOrderDto);
        }
        #endregion

        #region THEN
        private void ErrorConditionIsRaised()
        {
            Assert.Throws<ArgumentException>(() => _classUnderTest.CreateJdePlannedOrders(
                _inboundPlannedOrderList,
                _jdeItemMasterDetailList,
                0,
                _inputPlannedOrderDto,
                _openPlannedOrders));
        }

        private void ProductionOrderMustHaveCorrectData()
        {
            const string onlyTestingOne = "Newly created Production-Order is added to ouput list, should only contain 1 item.";

            Assert.AreEqual(_finalProductionOrderList.Count, OnlyOneProductionOrderAdded, onlyTestingOne);
            Assert.AreEqual(_finalProductionOrderList[0].SYEDUS, Service.Business.Constants.Syedus);
            Assert.AreEqual(_finalProductionOrderList[0].SYEDBT, _productionOrderDto.Syedbt);
            Assert.AreEqual(_finalProductionOrderList[0].SYEDTN, _productionOrderDto.Syedln);
            Assert.AreEqual(_finalProductionOrderList[0].SYEDLN, StartCount);
            Assert.AreEqual(_finalProductionOrderList[0].SYEDCT, Service.Business.Constants.Syedct);
            Assert.AreEqual(_finalProductionOrderList[0].SYTYTN, Service.Business.Constants.Sytytn);
            Assert.AreEqual(_finalProductionOrderList[0].SYEDDT, ConvertDate.DateToJdeDate(DateTime.Now));
            Assert.AreEqual(_finalProductionOrderList[0].SYDRIN, Service.Business.Constants.Sydrin);
            Assert.AreEqual(_finalProductionOrderList[0].SYEDDL, StartCount);
            Assert.AreEqual(_finalProductionOrderList[0].SYEDSP, Service.Business.Constants.Syedsp);
            Assert.AreEqual(_finalProductionOrderList[0].SYTNAC, Service.Business.Constants.Sytnac);
            Assert.AreEqual(_finalProductionOrderList[0].SYDCTO, Service.Business.Constants.Syedct);
            Assert.AreEqual(_finalProductionOrderList[0].SYITM, _inboundProductionOrderList[0].Ibitm);
            Assert.AreEqual(_finalProductionOrderList[0].SYLITM, _inboundProductionOrderList[0].Iblitm);
            Assert.AreEqual(_finalProductionOrderList[0].SYDRQJ, ConvertDate.DateToJdeDate(_inboundProductionOrderList[0].CyclePlannedBefore));
            Assert.AreEqual(_finalProductionOrderList[0].SYUORG, _inboundProductionOrderList[0].Qty*JdeMultiplier);
            Assert.AreEqual(_finalProductionOrderList[0].SYSOQS, _inboundProductionOrderList[0].Qty*JdeMultiplier);
            Assert.AreEqual(_finalProductionOrderList[0].SYUM, _inboundProductionOrderList[0].BaseUom);
            Assert.AreEqual(_finalProductionOrderList[0].SYMCU, _inboundProductionOrderList[0].Ibmcu);
            Assert.AreEqual(_finalProductionOrderList[0].SYVR01, _inboundProductionOrderList[0].ProOrder);
        }

        private void PlannedOrderMustHaveCorrectData()
        {
            _finalPlannedOrderList = _classUnderTest.CreateJdePlannedOrders(
                _inboundPlannedOrderList,
                _jdeItemMasterDetailList,
                Count,
                _inputPlannedOrderDto,
                _openPlannedOrders);

            const string onlyTestingOne = "Newly created Planned-Order is added to ouput list, should only contain 1 item.";

            Assert.AreEqual(_finalPlannedOrderList.Count, OnlyOnePlannedOrderAdded, onlyTestingOne);
            Assert.AreEqual(_finalPlannedOrderList[0].FTAITM, Ftaitm);
            Assert.AreEqual(_finalPlannedOrderList[0].FTDCTO, Service.Business.Constants.Forcast);
            Assert.AreEqual(_finalPlannedOrderList[0].FTDRIN, Service.Business.Constants.Ftdrin);
            Assert.AreEqual(_finalPlannedOrderList[0].FTDRQJ, ConvertDate.DateToJdeDate(_cyclePlannedBefore));
            Assert.AreEqual(_finalPlannedOrderList[0].FTEDBT, Ftedbt);
            Assert.AreEqual(_finalPlannedOrderList[0].FTEDDT, Fteddt);
            Assert.AreEqual(_finalPlannedOrderList[0].FTEDLN, Count);
            Assert.AreEqual(_finalPlannedOrderList[0].FTEDUS, Service.Business.Constants.Ftedus);
            Assert.AreEqual(_finalPlannedOrderList[0].FTFQT, Qyy*10000);
            Assert.AreEqual(_finalPlannedOrderList[0].FTITM, Ftitm);
            Assert.AreEqual(_finalPlannedOrderList[0].FTLITM, Ftlitm);
            Assert.AreEqual(_finalPlannedOrderList[0].FTMCU, Ftmcu.PadLeft(12));
            Assert.AreEqual(_finalPlannedOrderList[0].FTTNAC, Service.Business.Constants.Fttnac);
            Assert.AreEqual(_finalPlannedOrderList[0].FTTYPF, _cyclePlannedBefore >= DateTime.Today.Date ? Service.Business.Constants.ForwardDemand : Service.Business.Constants.OverdueDemand);
            Assert.AreEqual(_finalPlannedOrderList[0].FTTYTN, Service.Business.Constants.Fttytn);
            Assert.AreEqual(_finalPlannedOrderList[0].FTUORG, Qyy*10000);
        }

        private void PlannedOrderDtoDataMustBeCorrect()
        {
            Assert.AreEqual(_plannedOrderDto.Ftedbt, Unique15BitString);
            Assert.AreEqual(_plannedOrderDto.Fteddt, ConvertDate.DateToJdeDate(DateTime.Today.Date));
            Assert.AreEqual(_plannedOrderDto.Ftedtn, $"{Service.Business.Constants.OrrSysPrefix1}{Unique15BitString}");
        }

        private void ProductionOrderDtoDataMustBeCorrect()
        {
            Assert.AreEqual(_productionOrderDto.Syedbt, Unique15BitString);
            Assert.AreEqual(_productionOrderDto.Syedln, $"{Service.Business.Constants.OrrSysPrefix1}{Unique15BitString}");
        }
        #endregion
    }
}