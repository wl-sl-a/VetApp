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
    public class DirectionService : IDirectionService
    {
        private readonly IUnitOfWork unitOfWork;
        public DirectionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Direction> CreateDirection(Direction newDirection, string iden)
        {
            newDirection.Visiting = await unitOfWork.Visitings.GetVisitingByIdAsync(newDirection.VisitingId, iden);
            if (newDirection.Visiting != null)
            {
                newDirection.Visiting.Animal = await unitOfWork.Animals.GetAnimalByIdAsync(newDirection.Visiting.AnimalId, iden);
            }
            if (newDirection.Visiting.Animal != null)
            {
                if (unitOfWork.Directions.CheckVet(newDirection.Visiting.Animal.OwnerId, iden))
                {
                    await unitOfWork.Directions.AddAsync(newDirection);
                    await unitOfWork.CommitAsync();
                    return newDirection;
                }
            }
            return new Direction();
        }

        public async Task DeleteDirection(Direction direction)
        {
            unitOfWork.Directions.Remove(direction);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Direction>> GetAll(string iden)
        {
            return await unitOfWork.Directions
                .GetAllAsync(iden);
        }

        public async Task<Direction> GetDirectionById(int id, string iden)
        {
            return await unitOfWork.Directions
                .GetDirectionByIdAsync(id, iden);
        }

        public async Task<IEnumerable<Direction>> GetDirectionsByVisitingId(int visitingId, string iden)
        {
            return await unitOfWork.Directions
                .GetAllByVisitingIdAsync(visitingId, iden);
        }

        public async Task<IEnumerable<Direction>> GetDirectionsByAnimalId(int animalId, string iden)
        {
            return await unitOfWork.Directions
                .GetAllByAnimalIdAsync(animalId, iden);
        }

        public async Task UpdateDirection(int id, Direction direction)
        {
            direction.Id = id;
            unitOfWork.Directions.Entry(direction);
            await unitOfWork.CommitAsync();
        }

        public void print(string q, Direction direction, string iden)
        {
            var visiting = unitOfWork.Visitings.GetByIdAsync(direction.VisitingId).Result;
            var service = unitOfWork.Services.GetByIdAsync(direction.ServiceId).Result;


            var document = new PdfDocument();
            var page = document.AddPage();
            XFont font = new XFont("Times New Roman", 14, XFontStyle.Regular);
            var graphics = XGraphics.FromPdfPage(page);
            var tf = new XTextFormatter(graphics);
            var animal = unitOfWork.Animals.GetByIdAsync(visiting.AnimalId).Result;
            var doctor = unitOfWork.Doctors.GetByIdAsync(visiting.DoctorId).Result;

            DrawHeader(graphics, iden, doctor.Phone);
            DrawFooter(graphics);

            string text = "Direction Number " + direction.Id + "\nVet Conclusion № " + visiting.Id + 
                "\n\nDoctor: " + doctor.Specialty + " " + doctor.Surname + " " + doctor.Name +
                "\nAnimal: " + animal.Id + " " + animal.Name +
                "\n\nService: " + service.Id + " " + service.Name +
                "\n\n\n\n" + doctor.Specialty + " " + doctor.Surname + " " + doctor.Name + "       _____________________";



            XRect rect = new XRect(40, 100, 520, 400);
            graphics.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Justify;
            tf.DrawString(text, font, XBrushes.Black, rect, XStringFormats.TopLeft);

            document.Save(q);

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
