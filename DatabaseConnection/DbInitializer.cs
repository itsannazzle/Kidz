using Kidz.Models;

namespace Kidz.DatabaseConnection
{
    public static class DbInitializer
    {
        public static void Initialize(DatabaseContex contex)
        {
            contex.Database.EnsureCreated();

            if(contex.entityUser.Any())
            {
                return;
            }

            if(contex.entityHitHistory.Any())
            {
                return;
            }

            if(contex.entityResultHistory.Any())
            {
                return;
            }

            if(contex.entityRequestHistory.Any())
            {
                return;
            }

            if(contex.entityResponseHistory.Any())
            {
                return;
            }
            
            contex.SaveChanges();

            //  var modelUser = new UserModel[]
            // {
            // new UserModel{Username="Carson",Password="Alexander",DeviceId="12345Device"}
            // };
            // foreach (UserModel mu in modelUser)
            // {
            //     contex.entityUser.Add(mu);
            // }
            // contex.SaveChanges();

        }
    }
}