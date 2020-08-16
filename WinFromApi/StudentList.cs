using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFromApi.Models;

namespace WinFromApi
{
    public partial class StudentList : Form
    {
        public StudentList()
        {
            InitializeComponent();
        }

        private void StudentList_Load(object sender, EventArgs e)
        {
            GetList();
        }
        private void GetList()
        {
            object obj = null;
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync("http://localhost:56825/api/student/getallstudents").Result;
                var jsonString = result.Content.ReadAsStringAsync().Result;
                obj = JsonConvert.DeserializeObject<List<Student>>(jsonString);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = obj;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                Student student = new Student()
                {
                    Name = textBox1.Text,
                    Email = textBox2.Text,
                    GroupId = Convert.ToInt32(textBox3.Text)
                };
                var data = JsonConvert.SerializeObject(student);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var resp = client.PostAsync("http://localhost:56825/api/student/createstudent", content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    GetList();
                }
;
            }
        }
    }
}
