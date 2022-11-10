using System.Threading.Tasks;
using Devsharp.Services.DTOs;

namespace Devsharp.Services.Media
{
    public interface IPictureService
    {
        Task<bool> CheckExists(int ID);
        Task<PictureDTO> RegisterBase64PictureAsync(PictureUploadBase64DTO pictureUploadDTO);
        Task<PictureDTO> RegisterPictureAsync(PictureUploadDTO image);
        Task<PictureDTO> SearchPictureByIdAsync(int id);
    }
}