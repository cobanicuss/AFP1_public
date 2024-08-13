using System.Collections.Generic;
using System.Linq;
using Spm.OrrSys.Service.Domain;
using Spm.Shared;

namespace Spm.OrrSys.Service.Repositories
{
    public class FormatPlannedOrderData : IFormatPlannedOrderData
    {
        public IEnumerable<OrrSysF3460Z1> CastObjectToOrrSysF3460Z1(string ftedbt, string ftedtn, decimal fteddt, IEnumerable<object[]> resultList)
        {
            return resultList.Select(item => new OrrSysF3460Z1
            {
                FTEDUS = item[0].ToString(),
                FTEDBT = ftedbt,
                FTEDTN = ftedtn,
                FTEDLN = 0,
                FTTYTN = item[1].ToString(),
                FTEDDT = fteddt,
                FTDRIN = item[2].ToString(),
                FTEDSP = item[3].ToString(),
                FTTNAC = item[4].ToString(),
                FTITM = (double)item[5],
                FTLITM = item[6].ToString(),
                FTAITM = item[7].ToString(),
                FTMCU = item[8].ToString(),
                FTDRQJ = ConvertDate.ToTodayIfIsSmallerThanToday((decimal)item[9]),
                FTUORG = (double)item[10],
                FTFQT = (double)item[11],
                FTTYPF = item[12].ToString(),
                FTDCTO = item[13].ToString()
            }).ToList();
        }

        public IList<OrrSysF3460Z1> ExecuteGroupByQuery(IEnumerable<OrrSysF3460Z1> f3460Z1List)
        {
            var groupedBy = (from a in f3460Z1List
                             group a by new
                             {
                                 a.FTLITM,
                                 a.FTDRQJ,
                                 a.FTMCU
                             }
                                 into b
                             select new OrrSysF3460Z1
                             {
                                 FTEDUS = b.First().FTEDUS,
                                 FTEDBT = b.First().FTEDBT,
                                 FTEDTN = b.First().FTEDTN,
                                 FTEDLN = b.First().FTEDLN,
                                 FTTYTN = b.First().FTTYTN,
                                 FTEDDT = b.First().FTEDDT,
                                 FTDRIN = b.First().FTDRIN,
                                 FTEDSP = b.First().FTEDSP,
                                 FTTNAC = b.First().FTTNAC,
                                 FTITM = b.First().FTITM,
                                 FTLITM = b.First().FTLITM,
                                 FTAITM = b.First().FTAITM,
                                 FTMCU = b.First().FTMCU,
                                 FTDRQJ = b.First().FTDRQJ,
                                 FTUORG = b.Sum(x => x.FTUORG),
                                 FTFQT = b.Sum(x => x.FTFQT),
                                 FTTYPF = b.First().FTTYPF,
                                 FTDCTO = b.First().FTDCTO
                             }).ToList();
            return groupedBy;
        }

        public IList<OrrSysF3460Z1> InsertLineNumbers(IList<OrrSysF3460Z1> groupedF340Z1)
        {
            var lineNumberedF3460Z1 = groupedF340Z1;

            uint lineNr = 1000;

            foreach (var item in lineNumberedF3460Z1)
            {
                item.FTEDLN = (int)lineNr;

                lineNr = lineNr + 1000;
            }

            return lineNumberedF3460Z1;
        }
    }
}