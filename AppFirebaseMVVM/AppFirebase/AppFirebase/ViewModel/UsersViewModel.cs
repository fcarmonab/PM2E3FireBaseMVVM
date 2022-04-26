using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AppFirebase.Model;
using AppFirebase.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace AppFirebase.ViewModel
{
    public class UsersViewModel :BaseViewModel
    {
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public DateTime Fecha { get; set; }
        public byte[] Photo_Recibo { get; set; }        

        private DBFirebase services;
        public Command AddUsersCommand { get; }
        private ObservableCollection<Pagos> _pagos = new ObservableCollection<Pagos>();

        public ObservableCollection<Pagos> UsersList
        {
            get { return _pagos; }
            set
            {
                _pagos = value;
                OnPropertyChanged();
            }
        }

        public UsersViewModel()
        {
            services = new DBFirebase();
            UsersList = services.GetUsersList();
            //Photo_Recibo = null;
            AddUsersCommand = new Command(async () => await AddUsersAsync(Descripcion, Monto, Fecha, Photo_Recibo));
        }

        public async Task AddUsersAsync(string descripcion, double monto, DateTime fecha, byte[] photo_recibo)
        {
            await services.AddUser(descripcion, monto, fecha, photo_recibo);
            
        }
    }
}
