using ETicaret.DataAccessLayer.CodeFirst;
using System.Linq;

namespace UI.Layer.Models.UserModel
{
    public  class GetImagewithProductid
    {
         static DataContext db = new DataContext();
        public static bool GetImage(int productid)
        {
            try
            {
                var ımage = db.Images.Where(x => x.ProductID == productid).ToList();
                if (ımage == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
