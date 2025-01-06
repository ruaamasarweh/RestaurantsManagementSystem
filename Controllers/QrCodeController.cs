using Gp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing;
using System.Runtime.Versioning;

namespace Gp.Controllers
{
    [SupportedOSPlatform("windows")]
    public class QrCodeController : Controller
    {
        private readonly SystemDbContext _context;

        public QrCodeController(SystemDbContext context)
        {
            _context = context;
        }

        public IActionResult GenerateQrCode(int branchID)
        {
            var branch = _context.Branch
                .Where(b => b.BranchID == branchID)
                .Select(b => new
                {
                    b.BranchID,
                    b.LocationDescription,
                    RestaurantName = b.Restaurant!.RestaurantName!
                })
                .FirstOrDefault();

            if (branch == null)
            {
                return NotFound();
            }

            var fileName = $"qrcode_{branch.BranchID}.png";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/qrcodes", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                string qrContent = branchID.ToString();  


                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        using (Bitmap qrCodeImage = qrCode.GetGraphic(20))
                        {
                            qrCodeImage.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }
            }

            ViewBag.QrCodeImagePath = $"/qrcodes/{fileName}";
            ViewBag.RestaurantName = branch.RestaurantName;
            ViewBag.LocationDescription = branch.LocationDescription;
            ViewBag.BranchID=branch.BranchID;
            return View();
        }
    }
}

