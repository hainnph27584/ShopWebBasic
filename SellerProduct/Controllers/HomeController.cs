using Microsoft.AspNetCore.Mvc;
using SellerProduct.IServices;
using SellerProduct.Models;
using SellerProduct.Services;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SellerProduct.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServices productServices; // Interface
        private readonly IUserServices _userServices; // Interface
        private readonly ICartServices _cartServices; // Interface
        private readonly ICartDetailsServices _cartDetailServices; // Interface
        private readonly IBillServices _billServices; // Interface
        private readonly IBillDetailsServices _billDetailServices; // Interface
        ShopDbContext _ShopDbContext;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            productServices = new ProductServices();
            _ShopDbContext = new ShopDbContext();
            _userServices = new UserServices();
            _cartDetailServices = new CartDetailsServices();
            _billServices = new BillServices();
            _billDetailServices = new BillDetailsServices();
            _cartServices = new CartServices();
        }

        public IActionResult Index()
        {
            var sessionData = HttpContext.Session.GetString("tk");
            ViewData["User"] = sessionData;
            List<Product> products = productServices.GetAllProducts();
            return View(products); // Hành động trả về - hiển thị view cùng tên với action
        }

        [HttpGet]
        public IActionResult DangKy()
        {

            return View();
        }
        [HttpPost]

        public IActionResult DangKy(User user)
        {

            _userServices.CreateUser(user);
            return View("DangNhap");
        }


        [HttpGet]
        public IActionResult DangNhap()
        {

            return View();
        }
        [HttpPost]
        public IActionResult DangNhap(string userName, string password)
        {

            if (ModelState.IsValid)
            {
                //var f_password = GetMD5(password);
                var data = _ShopDbContext.User.Where(s => s.UserName.Equals(userName) && s.Password.Equals(password)).ToList();

                if (data.Count() > 0)
                {
                    ////add session
                    //string IdUser = Convert.ToString(data.FirstOrDefault().Id);

                    ViewData["User"] = data.FirstOrDefault().UserName;
                    //ViewData["idUser"] = data.FirstOrDefault().Id;

                    HttpContext.Session.SetString("tk", userName);

                    ViewBag.IdUser = data.FirstOrDefault().Id;
                    Guid iduser = ViewBag.IdUser;


                    HttpContext.Session.SetString("UserId", iduser.ToString());

                    return RedirectToAction("Index");
                }
                else
                {
                    if (password.Length <= 5 || userName.Length <= 5)
                    {
                        ViewBag.Mess = "Phải lớn hơn 6 kí tự";
                    }
                    else
                    {
                        if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9]+$") && !Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
                        {
                            ViewBag.MessKiTUDB = "Tài Khoản Hoặc mật khẩu không được chứa kí tự đặc biệt";
                        }
                    }
                    //ViewBag.error = "Login failed";
                    //return RedirectToAction("DangNhap");
                }
            }
            return View();

        }

        //question
        [HttpGet]
        public IActionResult DangNhap419()
        {

            return View();
        }
        [HttpPost]
        public IActionResult DangNhap419(string userName, string password)
        {

            if (ModelState.IsValid)
            {
                //var f_password = GetMD5(password);
                var data = _ShopDbContext.User.Where(s => s.UserName.Equals(userName) && s.Password.Equals(password)).ToList();

                if (data.Count() > 0)
                {
                    ////add session
                    //string IdUser = Convert.ToString(data.FirstOrDefault().Id);

                    ViewData["User"] = data.FirstOrDefault().UserName;
                    //ViewData["idUser"] = data.FirstOrDefault().Id;

                    HttpContext.Session.SetString("tk", userName);

                    ViewBag.IdUser = data.FirstOrDefault().Id;
                    Guid iduser = ViewBag.IdUser;


                    HttpContext.Session.SetString("UserId", iduser.ToString());

                    return RedirectToAction("Index");
                }
                else
                {
                    if (password.Length <= 5 || userName.Length <= 5)
                    {
                        ViewBag.Mess = "Phải lớn hơn 6 kí tự";
                    }

                    else if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9]+$") || !Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
                    {
                        ViewBag.MessKiTUDB = "Tài Khoản or mật khẩu không được chứa kí tự đặc biệt";
                    }
                    else
                    {
                        ViewBag.Mess = "TK Hoặc MK không chính xác";
                    }
                    //ViewBag.error = "Login failed";
                    //return RedirectToAction("DangNhap");
                }
            }
            return View();

        }

        //Logout
        public ActionResult Logout()
        {
            //ViewData["User"] = null;
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        public IActionResult CreateCart(Cart cart)
        {

            //_cartServices.CreateCart(cart);
            //_cartDetailServices.CreateCartDetails(cart);
            return View("Index");
        }

        public IActionResult GioHangChiTiet(Guid idUser)
        {

            var sessionData = HttpContext.Session.GetString("tk");
            ViewData["User"] = sessionData;
            //var sessionIdUser = HttpContext.Session.GetString("idTk");
            //ViewData["idUser"] = sessionIdUser;
            //idUser = Guid.Parse(sessionIdUser);
            string userId = HttpContext.Session.GetString("UserId");
            if (Guid.TryParse(userId, out Guid iduser))
            {
                ViewBag.UserId = userId;
            }
            idUser = Guid.Parse(userId); //id



            //var cartOfUser = _cartDetailServices.GetCartDetailsById(idUser);

            // Lấy dữ liệu từ session để truyền vào view
            //var cart = SessionServices.GetObjeFromSession(HttpContext.Session, "CartDetail");

            var listCartDetails = _cartDetailServices.GetAllCartDetails();
            ViewBag.listcartindb = _cartDetailServices.GetAllCartDetails();
            ViewBag.listCartDetails = listCartDetails.Where(c => c.UserId == idUser).ToList();
            ViewBag.listProduct = productServices.GetAllProducts();

            return View(); // truyền sang view
        }


        public IActionResult ThemVaoGioHang(Guid idProduct, Guid idUser)
        {
            var sessionIdUser = HttpContext.Session.GetString("idTk");
            ViewData["idUser"] = sessionIdUser;
            var sessionData = HttpContext.Session.GetString("tk");
            ViewData["User"] = sessionData;
            string userId = HttpContext.Session.GetString("UserId");
            if (Guid.TryParse(userId, out Guid iduser))
            {
                ViewBag.UserId = userId;
            }
            idUser = Guid.Parse(userId);
            List<Cart> carts = _cartServices.GetAllCarts();
            Cart objcart = new()
            {
                UserId = iduser,
                Description = "Giỏng Hàng Của" + ViewData["User"]
            };


            List<CartDetails> cartDetails = _cartDetailServices.GetAllCartDetails();

            CartDetails obj = new()
            {
                UserId = idUser,
                IdSp = idProduct,
                Quantity = 1
            };

            // Check sản phẩm đã có trong giỏ hàng hay chưa
            // Nếu có => Update +1 cho Amount
            // Nếu không => Create
            if (!carts.Any(c => c.UserId == idUser))
            {
                _cartServices.CreateCart(objcart);
            }
            if (cartDetails.Any(c => c.UserId == idUser && c.IdSp == idProduct))
            {
                // Update
                obj.Quantity = cartDetails.FirstOrDefault(c => c.UserId == idUser && c.IdSp == idProduct).Quantity + 1;
                var resultUpdate = _cartDetailServices.UpdateCartDetails(obj, obj.UserId, idProduct);

                if (resultUpdate)
                {
                    return RedirectToAction("GioHangChiTiet");
                }
            }
            else
            {

                //var result = _cartServices.CreateCart(cart);
                var result2 = _cartDetailServices.CreateCartDetails(obj);
                if (result2)
                {
                    return RedirectToAction("GioHangChiTiet");
                }
            }


            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeletePdInCart(Guid idUser, Guid idProduct)
        {
            //var result =  _cartDetailServices.DeleteCartDetails(idProduct, idUser);
            //return RedirectToAction("GioHangChiTiet", "Home");

            bool result = _cartDetailServices.DeleteCartDetails(idUser, idProduct);

            if (result)
            {
                TempData["Message"] = "Xóa sản phẩm khỏi giỏ hàng thành công!";
                return RedirectToAction("GioHangChiTiet");
            }
            else
            {
                TempData["Message"] = "Xóa sản phẩm khỏi giỏ hàng thất bại!";
                return RedirectToAction("GioHangChiTiet");
            }
        }




        [HttpPost]
        public ActionResult ThanhToan(/*Guid idUser, Guid idProduct*/)
        {

            var listCartDetails = _cartDetailServices.GetAllCartDetails();
            //var total = listCartDetails.Sum(c => c.Quantity * c.Product.Price);



            var sessionIdUser = HttpContext.Session.GetString("idTk");
            ViewData["idUser"] = sessionIdUser;



            string userId = HttpContext.Session.GetString("UserId");
            if (Guid.TryParse(userId, out Guid iduser))
            {
                ViewBag.UserId = userId;
            }
            Guid idUser = Guid.Parse(userId);

            var bill = new Bill
            {
                Id = Guid.NewGuid(),
                UserId = idUser,
                CreateDate = DateTime.Now,
                Status = 0
                //Total = total // Thêm tổng số tiền vào hóa đơn
            };

            _billServices.CreateBill(bill);

            foreach (var cartDetail in listCartDetails)
            {
                var product = _ShopDbContext.Products.FirstOrDefault(p => p.Id == cartDetail.IdSp);
                if (product != null && product.AvailableQuantity >= cartDetail.Quantity) // Kiểm tra số lượng sản phẩm trong kho đủ để bán hay không
                {
                    product.AvailableQuantity -= cartDetail.Quantity; // Trừ số lượng sản phẩm trong kho
                    _ShopDbContext.CartDetails.Remove(cartDetail); // Xóa sản phẩm trong giỏ hàng
                    //DeletePdInCart(idUser,idProduct); // Xóa sản phẩm trong giỏ hàng
                    _ShopDbContext.SaveChanges();

                    var billDetail = new BillDetails
                    {
                        ID = Guid.NewGuid(),
                        IdHD = bill.Id,
                        IdSP = cartDetail.IdSp,
                        Quantity = cartDetail.Quantity,
                        Price = cartDetail.Product.Price
                    };

                    _billDetailServices.CreateBillDetails(billDetail);
                }
                else
                {
                    ModelState.AddModelError("", "Sản phẩm trong giỏ hàng không đủ số lượng để bán");
                    return RedirectToAction("GioHangChiTiet", "Home");
                }
            }
            ModelState.AddModelError("", "Thanh toán thành công");
            return RedirectToAction("BIllOfUser", "Home");
        }

        public IActionResult BillOfUser()
        {
            var sessionIdUser = HttpContext.Session.GetString("idTk");
            ViewData["idUser"] = sessionIdUser;
            string userId = HttpContext.Session.GetString("UserId");
            if (Guid.TryParse(userId, out Guid iduser))
            {
                ViewBag.UserId = userId;
            }
            Guid idUser = Guid.Parse(userId);

            List<Bill> hd = _ShopDbContext.Bill.Where(x => x.UserId == idUser).ToList();
            ViewBag.ListBill = hd;


            return View();
        }
        public IActionResult BillDetailsOfUser(Guid idBill)
        {

            var sessionIdUser = HttpContext.Session.GetString("idTk");
            ViewData["idUser"] = sessionIdUser;
            string userId = HttpContext.Session.GetString("UserId");

            ViewBag.UserId = userId;

            Guid idUser = Guid.Parse(userId);

            List<Bill> hd = _ShopDbContext.Bill.Where(x => x.UserId == idUser).ToList();

            //var bill = hd.FirstOrDefault(b => b.Id == idBill);
            var bill = _billServices.GetBillById(idBill);
            //if (bill == null)
            //{
            //    return NotFound();
            //}

            var billDetails = _ShopDbContext.BillDetails.Where(bd => bd.IdHD == bill.Id).ToList();
            ViewBag.BillDetails = billDetails;

            var listProduct = productServices.GetAllProducts();
            ViewBag.Product = listProduct;
            return View();


        }




        public IActionResult Product()
        {
            return View();
        }
        public IActionResult Redirect()
        {
            return RedirectToAction("Search"); // chuyển hướng về action Search
        }
        // truyền dữ liệu từ action qua view
        public ActionResult Show()
        {
            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Nguyễn Nam Hải",
                Description = "Passed",
                Supplier = "Mama bank",
                Price = 672000,
                AvailableQuantity = 1,
                Status = 1
            };
            return View(product); // Truyền trực tiếp 1 Ojb Model duy nhất sang View

        }

        public ActionResult ShowAllProduct1()
        {
            List<Product> products = new List<Product>()
            {
                new Product()
                {
                     Id = Guid.NewGuid(),
                Name = "Nguyễn Nam Hải",
                Description = "Passed",
                Supplier = "Mama bank",
                Price = 672000,
                AvailableQuantity = 1,
                Status = 1
                },
                  new Product()
                {
                     Id = Guid.NewGuid(),
                Name = "Nguyễn  Hải",
                Description = "Passed",
                Supplier = "Mama bank",
                Price = 675000*4,
                AvailableQuantity = 1,
                Status = 0
                }
            };


            return View(products); // Truyền trực tiếp 1 Ojb Model duy nhất sang View

        }

        public ActionResult ShowAllProduct()
        {
            List<Product> products = productServices.GetAllProducts();
            return View(products);
        }
        public IActionResult Create() //Hiển thị form
        {
            var sessionData = HttpContext.Session.GetString("tk");
            ViewData["User"] = sessionData;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product, IFormFile imageFile) // thực hiện thêm
        {

            // trong trường hợp chúng ta thực hiện với thuộc tính Image
            // thuộc tính này đang là string => Không thể thao tác trực tiếp
            // với các file => Truyền thêm 1 tham số vào Action này
            // Truyền thêm 1 tham số imageFile kiểu IFormFile
            // Bước 1: Kiểm tra đường dẫn tới ảnh lấy được từ form
            if (imageFile != null && imageFile.Length > 0)// không được rỗng
            {
                // thực hiện trỏ tới thư mục root để lát thực hiện việc copy
                var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "images", "items", imageFile.FileName); // Bước 2
                //Kết quả : aaa/wwwroot/images/xxx.jpg
                var stream = new FileStream(path, FileMode.Create);
                // Vì chúng ta thực hiện việc copy => tạo mới => create
                imageFile.CopyTo(stream); //copy ảnh chọn ở form vào wwwroot/images/items
                // gán lại giá trị cho thuộc tính Image => bước 3
                product.Image = imageFile.FileName; //bước 4
            }

            if (productServices.CreateProduct(product))
            {
                return RedirectToAction("Search");
            }
            else return BadRequest();
        }



        //ViewData["oldProducts"] = products; nháp
        [HttpGet]
        public IActionResult Edit(Guid id) //  NGUYỄN NAM HẢI - PH 2 7 5 8 4
        {
            var sessionData = HttpContext.Session.GetString("tk");
            ViewData["User"] = sessionData;
            Product p = productServices.GetProductById(id);
            return View(p);
        }
        public IActionResult Edit(Product p, IFormFile imageFile) // Sửa
        {

            // thêm lịch sử chỉnh sửa vào sesion
            // lấy sản phẩm trong csdl
            var product = productServices.GetProductById(p.Id);


            // lấy list sản phẩm từ session
            var products = SessionServices.GetObjeFromSession(HttpContext.Session, "oldProduct");


            if (products.Count == 0)
            {
                products.Add(product);// thêm trực tiếp sản phẩm vào nếu List trống
                SessionServices.SetObjToSession(HttpContext.Session, "oldProduct", products);
            }
            else
            {
                if (SessionServices.CheckObjInList(p.Id, products))
                {// kiểm tra xem list lấy ra có chứa sp mình chọn hay k
                    Product pp = products.Find(x => x.Id == p.Id);
                    products.Remove(pp);
                    products.Add(product);
                    SessionServices.SetObjToSession(HttpContext.Session, "oldProduct", products);
                }
                else
                {
                    products.Add(product);// thêm trực tiếp sản phẩm vào nếu List chưa chứa sp đó
                    SessionServices.SetObjToSession(HttpContext.Session, "oldProduct", products);
                }
            }

            // NGUYỄN NAM HẢI - PH 2 7 5 8 4




            // trong trường hợp chúng ta thực hiện với thuộc tính Image
            // thuộc tính này đang là string => Không thể thao tác trực tiếp
            // với các file => Truyền thêm 1 tham số vào Action này
            // Truyền thêm 1 tham số imageFile kiểu IFormFile
            // Bước 1: Kiểm tra đường dẫn tới ảnh lấy được từ form
            if (imageFile != null && imageFile.Length > 0)// không được rỗng
            {
                // thực hiện trỏ tới thư mục root để lát thực hiện việc copy
                var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "images", "items", imageFile.FileName); // Bước 2
                //Kết quả : aaa/wwwroot/images/xxx.jpg
                var stream = new FileStream(path, FileMode.Create);
                // Vì chúng ta thực hiện việc copy => tạo mới => create
                imageFile.CopyTo(stream); //copy ảnh chọn ở form vào wwwroot/images/items
                // gán lại giá trị cho thuộc tính Image => bước 3
                p.Image = imageFile.FileName; //bước 4
            }
            List<Product> allproducts = productServices.GetAllProducts();
            if (allproducts.Any(x => x.Name == p.Name))
            {
                return View("Search");
            }
            else

                productServices.UpdateProduct(p);
            // NGUYỄN NAM HẢI - PH 2 7 5 8 4

            return RedirectToAction("Search"); return BadRequest();
        }

        public IActionResult Delete(Guid id)
        {
            if (productServices.DeleteProduct(id))
            {
                return RedirectToAction("Search");
            }
            else return View("Search");
        }

        public IActionResult Details(Guid id)
        {
            //ShopDbContext dbContext = new ShopDbContext();
            //  var products = dbContext.Products.Find(id);

            List<Product> allproducts = productServices.GetAllProducts();
            ViewBag.Product = allproducts;

            var products = productServices.GetProductById(id);
            return View(products);
        }

        public IActionResult ShowProduct()
        {
            return View();
        }

        public IActionResult Search(string name, int? status)
        {
            var sessionData = HttpContext.Session.GetString("tk");
            ViewData["User"] = sessionData;

            string userId = HttpContext.Session.GetString("UserId");
            if (Guid.TryParse(userId, out Guid iduser))
            {
                ViewBag.UserId = userId;
            }



            var p = productServices.GetProductByName(name);
            if (string.IsNullOrEmpty(name))
            {
                p = productServices.GetAllProducts();
            }
            return View(p);
        }

        public IActionResult ViewBag_ViewData()
        {
            var products = productServices.GetAllProducts();
            // ViewBag chứa dự liệu dạng dynamic, khi cần sử dụng
            // ta không cần khởi tạo mà gán thẳng giá trị vào
            ViewBag.Products = products;
            ViewBag.Messages = " ABCCCC";

            // ViewData chứ dữ liệu dạng Generic, dự liệu này được lưu dưới dạng key-value
            ViewData["Products"] = products;
            ViewData["Values"] = "Giá trị của ABBCCC";
            // Trong đó "Product" là key còn products là value
            return View();
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            // Lấy danh sách sản phẩm trong giỏ hàng ra từ session
            var products = SessionServices.GetObjeFromSession(HttpContext.Session, "Cart");
            // Tạo đối tượng Bill và lưu vào cơ sở dữ liệu

            var bill = new Bill
            {
                Id = Guid.NewGuid(),
                UserId = ViewBag.UserId,
                CreateDate = DateTime.Now,

            };
            _billServices.CreateBill(bill);
            // Duyệt qua từng sản phẩm trong giỏ hàng
            foreach (var product in products)
            {
                // Lấy sản phẩm từ DB bằng ID
                var dbProduct = productServices.GetProductById(product.Id);
                // Giảm số lượng sản phẩm trong DB đi số lượng sản phẩm trong giỏ hàng
                dbProduct.AvailableQuantity -= product.AvailableQuantity;
                // Cập nhật sản phẩm trong DB
                productServices.UpdateProduct(dbProduct);
                // Tạo đối tượng BillDetails và lưu vào cơ sở dữ liệu
                var billDetail = new BillDetails
                {
                    ID = Guid.NewGuid(),
                    IdHD = bill.Id,
                    IdSP = dbProduct.Id,
                    Quantity = product.AvailableQuantity,
                    Price = dbProduct.Price
                };
                _billDetailServices.CreateBillDetails(billDetail);
            }
            SessionServices.GetObjeFromSession(HttpContext.Session, "Cart");
            // Xóa danh sách sản phẩm trong giỏ hàng khỏi session
            HttpContext.Session.Remove("Cart");
            // Chuyển hướng về trang chủ hoặc trang thanh toán thành công
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Privacy()
        {
            // Đọc dữ liệu ra từ Session
            var sessionData = HttpContext.Session.GetString("mitom2trung");
            if (sessionData == null) ViewData["data"] = "Session không tồn tại";
            else ViewData["data"] = sessionData;
            return View();
        }

        public IActionResult TestSession()
        {
            //string message = "Em đói lắm không nghĩ được đâu";
            //// Đưa dữ liệu vào phiên làm việc (Session)
            //HttpContext.Session.SetString("mitom2trung", message);
            //// Đọc dữ liệu ra từ Session
            //var sessionData = HttpContext.Session.GetString("mitom2trung");
            //ViewData["data"] = sessionData;



            string message = "Em đói lắm không nghĩ được đâu";
            // Đưa dữ liệu vào phiên làm việc (Session)
            HttpContext.Session.SetString("oldShow", message);
            // Đọc dữ liệu ra từ Session
            var sessionData = HttpContext.Session.GetString("oldShow");
            ViewData["data"] = sessionData;


            /*
             * Timeout của session được tính như thế nào:
             * *Khi Session đã tồn tại, Bộ đếm thời gian sẽ được kích hoạt
             * ngay sau khi request cuối cùng được thực thi. Nếu sau khoảng 
             * thời gian idleTimeout mà không có request nào được thực thi
             * thì dữ liệu đó sẽ mất. Nếu trước khi thời gian timeout kết
             * thúc mà có 1 request bất kì được thực thi thì bộ đếm thời 
             * gian sẽ được reset.
             */
            return View();
        }
        public IActionResult AddToCart(Guid id, int quantity)
        {

            //// B1: Dựa vào ID lấy ra sản phẩm
            //var product = productServices.GetProductById(id);
            //// B2: Lấy danh sách sản phẩm ra từ Session
            //var products = SessionServices.GetObjeFromSession(HttpContext.Session, "Cart");

            //product.AvailableQuantity = 1;
            //if (products.Count == 0)
            //{

            //    product.AvailableQuantity = 1;
            //    products.Add(product);// thêm trực tiếp sản phẩm vào nếu List trống
            //    SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
            //}
            //else
            //{
            //    if (SessionServices.CheckObjInList(id, products))
            //    {// kiểm tra xem list lấy ra có chứa sp mình chọn hay chưa
            //     //productServices.UpdateProduct(product);
            //        //products.Remove(product);
            //        //SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
            //        product.AvailableQuantity = quantity++ ;             
            //        products.Add(product);
            //        SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);

            //        //return
            //        //        Content("Bình thường chúng ta sẽ thêm số lượng" +
            //        //    " nhưng vì lười nên không thêm mà chỉ đưa ra thông báo vô ích này");
            //    }
            //    else
            //    {
            //        products.Add(product);// thêm trực tiếp sản phẩm vào nếu List chưa chứa sp đó
            //        SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
            //    }
            //}
            //// B3: Kiểm tra và thêm sản phẩm vào giỏ hàng
            //return RedirectToAction("ShowCart"); /*Search*/




            // Bước 1: Dựa vào ID lấy ra sản phẩm.
            var product = productServices.GetProductById(id);

            // Bước 2: Lấy danh sách sản phẩm ra từ Session.
            var products = SessionServices.GetObjeFromSession(HttpContext.Session, "Cart");

            // Bước 3: Kiểm tra xem sản phẩm đã có trong giỏ hàng hay chưa.
            var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.AvailableQuantity += 1; // Tăng số lượng sản phẩm lên 1.
            }
            else
            {
                product.AvailableQuantity = 1;
                products.Add(product); // Thêm sản phẩm vào giỏ hàng.
            }

            // Bước 4: Cập nhật lại giỏ hàng trong Session.
            SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);

            // Bước 5: Chuyển hướng tới trang giỏ hàng.
            return RedirectToAction("ShowCart");

        }

        [HttpPost]
        public IActionResult Checkout1()
        {
            // Lấy danh sách sản phẩm trong giỏ hàng ra từ session
            var products = SessionServices.GetObjeFromSession(HttpContext.Session, "Cart");
            // Duyệt qua từng sản phẩm trong giỏ hàng
            foreach (var product in products)
            {
                // Lấy sản phẩm từ DB bằng ID
                var dbProduct = productServices.GetProductById(product.Id);
                // Giảm số lượng sản phẩm trong DB đi số lượng sản phẩm trong giỏ hàng
                dbProduct.AvailableQuantity -= product.AvailableQuantity;
                // Cập nhật sản phẩm trong DB
                productServices.UpdateProduct(dbProduct);
            }
            // Xóa danh sách sản phẩm trong giỏ hàng khỏi session
            HttpContext.Session.Remove("Cart");
            // Chuyển hướng về trang chủ hoặc trang thanh toán thành công
            return RedirectToAction("Index", "Home");
        }


        public IActionResult ShowCart()
        {
            var sessionData = HttpContext.Session.GetString("tk");
            ViewData["User"] = sessionData;
            // Lấy dữ liệu từ session để truyền vào view
            var products = SessionServices.GetObjeFromSession(HttpContext.Session, "Cart");
            return View(products); // truyền sang view
        }

        public IActionResult ShowOld()
        {
            var sessionData = HttpContext.Session.GetString("tk");
            ViewData["User"] = sessionData;
            // Lấy dữ liệu từ session để truyền vào view
            var products = SessionServices.GetObjeFromSession(HttpContext.Session, "oldProduct");


            return View(products); // truyền sang view
        }
        public IActionResult RollBack(Guid id)
        {

            // lấy list sản phẩm trong session
            var products = SessionServices.GetObjeFromSession(HttpContext.Session, "oldProduct");

            // tìm sản phẩm muốn rollback lại trong list session bằng Id
            Product rollback = products.Find(x => x.Id == id);

            // Lấy sản phẩm khi chưa thay đổi trước đó trong session và cập nhật lại
            productServices.UpdateProduct(rollback);

            // Xóa lịch sử thay đổi trước đó sau khi đã roll back
            products.Remove(rollback);
            SessionServices.SetObjToSession(HttpContext.Session, "oldProduct", products);

            return RedirectToAction("ShowAllProduct"); // truyền sang view
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}