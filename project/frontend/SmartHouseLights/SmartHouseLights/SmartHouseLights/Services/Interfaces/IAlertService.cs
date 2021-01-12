using System.Threading.Tasks;

namespace SmartHouseLights.Services.Interfaces
{
    public interface IAlertService
    {
        Task<bool> PopupOnDeleteGroup();

        Task<bool> PopupOnDeleteLight();
    }
}