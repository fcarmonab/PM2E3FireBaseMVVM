using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFirebase.Model;
using Firebase.Database;
using Firebase.Database.Query;

namespace AppFirebase.Services
{
    public class DBFirebase
    {
        public FirebaseClient client;

        public DBFirebase()
        {
            client = new FirebaseClient(
                "https://pm02e3grupo2-default-rtdb.firebaseio.com/");
        }

        public ObservableCollection<Pagos> GetUsersList()
        {
            var userdata = client
                .Child("Pagos")
                .AsObservable<Pagos>()
                .AsObservableCollection();
            return userdata;
        }

        public async Task AddUser(string descripcion, double monto, DateTime fecha, byte[] photo_recibo)
        {
            Pagos u = new Pagos() {Descripcion = descripcion, Monto = monto, Fecha = fecha, Photo_Recibo = photo_recibo};
            await client
                .Child("Pagos")
                .PostAsync(u);
        }

        public async Task UpdateUser(string descripcion, double monto, DateTime fecha, byte[] photo_recibo)
        {
            var toUpdateUser = (await client
                .Child("Pagos")
                .OnceAsync<Pagos>()).FirstOrDefault
                (a => a.Object.Descripcion == descripcion && a.Object.Monto == monto);

            Pagos u = new Pagos() {Descripcion = descripcion, Monto = monto, Fecha = fecha, Photo_Recibo = photo_recibo};
            await client
                .Child("Pagos")
                .Child(toUpdateUser.Key)
                .PutAsync(u);
        }

        public async Task DeleteUser(string descripcion, double monto)
        {
            var toDeleteUser = (await client
                .Child("Pagos")
                .OnceAsync<Pagos>()).FirstOrDefault
                (a => a.Object.Descripcion == descripcion && a.Object.Monto == monto);
            await client.Child("Pagos").Child(toDeleteUser.Key).DeleteAsync();
        }
    }
}
