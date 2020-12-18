using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Cursova.Models;

namespace Cursova.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult GetTableMotherboards()
        {
            var FormData = Request.Form;
            int page = int.Parse(FormData.Get("page")) - 1;
            int size = int.Parse(FormData.Get("size"));
            var sortField = FormData.Get("sorters[0][field]");
            var sortDir = FormData.Get("sorters[0][dir]");
            var query = $"SELECT * FROM motherboards";
            var result = DatabaseReader.ExecuteQuery(query);
            var count = result.Rows.Count;
            var pageCount = Math.Ceiling((double)count / (double)size);

            query = $@"select mb.mb_id as Id, mb.name as Name, mb.series as Series, mbpr.name as MbPr, sck.name as Sck, chs.name as Chs, sch.name as Sch
                        from motherboards mb
                        inner join motherboards_producers mbpr on mbpr.mb_pr_id=mb.mb_pr_id
                        inner join sockets sck on sck.sck_id=mb.sck_id
                        inner join chipsets chs on chs.chs_id=mb.chs_id
                        inner join sound_chip sch on sch.sch_id=mb.sch_id";

            if (sortField != null && sortDir != null && sortField != "Action")
            {
                query += $" ORDER BY {sortField} {sortDir}";
            }

            if (page != 0)
            {
                query = $@"select top({size}) mb.mb_id as Id, mb.name as Name, mb.series as Series, mbpr.name as MbPr, sck.name as Sck, chs.name as Chs, sch.name as Sch
                        from motherboards mb
                        inner join motherboards_producers mbpr on mbpr.mb_pr_id=mb.mb_pr_id
                        inner join sockets sck on sck.sck_id=mb.sck_id
                        inner join chipsets chs on chs.chs_id=mb.chs_id
                        inner join sound_chip sch on sch.sch_id=mb.sch_id";

                if (sortField != null && sortDir != null && sortField != "Action")
                {
                    query += $" ORDER BY {sortField} {sortDir}";
                }
            }
            var data = DatabaseReader.ExecuteQuery(query).DataTableToList<MotherboardView>().Skip(page * size).Take(size);

            foreach (var item in data)
            {
                query = $@"select distinct r.name as Name
                            from motherboards mb
                            inner join mb_ram mbram on mbram.mb_id = { item.Id }
                            inner join ram r on r.ram_id = mbram.ram_id";

                var rams = DatabaseReader.ExecuteQuery(query);
                foreach (DataRow ram in rams.Rows)
                {
                    item.Ram.Add(ram[0].ToString() + "\n");
                }
            }


            return new JsonResult { Data = new { data = data, last_page = pageCount } };
        }

        public ActionResult GetMotherboardDetail(int id)
        {
            try
            {
                string query = $"select mb.con as Con, int_vid as IntVid, ram_max as RamMax, ram_amo as RamAmo, m2_amo as M2Amo, sata_amo as SataAmo, pcie_amo as PcieAmo, fmb_vid_out as FmbVidOut, form_f as FormF, price as Price, width as Width, height as Height, mbpr.logo as Logo from motherboards mb, motherboards_producers mbpr where mb_id={id}";
                var result = DatabaseReader.ExecuteQuery(query);
                List<string> res = new List<string>();

                for (int i = 0; i < result.Columns.Count; i++)
                {
                    res.Add(result.Rows[0][i].ToString());
                }

                return new JsonResult { Data = new { success = true, detail = res } };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, message = ex.Message } };
            }
        }

        public ActionResult DeleteMotherboard(int id)
        {
            try
            {
                string query = $@"delete from motherboards where mb_id={id}";
                DatabaseReader.ExecuteNonQuery(query);


                return new JsonResult { Data = new { success = true } };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, message = ex.Message } };
            }
        }

        [HttpGet]
        public ActionResult EditMotherboard(int id)
        {
            string query = $@"select * from motherboards where mb_id={id}";
            var result = DatabaseReader.ExecuteQuery(query).DataTableToList<Motherboard>().FirstOrDefault();

            query = "select * from motherboards_producers";
            var Producers = DatabaseReader.ExecuteQuery(query).DataTableToList<MotherboardsProducer>();
            ViewBag.Producers = Producers;

            query = "select * from sockets";
            var Sockets = DatabaseReader.ExecuteQuery(query).DataTableToList<Socket>();
            ViewBag.Sockets = Sockets;

            query = "select * from chipsets";
            var Chipsets = DatabaseReader.ExecuteQuery(query).DataTableToList<Chipset>();
            ViewBag.Chipsets = Chipsets;

            query = "select * from sound_ship";
            var SoundChips = DatabaseReader.ExecuteQuery(query).DataTableToList<SoundChip>();
            ViewBag.SoundChips = SoundChips;

            return View(result);
        }

        [HttpPost]
        public ActionResult EditMotherboard(Motherboard motherboard)
        {
            try
            {
                string query = $@"update motherboards set mb_id={motherboard.Id}, name=N'{motherboard.Name}', con=N'{motherboard.Con}', int_vid={motherboard.IntVid}' ram_max={motherboard.RamMax}, ram_amo={motherboard.RamAmo}, m2_amo={motherboard.M2Amo}, sata_amo={motherboard.SataAmo}, pcie_amo={motherboard.PcieAmo}, fmb_vid_out=N'{motherboard.FmbVidOut}', form_f=N'{motherboard.FormF}', price={motherboard.Price}, width={motherboard.Width}, height={motherboard.Height}, series=N'{motherboard.Series}', mb_pr_id={motherboard.MbPrId}, sch_id={motherboard.SchId}, chs_id={motherboard.ChsId}, sck_id={motherboard.SckId}
                                    where mb_id={motherboard.Id}";
                DatabaseReader.ExecuteNonQuery(query);

                return new JsonResult { Data = new { success = true } };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, message = ex.Message } };
            }
        }

        [HttpGet]
        public ActionResult AddMotherboard()
        {
            string query = "select * from motherboards_producers";
            var Producers = DatabaseReader.ExecuteQuery(query).DataTableToList<MotherboardsProducer>();
            ViewBag.Producers = new List<string>() { "ASUS", "MSI", "Gigabyte", "Intel" };

            query = "select * from sockets";
            var Sockets = DatabaseReader.ExecuteQuery(query).DataTableToList<Socket>();
            ViewBag.Sockets = new List<string>() { "Socket AM4", "LGA1151", "LGA2011" };

            query = "select * from chipsets";
            var Chipsets = DatabaseReader.ExecuteQuery(query).DataTableToList<Chipset>();
            ViewBag.Chipsets = new List<string>() { "Intel H310", "AMD B450", "Intel 430HX" };

            query = "select * from sound_chip";
            var SoundChips = DatabaseReader.ExecuteQuery(query).DataTableToList<SoundChip>();
            ViewBag.SoundChips = new List<string>() { "Realtek ALC887", "Realtek ALC655", "Atoms 3" };

            return View();
        }

        [HttpPost]
        public ActionResult AddMotherboard(Motherboard motherboard)
        {
            try
            {
                string baseq = "select mb_id from motherboards order by mb_id";
                var baser = DatabaseReader.ExecuteQuery(baseq).DataTableToList<Motherboard>();

                int id = baser.Last().Id++;

                string query = $@"insert into motherboards(mb_id, name, con, int_vid, ram_max, ram_amo, m2_amo, sata_amo, pcie_amo, fmb_vid_out, form_f, price, width, height, series, mb_pr_id, sch_id, chs_id, sck_id) values ({id}, N'{motherboard.Name}', N'{motherboard.Con}', {motherboard.IntVid}, {motherboard.RamMax}, {motherboard.RamAmo}, {motherboard.M2Amo}, {motherboard.SataAmo}, {motherboard.PcieAmo}, N'{motherboard.FmbVidOut}', N'{motherboard.FormF}', {motherboard.Price}, {motherboard.Width}, {motherboard.Height}, N'{motherboard.Series}', {motherboard.MbPrId}, {motherboard.SchId}, {motherboard.ChsId}, {motherboard.SckId});";
                DatabaseReader.ExecuteNonQuery(query);

                return new JsonResult { Data = new { success = true } };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, message = ex.Message } };
            }

        }
    }
}