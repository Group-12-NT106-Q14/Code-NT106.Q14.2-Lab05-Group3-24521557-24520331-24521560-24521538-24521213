using HtmlAgilityPack;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code_NT106.Q14._2_Lab05_Group3_24521557_24520331_24521560_24521538_24521213
{
    public partial class Bai04 : Form
    {
        private readonly string moviesJsonPath = "movies.json";

        private List<MovieSummary> movies = new List<MovieSummary>();
        private MovieSummary selectedMovie;

        public Bai04()
        {
            InitializeComponent();
            this.Text = "Bài 04 – Quản lý phòng vé (phiên bản 5)";
            InitWebView2Async();
        }

        private async void InitWebView2Async()
        {
            try
            {
                await webViewDetail.EnsureCoreWebView2Async(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo WebView2: " + ex.Message);
            }
        }

        private async void btnCrawl_Click(object sender, EventArgs e)
        {
            btnCrawl.Enabled = false;
            btnLoadFromJson.Enabled = false;
            btnBook.Enabled = false;
            progressBar.Visible = true;
            lblStatus.Text = "Đang tải trang phim...";

            try
            {
                movies = await CrawlMoviesAsync();
                SaveMoviesToJson(movies);
                lblStatus.Text = $"Đã lấy {movies.Count} phim, đã lưu vào {moviesJsonPath}";
                BindMoviesToFlowLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi crawl dữ liệu: " + ex.Message);
            }
            finally
            {
                progressBar.Visible = false;
                btnCrawl.Enabled = true;
                btnLoadFromJson.Enabled = true;
                btnBook.Enabled = true;
            }
        }

        private void btnLoadFromJson_Click(object sender, EventArgs e)
        {
            if (!File.Exists(moviesJsonPath))
            {
                MessageBox.Show($"Không tìm thấy file {moviesJsonPath}, hãy crawl trước.");
                return;
            }

            try
            {
                string json = File.ReadAllText(moviesJsonPath);
                movies = JsonConvert.DeserializeObject<List<MovieSummary>>(json) ?? new List<MovieSummary>();
                lblStatus.Text = $"Đã đọc {movies.Count} phim từ {moviesJsonPath}";
                BindMoviesToFlowLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc JSON: " + ex.Message);
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (movies == null || movies.Count == 0)
            {
                MessageBox.Show("Chưa có danh sách phim. Hãy crawl hoặc đọc JSON trước.");
                return;
            }

            string initMovie = selectedMovie != null ? selectedMovie.Name : null;

            using (var f = new BookingForm(movies, initMovie))
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);
            }
        }

        private async Task<List<MovieSummary>> CrawlMoviesAsync()
        {
            string url = "https://betacinemas.vn/phim.htm";
            var list = new List<MovieSummary>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                string html = await client.GetStringAsync(url);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                var filmInfoNodes = doc.DocumentNode.SelectNodes(
                    "//div[contains(@class,'film-info') and contains(@class,'film-xs-info')]");

                if (filmInfoNodes == null || filmInfoNodes.Count == 0)
                    throw new Exception("Không tìm thấy danh sách phim trong trang HTML.");

                progressBar.Visible = true;
                progressBar.Minimum = 0;
                progressBar.Maximum = filmInfoNodes.Count;
                progressBar.Value = 0;

                int index = 0;
                foreach (var filmInfo in filmInfoNodes)
                {
                    var linkNode = filmInfo.SelectSingleNode(".//h3/a");
                    if (linkNode == null)
                        continue;

                    string name = WebUtility.HtmlDecode(linkNode.InnerText.Trim());
                    string href = linkNode.GetAttributeValue("href", "").Trim();
                    string detailUrl = new Uri(new Uri("https://betacinemas.vn"), href).ToString();

                    var liGenre = filmInfo.SelectSingleNode(".//ul/li[1]");
                    string genre = "";
                    if (liGenre != null)
                    {
                        genre = WebUtility.HtmlDecode(liGenre.InnerText.Trim());
                        int idx = genre.IndexOf(':');
                        if (idx >= 0 && idx + 1 < genre.Length)
                            genre = genre.Substring(idx + 1).Trim();
                    }

                    var liDuration = filmInfo.SelectSingleNode(".//ul/li[2]");
                    string duration = "";
                    if (liDuration != null)
                    {
                        duration = WebUtility.HtmlDecode(liDuration.InnerText.Trim());
                        int idx = duration.IndexOf(':');
                        if (idx >= 0 && idx + 1 < duration.Length)
                            duration = duration.Substring(idx + 1).Trim();
                    }

                    var productItem = filmInfo.ParentNode;
                    if (productItem != null)
                        productItem = productItem.ParentNode;
                    var imgNode = productItem?.SelectSingleNode(".//img[contains(@class,'border-radius-20')]");
                    string posterUrl = "";
                    if (imgNode != null)
                    {
                        string src = imgNode.GetAttributeValue("src", "").Trim();
                        posterUrl = new Uri(new Uri("https://betacinemas.vn"), src).ToString();
                    }

                    string id = null;
                    var uri = new Uri(detailUrl);
                    var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                    if (!string.IsNullOrEmpty(query["gf"]))
                        id = query["gf"];

                    list.Add(new MovieSummary
                    {
                        Id = id,
                        Name = name,
                        DetailUrl = detailUrl,
                        PosterUrl = posterUrl,
                        Genre = genre,
                        Duration = duration
                    });

                    index++;
                    progressBar.Value = index;
                    lblStatus.Text = $"Đang trích xuất phim {index}/{filmInfoNodes.Count}: {name}";
                    await Task.Delay(50);
                }
            }

            return list;
        }

        private void SaveMoviesToJson(List<MovieSummary> list)
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(moviesJsonPath, json);
        }

        private void BindMoviesToFlowLayout()
        {
            flpMovies.Controls.Clear();
            selectedMovie = null;

            foreach (var mv in movies)
            {
                var panel = CreateMovieItemPanel(mv);
                flpMovies.Controls.Add(panel);
            }
        }

        private Panel CreateMovieItemPanel(MovieSummary movie)
        {
            var panel = new Panel
            {
                Width = flpMovies.ClientSize.Width - 25,
                Height = 90,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                BackColor = Color.White,
                Tag = movie,
                Cursor = Cursors.Hand
            };

            var pic = new PictureBox
            {
                Width = 60,
                Height = 80,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(5, 5)
            };
            if (!string.IsNullOrEmpty(movie.PosterUrl))
            {
                try
                {
                    pic.LoadAsync(movie.PosterUrl);
                }
                catch
                {
                }
            }

            var lblName = new Label
            {
                AutoSize = false,
                Location = new Point(75, 5),
                Size = new Size(panel.Width - 80, 40),
                Text = movie.Name,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            var lblUrl = new Label
            {
                AutoSize = false,
                Location = new Point(75, 45),
                Size = new Size(panel.Width - 80, 40),
                Text = movie.DetailUrl,
                ForeColor = Color.Blue
            };

            panel.Controls.Add(pic);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblUrl);

            panel.Click += MoviePanel_Click;
            pic.Click += MoviePanel_Click;
            lblName.Click += MoviePanel_Click;
            lblUrl.Click += MoviePanel_Click;

            return panel;
        }

        private void MoviePanel_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (c == null) return;

            var panel = c as Panel ?? c.Parent as Panel;
            if (panel == null) return;

            var mv = panel.Tag as MovieSummary;
            if (mv == null) return;

            selectedMovie = mv;

            if (webViewDetail.CoreWebView2 != null)
            {
                webViewDetail.CoreWebView2.Navigate(mv.DetailUrl);
            }
            else
            {
                webViewDetail.Source = new Uri(mv.DetailUrl);
            }

            lblStatus.Text = $"Đang xem: {mv.Name}";
        }
    }
}