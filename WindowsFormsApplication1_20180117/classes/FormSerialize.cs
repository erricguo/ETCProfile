using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ETCProfiler.classes
{
    public class FormSerialize
    {
        public void Serialize(string File, Control root)
        {
            FormFields field = new FormFields(root);
            SerializeControl(field, root);


            XmlSerializer xml = new XmlSerializer(typeof(FormFields));
            
            //TextWriter writer = new StreamWriter(File);
            try
            {
                var json = JsonConvert.SerializeObject(field);
                using (StreamWriter sw = new StreamWriter(File+".json"))   //小寫TXT     
                {
                    // Add some text to the file.
                    sw.Write(json);
                }
                //xml.Serialize(writer, field);
            }
            finally
            {
                
                //writer.Close();
            }
        }
        //遞迴找出所有控制項,加入至集合
        private void SerializeControl(FormFields Field, Control Ctrl)
        {
            foreach (Control ctrl in Ctrl.Controls)
            {
                if (ctrl.HasChildren)
                {
                    FormFields field = new FormFields(ctrl);
                    Field.ChildControls.Add(field);
                    if (ctrl.Controls.Count > 0)
                        SerializeControl(field, ctrl);
                }
                else
                {
                    if (ctrl.Text != "" || ctrl.Name != "")
                    {
                        FormFields field = new FormFields(ctrl);
                        Field.ChildControls.Add(field);
                        if (ctrl.Controls.Count > 0)
                            SerializeControl(field, ctrl);
                    }
                }
                
            }
        }

        public void Deserialize(string File, Control Ctrl)
        {
            XmlSerializer xml = new XmlSerializer(typeof(FormFields));
            FileStream fs = new FileStream(File, FileMode.Open);
            FormFields field = null;
            try
            {
                var o = xml.Deserialize(fs);
                if (o != null)
                    field = (FormFields)o;
            }
            finally
            {
                fs.Close();
            }

            //Form
            if (Ctrl.Name == field.Name)
                Ctrl.Text = field.Text;
            DeserializeControl(field, Ctrl);
        }

        //遞迴找出所有控制項,並指定Text屬性
        private void DeserializeControl(FormFields Field, Control Ctrl)
        {
            if (Field == null)
                return;

            //Form.Controls
            foreach (FormFields field in Field.ChildControls)
            {
                if (Ctrl.Controls[field.Name] != null)
                {
                    Ctrl.Controls[field.Name].Text = field.Text;
                    if (field.ChildControls.Count > 0)
                        DeserializeControl(field, Ctrl.Controls[field.Name]);
                }
            }
        }
    }
}
