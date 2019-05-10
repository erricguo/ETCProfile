

namespace ETCProfiler.classes
{
    // CETTinCream 锡焊类

    public class CETTinCream
    {
        public string m_strName { get; set; }
        public string m_strManufacturers { get; set; }
        public string m_strType { get; set; }

        // 预热
        public float m_fStartTempl1 { get; set; }
        public float m_fMinSlopeValue1 { get; set; }
        public float m_fMaxSlopeValue1 { get; set; }
        public float m_fTargetSlopeValue1 { get; set; }
        // 浸泡
        public float m_fStartTempl2 { get; set; }
        public float m_fEndTempl2 { get; set; }
        public float m_fMinSlopeValue2 { get; set; }
        public float m_fMaxSlopeValue2 { get; set; }
        public float m_fTargetSlopeValue2 { get; set; }

        public float m_fTimeRangeMin2 { get; set; }
        public float m_fTimeRangeMax2 { get; set; }
        public float m_fTimeRangeTarget2 { get; set; }
        // 回流 
        public float m_fStartTempl3 { get; set; }
        public float m_fMinSlopeValue3 { get; set; }
        public float m_fMaxSlopeValue3 { get; set; }
        public float m_fTargetSlopeValue3 { get; set; }

        public float m_fTimeRangeMin3 { get; set; }
        public float m_fTimeRangeMax3 { get; set; }
        public float m_fTimeRangeTarget3 { get; set; }

        public float m_fMinTemplValue3 { get; set; }
        public float m_fMaxTemplValue3 { get; set; }
        public float m_fTargetTemplValue3 { get; set; }


        // 冷却

        public float m_fMinSlopeValue4 { get; set; }
        public float m_fMaxSlopeValue4 { get; set; }
        public float m_fTargetSlopeValue4 { get; set; }

