using Newtonsoft.Json;
using SellerProduct.Models;

namespace SellerProduct.Controllers
{
    public class SessionServices
    {
        //1. Đọc dữ liệu từ Session => Trả về 1 list
        public static List<Product> GetObjeFromSession(ISession session, string key)
        {
            string jsonData = session.GetString(key); // lấy dữ liệu dạng string từ Session
            if (jsonData == null)
            {
                return new List<Product>();// Khởi tạo 1 list mới để chứa sp khi dữ liệu
                // lấy ra null <=> Session chưa được tạo ra -> Lần đầu làm chuyện ấy
            }
            else
            {
                // nếu dữ liệu có thì ta có thể chuyển đổi nó về dạng List
                var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
                return products;
            }
        }
        //2: Ghi đè dữ liệu vào Session từ 1 list
        public static void SetObjToSession(ISession session, string key, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data); // chuyển đổi dữ liệu về jsonData
            session.SetString(key, jsonData);// ghi đè vào Session
        }
        // 3: Kiểm tra xem 1 đối tượng có nằm trong 1 list hay không
        public static bool CheckObjInList(Guid id, List<Product> products)
        {

            //return products.Any(x => x.Id == id);

            var tim = products.Find(x => x.Id == id);
            var xoa = products.Remove(tim);
          
            return xoa;

        }


        // Gio Hang
        //1. Đọc dữ liệu từ Session => Trả về 1 list
        public static List<CartDetails> GetObjeFromSessionOfCartDetail(ISession session, string key)
        {
            string jsonData = session.GetString(key); // lấy dữ liệu dạng string từ Session
            if (jsonData == null)
            {
                return new List<CartDetails>();// Khởi tạo 1 list mới để chứa sp khi dữ liệu
                // lấy ra null <=> Session chưa được tạo ra -> Lần đầu làm chuyện ấy
            }
            else
            {
                // nếu dữ liệu có thì ta có thể chuyển đổi nó về dạng List
                var cartdetails = JsonConvert.DeserializeObject<List<CartDetails>>(jsonData);
                return cartdetails;
            }
        }
        //2: Ghi đè dữ liệu vào Session từ 1 list
        public static void SetObjToSessionOfCartDetail(ISession session, string key, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data); // chuyển đổi dữ liệu về jsonData
            session.SetString(key, jsonData);// ghi đè vào Session
        }
        // 3: Kiểm tra xem 1 đối tượng có nằm trong 1 list hay không
        public static bool CheckObjInListOfCartDetail(Guid id, List<CartDetails> cartDetails)
        {
            return cartDetails.Any(x => x.Id == id);
        }

    }
}
