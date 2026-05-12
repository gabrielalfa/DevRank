using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DevRank.Services
{
    public static class AvatarStorageService
    {
        private const int MaxBytes = 2 * 1024 * 1024;
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

        public static string Save(HttpPostedFileBase file, string username, HttpServerUtilityBase server)
        {
            if (file == null || file.ContentLength == 0)
            {
                return null;
            }

            if (file.ContentLength > MaxBytes)
            {
                throw new InvalidOperationException("A foto deve ter no máximo 2MB.");
            }

            var extension = Path.GetExtension(file.FileName ?? string.Empty).ToLowerInvariant();

            if (!AllowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Use uma imagem JPG, PNG, WEBP ou GIF.");
            }

            var safeUsername = Regex.Replace(username ?? "devrank", @"[^a-zA-Z0-9\-]", "-").Trim('-').ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(safeUsername))
            {
                safeUsername = "devrank";
            }

            var relativeFolder = "~/Content/Uploads/Avatars";
            var absoluteFolder = server.MapPath(relativeFolder);
            Directory.CreateDirectory(absoluteFolder);

            var fileName = safeUsername + "-" + Guid.NewGuid().ToString("N") + extension;
            var absolutePath = Path.Combine(absoluteFolder, fileName);
            file.SaveAs(absolutePath);

            return relativeFolder + "/" + fileName;
        }
    }
}
