using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webtruyen.Models;

using Version = Lucene.Net.Util.Version;
namespace webtruyen.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        DB myDb = new DB();
        StandardAnalyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);

        public ActionResult searchPartial()
        {
            return PartialView(myDb.TRUYENs);
        }
        public ActionResult Index() {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string searchInput = collection["searchString"].ToString();
            try
            {
                if (searchInput != null)
                {
                    List<TRUYEN> listTr = Search(searchInput);
                    List<CHUONGTRUYEN> result = SearchChuong(searchInput);

                    if (result.Count != 0)
                    {
                        foreach (var item in result)
                        {
                            int i = int.Parse(item.MATRUYEN.ToString());
                            var checkExist = listTr.SingleOrDefault(x => x.MATRUYEN.Equals(i));
                            if (checkExist == null)
                            {
                                listTr.Add(myDb.TRUYENs.FirstOrDefault(p => p.MATRUYEN == i));
                            }
                        }
                    }
                    return View(listTr);
                }
                else return ViewBag("Không Được Bỏ Trống");

              //  return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        public static string RemoveUnicode(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(stFormD[ich]);
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        Directory indexLucene()
        {
            List<TRUYEN> listTruyen = myDb.TRUYENs.ToList();

            var dir = new RAMDirectory();

            using (var writer = new IndexWriter(dir, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var item in listTruyen)
                {
                    var document = new Document();

                    document.Add(new Field("MATRUYEN", item.MATRUYEN.ToString(), Field.Store.YES, Field.Index.ANALYZED));

                    document.Add(new Field("TENTRUYEN", item.TENTRUYEN.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("TENTRUYEN", RemoveUnicode(item.TENTRUYEN.ToString()), Field.Store.YES, Field.Index.ANALYZED));

                    document.Add(new Field("MOTA", item.MOTA.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("MOTA", RemoveUnicode(item.MOTA.ToString()), Field.Store.YES, Field.Index.ANALYZED));

                    document.Add(new Field("HINHANH", item.HINHANH.ToString(), Field.Store.YES, Field.Index.ANALYZED));

                    writer.AddDocument(document);
                }


                writer.Optimize();
                writer.Flush(true, true, true);
            }
            return dir;
        }


        Directory indexLuceneChuong()
        {
            List<CHUONGTRUYEN> listChuongTruyen = myDb.CHUONGTRUYENs.ToList();

            var dir = new RAMDirectory();

            using (var writer = new IndexWriter(dir, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var item in listChuongTruyen)
                {
                    var document = new Document();

                    document.Add(new Field("TENCHUONG", item.TENCHUONG.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("TENCHUONG", RemoveUnicode(item.TENCHUONG.ToString()), Field.Store.YES, Field.Index.ANALYZED));

                    document.Add(new Field("TENPHU", item.TENPHU.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("TENPHU", RemoveUnicode(item.TENPHU.ToString()), Field.Store.YES, Field.Index.ANALYZED));

                    document.Add(new Field("NOIDUNG", item.NOIDUNG.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("NOIDUNG", RemoveUnicode(item.NOIDUNG.ToString()), Field.Store.YES, Field.Index.ANALYZED));

                    document.Add(new Field("MATRUYEN", item.MATRUYEN.ToString(), Field.Store.YES, Field.Index.ANALYZED));

                    writer.AddDocument(document);
                }


                writer.Optimize();
                writer.Flush(true, true, true);
            }
            return dir;
        }


        public List<TRUYEN> Search(string keyword)
        {
            List<TRUYEN> result = new List<TRUYEN>();

            var index = indexLucene();

            using (var reader = IndexReader.Open(index, true))
            using (var searcher = new IndexSearcher(reader))
            {
                var queryParser = new MultiFieldQueryParser(Version.LUCENE_30, "TENTRUYEN|MOTA".Split('|'), analyzer);
                queryParser.AllowLeadingWildcard = true;

                Query query = queryParser.Parse(keyword);

                TopDocs hits = searcher.Search(query, 10);

                foreach (ScoreDoc scoreDoc in hits.ScoreDocs.OrderByDescending(s => s.Score))
                {
                    Document document = searcher.Doc(scoreDoc.Doc);
                    TRUYEN b = new TRUYEN();

                    b.MATRUYEN = int.Parse(document.Get("MATRUYEN"));
                    b.TENTRUYEN = document.Get("TENTRUYEN");
                    b.HINHANH = document.Get("HINHANH");

                    result.Add(b);
                }
            }
            return result;
        }


        public List<CHUONGTRUYEN> SearchChuong(string keyword)
        {
            List<CHUONGTRUYEN> result = new List<CHUONGTRUYEN>();

            var index = indexLuceneChuong();

            using (var reader = IndexReader.Open(index, true))
            using (var searcher = new IndexSearcher(reader))
            {
                var queryParser = new MultiFieldQueryParser(Version.LUCENE_30, "TENCHUONG|TENPHU|NOIDUNG".Split('|'), analyzer);
                queryParser.AllowLeadingWildcard = true;

                Query query = queryParser.Parse(keyword);

                TopDocs hits = searcher.Search(query, 10);

                foreach (ScoreDoc scoreDoc in hits.ScoreDocs.OrderByDescending(s => s.Score))
                {
                    Document document = searcher.Doc(scoreDoc.Doc);
                    CHUONGTRUYEN b = new CHUONGTRUYEN();

                    b.MATRUYEN = int.Parse(document.Get("MATRUYEN"));

                    result.Add(b);
                }
            }
            return result;
        }
    }
}