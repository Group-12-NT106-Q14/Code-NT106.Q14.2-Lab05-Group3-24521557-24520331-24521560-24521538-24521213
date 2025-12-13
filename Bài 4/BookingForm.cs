using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    public partial class BookingForm : Form
    {
        private class MovieInfo
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public List<int> Rooms { get; set; }
            public int TotalSeats { get; set; }
            public int SoldSeats { get; set; }
            public decimal Revenue { get; set; }
            public string PosterUrl { get; set; }
            public int RemainingSeats => TotalSeats - SoldSeats;
            public decimal SalesPercentage => TotalSeats > 0 ? (decimal)SoldSeats / TotalSeats * 100 : 0;
        }

        private const string GmailUser = "group12.nt106.q14@gmail.com";
        private const string GmailAppPassword = "tmjx bacw rvsg dybr";

        private Dictionary<string, MovieInfo> movies = new Dictionary<string, MovieInfo>();
        private Dictionary<string, decimal> seatPriceMultipliers = new Dictionary<string, decimal>();
        private Dictionary<string, bool> bookedSeats = new Dictionary<string, bool>();
        private Dictionary<string, Button> seatButtons = new Dictionary<string, Button>();
        private List<string> selectedSeats = new List<string>();
        private int currentRoom = 0;
        private const int TOTAL_SEATS_PER_ROOM = 188;

        public BookingForm(List<MovieSummary> movieSummaries, string initMovieName = null)
        {
            InitializeComponent();
            this.Text = "Đặt vé xem phim (phiên bản 5)";
            InitializeSeatMultipliers();
            InitializeMoviesFromSummaries(movieSummaries);
            UpdateMovieComboBox();

            if (!string.IsNullOrEmpty(initMovieName))
            {
                int index = cmbMovie.Items.IndexOf(initMovieName);
                if (index >= 0)
                    cmbMovie.SelectedIndex = index;
            }
        }

        private void InitializeSeatMultipliers()
        {
            string[] discountSeats = { "A1", "A5", "C1", "C5" };
            foreach (var seat in discountSeats)
                seatPriceMultipliers[seat] = 0.25m;

            string[] normalSeats = { "A2", "A3", "A4", "C2", "C3", "C4" };
            foreach (var seat in normalSeats)
                seatPriceMultipliers[seat] = 1m;

            string[] vipSeats = { "B2", "B3", "B4" };
            foreach (var seat in vipSeats)
                seatPriceMultipliers[seat] = 2m;

            string[] coupleSeats = { "M1+2", "M3+4", "M5+6", "M7+8", "M9+10", "M11+12" };
            foreach (var seat in coupleSeats)
                seatPriceMultipliers[seat] = 2m;
        }

        private void InitializeMoviesFromSummaries(List<MovieSummary> summaries)
        {
            movies.Clear();
            bookedSeats.Clear();

            Random rnd = new Random();

            foreach (var s in summaries)
            {
                int roomCount = rnd.Next(1, 4);
                List<int> rooms = new List<int>();

                while (rooms.Count < roomCount)
                {
                    int r = rnd.Next(1, 9);
                    if (!rooms.Contains(r))
                    {
                        rooms.Add(r);
                        InitializeSeatsForRoom(r);
                    }
                }

                movies[s.Name] = new MovieInfo
                {
                    Name = s.Name,
                    Price = 80000m,
                    Rooms = rooms,
                    TotalSeats = rooms.Count * TOTAL_SEATS_PER_ROOM,
                    SoldSeats = 0,
                    Revenue = 0,
                    PosterUrl = s.PosterUrl
                };
            }
        }

        private void InitializeSeatsForRoom(int room)
        {
            char[] rows = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M' };
            foreach (char row in rows)
            {
                if (row == 'M')
                {
                    string[] coupleSeats = { "M1+2", "M3+4", "M5+6", "M7+8", "M9+10", "M11+12" };
                    foreach (var seat in coupleSeats)
                    {
                        string seatKey = $"{room}_{seat}";
                        if (!bookedSeats.ContainsKey(seatKey))
                            bookedSeats[seatKey] = false;
                    }
                }
                else
                {
                    for (int col = 1; col <= 14; ++col)
                    {
                        string seatKey = $"{room}_{row}{col}";
                        if (!bookedSeats.ContainsKey(seatKey))
                            bookedSeats[seatKey] = false;
                    }
                }
            }
        }

        private void UpdateMovieComboBox()
        {
            cmbMovie.Items.Clear();
            foreach (var movie in movies.Keys)
            {
                cmbMovie.Items.Add(movie);
            }
        }

        private void cmbMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMovie.SelectedItem == null) return;

            string selectedMovie = cmbMovie.SelectedItem.ToString();
            cmbRoom.Items.Clear();

            if (movies.ContainsKey(selectedMovie))
            {
                foreach (var room in movies[selectedMovie].Rooms)
                {
                    cmbRoom.Items.Add($"Phòng {room}");
                }
            }

            cmbRoom.Enabled = true;
            panelSeats.Visible = false;
        }

        private void cmbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRoom.SelectedItem == null) return;

            currentRoom = int.Parse(cmbRoom.SelectedItem.ToString().Split(' ')[1]);
            selectedSeats.Clear();
            CreateSeatLayout();
            panelSeats.Visible = true;
        }

        private void CreateSeatLayout()
        {
            panelSeats.Controls.Clear();
            seatButtons.Clear();

            int startX = 50, startY = 80, buttonWidth = 35, buttonHeight = 30, spacingX = 40, spacingY = 35;
            char[] rows = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M' };

            Label screenLabel = new Label
            {
                Text = "Screen",
                Location = new Point(startX + 200, 10),
                Size = new Size(200, 30),
                Font = new Font("Arial", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.LightGray
            };
            panelSeats.Controls.Add(screenLabel);

            Panel screenPanel = new Panel
            {
                Location = new Point(startX + 50, 45),
                Size = new Size(500, 20),
                BackColor = Color.Transparent,
            };
            screenPanel.Paint += (s, e) =>
            {
                Point[] points = {
                    new Point(50,0), new Point(450,0), new Point(500,20), new Point(0,20)
                };
                e.Graphics.DrawPolygon(Pens.Black, points);
            };
            panelSeats.Controls.Add(screenPanel);

            for (int i = 0; i < rows.Length; i++)
            {
                Label rowLabel = new Label
                {
                    Text = rows[i].ToString(),
                    Location = new Point(20, startY + i * spacingY + 5),
                    Size = new Size(20, 20),
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panelSeats.Controls.Add(rowLabel);

                if (rows[i] == 'M')
                {
                    string[] coupleSeats = { "1+2", "3+4", "5+6", "7+8", "9+10", "11+12" };
                    for (int j = 0; j < coupleSeats.Length; j++)
                    {
                        string seatName = $"M{coupleSeats[j]}";
                        string seatKey = $"{currentRoom}_{seatName}";

                        Button btn = new Button
                        {
                            Text = coupleSeats[j],
                            Location = new Point(startX + j * spacingX * 2 + (j * 10), startY + i * spacingY),
                            Size = new Size(buttonWidth * 2 + 10, buttonHeight),
                            Font = new Font("Arial", 8, FontStyle.Bold),
                            Tag = seatName,
                            FlatStyle = FlatStyle.Flat
                        };

                        SetSeatButtonColor(btn, seatName, seatKey);
                        btn.Click += SeatButton_Click;
                        seatButtons[seatKey] = btn;
                        panelSeats.Controls.Add(btn);
                    }
                }
                else
                {
                    for (int j = 1; j <= 14; ++j)
                    {
                        string seatName = $"{rows[i]}{j}";
                        string seatKey = $"{currentRoom}_{seatName}";

                        Button btn = new Button
                        {
                            Text = j.ToString(),
                            Location = new Point(startX + (j - 1) * spacingX, startY + i * spacingY),
                            Size = new Size(buttonWidth, buttonHeight),
                            Font = new Font("Arial", 8),
                            Tag = seatName,
                            FlatStyle = FlatStyle.Flat
                        };

                        SetSeatButtonColor(btn, seatName, seatKey);
                        btn.Click += SeatButton_Click;
                        seatButtons[seatKey] = btn;
                        panelSeats.Controls.Add(btn);
                    }
                }
            }
        }

        private void SetSeatButtonColor(Button btn, string seatName, string seatKey)
        {
            if (bookedSeats.ContainsKey(seatKey) && bookedSeats[seatKey])
            {
                btn.BackColor = Color.DarkGray;
                btn.ForeColor = Color.White;
                btn.Enabled = false;
                return;
            }

            if (selectedSeats.Contains(seatName))
            {
                btn.BackColor = Color.Yellow;
                btn.ForeColor = Color.Black;
                return;
            }

            char row = seatName[0];
            if ((row == 'A' || row == 'B' || row == 'C'))
            {
                btn.BackColor = Color.Gray;
                btn.ForeColor = Color.White;
                btn.Enabled = false;
            }
            else if (row == 'M')
            {
                btn.BackColor = Color.DodgerBlue;
                btn.ForeColor = Color.White;
                btn.Enabled = true;
            }
            else if ((row == 'F' || row == 'G' || row == 'I'))
            {
                int col = int.Parse(seatName.Substring(1));
                if ((row == 'F' && (col == 7 || col == 8)) ||
                    (row == 'G' && (col == 7 || col == 8)) ||
                    (row == 'I' && (col >= 5 && col <= 10)))
                {
                    btn.BackColor = Color.Orange;
                    btn.ForeColor = Color.Black;
                }
                else
                {
                    btn.BackColor = Color.White;
                    btn.ForeColor = Color.Black;
                }
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
            }
        }

        private void SeatButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string seatName = btn.Tag.ToString();
            string seatKey = $"{currentRoom}_{seatName}";

            if (bookedSeats.ContainsKey(seatKey) && bookedSeats[seatKey])
            {
                MessageBox.Show("Ghế này đã được đặt!");
                return;
            }

            if (selectedSeats.Contains(seatName))
                selectedSeats.Remove(seatName);
            else
                selectedSeats.Add(seatName);

            SetSeatButtonColor(btn, seatName, seatKey);
            UpdateSelectedSeatsDisplay();
        }

        private void UpdateSelectedSeatsDisplay()
        {
            txtSelectedSeats.Text = string.Join(", ", selectedSeats);
        }

        private async void btnCalculate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên khách hàng!");
                return;
            }

            if (string.IsNullOrEmpty(txtCustomerEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email khách hàng!");
                return;
            }

            if (!txtCustomerEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email không hợp lệ!");
                return;
            }

            if (cmbMovie.SelectedItem == null || cmbRoom.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phim và phòng chiếu!");
                return;
            }

            if (selectedSeats.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một ghế!");
                return;
            }

            btnCalculate.Enabled = false;
            btnReset.Enabled = false;
            btnExit.Enabled = false;

            string customerName = txtCustomerName.Text.Trim();
            string customerEmail = txtCustomerEmail.Text.Trim();
            string movieName = cmbMovie.SelectedItem.ToString();
            decimal basePrice = movies[movieName].Price;
            decimal totalAmount = 0m;

            var seatsForEmail = selectedSeats.ToList();

            foreach (var seatName in seatsForEmail)
            {
                decimal multiplier = seatPriceMultipliers.ContainsKey(seatName) ? seatPriceMultipliers[seatName] : 1m;
                totalAmount += basePrice * multiplier;

                string seatKey = $"{currentRoom}_{seatName}";
                bookedSeats[seatKey] = true;
            }

            string sendErr = null;
            bool sent = false;

            try
            {
                ShowSending("Đang gửi email xác nhận vé...");
                await System.Threading.Tasks.Task.Delay(80);

                string posterUrl = "";
                if (movies.ContainsKey(movieName) && movies[movieName].PosterUrl != null)
                    posterUrl = movies[movieName].PosterUrl;

                await SendBookingEmailAsync(customerEmail, customerName, movieName, currentRoom, seatsForEmail, totalAmount, posterUrl);
                sent = true;
            }
            catch (Exception ex)
            {
                sendErr = ex.Message;
            }
            finally
            {
                HideSending();
            }

            if (!sent)
            {
                MessageBox.Show("Đặt vé thành công nhưng gửi email thất bại: " + sendErr);
            }
            else
            {
                string result = "";
                result += $"Họ và tên: {customerName}\r\n";
                result += $"Email: {customerEmail}\r\n";
                result += $"Tên phim: {movieName}\r\n";
                result += $"Phòng chiếu: {currentRoom}\r\n";
                result += $"Vé đã chọn: {string.Join(", ", seatsForEmail)}\r\n";
                result += $"Tổng tiền: {totalAmount:N0}đ\r\n";
                txtResult.Text = result;
                MessageBox.Show("Đặt vé thành công! Email xác nhận đã được gửi.");
            }

            selectedSeats.Clear();
            CreateSeatLayout();
            UpdateSelectedSeatsDisplay();

            btnCalculate.Enabled = true;
            btnReset.Enabled = true;
            btnExit.Enabled = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtCustomerName.Clear();
            txtCustomerEmail.Clear();
            cmbMovie.SelectedIndex = -1;
            cmbRoom.SelectedIndex = -1;
            cmbRoom.Items.Clear();
            cmbRoom.Enabled = false;
            txtResult.Clear();
            txtSelectedSeats.Clear();
            selectedSeats.Clear();
            panelSeats.Visible = false;
            panelSeats.Controls.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task SendBookingEmailAsync(string toEmail, string customerName, string movieName, int room, List<string> seats, decimal totalAmount, string posterUrl)
        {
            string bookingCode = GenBookingCode();
            DateTime bookingTime = DateTime.Now;
            byte[] posterBytes = null;
            string posterMime = "image/jpeg";
            string posterExt = ".jpg";

            if (!string.IsNullOrEmpty(posterUrl))
            {
                var poster = await TryDownloadPosterAsync(posterUrl);
                posterBytes = poster.Item1;
                posterMime = poster.Item2;
                posterExt = poster.Item3;
            }

            string seatsText = string.Join(", ", seats);
            string plain = BuildPlainText(customerName, movieName, room, seatsText, totalAmount, bookingCode, bookingTime);
            string cid = posterBytes != null ? MimeUtils.GenerateMessageId() : null;
            string html = BuildHtml(customerName, movieName, room, seatsText, totalAmount, bookingCode, bookingTime, cid);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("GROUP 12 CINEMA", GmailUser));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = $"Xác nhận đặt vé - {movieName}";

            var alternative = new Multipart("alternative");
            alternative.Add(new TextPart("plain") { Text = plain });
            alternative.Add(new TextPart("html") { Text = html });

            if (posterBytes != null)
            {
                var imagePart = new MimePart(posterMime)
                {
                    Content = new MimeContent(new MemoryStream(posterBytes)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Inline),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    ContentId = cid,
                    FileName = "poster" + posterExt
                };

                var related = new MultipartRelated();
                related.Add(alternative);
                related.Add(imagePart);
                message.Body = related;
            }
            else
            {
                message.Body = alternative;
            }

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp.gmail.com", 465, true);
                await smtp.AuthenticateAsync(GmailUser, GmailAppPassword.Replace(" ", ""));
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
        }

        private async Task<Tuple<byte[], string, string>> TryDownloadPosterAsync(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                byte[] data = await client.GetByteArrayAsync(url);

                string mime = "image/jpeg";
                string ext = ".jpg";

                try
                {
                    string path = new Uri(url).AbsolutePath;
                    string e = Path.GetExtension(path).ToLowerInvariant();
                    if (e == ".png") { mime = "image/png"; ext = ".png"; }
                    else if (e == ".gif") { mime = "image/gif"; ext = ".gif"; }
                    else if (e == ".webp") { mime = "image/webp"; ext = ".webp"; }
                    else if (e == ".jpeg") { mime = "image/jpeg"; ext = ".jpeg"; }
                    else if (e == ".jpg") { mime = "image/jpeg"; ext = ".jpg"; }
                }
                catch
                {
                }

                return Tuple.Create(data, mime, ext);
            }
        }

        private string BuildPlainText(string customerName, string movieName, int room, string seatsText, decimal totalAmount, string bookingCode, DateTime bookingTime)
        {
            var sb = new StringBuilder();
            sb.AppendLine("GROUP 12 CINEMA");
            sb.AppendLine("XÁC NHẬN ĐẶT VÉ");
            sb.AppendLine("");
            sb.AppendLine($"Xin chào {customerName},");
            sb.AppendLine("Đặt vé của bạn đã được xác nhận.");
            sb.AppendLine("");
            sb.AppendLine($"Mã đặt vé: {bookingCode}");
            sb.AppendLine($"Phim: {movieName}");
            sb.AppendLine($"Phòng chiếu: {room}");
            sb.AppendLine($"Ghế: {seatsText}");
            sb.AppendLine($"Thời gian đặt: {bookingTime:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Tổng tiền: {totalAmount:N0}đ");
            sb.AppendLine("");
            sb.AppendLine("Chúc bạn xem phim vui vẻ!");
            return sb.ToString();
        }

        private string BuildHtml(string customerName, string movieName, int room, string seatsText, decimal totalAmount, string bookingCode, DateTime bookingTime, string posterCid)
        {
            string hero = "";
            if (!string.IsNullOrEmpty(posterCid))
            {
                hero = $@"
<tr>
  <td style=""padding:0;"">
    <img src=""cid:{posterCid}"" alt=""Poster"" style=""width:100%;max-width:600px;display:block;"" />
  </td>
</tr>";
            }

            string html = $@"
<!DOCTYPE html>
<html>
<head>
<meta charset=""utf-8"">
<meta name=""viewport"" content=""width=device-width, initial-scale=1"">
</head>
<body style=""margin:0;padding:0;background:#f3f4f6;font-family:Arial,Helvetica,sans-serif;"">
  <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f3f4f6;padding:24px 0;"">
    <tr>
      <td align=""center"">
        <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""width:600px;max-width:600px;background:#ffffff;border-radius:16px;overflow:hidden;box-shadow:0 10px 30px rgba(0,0,0,0.10);"">
          <tr>
            <td style=""background:linear-gradient(90deg,#111827,#1f2937);padding:18px 24px;color:#ffffff;"">
              <div style=""font-size:18px;font-weight:700;letter-spacing:0.6px;"">GROUP 12 CINEMA</div>
              <div style=""font-size:12px;opacity:0.9;margin-top:4px;"">Xác nhận đặt vé</div>
            </td>
          </tr>
          {hero}
          <tr>
            <td style=""padding:22px 24px;"">
              <div style=""font-size:16px;color:#111827;font-weight:700;"">Xin chào {EscapeHtml(customerName)},</div>
              <div style=""margin-top:8px;font-size:14px;color:#374151;line-height:20px;"">
                Đặt vé của bạn đã được xác nhận. Vui lòng kiểm tra thông tin bên dưới:
              </div>

              <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""margin-top:16px;border-collapse:collapse;font-size:13px;"">
                <tr>
                  <td style=""padding:10px 12px;border:1px solid #e5e7eb;color:#6b7280;width:35%;"">Mã đặt vé</td>
                  <td style=""padding:10px 12px;border:1px solid #e5e7eb;color:#111827;font-weight:700;"">{EscapeHtml(bookingCode)}</td>
                </tr>
                <tr>
                  <td style=""padding:10px 12px;border:1px solid #e5e7eb;color:#6b7280;"">Phim</td>
                  <td style=""padding:10px 12px;border:1px solid #e5e7eb;color:#111827;font-weight:700;"">{EscapeHtml(movieName)}</td>
                </tr>
                <tr>
                  <td style=""padding:10px 12px;border:1px solid #e5e7eb;color:#6b7280;"">Phòng chiếu</td>
                  <td style=""padding:10px 12px;border:1px solid #e5e7eb;color:#111827;"">{room}</td>
                </tr>
                <tr>
                  <td style=""padding:10px 12px;border:1px solid #e5e7eb;color:#6b7280;"">Ghế</td>
                  <td style=""padding:10px 12px;border:1px solid #e5e7eb;color:#111827;"">{EscapeHtml(seatsText)}</td>
                </tr>
                <tr>
                  <td style=""padding:10px 12px;border:1px solid #e5e7eb;color:#6b7280;"">Thời gian đặt</td>
                  <td style=""padding:10px 12px;border:1px solid #e5e7eb;color:#111827;"">{bookingTime:dd/MM/yyyy HH:mm}</td>
                </tr>
                <tr>
                  <td style=""padding:10px 12px;border:1px solid #e5e7eb;color:#6b7280;"">Tổng tiền</td>
                  <td style=""padding:10px 12px;border:1px solid #e5e7eb;color:#111827;font-weight:700;"">{totalAmount:N0}đ</td>
                </tr>
              </table>

              <div style=""margin-top:18px;padding:14px 16px;background:#f9fafb;border-radius:12px;color:#374151;font-size:13px;line-height:18px;border:1px solid #e5e7eb;"">
                Chúc bạn xem phim vui vẻ!
              </div>

              <div style=""margin-top:14px;font-size:12px;color:#6b7280;line-height:18px;"">
                Nếu bạn không thực hiện giao dịch này, vui lòng bỏ qua email.
              </div>
            </td>
          </tr>
          <tr>
            <td style=""padding:14px 24px;background:#111827;color:#9ca3af;font-size:12px;line-height:18px;"">
              GROUP 12 CINEMA • Email xác nhận tự động
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>";
            return html;
        }

        private string EscapeHtml(string s)
        {
            if (s == null) return "";
            return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&#39;");
        }

        private string GenBookingCode()
        {
            string g = Guid.NewGuid().ToString("N").ToUpperInvariant();
            return g.Substring(0, 10);
        }
        private SendingForm sendingForm;

        private void ShowSending(string text)
        {
            if (sendingForm == null || sendingForm.IsDisposed)
                sendingForm = new SendingForm(text);
            else
                sendingForm.SetText(text);

            this.UseWaitCursor = true;
            sendingForm.StartPosition = FormStartPosition.CenterParent;
            sendingForm.Show(this);
            sendingForm.BringToFront();
            sendingForm.Refresh();
        }

        private void HideSending()
        {
            this.UseWaitCursor = false;
            if (sendingForm != null && !sendingForm.IsDisposed)
                sendingForm.Close();
        }

        private sealed class SendingForm : Form
        {
            private readonly Label lbl;
            private readonly ProgressBar pb;

            public SendingForm(string text)
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.ControlBox = false;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.ShowInTaskbar = false;
                this.TopMost = true;
                this.Width = 420;
                this.Height = 150;

                lbl = new Label();
                lbl.Dock = DockStyle.Top;
                lbl.Height = 60;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Font = new Font("Arial", 10, FontStyle.Bold);
                lbl.Text = text;

                pb = new ProgressBar();
                pb.Style = ProgressBarStyle.Marquee;
                pb.MarqueeAnimationSpeed = 30;
                pb.Dock = DockStyle.Top;
                pb.Height = 18;

                var p = new Panel();
                p.Dock = DockStyle.Fill;
                p.Padding = new Padding(22, 10, 22, 22);
                p.Controls.Add(pb);

                this.Controls.Add(p);
                this.Controls.Add(lbl);
            }

            public void SetText(string text)
            {
                lbl.Text = text;
            }
        }
    }
}