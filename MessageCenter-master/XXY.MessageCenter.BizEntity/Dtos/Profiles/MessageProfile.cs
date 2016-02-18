using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbEntity;

namespace XXY.MessageCenter.BizEntity.Dtos.Profiles {
    public class MessageProfile : Profile {

        protected override void Configure() {
            base.Configure();

            Mapper.CreateMap<BaseMessageDto, BaseMessage>()
               .Include<EmailDto, EMailMessage>()
               .Include<QQDto, QQMessage>()
               .Include<SMSDto, SMSMessage>()
               .Include<WeChatDto, WeChatMessage>()
               .Include<TxtDto, TxtMessage>()
               ;

            Mapper.CreateMap<EmailDto, EMailMessage>();
            Mapper.CreateMap<SMSDto, SMSMessage>();
            Mapper.CreateMap<QQDto, QQMessage>();
            Mapper.CreateMap<WeChatDto, WeChatMessage>();
            Mapper.CreateMap<TxtDto, TxtMessage>();

            Mapper.Configuration.Seal();
        }

    }
}
