﻿using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ManageMaterialPBA
{
    class TXTClass
    {
        
        public void FileNotExistAndCreate(string filepath)
        {
            if (!File.Exists(filepath)) new FileStream(filepath, FileMode.Create, FileAccess.Write).Close();
        }
        
        public DataTable DataTable_Select(DataTable dt, string condition)
        {
            DataTable table = new DataTable();
            try
            {
                DataRow[] rowArray = dt.Select(condition);
                DataRow[] rowArray2 = rowArray;
                table = dt.Clone();
                if (rowArray.Length <= 0)
                {
                    return table;
                }
                for (int i = 0; i < rowArray.Length; i++)
                {
                    table.Rows.Add(rowArray2[i].ItemArray);
                }
                table.AcceptChanges();
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
            return table;
        }
        
        public DataTable DataTable_Select(DataTable dt, string condition, string ordercondition)
        {
            DataTable table = new DataTable();
            try
            {
                DataRow[] rowArray = dt.Select(condition, ordercondition);
                table = dt.Clone();
                if (rowArray.Length <= 0)
                {
                    return table;
                }
                for (int i = 0; i < rowArray.Length; i++)
                {
                    table.Rows.Add(rowArray[i].ItemArray);
                }
                table.AcceptChanges();
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
            return table;
        }
        
        public string txtRead(string filepath)
        {
            string str = "";
            try
            {
                this.FileNotExistAndCreate(filepath);
                FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                str = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
            return str;
        }
       
        public DataTable txtRead(string filepath, char split)
        {
            DataTable table = new DataTable();
            try
            {
                string str2;
                this.FileNotExistAndCreate(filepath);
                FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                string str = reader.ReadLine();
                switch (str)
                {
                    case "":
                    case null:
                        return table;
                }
                int length = str.Split(new char[] { split }).Length;
                for (int i = 0; i < length; i++)
                {
                    str2 = "c" + i.ToString();
                    table.Columns.Add(str2, typeof(string));
                }
                while ((str != null) && (str != ""))
                {
                    string[] strArray2 = str.Split(new char[] { split });
                    DataRow row = table.NewRow();
                    for (int j = 0; j < length; j++)
                    {
                        str2 = "c" + j.ToString();
                        row[str2] = strArray2[j];
                    }
                    table.Rows.Add(row);
                    table.AcceptChanges();
                    str = reader.ReadLine();
                }
                reader.Close();
                stream.Close();
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
            return table;
        }

        public DataTable txtRead(string filepath, char[] split)
        {
            DataTable table = new DataTable();
            try
            {
                string str2;
                this.FileNotExistAndCreate(filepath);
                FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                string str = reader.ReadLine();
                switch (str)
                {
                    case "":
                    case null:
                        return table;
                }
                int length = str.Split(split).Length;
                for (int i = 0; i < length; i++)
                {
                    str2 = "c" + i.ToString();
                    table.Columns.Add(str2, typeof(string));
                }
                while ((str != null) && (str != ""))
                {
                    string[] strArray2 = str.Split(split);
                    DataRow row = table.NewRow();
                    for (int j = 0; j < length; j++)
                    {
                        str2 = "c" + j.ToString();
                        row[str2] = strArray2[j];
                    }
                    table.Rows.Add(row);
                    table.AcceptChanges();
                    str = reader.ReadLine();
                }
                reader.Close();
                stream.Close();
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
            return table;
        }

        public string txtRead(string filepath, int rowbh)
        {
            string str = "";
            try
            {
                this.FileNotExistAndCreate(filepath);
                FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                int num = 0;
                for (string str2 = reader.ReadLine(); str2 != null; str2 = reader.ReadLine())
                {
                    num++;
                    if (num == rowbh)
                    {
                        str = str2;
                        break;
                    }
                }
                reader.Close();
                stream.Close();
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
            return str;
        }

        public DataTable txtRead(string filepath, char split, string condition)
        {
            DataTable table = new DataTable();
            try
            {
                int num2;
                string str2;
                this.FileNotExistAndCreate(filepath);
                FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                string str = reader.ReadLine();
                switch (str)
                {
                    case "":
                    case null:
                        return table;
                }
                int length = str.Split(new char[] { split }).Length;
                DataTable table2 = new DataTable();
                for (num2 = 0; num2 < length; num2++)
                {
                    str2 = "c" + num2.ToString();
                    table2.Columns.Add(str2, typeof(string));
                }
                while ((str != null) && (str != ""))
                {
                    string[] strArray2 = str.Split(new char[] { split });
                    DataRow row = table2.NewRow();
                    for (int i = 0; i < length; i++)
                    {
                        str2 = "c" + i.ToString();
                        row[str2] = strArray2[i];
                    }
                    table2.Rows.Add(row);
                    table2.AcceptChanges();
                    str = reader.ReadLine();
                }
                reader.Close();
                stream.Close();
                DataRow[] rowArray = table2.Select(condition);
                table = table2.Clone();
                if (rowArray.Length > 0)
                {
                    for (num2 = 0; num2 < rowArray.Length; num2++)
                    {
                        table.Rows.Add(rowArray[num2].ItemArray);
                    }
                    table.AcceptChanges();
                }
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
            return table;
        }

        public DataTable txtRead(string filepath, char[] split, string condition)
        {
            DataTable table = new DataTable();
            try
            {
                int num2;
                string str2;
                this.FileNotExistAndCreate(filepath);
                FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                string str = reader.ReadLine();
                switch (str)
                {
                    case "":
                    case null:
                        return table;
                }
                int length = str.Split(split).Length;
                DataTable table2 = new DataTable();
                for (num2 = 0; num2 < length; num2++)
                {
                    str2 = "c" + num2.ToString();
                    table2.Columns.Add(str2, typeof(string));
                }
                while ((str != null) && (str != ""))
                {
                    string[] strArray2 = str.Split(split);
                    DataRow row = table2.NewRow();
                    for (int i = 0; i < length; i++)
                    {
                        str2 = "c" + i.ToString();
                        row[str2] = strArray2[i];
                    }
                    table2.Rows.Add(row);
                    table2.AcceptChanges();
                    str = reader.ReadLine();
                }
                reader.Close();
                stream.Close();
                DataRow[] rowArray = table2.Select(condition);
                table = table2.Clone();
                if (rowArray.Length > 0)
                {
                    for (num2 = 0; num2 < rowArray.Length; num2++)
                    {
                        table.Rows.Add(rowArray[num2].ItemArray);
                    }
                    table.AcceptChanges();
                }
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
            return table;
        }

        
        public DataTable txtRead(string filepath, char split, string condition, string ordercondition)
        {
            DataTable table = new DataTable();
            try
            {
                int num2;
                string str2;
                this.FileNotExistAndCreate(filepath);
                FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                string str = reader.ReadLine();
                switch (str)
                {
                    case "":
                    case null:
                        return table;
                }
                int length = str.Split(new char[] { split }).Length;
                DataTable table2 = new DataTable();
                for (num2 = 0; num2 < length; num2++)
                {
                    str2 = "c" + num2.ToString();
                    table2.Columns.Add(str2, typeof(string));
                }
                while ((str != null) && (str != ""))
                {
                    string[] strArray2 = str.Split(new char[] { split });
                    DataRow row = table2.NewRow();
                    for (int i = 0; i < length; i++)
                    {
                        str2 = "c" + i.ToString();
                        row[str2] = strArray2[i];
                    }
                    table2.Rows.Add(row);
                    table2.AcceptChanges();
                    str = reader.ReadLine();
                }
                reader.Close();
                stream.Close();
                DataRow[] rowArray = table2.Select(condition, ordercondition);
                table = table2.Clone();
                if (rowArray.Length > 0)
                {
                    for (num2 = 0; num2 < rowArray.Length; num2++)
                    {
                        table.Rows.Add(rowArray[num2].ItemArray);
                    }
                    table.AcceptChanges();
                }
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
            return table;
        }

        public DataTable txtRead(string filepath, char[] split, string condition, string ordercondition)
        {
            DataTable table = new DataTable();
            try
            {
                int num2;
                string str2;
                this.FileNotExistAndCreate(filepath);
                FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                string str = reader.ReadLine();
                switch (str)
                {
                    case "":
                    case null:
                        return table;
                }
                int length = str.Split(split).Length;
                DataTable table2 = new DataTable();
                for (num2 = 0; num2 < length; num2++)
                {
                    str2 = "c" + num2.ToString();
                    table2.Columns.Add(str2, typeof(string));
                }
                while ((str != null) && (str != ""))
                {
                    string[] strArray2 = str.Split(split);
                    DataRow row = table2.NewRow();
                    for (int i = 0; i < length; i++)
                    {
                        str2 = "c" + i.ToString();
                        row[str2] = strArray2[i];
                    }
                    table2.Rows.Add(row);
                    table2.AcceptChanges();
                    str = reader.ReadLine();
                }
                reader.Close();
                stream.Close();
                DataRow[] rowArray = table2.Select(condition, ordercondition);
                table = table2.Clone();
                if (rowArray.Length > 0)
                {
                    for (num2 = 0; num2 < rowArray.Length; num2++)
                    {
                        table.Rows.Add(rowArray[num2].ItemArray);
                    }
                    table.AcceptChanges();
                }
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
            return table;
        }

       
        public void txtWrite(string filepath, string str)
        {
            try
            {
                this.FileNotExistAndCreate(filepath);
                FileStream stream = new FileStream(filepath, FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(str);
                writer.Flush();
                writer.Close();
                stream.Close();
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
        }

        public void txtDelete(string filepath, char split, string condition)
        {
            DataTable table = new DataTable();
            try
            {
                this.FileNotExistAndCreate(filepath);
                FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                string str = reader.ReadLine();
                if ((str != "") && (str != null))
                {
                    int num2;
                    string str2;
                    int num3;
                    int length = str.Split(new char[] { split }).Length;
                    for (num2 = 0; num2 < length; num2++)
                    {
                        str2 = "c" + num2.ToString();
                        table.Columns.Add(str2, typeof(string));
                    }
                    while ((str != null) && (str != ""))
                    {
                        string[] strArray2 = str.Split(new char[] { split });
                        DataRow row = table.NewRow();
                        num3 = 0;
                        while (num3 < length)
                        {
                            str2 = "c" + num3.ToString();
                            row[str2] = strArray2[num3];
                            num3++;
                        }
                        table.Rows.Add(row);
                        table.AcceptChanges();
                        str = reader.ReadLine();
                    }
                    reader.Close();
                    stream.Close();
                    DataRow[] rowArray = table.Select(condition);
                    if (rowArray.Length > 0)
                    {
                        for (num2 = 0; num2 < rowArray.Length; num2++)
                        {
                            table.Rows.Remove(rowArray[num2]);
                        }
                        table.AcceptChanges();
                    }
                    File.Delete(filepath);
                    File.Create(filepath).Close();
                    for (num2 = 0; num2 < table.Rows.Count; num2++)
                    {
                        string str3 = table.Rows[num2]["c0"].ToString();
                        for (num3 = 1; num3 < length; num3++)
                        {
                            str2 = "c" + num3.ToString();
                            string str4 = table.Rows[num2][str2].ToString();
                            str3 = str3 + split + str4;
                        }
                        this.txtWrite(filepath, str3);
                    }
                }
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
        }
        
        public void txtModify(string filepath, char split, string content, string condition)
        {
            DataTable table = new DataTable();
            try
            {
                this.FileNotExistAndCreate(filepath);
                FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                string str = reader.ReadLine();
                if ((str != "") && (str != null))
                {
                    int num2;
                    string str2;
                    int num3;
                    int length = str.Split(new char[] { split }).Length;
                    for (num2 = 0; num2 < length; num2++)
                    {
                        str2 = "c" + num2.ToString();
                        table.Columns.Add(str2, typeof(string));
                    }
                    while ((str != null) && (str != ""))
                    {
                        string[] strArray2 = str.Split(new char[] { split });
                        DataRow row = table.NewRow();
                        num3 = 0;
                        while (num3 < length)
                        {
                            str2 = "c" + num3.ToString();
                            row[str2] = strArray2[num3];
                            num3++;
                        }
                        table.Rows.Add(row);
                        table.AcceptChanges();
                        str = reader.ReadLine();
                    }
                    reader.Close();
                    stream.Close();
                    DataRow[] rowArray = table.Select(condition);
                    if (rowArray.Length > 0)
                    {
                        for (num2 = 0; num2 < rowArray.Length; num2++)
                        {
                            string[] newArray = content.Split(new char[] { split });
                            rowArray[num2].BeginEdit();
                            for (int col = 0; col < newArray.Length; col++)
                            {
                                rowArray[num2][col] = newArray[col];
                            }
                            rowArray[num2].EndEdit();
                        }
                        table.AcceptChanges();
                    }
                    File.Delete(filepath);
                    File.Create(filepath).Close();
                    for (num2 = 0; num2 < table.Rows.Count; num2++)
                    {
                        string str3 = table.Rows[num2]["c0"].ToString();
                        for (num3 = 1; num3 < length; num3++)
                        {
                            str2 = "c" + num3.ToString();
                            string str4 = table.Rows[num2][str2].ToString();
                            str3 = str3 + split + str4;
                        }
                        this.txtWrite(filepath, str3);
                    }
                }
            }
            catch (Exception exception)
            {
                string text = exception.ToString();
                if (exception.InnerException != null)
                {
                    text = text + exception.InnerException.ToString();
                }
                if (exception.StackTrace != null)
                {
                    text = text + exception.StackTrace.ToString();
                }
                MessageBox.Show(text);
            }
        }
    }
}
