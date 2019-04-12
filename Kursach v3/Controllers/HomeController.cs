using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;
using Kursach_v3.Models;

namespace Kursach_v3.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
        Text text = new Text();
            return View("Index",text);
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file,  Text text, int Shift=0)
        {
            if (file != null && file.ContentLength > 0 && (file.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || file.ContentType == "text/plain"))
            {
                try
                {
                    if (file.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    {
                        //загружаем docx конвертим его в txt и далее просто считываем текст с него
                        string path = Path.Combine(Server.MapPath("~/App_Data"),
                                                   Path.GetFileName("result.docx"));
                        file.SaveAs(path);


                        string docxFile = Path.Combine(Server.MapPath("~/App_Data"),
                                                   Path.GetFileName("result.docx"));
                        string textFile = Path.ChangeExtension(docxFile, ".txt");

                        Document document = new Document(docxFile);
                        document.SaveToFile(textFile, FileFormat.Txt);


                        //закидываем текст из txt в экземпляр класса text
                        var dataFile = Server.MapPath("~/App_Data/result.txt");
                        text.AllText = System.IO.File.ReadAllLines(dataFile);

                        text.Shift = Shift;
                        text.Decipher();
                        System.IO.File.WriteAllLines(dataFile, text.ReadableText);
                        ViewBag.Message1 = "Дешифровка произошла успешно!";

                        //создаем docx файл
                        Document doc = new Document();
                        Section section = doc.AddSection();
                        Paragraph Para = section.AddParagraph();
                        foreach (var item in text.ReadableText)
                        {
                            Para.AppendText(item);
                        }
                        doc.SaveToFile(Server.MapPath("~/App_Data/result.docx"), FileFormat.Docx);
                    }
                    else //реюз предыдущего кода для .txt
                    {
                        string path = Path.Combine(Server.MapPath("~/App_Data"),
                                                   Path.GetFileName("result.txt"));
                        file.SaveAs(path);
                        var dataFile = Server.MapPath("~/App_Data/result.txt");
                        text.AllText = System.IO.File.ReadAllLines(dataFile);

                        text.Shift = Shift;
                        text.Decipher();
                        System.IO.File.WriteAllLines(dataFile, text.ReadableText);
                        ViewBag.Message1 = "Дешифровка произошла успешно!";

                        //создаем docx файл
                        Document doc = new Document();
                        Section section = doc.AddSection();
                        Paragraph Para = section.AddParagraph();
                        foreach (var item in text.ReadableText)
                        {
                            Para.AppendText(item);
                        }
                        doc.SaveToFile(Server.MapPath("~/App_Data/result.docx"), FileFormat.Docx);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ИСКЛЮЧЕНИЕ:" + ex.Message.ToString();
                }
            }
            else if(text.InputText!=null)
            {
                text.Shift = Shift;
                text.InputToAll();
                text.Decipher();

                var dataFile = Server.MapPath("~/App_Data/result.txt");
                System.IO.File.WriteAllLines(dataFile, text.ReadableText);
                ViewBag.Message1 = "Дешифровка произошла успешно!";

                //создаем docx файл
                Document doc = new Document();
                Section section = doc.AddSection();
                Paragraph Para = section.AddParagraph();
                foreach (var item in text.ReadableText)
                {
                    Para.AppendText(item);
                }
                doc.SaveToFile(Server.MapPath("~/App_Data/result.docx"), FileFormat.Docx);
                doc.SaveToFile(Server.MapPath("~/App_Data/result.txt"), FileFormat.Txt);
            }
            else
            {
                ViewBag.Message = "Вы что-то сделали не так! У меня все работало как надо! Попробуйте загрузить файл .txt или .docx формата с текстом!";
            }
            return View("Index", text);
        }
        public ActionResult Download()
        {
            string path = Server.MapPath("~/App_Data/result.txt");
            string contentType = "text/plain";

            return File(path, contentType, "result.txt");
        }
        public ActionResult DownloadDocx()
        {
            string path = Server.MapPath("~/App_Data/result.docx");
            string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            return File(path, contentType, "result.docx");

        }
        public ActionResult Cat()
        {
            return View();
        }
    }

}