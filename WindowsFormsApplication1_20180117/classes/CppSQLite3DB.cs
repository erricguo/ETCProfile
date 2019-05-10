using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETCProfiler.classes
{
    public class CppSQLite3DB
    {
        public CppSQLite3DB()
        {
            InitTalbe();
        }

        private void InitTalbe()
        {
            //var db = new SQLiteConnection("etcdb.s3db");
            var db = new SQLiteConnection("Config.etc");
            db.CreateTables<ReflowData, CETAnalyseCondition>();

        }



        public class ReflowData
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Areatype { get; set; }
            public double Area_1{ get; set; }
            public double Area_2 { get; set; }
            public double Area_3 { get; set; }
            public double Area_4 { get; set; }
            public double Area_5 { get; set; }
            public double Area_6 { get; set; }
            public double Area_7 { get; set; }
            public double Area_8 { get; set; }
            public double Area_9 { get; set; }
            public double Area_10 { get; set; }
            public double Area_11{ get; set; }
            public double Area_12 { get; set; }
            public double Area_13 { get; set; }
            public double Area_14 { get; set; }
            public double Area_15 { get; set; }
            public double Area_16 { get; set; }
            public double Area_17 { get; set; }
            public double Area_18 { get; set; }
            public double Area_19 { get; set; }
            public double Area_20 { get; set; }
        }


    }
}
