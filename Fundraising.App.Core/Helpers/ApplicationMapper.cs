using AutoMapper;
using Fundraising.App.Core.Entities;
using Fundraising.App.Core.Options;

namespace Fundraising.App.Core.Helpers
{
    class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Project, OptionsProject>().ReverseMap();
            CreateMap<Reward, OptionReward>().ReverseMap();
            CreateMap<Payment, OptionPayment>().ReverseMap();
        }
    }
}
