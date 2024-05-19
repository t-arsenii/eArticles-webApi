using System.Drawing;
using System.Drawing.Drawing2D;

public class ResizeImageService
{
    public Bitmap ResizeImage(Image image, int width, int height)
    {
        var destImage = new Bitmap(width, height);

        using (var graphics = Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            float scaleX = (float)width / image.Width;
            float scaleY = (float)height / image.Height;

            float scale = Math.Max(scaleX, scaleY);

            int newWidth = (int)(image.Width * scale);
            int newHeight = (int)(image.Height * scale);

            int posX = (width - newWidth) / 2;
            int posY = (height - newHeight) / 2;

            graphics.DrawImage(image, posX, posY, newWidth, newHeight);
        }

        return destImage;
    }
}
