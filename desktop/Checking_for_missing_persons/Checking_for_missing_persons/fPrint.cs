using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Wordroller;
using Wordroller.Content.Tables;
using Wordroller.Styles;


namespace Cerberus
{
    public partial class fPrint : Form
    {
        static int j = 0;
        static DateTime date = DateTime.Now;
        public static string list = "";
        string ears = date.Year.ToString();
        List<Propusk> lst = new List<Propusk>();
        public static List<Propusk> lstSort = new List<Propusk>();
        public static List<History> lstHistory = new List<History>();
        public string fille = null;
        string path = "fileToPrint.docx";
        public static string h = "";

        public fPrint(string f)
        {
            list = f;
            InitializeComponent();
            ZapisInDateList();
            label2.Text = $"{ears} года";
            label8.Text = $"{ears} года";
        }
        public void ZapisInDateList()
        {
            int c = 1;
            string t = list;
            t = t.Replace("\n", "");
            string[] lines = t.Split('|');
            for (int i = 0; i < lines.Length - 1; i += 5, c++)
            {
                lst.Add(new Propusk() { Counter = c.ToString() + ".", NameStudents = lines[0 + i], DatePropuska = lines[1 + i], CntChasov = int.Parse(lines[2 + i]), OboznacheniePropuska = lines[3 + i], OpisaniePropuska = lines[4 + i] });
            }

        }
        protected void SaveTempDocument(WordDocument document, string fileName)
        {
            var path = Path.Combine("", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                document.Save(fileStream);
            }
        }
        public void Table_Width_SumOfColumnWidths(WordDocument document)
        {
            lst = lst.OrderBy(r => r.NameStudents).ToList();
            var tSection = document.Body.Sections.First();

            document.Styles.DocumentDefaults.RunProperties.Font.HighAnsi = "Times New Roman";
            var paragraph1 = tSection.AppendParagraph();
            paragraph1.AppendText($"                                                        Сведения о посещаемости группы Nº {comboBox3.Text}\n");
            paragraph1.AppendText($"                                                           c {dateTimePicker1.Text} по {dateTimePicker2.Text} {ears} года\n");

            var section = document.Body.Sections.First();
            var table = section.AppendTable(new CreateTableParameters(lst.Count + 1, WidthUnit.Cm, new double[] { 1.1f, 4.15f, 2.2f, 2.85f, 2f, 3.5f }, DefaultTableStyles.TableGrid));
            var row = table.Rows.First();
            var cells = row.Cells.ToArray();

            cells[0].Paragraphs.First().AppendText("№ П/П");
            cells[1].Paragraphs.First().AppendText("Ф.И.О.");
            cells[2].Paragraphs.First().AppendText("Дата пропуска");
            cells[3].Paragraphs.First().AppendText("Количество пропущенных часов");
            cells[4].Paragraphs.First().AppendText("Причина пропуска");
            cells[5].Paragraphs.First().AppendText("Примечание");

            for (int i = 1, counter = 0; i < lst.Count + 1; i++, counter++)
            {
                var current = table.Rows.ToArray()[i];
                var currentCells = current.Cells.ToArray();

                Propusk propusk = lst[counter];

                currentCells[0].Paragraphs.First().AppendText(propusk.Counter);
                currentCells[1].Paragraphs.First().AppendText(propusk.NameStudents);
                currentCells[2].Paragraphs.First().AppendText(propusk.DatePropuska);
                currentCells[3].Paragraphs.First().AppendText(propusk.CntChasov.ToString());
                currentCells[4].Paragraphs.First().AppendText(propusk.OboznacheniePropuska);
                currentCells[5].Paragraphs.First().AppendText(propusk.OpisaniePropuska);
            }
            paragraph1 = document.Body.Content.AppendParagraph();
            paragraph1.AppendText($"\n");
            paragraph1.AppendText($"\tУсловные обозначения:      б - болезнь, не подтвержденная справкой;\n");
            paragraph1.AppendText($"\t\t\t                      в - вызов в военкомат; с - медицинская справка; з - заявление;\n");
            paragraph1.AppendText($"\t\t\t                      б/у - пропуск без уважительной причины; д - дежурство;\n");
            paragraph1.AppendText($"\t\t\t                      н/б - участие в соревнованиях, олимпиадах, общественных\n");
            paragraph1.AppendText($"\t\t\t                      мероприятиях, семейные обстоятельства.\n");
            paragraph1.AppendText($"\n");
            paragraph1.AppendText($"Классный руководитель:__________________\n");
            paragraph1.AppendText($"Староста:__________________\n");
            SaveTempDocument(document, path);
        }
        public void CreateDocxHistory(WordDocument document)
        {

            var tSection = document.Body.Sections.First();

            document.Styles.DocumentDefaults.RunProperties.Font.HighAnsi = "Times New Roman";
            var paragraph1 = tSection.AppendParagraph();
            paragraph1.AppendText($"                                                Отчет о количестве пропусков группы Nº {comboBox1.Text}\n");
            paragraph1.AppendText($"                                               в период c {dateTimePicker4.Text} по {dateTimePicker3.Text} {ears} года\n");
            paragraph1.AppendText($"\n");

            var section = document.Body.Sections.First();
            var table = section.AppendTable(new CreateTableParameters(lstHistory.Count + 1, WidthUnit.Cm, new double[] { 1.2f, 4.5f, 5f, 5f }, DefaultTableStyles.TableGrid));
            var row = table.Rows.First();
            var cells = row.Cells.ToArray();

            cells[0].Paragraphs.First().AppendText("№ П/П");
            cells[1].Paragraphs.First().AppendText("Ф.И.О.");
            cells[2].Paragraphs.First().AppendText("Часы по уважительной причине");
            cells[3].Paragraphs.First().AppendText("Часы по неуважительной причине");

            for (int i = 1, counter = 0; i < lstHistory.Count + 1; i++, counter++)
            {
                var current = table.Rows.ToArray()[i];
                var currentCells = current.Cells.ToArray();

                History history = lstHistory[counter];

                currentCells[0].Paragraphs.First().AppendText(history.CounterH);
                currentCells[1].Paragraphs.First().AppendText(history.NameStudentsH);
                currentCells[2].Paragraphs.First().AppendText(history.CntChasUvazH.ToString());
                currentCells[3].Paragraphs.First().AppendText(history.CntChasNeUvazH.ToString());
            }
            SaveTempDocument(document, path);
        }
        public void Print()
        {
            Document doc = new Document();
            doc.LoadFromFile(path);
            PrintDialog dialog = new PrintDialog();
            dialog.AllowPrintToFile = true;
            dialog.AllowCurrentPage = true;
            dialog.AllowSomePages = true;
            dialog.UseEXDialog = true;
            doc.PrintDialog = dialog;
            PrintDocument printDoc = doc.PrintDocument;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }
        public static void SaveHistori()
        {
            using (StreamWriter writer = new StreamWriter("Histori.txt", true, System.Text.Encoding.UTF8))
            {
                writer.Write(list);
            }
            if (MessageBox.Show($"Очистить список недели?", "Внимание!",
              MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (StreamWriter writer = new StreamWriter("Week.txt", false, System.Text.Encoding.UTF8))
                {
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            if (fille != null) { saveFile1.FileName = fille; } else { saveFile1.FileName = $"Новый рапорт от {date.Day.ToString() + "." + date.Month.ToString() + "." + date.Year.ToString()}.docx"; }
            saveFile1.Filter = "Документ (*.docx)|*.docx";
            saveFile1.OverwritePrompt = false;
            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                string baseFileName = saveFile1.FileName;
                string fileName = baseFileName;
                int n = 1;
                while (System.IO.File.Exists(fileName))
                {
                    fileName = Path.Combine(Path.GetDirectoryName(baseFileName), Path.GetFileNameWithoutExtension(baseFileName) + "(" + n.ToString() + ")" + Path.GetExtension(baseFileName));
                    n++;
                }
                path = fileName;
            }
            using (var document = PrintHelper.CreateNewDocument())
            {
                Table_Width_SumOfColumnWidths(document);

            }
            path = "fileToPrint.docx";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (var document = PrintHelper.CreateNewDocument())
            {
                Table_Width_SumOfColumnWidths(document);
            }
            Print();
            System.IO.File.Delete(path);
        }
        public static void ReadHistory()
        {
            try
            {
                using (StreamReader reader = new StreamReader("Histori.txt"))
                {
                    h = System.IO.File.ReadAllText("Histori.txt").ToString();

                    if (h == "")
                    {
                        MessageBox.Show($"Фаил истории пуст!", "Внимание!",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        int c = 1;
                        string t = h;
                        t = t.Replace("\n", "");
                        string[] lines = t.Split('|');
                        for (int i = 0; i < lines.Length - 1; i += 5, c++)
                        {
                            lstSort.Add(new Propusk() { Counter = c.ToString() + ".", NameStudents = lines[0 + i], DatePropuska = lines[1 + i], CntChasov = int.Parse(lines[2 + i]), OboznacheniePropuska = lines[3 + i], OpisaniePropuska = lines[4 + i] });
                        }
                        lstSort = lstSort.OrderBy(r => r.NameStudents).ToList();
                        Dictionary<string, History> nameToNodeMap = new Dictionary<string, History>();
                        var first = lstSort[0];
                        if (first.OboznacheniePropuska != "б/у")
                        {
                            nameToNodeMap.Add(first.NameStudents, new History()
                            {
                                NameStudentsH = first.NameStudents,
                                CntChasUvazH = first.CntChasov,
                                CntChasNeUvazH = 0
                            });
                        }
                        else
                        {
                            nameToNodeMap.Add(first.NameStudents, new History()
                            {
                                NameStudentsH = first.NameStudents,
                                CntChasUvazH = 0,
                                CntChasNeUvazH = first.CntChasov
                            });
                        }

                        for (int i = 1; i < lstSort.Count - 1; i++)
                        {
                            string key = lstSort[i].NameStudents;
                            if (key == lstSort[i - 1].NameStudents)
                            {
                                History h;
                                if (nameToNodeMap.TryGetValue(key, out h))
                                {
                                    h = nameToNodeMap[key];

                                    History h2 = new History()
                                    {
                                        NameStudentsH = h.NameStudentsH,
                                        CntChasUvazH = h.CntChasUvazH,
                                        CntChasNeUvazH = h.CntChasNeUvazH
                                    };

                                    if (lstSort[i].OboznacheniePropuska != "б/у")
                                    {
                                        h2.CntChasUvazH += lstSort[i].CntChasov;
                                    }
                                    else
                                    {
                                        h2.CntChasNeUvazH += lstSort[i].CntChasov;
                                    }
                                    nameToNodeMap.Remove(key);
                                    nameToNodeMap.Add(key, h2);
                                }
                                else
                                {
                                    h = new History();
                                    h.NameStudentsH = key;
                                    if (lstSort[i].OboznacheniePropuska != "б/у")
                                    {
                                        h.CntChasUvazH = lstSort[i].CntChasov;
                                        h.CntChasNeUvazH = 0;
                                    }
                                    else
                                    {
                                        h.CntChasUvazH = 0;
                                        h.CntChasNeUvazH = lstSort[i].CntChasov;
                                    }
                                    nameToNodeMap.Add(key, h);
                                }
                            }
                            else
                            {
                                History h = new History();
                                h.NameStudentsH = key;
                                if (lstSort[i].OboznacheniePropuska != "б/у")
                                {
                                    h.CntChasUvazH = lstSort[i].CntChasov;
                                    h.CntChasNeUvazH = 0;
                                }
                                else
                                {
                                    h.CntChasUvazH = 0;
                                    h.CntChasNeUvazH = lstSort[i].CntChasov;
                                }
                                nameToNodeMap.Add(key, h);
                            }
                        }
                        lstHistory.AddRange(nameToNodeMap.Values);

                        lstHistory = lstHistory.Select(b =>
                        {
                            b.CounterH = $"{j}. ";
                            j++;

                            return b;
                        }).ToList();

                    }
                }
            }
            catch
            {
                using (StreamWriter writer = new StreamWriter("Histori.txt", true, System.Text.Encoding.UTF8))
                {
                }
                MessageBox.Show($"Нет файла истории\nСоздан пустой фаил истории", "Внимание!",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReadHistory();
            SaveFileDialog saveFile1 = new SaveFileDialog();
            if (fille != null) { saveFile1.FileName = fille; } else { saveFile1.FileName = $"История пропусков {date.Day.ToString() + "." + date.Month.ToString() + "." + date.Year.ToString()}.docx"; }
            saveFile1.Filter = "Документ (*.docx)|*.docx";
            saveFile1.OverwritePrompt = false;
            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                string baseFileName = saveFile1.FileName;
                string fileName = baseFileName;
                int n = 1;
                while (System.IO.File.Exists(fileName))
                {
                    fileName = Path.Combine(Path.GetDirectoryName(baseFileName), Path.GetFileNameWithoutExtension(baseFileName) + "(" + n.ToString() + ")" + Path.GetExtension(baseFileName));
                    n++;
                }
                path = fileName;
            }

            using (var document = PrintHelper.CreateNewDocument())
            {
                CreateDocxHistory(document);
            }
            lstHistory.Clear();
            path = "fileToPrint.docx";
            System.IO.File.Delete("fileToPrint.docx");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var document = PrintHelper.CreateNewDocument())
            {
                CreateDocxHistory(document);
            }
            Print();
            System.IO.File.Delete("fileToPrint.docx");

        }

        private void fPrint_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorM.colorStyle;
        }
        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            panel4.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = Cerberus.Properties.Resources.CloseR;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Cerberus.Properties.Resources.CloseB;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveHistori();
        }
    }
}
