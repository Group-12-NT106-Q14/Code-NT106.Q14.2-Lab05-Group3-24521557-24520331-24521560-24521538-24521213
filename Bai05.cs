using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MailKit.Security;
using MimeKit;

namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    public partial class Bai05 : Form
    {
        private readonly string connectionString = "Data Source=Foods.db;Version=3;";

        private const string InboxEmail = "group12.nt106.q14@gmail.com";
        private const string InboxAppPassword = "tmjx bacw rvsg dybr";
        private const string ImapServer = "imap.gmail.com";
        private const int ImapPort = 993;

        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 465;

        private readonly System.Windows.Forms.Timer previewTimer = new System.Windows.Forms.Timer();

        public Bai05()
        {
            InitializeComponent();

            previewTimer.Interval = 500;
            previewTimer.Tick += PreviewTimer_Tick;

            txtHinhAnh.TextChanged += txtHinhAnh_TextChanged;
        }

        private void Bai05_Load(object sender, EventArgs e)
        {
            InitializeDatabase();
            LoadDataToListView();
        }

        private static string NormalizeFoodNameKey(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";
            string s = Regex.Replace(input.Trim(), @"\s+", " ");
            s = s.Normalize(NormalizationForm.FormKC);
            return s.ToLowerInvariant();
        }

        private void InitializeDatabase()
        {
            try
            {
                if (!File.Exists("Foods.db"))
                    SQLiteConnection.CreateFile("Foods.db");

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string createUserTable = @"CREATE TABLE IF NOT EXISTS NguoiDung (
                        IDNCC INTEGER PRIMARY KEY AUTOINCREMENT,
                        HoVaTen TEXT NOT NULL,
                        QuyenHan TEXT DEFAULT 'User'
                    )";

                    string createFoodTable = @"CREATE TABLE IF NOT EXISTS MonAn (
                        IDMA INTEGER PRIMARY KEY AUTOINCREMENT,
                        TenMonAn TEXT NOT NULL,
                        TenMonAnKey TEXT,
                        HinhAnh TEXT,
                        IDNCC INTEGER,
                        FOREIGN KEY (IDNCC) REFERENCES NguoiDung(IDNCC)
                    )";

                    string createEmailProcessed = @"CREATE TABLE IF NOT EXISTS EmailProcessed (
                        MessageId TEXT PRIMARY KEY,
                        ProcessedAt TEXT DEFAULT (datetime('now'))
                    )";

                    using (SQLiteCommand cmd = new SQLiteCommand(createUserTable, conn)) cmd.ExecuteNonQuery();
                    using (SQLiteCommand cmd = new SQLiteCommand(createFoodTable, conn)) cmd.ExecuteNonQuery();
                    using (SQLiteCommand cmd = new SQLiteCommand(createEmailProcessed, conn)) cmd.ExecuteNonQuery();

                    EnsureNguoiDungEmailColumn(conn);
                    EnsureMonAnKeyColumn(conn);
                    AddDefaultData(conn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo database: {ex.Message}");
            }
        }

        private void EnsureNguoiDungEmailColumn(SQLiteConnection conn)
        {
            bool hasEmail = false;

            using (var cmd = new SQLiteCommand("PRAGMA table_info(NguoiDung);", conn))
            using (var r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    var col = r["name"]?.ToString();
                    if (string.Equals(col, "Email", StringComparison.OrdinalIgnoreCase))
                    {
                        hasEmail = true;
                        break;
                    }
                }
            }

            if (!hasEmail)
            {
                using var cmd2 = new SQLiteCommand("ALTER TABLE NguoiDung ADD COLUMN Email TEXT;", conn);
                cmd2.ExecuteNonQuery();
            }
        }

        private void EnsureMonAnKeyColumn(SQLiteConnection conn)
        {
            bool hasKey = false;

            using (var cmd = new SQLiteCommand("PRAGMA table_info(MonAn);", conn))
            using (var r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    var col = r["name"]?.ToString();
                    if (string.Equals(col, "TenMonAnKey", StringComparison.OrdinalIgnoreCase))
                    {
                        hasKey = true;
                        break;
                    }
                }
            }

            if (!hasKey)
            {
                using var alter = new SQLiteCommand("ALTER TABLE MonAn ADD COLUMN TenMonAnKey TEXT;", conn);
                alter.ExecuteNonQuery();
            }

            using (var sel = new SQLiteCommand("SELECT IDMA, TenMonAn FROM MonAn WHERE IFNULL(TenMonAnKey,'')='';", conn))
            using (var rd = sel.ExecuteReader())
            {
                while (rd.Read())
                {
                    int id = Convert.ToInt32(rd["IDMA"]);
                    string ten = rd["TenMonAn"]?.ToString() ?? "";
                    string key = NormalizeFoodNameKey(ten);

                    using var up = new SQLiteCommand("UPDATE MonAn SET TenMonAnKey=@Key WHERE IDMA=@Id;", conn);
                    up.Parameters.AddWithValue("@Key", key);
                    up.Parameters.AddWithValue("@Id", id);
                    up.ExecuteNonQuery();
                }
            }

            using var idx = new SQLiteCommand("CREATE INDEX IF NOT EXISTS idx_MonAn_TenMonAnKey ON MonAn(TenMonAnKey);", conn);
            idx.ExecuteNonQuery();
        }

        private void AddDefaultData(SQLiteConnection conn)
        {
            string checkData = "SELECT COUNT(*) FROM MonAn";
            using (SQLiteCommand cmd = new SQLiteCommand(checkData, conn))
            {
                long count = (long)cmd.ExecuteScalar();
                if (count == 0)
                {
                    string[] defaultUsers = { "Trương Vĩnh Nguyên", "Reyna", "Đào Mạnh Nhân" };
                    foreach (string user in defaultUsers)
                    {
                        string insertUser = "INSERT INTO NguoiDung (HoVaTen, QuyenHan) VALUES (@HoVaTen, 'User')";
                        using (SQLiteCommand cmdUser = new SQLiteCommand(insertUser, conn))
                        {
                            cmdUser.Parameters.AddWithValue("@HoVaTen", user);
                            cmdUser.ExecuteNonQuery();
                        }
                    }

                    string[][] defaultFoods = new string[][]
                    {
                        new string[] { "Phở", "https://i-giadinh.vnecdn.net/2023/08/18/Bc9Thnhphm9-1692350453-3832-1692350464.jpg", "1" },
                        new string[] { "Bún bò Huế", "https://mms.img.susercontent.com/vn-11134513-7r98o-lsvdf3utj44905@resize_ss640x400!@crop_w640_h400_cT", "2" },
                        new string[] { "Cơm tấm", "https://sakos.vn/wp-content/uploads/2024/09/bia.jpg", "3" },
                        new string[] { "Bánh mì", "https://cleverjunior.vn/wp-content/uploads/2022/08/gioi-thieu-banh-mi-bang-tieng-anh-1-768x480.jpg", "1" },
                        new string[] { "Gỏi cuốn", "https://cdn.tcdulichtphcm.vn/upload/2-2021/images/2021-05-14/1620967472-5fd6cc95e23f4d1eb34009678c2d6556.jpg", "2" }
                    };

                    foreach (string[] food in defaultFoods)
                    {
                        string insertFood = "INSERT INTO MonAn (TenMonAn, TenMonAnKey, HinhAnh, IDNCC) VALUES (@TenMonAn, @TenMonAnKey, @HinhAnh, @IDNCC)";
                        using (SQLiteCommand cmdFood = new SQLiteCommand(insertFood, conn))
                        {
                            cmdFood.Parameters.AddWithValue("@TenMonAn", food[0]);
                            cmdFood.Parameters.AddWithValue("@TenMonAnKey", NormalizeFoodNameKey(food[0]));
                            cmdFood.Parameters.AddWithValue("@HinhAnh", food[1]);
                            cmdFood.Parameters.AddWithValue("@IDNCC", food[2]);
                            cmdFood.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void LoadDataToListView()
        {
            try
            {
                listViewMonAn.Items.Clear();

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT m.IDMA, m.TenMonAn, m.HinhAnh, n.HoVaTen 
                                   FROM MonAn m 
                                   INNER JOIN NguoiDung n ON m.IDNCC = n.IDNCC 
                                   ORDER BY m.IDMA";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["IDMA"].ToString());
                            item.SubItems.Add(reader["TenMonAn"].ToString());
                            item.SubItems.Add(reader["HinhAnh"].ToString());
                            item.SubItems.Add(reader["HoVaTen"].ToString());
                            listViewMonAn.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}");
            }
        }

        private void txtHinhAnh_TextChanged(object sender, EventArgs e)
        {
            previewTimer.Stop();
            previewTimer.Start();
        }

        private void PreviewTimer_Tick(object sender, EventArgs e)
        {
            previewTimer.Stop();
            LoadPreviewImage();
        }

        private void LoadPreviewImage()
        {
            string s = txtHinhAnh.Text.Trim();

            if (string.IsNullOrWhiteSpace(s))
            {
                SetPictureBoxImage(pictureBoxPreview, null);
                return;
            }

            if (IsHttpUrl(s))
            {
                LoadImageFromUrl(s, pictureBoxPreview);
                return;
            }

            if (File.Exists(s))
            {
                LoadImageFromFile(s, pictureBoxPreview);
                return;
            }

            SetPictureBoxImage(pictureBoxPreview, null);
        }

        private bool IsHttpUrl(string s)
        {
            return Uri.TryCreate(s, UriKind.Absolute, out var uri)
                   && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }

        private bool IsValidImagePathOrUrl(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            try
            {
                if (IsHttpUrl(input)) return true;

                if (!File.Exists(input)) return false;

                string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
                string ext = Path.GetExtension(input).ToLower();
                return validExtensions.Contains(ext);
            }
            catch
            {
                return false;
            }
        }

        private string SaveBytesToImagesFolder(byte[] bytes, string fileNameHint)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string imagesDir = Path.Combine(baseDir, "Images");
            Directory.CreateDirectory(imagesDir);

            string ext = Path.GetExtension(fileNameHint ?? "");
            if (string.IsNullOrWhiteSpace(ext)) ext = ".jpg";

            string fileName = $"{Guid.NewGuid():N}{ext}";
            string fullPath = Path.Combine(imagesDir, fileName);
            File.WriteAllBytes(fullPath, bytes);

            return Path.Combine("Images", fileName);
        }

        private void SetPictureBoxImage(PictureBox pb, Image img)
        {
            if (pb.Image != null)
            {
                var old = pb.Image;
                pb.Image = null;
                old.Dispose();
            }
            pb.Image = img;
        }

        private void LoadImageFromUrl(string url, PictureBox pb)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] imageData = client.DownloadData(url);
                    using (MemoryStream ms = new MemoryStream(imageData))
                    using (Image temp = Image.FromStream(ms))
                    {
                        SetPictureBoxImage(pb, new Bitmap(temp));
                    }
                }
            }
            catch
            {
                SetPictureBoxImage(pb, null);
            }
        }

        private void LoadImageFromFile(string pathInDb, PictureBox pb)
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string fullPath = Path.IsPathRooted(pathInDb) ? pathInDb : Path.Combine(baseDir, pathInDb);

                if (!File.Exists(fullPath))
                {
                    SetPictureBoxImage(pb, null);
                    return;
                }

                byte[] bytes = File.ReadAllBytes(fullPath);
                using (var ms = new MemoryStream(bytes))
                using (var temp = Image.FromStream(ms))
                {
                    SetPictureBoxImage(pb, new Bitmap(temp));
                }
            }
            catch
            {
                SetPictureBoxImage(pb, null);
            }
        }

        private void LoadImageFromReference(string imgRef, PictureBox pb)
        {
            if (string.IsNullOrWhiteSpace(imgRef))
            {
                SetPictureBoxImage(pb, null);
                return;
            }

            if (IsHttpUrl(imgRef))
                LoadImageFromUrl(imgRef, pb);
            else
                LoadImageFromFile(imgRef, pb);
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.webp";
            openFileDialog1.Title = "Chọn ảnh món ăn";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtHinhAnh.Text = openFileDialog1.FileName;
                LoadImageFromReference(txtHinhAnh.Text.Trim(), pictureBoxPreview);
            }
        }

        private void btnGuiDongGop_Click(object sender, EventArgs e)
        {
            string senderEmail = txtSenderEmail.Text.Trim();
            string senderAppPassword = txtSenderAppPassword.Text;
            string tenMonAn = txtTenMonAn.Text.Trim();
            string hinhAnhInput = txtHinhAnh.Text.Trim();

            if (string.IsNullOrWhiteSpace(senderEmail) || string.IsNullOrWhiteSpace(senderAppPassword))
            {
                MessageBox.Show("Vui lòng nhập Email và AppPassword của bạn để gửi đóng góp.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tenMonAn))
            {
                MessageBox.Show("Vui lòng nhập tên món ăn!");
                return;
            }

            bool hasImage = !string.IsNullOrWhiteSpace(hinhAnhInput);
            bool isUrl = hasImage && IsHttpUrl(hinhAnhInput);
            bool isFile = hasImage && File.Exists(hinhAnhInput);

            if (hasImage && !isUrl && !isFile)
            {
                MessageBox.Show("Hình ảnh không hợp lệ. Nhập URL ảnh hoặc chọn file ảnh từ máy.");
                return;
            }

            if (hasImage && isFile && !IsValidImagePathOrUrl(hinhAnhInput))
            {
                MessageBox.Show("File ảnh không hợp lệ.");
                return;
            }

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("", senderEmail));
                message.To.Add(new MailboxAddress("", InboxEmail));
                message.Subject = "Đóng góp món ăn";

                var builder = new BodyBuilder();

                if (hasImage && isUrl)
                {
                    builder.TextBody = $"{tenMonAn};{hinhAnhInput}";
                }
                else if (hasImage && isFile)
                {
                    builder.TextBody = $"{tenMonAn};";
                    builder.Attachments.Add(hinhAnhInput);
                }
                else
                {
                    builder.TextBody = $"{tenMonAn};";
                }

                message.Body = builder.ToMessageBody();

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(SmtpServer, SmtpPort, SecureSocketOptions.SslOnConnect);
                    smtp.Authenticate(senderEmail, senderAppPassword);
                    smtp.Send(message);
                    smtp.Disconnect(true);
                }

                MessageBox.Show("Đã gửi đóng góp. Bấm 'Tải đóng góp' để cập nhật vào danh sách.");

                txtTenMonAn.Clear();
                txtHinhAnh.Clear();
                SetPictureBoxImage(pictureBoxPreview, null);
            }
            catch (MailKit.Security.AuthenticationException)
            {
                MessageBox.Show("Gửi email thất bại. Gmail của bạn cần App Password (bật 2FA) hoặc nhập sai App Password.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gửi email thất bại: {ex.Message}");
            }
        }

        private void btnTaiDongGop_Click(object sender, EventArgs e)
        {
            int emailsFound = 0;
            int emailsProcessed = 0;
            int emailsSkippedAlreadyProcessed = 0;
            int foodsInserted = 0;
            int foodsSkippedExisting = 0;
            int emailsMarkedRead = 0;

            try
            {
                using (var client = new ImapClient())
                {
                    client.Connect(ImapServer, ImapPort, SecureSocketOptions.SslOnConnect);
                    client.Authenticate(InboxEmail, InboxAppPassword);

                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadWrite);

                    var query = SearchQuery.SubjectContains("Đóng góp món ăn");
                    var uids = inbox.Search(query);
                    emailsFound = uids.Count;

                    if (emailsFound == 0)
                    {
                        MessageBox.Show("Không có email có tiêu đề: Đóng góp món ăn.");
                        client.Disconnect(true);
                        return;
                    }

                    using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                    {
                        conn.Open();

                        foreach (var uid in uids)
                        {
                            var msg = inbox.GetMessage(uid);

                            string subject = (msg.Subject ?? "").Trim();
                            if (!subject.Equals("Đóng góp món ăn", StringComparison.OrdinalIgnoreCase))
                                continue;

                            string messageId = (msg.MessageId ?? "").Trim();
                            if (string.IsNullOrEmpty(messageId))
                                messageId = $"{uid.Id}@{ImapServer}";

                            if (IsEmailProcessed(conn, messageId))
                            {
                                emailsSkippedAlreadyProcessed++;
                                inbox.AddFlags(uid, MessageFlags.Seen, true);
                                emailsMarkedRead++;
                                continue;
                            }

                            var c = GetContributor(msg);
                            int idncc = GetOrCreateUser(conn, c.Name, c.Email);

                            string body = GetPlainBody(msg);
                            var items = ParseFoodsFromBody(body);
                            var attachmentPaths = SaveImageAttachmentsToApp(msg);
                            int attachIndex = 0;

                            foreach (var it in items)
                            {
                                string tenMon = (it.TenMon ?? "").Trim();
                                string hinh = (it.HinhAnh ?? "").Trim();

                                if (string.IsNullOrWhiteSpace(tenMon))
                                    continue;

                                if (string.IsNullOrWhiteSpace(hinh) && attachIndex < attachmentPaths.Count)
                                {
                                    hinh = attachmentPaths[attachIndex];
                                    attachIndex++;
                                }

                                if (!string.IsNullOrWhiteSpace(hinh) && !IsHttpUrl(hinh))
                                {
                                    string full = ResolveToFullPathIfRelative(hinh);
                                    if (!File.Exists(full)) continue;
                                    if (!IsValidImagePathOrUrl(full)) continue;
                                }

                                if (FoodExistsByNameKey(conn, tenMon))
                                {
                                    foodsSkippedExisting++;
                                    continue;
                                }

                                string insertFood = @"
INSERT INTO MonAn (TenMonAn, TenMonAnKey, HinhAnh, IDNCC)
VALUES (@TenMonAn, @TenMonAnKey, @HinhAnh, @IDNCC)";

                                using (SQLiteCommand cmd = new SQLiteCommand(insertFood, conn))
                                {
                                    cmd.Parameters.AddWithValue("@TenMonAn", tenMon);
                                    cmd.Parameters.AddWithValue("@TenMonAnKey", NormalizeFoodNameKey(tenMon));
                                    cmd.Parameters.AddWithValue("@HinhAnh", hinh);
                                    cmd.Parameters.AddWithValue("@IDNCC", idncc);
                                    cmd.ExecuteNonQuery();
                                }

                                foodsInserted++;
                            }

                            MarkEmailProcessed(conn, messageId);

                            inbox.AddFlags(uid, MessageFlags.Seen, true);
                            emailsMarkedRead++;
                            emailsProcessed++;
                        }
                    }

                    client.Disconnect(true);
                }

                LoadDataToListView();

                MessageBox.Show(
                    "Tải đóng góp hoàn tất!\n\n" +
                    $"- Email tìm thấy: {emailsFound}\n" +
                    $"- Email đã xử lý: {emailsProcessed}\n" +
                    $"- Email bỏ qua (đã xử lý trước đó): {emailsSkippedAlreadyProcessed}\n" +
                    $"- Email đánh dấu đã đọc: {emailsMarkedRead}\n" +
                    $"- Món ăn thêm mới: {foodsInserted}\n" +
                    $"- Món ăn bỏ qua (đã tồn tại): {foodsSkippedExisting}"
                );
            }
            catch (MailKit.Security.AuthenticationException)
            {
                MessageBox.Show("Không đăng nhập được email nhận. Kiểm tra lại App Password của hộp thư nhận.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải email: {ex.Message}");
            }
        }

        private string ResolveToFullPathIfRelative(string pathInDb)
        {
            if (string.IsNullOrWhiteSpace(pathInDb)) return pathInDb;
            if (Path.IsPathRooted(pathInDb)) return pathInDb;
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathInDb);
        }

        private List<string> SaveImageAttachmentsToApp(MimeMessage msg)
        {
            var saved = new List<string>();

            foreach (var attachment in msg.Attachments)
            {
                if (attachment is MimePart part)
                {
                    string fileName = part.FileName ?? "";
                    string ext = Path.GetExtension(fileName).ToLower();
                    string[] ok = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

                    bool isImageByMime = part.ContentType != null && part.ContentType.MimeType != null
                        && part.ContentType.MimeType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);

                    bool isImageByExt = ok.Contains(ext);

                    if (!isImageByMime && !isImageByExt) continue;

                    using (var ms = new MemoryStream())
                    {
                        part.Content.DecodeTo(ms);
                        byte[] bytes = ms.ToArray();
                        if (bytes.Length == 0) continue;

                        string relative = SaveBytesToImagesFolder(bytes, fileName);
                        saved.Add(relative);
                    }
                }
            }

            return saved;
        }

        private (string Name, string Email) GetContributor(MimeMessage msg)
        {
            try
            {
                MailboxAddress mb =
                    msg.From.Mailboxes.FirstOrDefault()
                    ?? msg.Sender
                    ?? msg.ReplyTo.Mailboxes.FirstOrDefault();

                string name = mb?.Name?.Trim() ?? "";
                string email = mb?.Address?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(name))
                {
                    string rawFrom = msg.Headers["From"];
                    if (!string.IsNullOrWhiteSpace(rawFrom))
                    {
                        try
                        {
                            var parsed = MailboxAddress.Parse(rawFrom);
                            if (!string.IsNullOrWhiteSpace(parsed.Name)) name = parsed.Name.Trim();
                            if (string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(parsed.Address)) email = parsed.Address.Trim();
                        }
                        catch { }
                    }
                }

                return (name, email);
            }
            catch
            {
                return ("", "");
            }
        }

        private string GetPlainBody(MimeMessage msg)
        {
            if (!string.IsNullOrWhiteSpace(msg.TextBody))
                return msg.TextBody;

            if (!string.IsNullOrWhiteSpace(msg.HtmlBody))
                return HtmlToPlainText(msg.HtmlBody);

            return "";
        }

        private string HtmlToPlainText(string html)
        {
            if (string.IsNullOrWhiteSpace(html)) return "";

            string noScript = Regex.Replace(html, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            string noStyle = Regex.Replace(noScript, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            string text = Regex.Replace(noStyle, "<.*?>", " ");
            text = WebUtility.HtmlDecode(text);
            text = Regex.Replace(text, @"\s+", " ").Trim();
            return text;
        }

        private struct FoodItem
        {
            public string TenMon;
            public string HinhAnh;
        }

        private List<FoodItem> ParseFoodsFromBody(string body)
        {
            var result = new List<FoodItem>();
            if (string.IsNullOrWhiteSpace(body)) return result;

            var lines = body.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var raw in lines)
            {
                var line = raw.Trim();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(new[] { ';' }, 2);

                string ten = parts.Length >= 1 ? parts[0].Trim() : "";
                string hinh = parts.Length == 2 ? parts[1].Trim() : "";

                result.Add(new FoodItem { TenMon = ten, HinhAnh = hinh });
            }

            return result;
        }

        private int GetOrCreateUser(SQLiteConnection conn, string nameFromEmail, string email)
        {
            string name = (nameFromEmail ?? "").Trim();
            string mail = (email ?? "").Trim();

            if (!string.IsNullOrWhiteSpace(mail))
            {
                using var cmdFind = new SQLiteCommand("SELECT IDNCC, HoVaTen FROM NguoiDung WHERE Email = @Email ORDER BY IDNCC DESC LIMIT 1;", conn);
                cmdFind.Parameters.AddWithValue("@Email", mail);
                using var rd = cmdFind.ExecuteReader();
                if (rd.Read())
                {
                    int id = Convert.ToInt32(rd["IDNCC"]);
                    string oldName = rd["HoVaTen"]?.ToString() ?? "";

                    if (!string.IsNullOrWhiteSpace(name) && (string.IsNullOrWhiteSpace(oldName) || oldName == "Người ẩn danh"))
                    {
                        using var cmdUp = new SQLiteCommand("UPDATE NguoiDung SET HoVaTen=@HoVaTen WHERE IDNCC=@IDNCC;", conn);
                        cmdUp.Parameters.AddWithValue("@HoVaTen", name);
                        cmdUp.Parameters.AddWithValue("@IDNCC", id);
                        cmdUp.ExecuteNonQuery();
                        return id;
                    }

                    return id;
                }
            }

            if (string.IsNullOrWhiteSpace(name))
                name = "Người ẩn danh";

            using var cmdIns = new SQLiteCommand("INSERT INTO NguoiDung (HoVaTen, QuyenHan, Email) VALUES (@HoVaTen, 'User', @Email); SELECT last_insert_rowid();", conn);
            cmdIns.Parameters.AddWithValue("@HoVaTen", name);
            cmdIns.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(mail) ? (object)DBNull.Value : mail);
            return Convert.ToInt32(cmdIns.ExecuteScalar());
        }

        private bool FoodExistsByNameKey(SQLiteConnection conn, string tenMonAn)
        {
            string key = NormalizeFoodNameKey(tenMonAn);

            const string q = "SELECT COUNT(*) FROM MonAn WHERE TenMonAnKey = @Key";
            using (SQLiteCommand cmd = new SQLiteCommand(q, conn))
            {
                cmd.Parameters.AddWithValue("@Key", key);
                long c = (long)cmd.ExecuteScalar();
                return c > 0;
            }
        }

        private bool IsEmailProcessed(SQLiteConnection conn, string messageId)
        {
            string q = "SELECT COUNT(*) FROM EmailProcessed WHERE MessageId = @MessageId";
            using (SQLiteCommand cmd = new SQLiteCommand(q, conn))
            {
                cmd.Parameters.AddWithValue("@MessageId", messageId);
                long c = (long)cmd.ExecuteScalar();
                return c > 0;
            }
        }

        private void MarkEmailProcessed(SQLiteConnection conn, string messageId)
        {
            string q = "INSERT OR IGNORE INTO EmailProcessed (MessageId) VALUES (@MessageId)";
            using (SQLiteCommand cmd = new SQLiteCommand(q, conn))
            {
                cmd.Parameters.AddWithValue("@MessageId", messageId);
                cmd.ExecuteNonQuery();
            }
        }

        private void btnTimMonAn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    int count = Convert.ToInt32(new SQLiteCommand("SELECT COUNT(*) FROM MonAn", conn).ExecuteScalar());
                    if (count == 0)
                    {
                        MessageBox.Show("Chưa có món ăn nào trong database!");
                        return;
                    }

                    string query = @"SELECT m.TenMonAn, m.HinhAnh, n.HoVaTen 
                                   FROM MonAn m 
                                   INNER JOIN NguoiDung n ON m.IDNCC = n.IDNCC 
                                   ORDER BY RANDOM() LIMIT 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string tenMonAn = reader["TenMonAn"].ToString();
                            string hinhAnh = reader["HinhAnh"].ToString();
                            string nguoiDongGop = reader["HoVaTen"].ToString();

                            lblKetQua.Text = $"{tenMonAn}\n\n(Đóng góp bởi: {nguoiDongGop})";
                            LoadImageFromReference(hinhAnh, pictureBoxMonAn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm món ăn: {ex.Message}");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa toàn bộ dữ liệu?", "Xác nhận", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                    {
                        conn.Open();

                        using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM MonAn", conn)) cmd.ExecuteNonQuery();
                        using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM NguoiDung", conn)) cmd.ExecuteNonQuery();
                        using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM EmailProcessed", conn)) cmd.ExecuteNonQuery();

                        using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM sqlite_sequence WHERE name='MonAn'", conn)) cmd.ExecuteNonQuery();
                        using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM sqlite_sequence WHERE name='NguoiDung'", conn)) cmd.ExecuteNonQuery();

                        AddDefaultData(conn);
                    }

                    LoadDataToListView();
                    lblKetQua.Text = "";
                    SetPictureBoxImage(pictureBoxMonAn, null);
                    SetPictureBoxImage(pictureBoxPreview, null);
                    MessageBox.Show("Đã xóa và khôi phục dữ liệu mẫu!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa dữ liệu: {ex.Message}");
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}