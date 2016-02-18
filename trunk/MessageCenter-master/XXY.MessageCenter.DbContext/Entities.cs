using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;

namespace XXY.MessageCenter.DbContext {
    public class Entities : System.Data.Entity.DbContext {

        public Entities()
            : base("Entities") {

            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema("MESSAGECENTER");

            modelBuilder.Conventions.Add(new OracleConversion());

            var entry = modelBuilder.Entity<Template>();
            entry.Property(p => p.Code)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));
            entry.Property(p => p.AppCode)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));
            entry.Property(p => p.MsgType)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));
            entry.Property(p => p.Lang)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));



            base.OnModelCreating(modelBuilder);
        }






        public DbSet<Template> Templates {
            get;
            set;
        }

        public DbSet<EMailMessage> EmailMessages {
            get;
            set;
        }

        public DbSet<QQMessage> QQMessages {
            get;
            set;
        }

        public DbSet<SMSMessage> SMSMessages {
            get;
            set;
        }

        public DbSet<WeChatMessage> WeChatMessages {
            get;
            set;
        }

        /// <summary>
        /// 站内信息
        /// </summary>
        public DbSet<TxtMessage> TxtMessages {
            get;
            set;
        }

        public DbSet<FailedMessage> FailedMessages {
            get;
            set;
        }
    }
}
