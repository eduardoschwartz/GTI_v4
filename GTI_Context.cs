using GTI_v4.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GTI_v4 {
    public class GTI_Context:DbContext {
        public GTI_Context(string Connection_Name) : base(Connection_Name) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            Database.SetInitializer<GTI_Context>(null);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(14, 4));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuario { get; set; }


    }
}
