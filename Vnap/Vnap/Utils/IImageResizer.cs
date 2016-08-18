using System.Threading.Tasks;

namespace Vnap.Utils
{
    public interface IImageResizer
    {
        Task<byte[]> CropResize(byte[] imageData, float width, float height);
        Task<byte[]> FitResize(byte[] imageData, float maxWidth, float maxHeight);
    }
}
