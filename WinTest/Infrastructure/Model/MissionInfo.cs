using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTest.Infrastructure.Model
{
    public enum MissionType
    {
        Repair,
        ReturnItem,
        FindPerson,
        FindItem,
        KillPerson
    }

    public class MissionInfo
    {
        public int ID { get; set; }
        public uint IconKey { get; set; }
        public int TotalValue { get; set; }
        public int Value { get; set; }
        public int QL { get; set; }
        public float CoordX { get; set; }
        public float CoordY { get; set; }
        public int Cash { get; set; }
        public string CashStr { get; set; }
        public int XP { get; set; }
        public string XPStr { get; set; }
        public MissionType MissionType { get; set; }
        public string TypeStr { get; set; }

        public string pName { get; set; }
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<MissionInfo> MissionInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DatabaseContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

    }
}
