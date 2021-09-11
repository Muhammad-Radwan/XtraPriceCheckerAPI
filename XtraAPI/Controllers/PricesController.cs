using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XtraDA;
namespace XtraAPI.Controllers
{
    //tbl007.ProductName, tbl007.Barcode, tbl007.EndUserPrice, tbl007.Unit,
    public class PricesController : ApiController
    {
        public IHttpActionResult GetProductInfo(string Barcode)
        {
            using (XtrDB01_04_2021Entities DBContext = new XtrDB01_04_2021Entities())
            {
                var result = (from p in DBContext.TBL007
                              join cu in DBContext.TBL128 on p.CardGuide equals cu.MainGuide into ntb
                              from sub in ntb.DefaultIfEmpty()
                              where p.Barcode.Contains(Barcode) || sub.Barcode.Contains(Barcode)
                              select new
                              {
                                  p.ProductName,
                                  p.Barcode,
                                  p.EndUserPrice,
                                  p.Unit
                              });
                return Ok(result.ToList());
            }
        }

        // POST api/values
        public void Post([FromBody] TBL007 _tbl007)
        {
            using (XtrDB01_04_2021Entities DBContext = new XtrDB01_04_2021Entities())
            {
                DBContext.TBL007.Add(_tbl007);
                //var result = (from p in DBContext.TBL007
                //              join cu in DBContext.TBL128 on p.CardGuide equals cu.MainGuide into ntb
                //              from sub in ntb.DefaultIfEmpty()
                //              where p.Barcode.Contains(Barcode) || sub.Barcode.Contains(Barcode)
                //              select new
                //              {
                //                  p.ProductName,
                //                  p.Barcode,
                //                  p.EndUserPrice,
                //                  p.Unit
                //              });
                //return Ok(result.ToList());
            }
        }
    }
}
