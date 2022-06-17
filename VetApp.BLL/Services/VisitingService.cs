using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Core;
using VetApp.Core.Models;
using VetApp.Core.Services;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
using PdfSharp.Charting;
using PdfSharp.Internal;
using System.Diagnostics;
using System;
using System.IO;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace VetApp.BLL.Services
{
    public class VisitingService : IVisitingService
    {
        private readonly IUnitOfWork unitOfWork;
        public VisitingService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Visiting> CreateVisiting(Visiting newVisiting, string iden)
        {
            newVisiting.Animal = await unitOfWork.Animals.GetAnimalByIdAsync(newVisiting.AnimalId, iden);
            if (newVisiting.Animal != null)
            {
                if (unitOfWork.Visitings.CheckVet(newVisiting.Animal.OwnerId, iden))
                {
                    await unitOfWork.Visitings.AddAsync(newVisiting);
                    await unitOfWork.CommitAsync();
                    return newVisiting;
                }
            }
            return new Visiting();
        }

        public async Task DeleteVisiting(Visiting visiting)
        {
            unitOfWork.Visitings.Remove(visiting);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Visiting>> GetAll(string iden)
        {
            return await unitOfWork.Visitings
                .GetAllAsync(iden);
        }

        public async Task<Visiting> GetVisitingById(int id, string iden)
        {
            return await unitOfWork.Visitings
                .GetVisitingByIdAsync(id, iden);
        }

        public async Task<IEnumerable<Visiting>> GetVisitingsByAnimalId(int animalId, string iden)
        {
            return await unitOfWork.Visitings
                .GetAllByAnimalIdAsync(animalId, iden);
        }

        public async Task<IEnumerable<Visiting>> GetVisitingsByDoctorId(int doctorId, string iden)
        {
            return await unitOfWork.Visitings
                .GetAllByDoctorIdAsync(doctorId, iden);
        }

        public void print(string q, Visiting visiting, string iden)
        {
            var doctor = unitOfWork.Doctors.GetByIdAsync(visiting.DoctorId, iden);
            var animal = unitOfWork.Animals.GetByIdAsync(visiting.AnimalId); 
            
            var document = new PdfDocument();
            var page = document.AddPage();
            XFont font = new XFont("Times New Roman", 14, XFontStyle.Regular);
            var graphics = XGraphics.FromPdfPage(page);
            var tf = new XTextFormatter(graphics);

            DrawHeader(graphics, iden, doctor.Result.Phone); // 80
            DrawFooter(graphics); // 60

            string text = "Vet Conclusion № " + visiting.Id + "\n\nDoctor: " + doctor.Result.Specialty + " " + doctor.Result.Surname + " " + doctor.Result.Name +
                "\nAnimal: " + visiting.AnimalId + " " + animal.Result.Name +
                "\n\nDate: " + visiting.Date + "\nTime: " + visiting.Time + "\n\nDiagnosis: " + visiting.Diagnosis +
                "\n\nAnalyzes: " + visiting.Analyzes + "\nExamination: " + visiting.Examination +
                "\nMedicines: " + visiting.Medicines+
                "\n\n\n\n" + doctor.Result.Specialty + " " + doctor.Result.Surname + " " + doctor.Result.Name + "       _____________________";



            XRect rect = new XRect(40, 100, 520, 400);
            graphics.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Justify;
            tf.DrawString(text, font, XBrushes.Black, rect, XStringFormats.TopLeft);

            document.Save(q);

        }

        public async Task UpdateVisiting(int id, Visiting visiting)
        {
            visiting.Id = id;
            unitOfWork.Visitings.Entry(visiting);
            await unitOfWork.CommitAsync();
        }


        void DrawHeader(XGraphics graphics, string iden, string phone)
        {
            var font = new XFont("Times New Roman", 12, 0);

            graphics.DrawString("Veterinary Clinic:", font, XBrushes.Gray, 20, 20);
            graphics.DrawString(iden, font, XBrushes.Black, 150, 20);

            graphics.DrawString("Doctor's phone:", font, XBrushes.Gray, 20, 40);
            graphics.DrawString(phone, font, XBrushes.Black, 150, 40);

            graphics.DrawString("Date and Time", font, XBrushes.Gray, 20, 60);
            graphics.DrawString(DateTime.Now.ToString("F"), font, XBrushes.Black, 150, 60); ;

            var pen = new XPen(new XColor());
            graphics.DrawLine(pen, 0, 80, graphics.PageSize.Width, 80);
        }

        void DrawFooter(XGraphics graphics)
        {
            var pen = new XPen(new XColor());
            graphics.DrawLine(pen, 0, graphics.PageSize.Height - 60, graphics.PageSize.Width, graphics.PageSize.Height - 60);

            var font = new XFont("Times New Roman", 14);
            graphics.DrawString("Made with Vet App", font, XBrushes.Gray, 10, graphics.PageSize.Height - 30);
        }
    }
}