        public CETTinCream()
        {
            // 
            m_strName = "";
            m_strManufacturers = "";
            m_strType = "";
            // 预热
            m_fStartTempl1 = 35;
            m_fMinSlopeValue1 = 1;
            m_fMaxSlopeValue1 = 2;
            m_fTargetSlopeValue1 = 1.5f;
            // 浸泡
            m_fStartTempl2 = 150;
            m_fEndTempl2 = 160;
            m_fMinSlopeValue2 = 0.3f;
            m_fMaxSlopeValue2 = 0.8f;
            m_fTargetSlopeValue2 = 0.5f;

            m_fTimeRangeMin2 = 90;
            m_fTimeRangeMax2 = 150;
            m_fTimeRangeTarget2 = 120;
            // 回流
            m_fStartTempl3 = 183;
            m_fMinSlopeValue3 = 2;
            m_fMaxSlopeValue3 = 4;
            m_fTargetSlopeValue3 = 3;

            m_fTimeRangeMin3 = 30;
            m_fTimeRangeMax3 = 90;
            m_fTimeRangeTarget3 = 60;

            m_fMinTemplValue3 = 215;
            m_fMaxTemplValue3 = 225;
            m_fTargetTemplValue3 = 220;

            // 冷却

            m_fMinSlopeValue4 = 1.3f;
            m_fMaxSlopeValue4 = 2;
            m_fTargetSlopeValue4 = 1.5f;
        }
        //這邊之後要改
        public void ReadFromDB(int nID)
        {
            string strSql =$"select * from ETCTinCream where ID = {nID};";
            /*CppSQLite3Table t = theApp.m_ETCDB.getTable(strSql);
            for (int row = 0; row < t.numRows(); row++)
            {
                t.setRow(row);

                // 产品名称
                CString str = t.getStringField("Name");
                ConvertUtf8ToGBK(str);

                m_strName = str;
                // 产品编码
                str = t.getStringField("Manufacturers");
                ConvertUtf8ToGBK(str);
                m_strManufacturers = str;

                // 预热
                m_fStartTempl1 = t.getFloatField("StartTempl1");
                m_fMinSlopeValue1 = t.getFloatField("MinXieLvValue1");
                m_fMaxSlopeValue1 = t.getFloatField("MaxXieLvValue1");
                m_fTargetSlopeValue1 = t.getFloatField("TargetXieLvValue1");
                // 浸泡
                m_fStartTempl2 = t.getFloatField("StartTempl2");
                m_fEndTempl2 = t.getFloatField("EndTemp2");
                m_fMinSlopeValue2 = t.getFloatField("MinXieLvValue2");
                m_fMaxSlopeValue2 = t.getFloatField("MaxXieLvValue2");
                m_fTargetSlopeValue2 = t.getFloatField("TargetXieLvValue2");

                m_fTimeRangeMin2 = t.getFloatField("TimeRangeMin2");
                m_fTimeRangeMax2 = t.getFloatField("TimeRangeMax2");
                m_fTimeRangeTarget2 = t.getFloatField("TimeRangeTarget2");
                // 回流
                m_fStartTempl3 = t.getFloatField("StartTempl3");
                m_fMinSlopeValue3 = t.getFloatField("MinXieLvValue3");
                m_fMaxSlopeValue3 = t.getFloatField("MaxXieLvValue3");
                m_fTargetSlopeValue3 = t.getFloatField("TargetXieLvValue3");

                m_fTimeRangeMin3 = t.getFloatField("TimeRangeMin3");
                m_fTimeRangeMax3 = t.getFloatField("TimeRangeMax3");
                m_fTimeRangeTarget3 = t.getFloatField("TimeRangeTarget3");

                m_fMinTemplValue3 = t.getFloatField("MinTemplValue3");
                m_fMaxTemplValue3 = t.getFloatField("MaxTemplValue3");
                m_fTargetTemplValue3 = t.getFloatField("TargetTemplValue3");

                // 冷却
                m_fMinSlopeValue4 = t.getFloatField("MinXieLvValue4");
                m_fMaxSlopeValue4 = t.getFloatField("MaxXieLvValue4");
                m_fTargetSlopeValue4 = t.getFloatField("TargetXieLvValue4");
            }*/

        }
        //這邊之後要改
        public void UpdateTinCreamToDB(int nID)
        {
            /*// 打开一个通用的流，以模板的方式向表中插入多项数据
            CString strSQL;
            CString strTempSql;


            strSQL.Format("update ETCTinCream set ");

            // cID
            // 	strTempSql.Format("ID = %i,",nID);
            // 	strSQL += strTempSql;

            // 设备名称
            ConvertGBKToUtf8(m_strName);
            strTempSql.Format("Name = '%s',", m_strName);
            strSQL += strTempSql;

            // 设备密码
            ConvertGBKToUtf8(m_strManufacturers);
            strTempSql.Format("Manufacturers = '%s',", m_strManufacturers);
            strSQL += strTempSql;

            // 高度
            strTempSql.Format("StartTempl1 = %3.1f,", m_fStartTempl1);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("MinXieLvValue1 = %3.1f,", m_fMinSlopeValue1);
            strSQL += strTempSql;

            // 高度
            strTempSql.Format("MaxXieLvValue1 = %3.1f,", m_fMaxSlopeValue1);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("TargetXieLvValue1 = %3.1f,", m_fTargetSlopeValue1);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("StartTempl2 = %3.1f,", m_fStartTempl2);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("EndTemp2 = %3.1f,", m_fEndTempl2);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("MinXieLvValue2 = %3.1f,", m_fMinSlopeValue2);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("MaxXieLvValue2 = %3.1f,", m_fMaxSlopeValue2);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("TargetXieLvValue2 = %3.1f,", m_fTargetSlopeValue2);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("TimeRangeMin2 = %3.1f,", m_fTimeRangeMin2);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("TimeRangeMax2 = %3.1f,", m_fTimeRangeMax2);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("TimeRangeTarget2 = %3.1f,", m_fTimeRangeTarget2);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("StartTempl3 = %3.1f,", m_fStartTempl3);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("MinXieLvValue3 = %3.1f,", m_fMinSlopeValue3);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("MaxXieLvValue3 = %3.1f,", m_fMaxSlopeValue3);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("TargetXieLvValue3 = %3.1f,", m_fTargetSlopeValue3);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("TimeRangeMin3 = %3.1f,", m_fTimeRangeMin3);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("TimeRangeMax3 = %3.1f,", m_fTimeRangeMax3);
            strSQL += strTempSql;
            // m_fTimeRangeMax3
            strTempSql.Format("TimeRangeTarget3 = %3.1f,", m_fTimeRangeTarget3);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("MinTemplValue3 = %3.1f,", m_fMinTemplValue3);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("MaxTemplValue3 = %3.1f,", m_fMaxTemplValue3);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("TargetTemplValue3 = %3.1f,", m_fTargetTemplValue3);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("MinXieLvValue4 = %3.1f,", m_fMinSlopeValue4);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("MaxXieLvValue4 = %3.1f,", m_fMaxSlopeValue4);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("TargetXieLvValue4 = %3.1f ", m_fTargetSlopeValue4);
            strSQL += strTempSql;


            strTempSql.Format("WHERE ID = %i;", nID);
            strSQL += strTempSql;

            //	AfxMessageBox(strSQL);
            // 执行写操作
            int nRows = theApp.m_ETCDB.execDML(strSQL);*/
        }
        //這邊之後要改
        public void InsertTinCreamToDB(int nID)
        {// 打开一个通用的流，以模板的方式向表中插入多项数据
            /*CString strSQL;
            CString strTempSql;

            strSQL = "insert into ETCTinCream (ID,Name,Manufacturers,StartTempl1,MinXieLvValue1,MaxXieLvValue1,"

        "TargetXieLvValue1,StartTempl2,EndTemp2,MinXieLvValue2,MaxXieLvValue2,TargetXieLvValue2,TimeRangeMin2,"

        "TimeRangeMax2,TimeRangeTarget2,StartTempl3,MinXieLvValue3,MaxXieLvValue3,TargetXieLvValue3,"

        "TimeRangeMin3,TimeRangeMax3,TimeRangeTarget3,MinTemplValue3,MaxTemplValue3,TargetTemplValue3,"

        "MinXieLvValue4,MaxXieLvValue4,TargetXieLvValue4) values ( ";

            strTempSql.Format("%i,", 3);
            strSQL += strTempSql;

            // 设备名称
            ConvertGBKToUtf8(m_strName);
            strTempSql.Format("'%s',", m_strName);
            strSQL += strTempSql;

            // 设备密码
            ConvertGBKToUtf8(m_strManufacturers);
            strTempSql.Format("'%s',", m_strManufacturers);
            strSQL += strTempSql;

            // 高度
            strTempSql.Format("%3.1f,", m_fStartTempl1);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_fMinSlopeValue1);
            strSQL += strTempSql;

            // 高度
            strTempSql.Format("%3.1f,", m_fMaxSlopeValue1);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_fTargetSlopeValue1);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("%3.1f,", m_fStartTempl2);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_fEndTempl2);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("%3.1f,", m_fMinSlopeValue2);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_fMaxSlopeValue2);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("%3.1f,", m_fTargetSlopeValue2);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_fTimeRangeMin2);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("%3.1f,", m_fTimeRangeMax2);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_fTimeRangeTarget2);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("%3.1f,", m_fStartTempl3);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_fMinSlopeValue3);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("%3.1f,", m_fMaxSlopeValue3);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_fTargetSlopeValue3);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("%3.1f,", m_fTimeRangeMin3);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_fTimeRangeMax3);
            strSQL += strTempSql;
            // m_fTimeRangeMax3
            strTempSql.Format("%3.1f,", m_fTimeRangeTarget3);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_fMinTemplValue3);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("%3.1f,", m_fMaxTemplValue3);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_fTargetTemplValue3);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_fMinSlopeValue4);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("%3.1f,", m_fMaxSlopeValue4);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f ); ", m_fTargetSlopeValue4);
            strSQL += strTempSql;


            //	strTempSql.Format("WHERE ID = %i;",nID);
            //	strSQL += strTempSql;

            // 执行写操作
            int nRows = theApp.m_ETCDB.execDML(strSQL);*/
        }
        //這邊之後要改
        public void DeleteTinCreamFromDB(int nID)
        {// 打开一个通用的流，以模板的方式向表中插入多项数据
            string strSQL = $"delete from ETCTinCream where ID = {nID}";
            // 执行写操作
            //int nRows = theApp.m_ETCDB.execDML(strSQL);
        }

        
    }

}
