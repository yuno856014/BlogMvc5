using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WedBlogs.Helpers
{
    public static class Utilities
    {
        public static int GetFileSize(string urlfile)
        {
            int sizeFile = 0;
            try
            {
                Uri uriPath = new Uri(urlfile);
                var webRequest = HttpWebRequest.Create(uriPath);
                webRequest.Method = "HEAD";
                using (var wedResponse = webRequest.GetResponse())
                {
                    var fileSize = webRequest.Headers.Get("Content-Length");
                    var fileSizeInMegaByte = Math.Round(Convert.ToDouble(fileSize));
                    sizeFile = Convert.ToInt32(fileSizeInMegaByte);
                }

            }
            catch
            {
                return sizeFile;
            }
            return sizeFile;
        }
        public static string ToTitleCase(string str)
        {
            string result = str;
            if (!string.IsNullOrEmpty(str))
            {
                var words = str.Split(' ');
                for (int index = 0; index < words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
                result = string.Join(" ", words);
            }
            return result;
        }
        public static bool KiemTraHoVaTen(string input)
        {
            bool result = false;
            try
            {
                Match match = Regex.Match(input, @"(\d+)");
                if (match.Success)
                {
                    var number = int.Parse(match.Groups[1].Value);
                    return true;
                }
            }
            catch
            {
                return true;
            }
            return result;
        }
        public static bool IsInteger(string str)
        {
            Regex r = new Regex(@"^[0-9]+$");
            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!r.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch
            {

            }
            return false;
        }
        public static bool IsNumber(this string aNumber)
        {
            BigInteger temp_big_int;
            var is_number = BigInteger.TryParse(aNumber, out temp_big_int);
            return is_number;
        }
        public static string GetDomain(string url)
        {
            string host = "";
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    Uri myUri = new Uri(url.Trim().ToLower());
                    host = myUri.Host.ToLower();
                }
            }
            catch
            {
                host = "";
            }
            return host;
        }
        public static string RemoveLinks(string html)
        {
            try
            {
                if (!string.IsNullOrEmpty(html))
                {
                    Regex r = new Regex(@"\<a href=.*?\>");
                    html = r.Replace(html, "");
                    r = new Regex(@"\</a>\>");
                    html = r.Replace(html, "");
                }
                return html;
            }
            catch
            {
                return html;
            }
        }
        public static string ScriptHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
        public static string Right(string value, int length)
        {
            return value.Substring(value.Length - length);
        }
        public static int PAGE_SIZE = 20;
        public static string SEOUrl(string url)
        {
            url = url.ToLower();
            url = Regex.Replace(url, @"[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            url = Regex.Replace(url, @"[éèẹẻẽêếềệểễ]", "e");
            url = Regex.Replace(url, @"[óòọỏõôốồộổỗơớờợởỡ]", "o");
            url = Regex.Replace(url, @"[úùụủũưứừựữử]", "u");
            url = Regex.Replace(url, @"[íìịỉĩ]", "i");
            url = Regex.Replace(url, @"[ýỳỵỷỹ]", "y");
            url = Regex.Replace(url, @"[đ]", "d");
            //2. Chỉ cho phép nhuận [0-9a-z\s]
            url = Regex.Replace(url.Trim(), @"[^0-9a-z\s]", "").Trim();
            //xử lý nhiều hơn 1 khoảng trắng --> 1 kt
            url = Regex.Replace(url.Trim(), @"\s+", "-");
            //thay khoảng trắng bằng -
            url = Regex.Replace(url, @"\s", "-");
            while (true)
            {
                if (url.IndexOf("--") != -1)
                {
                    url = url.Replace("--", "-");
                }
                else
                {
                    break;
                }
            }
            return url;
        }
        public static string GetRamdomKey(int length = 5)
        {
            string pattern = @"0123456789xzcvbnmasdfghjklqwertyuio[]{}:~!@#$%^&*()+";
            Random rd = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
                {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }
            return sb.ToString(); 
        }
        public static async Task<string> UploadFile(IFormFile file,string sDirectory,string newname = null)
        {
            try
            {
                if (newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory,newname);
                string path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images",sDirectory);
                if(!Directory.Exists(path2))
                {
                    Directory.CreateDirectory(path2);
                }
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif", "doc", "docx", "pdf" };
                var fileExt = Path.GetExtension(file.FileName).Substring(1);
                if(!supportedTypes.Contains(fileExt.ToLower()))/// khác các file định nghĩa
                {
                    return null;
                }    
                else
                {
                    using(var stream = new FileStream(path,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return newname;
                }    
            }
            catch
            {
                return null;
            }
        }
        public static void Great_folder(string link)
        {
            string path = link;
            if(!Directory.Exists(@path))
            {
                Directory.CreateDirectory(path);
            }    
        }
        public static string RandTime()
        {
            Random r = new Random();
            string rand = DateTime.Now.ToString().Replace("/", ":").Replace(":", "-").Replace(" ", "-").ToLower();
            rand = rand + r.Next(100, 999);
            return rand;
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }catch
            {
                return false;
            }
        }
        public static List<string> ExtracLink(string html)
        {
            List<string> list = new List<string>();
            Regex regex = new Regex("(?:href|src) =[\"|']?(.*?)[\"|'|>]+", RegexOptions.Singleline | RegexOptions.CultureInvariant);
            if(regex.IsMatch(html))
            {
                foreach(Match match in regex.Matches(html))
                {
                    list.Add(match.Groups[1].Value);
                }    
            }
            return list;

        }
    }
}
