using System.Threading.Tasks;
using SmartHouseLights.Services.Interfaces;
using Xamarin.Forms;

namespace SmartHouseLights.Services
{
    public class AlertService : IAlertService
    {
        public async Task<bool> PopupOnDeleteGroup()
        {
            bool action = await Shell.Current.DisplayAlert("Delete group",
                "Are you sure you want to delete this group?",
                "Yes", "No");

            return action;
        }

        public async Task<bool> PopupOnDeleteLight()
        {
            bool action = await Shell.Current.DisplayAlert("Delete light", 
                "Are you sure you want to delete this light?", 
                "Yes", "No");
            return action;
        }
    }
}